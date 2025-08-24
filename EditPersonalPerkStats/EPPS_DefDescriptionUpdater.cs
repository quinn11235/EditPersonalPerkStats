
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Base.Defs;

namespace EditPersonalPerkStats
{
    internal static class EPPS_DefDescriptionUpdater
    {
        private static readonly HashSet<string> Targets = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Farsighted","Healer","Quarterback","Sniperist","Trooper",
            "Reckless","Bombardier","Cautious","Strongman","Thief",
            "Resourceful","Self-Defense","Self-Defense Specialist","Close-Quarters Specialist","Biochemist",
            "Self Defence","Self Defence Specialist","Close Quarters Specialist"
        };

        // store originals so we can restore when mod is disabled
        private static readonly Dictionary<object, object> _originalDesc = new Dictionary<object, object>(ReferenceEqualityComparer.Instance);
        private static MemberInfo _veDescSetter;
        private static MemberInfo _veDispGetter;

        public static int Apply(object cfg, Action<string> logInfo = null)
        {
            try
            {
                var repo = EPPS_Main.RepoSafe();
                if (repo == null) { logInfo?.Invoke("[EPPS] Repo not ready."); return 0; }

                var abilityDefT = FindType("PhoenixPoint.Common.Entities.Abilities.AbilityDef")
                               ?? FindType("PhoenixPoint.Tactical.Entities.Abilities.TacticalAbilityDef");
                if (abilityDefT == null) { logInfo?.Invoke("[EPPS] AbilityDef type not found."); return 0; }

                var getAllDefs = typeof(DefRepository).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                               .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == "GetAllDefs" && m.GetParameters().Length == 0);
                if (getAllDefs == null) { logInfo?.Invoke("[EPPS] DefRepository.GetAllDefs<> not found."); return 0; }

                var defs = getAllDefs.MakeGenericMethod(abilityDefT).Invoke(repo, null) as IEnumerable;
                if (defs == null) { logInfo?.Invoke("[EPPS] GetAllDefs<AbilityDef>() returned null."); return 0; }

                var veGetter = GetFieldOrGetter(abilityDefT, "ViewElementDef");
                object firstVE = defs.Cast<object>().Select(a => GetValue(veGetter, a)).FirstOrDefault(v => v != null);
                if (firstVE == null) { logInfo?.Invoke("[EPPS] No ViewElementDef found on AbilityDef."); return 0; }

                var veType = firstVE.GetType();
                _veDescSetter = _veDescSetter ?? GetFieldOrSetter(veType, "Description");
                _veDispGetter = _veDispGetter ?? GetFieldOrGetter(veType, "DisplayName1");

                var ltbType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(SafeTypes).FirstOrDefault(t => t.Name == "LocalizedTextBind");
                if (ltbType == null) { logInfo?.Invoke("[EPPS] LocalizedTextBind type not found."); return 0; }
                var ltbCtor = ltbType.GetConstructor(new[] { typeof(string), typeof(bool) }) ?? ltbType.GetConstructor(new[] { typeof(string) });
                if (ltbCtor == null) { logInfo?.Invoke("[EPPS] LocalizedTextBind suitable ctor not found."); return 0; }

                int updated = 0;
                foreach (var a in defs)
                {
                    var ve = GetValue(veGetter, a);
                    if (ve == null) continue;

                    var dispBind = GetValue(_veDispGetter, ve);
                    var dispName = LocalizeBind(dispBind);
                    if (string.IsNullOrEmpty(dispName)) continue;
                    if (!Targets.Contains(dispName)) continue;

                    string newText = EPPS_TextGen.BuildDescription(dispName, cfg);
                    if (string.IsNullOrEmpty(newText)) continue;

                    // capture original once
                    if (!_originalDesc.ContainsKey(ve))
                    {
                        var curDesc = GetValue(GetFieldOrGetter(veType, "Description"), ve);
                        _originalDesc[ve] = curDesc;
                    }

                    object ltb = ltbCtor.GetParameters().Length == 2 ? ltbCtor.Invoke(new object[] { newText, true })
                                                                     : ltbCtor.Invoke(new object[] { newText });
                    SetValue(_veDescSetter, ve, ltb);
                    updated++;
                }

                logInfo?.Invoke($"[EPPS] ViewElement descriptions updated: {updated}");
                return updated;
            }
            catch (Exception e)
            {
                logInfo?.Invoke("[EPPS] EPPS_DefDescriptionUpdater.Apply failed: " + e);
                return 0;
            }
        }

        public static void Reset(Action<string> logInfo = null)
        {
            try
            {
                int restored = 0;
                foreach (var kvp in _originalDesc)
                {
                    try { SetValue(_veDescSetter ?? GetFieldOrSetter(kvp.Key.GetType(), "Description"), kvp.Key, kvp.Value); restored++; } catch {}
                }
                _originalDesc.Clear();
                if (restored > 0) logInfo?.Invoke($"[EPPS] Restored original descriptions: {restored}");
            }
            catch (Exception e)
            {
                logInfo?.Invoke("[EPPS] EPPS_DefDescriptionUpdater.Reset failed: " + e);
            }
        }

        private static string LocalizeBind(object ltb)
        {
            if (ltb == null) return null;
            try
            {
                var t = ltb.GetType();
                var mi = t.GetMethod("LocalizeEnglish", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (mi == null) mi = t.GetMethod("Localize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                if (mi != null)
                {
                    var o = mi.Invoke(ltb, null);
                    if (o is string s) return s;
                    if (o != null) return o.ToString();
                }
                // Fallback to ToString
                return ltb.ToString();
            }
            catch { return null; }
        }

        private static IEnumerable<Type> SafeTypes(System.Reflection.Assembly a)
        {
            try { return a.GetTypes(); } catch { return Array.Empty<Type>(); }
        }

        private static Type FindType(string full) => AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetType(full, false)).FirstOrDefault(t => t != null);
        private static MemberInfo GetFieldOrGetter(Type t, string name)
        {
            var fi = t.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null) return fi;
            var pi = t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (pi != null && pi.CanRead) return pi.GetGetMethod(true);
            return null;
        }
        private static MemberInfo GetFieldOrSetter(Type t, string name)
        {
            var fi = t.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null) return fi;
            var pi = t.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (pi != null && pi.CanWrite) return pi.GetSetMethod(true);
            return null;
        }
        private static object GetValue(MemberInfo m, object instance)
        {
            if (m == null) return null;
            if (m is FieldInfo fi) return fi.GetValue(instance);
            if (m is MethodInfo mi) return mi.Invoke(instance, null);
            return null;
        }
        private static void SetValue(MemberInfo m, object instance, object val)
        {
            if (m is FieldInfo fi) { fi.SetValue(instance, val); return; }
            if (m is MethodInfo mi)
            {
                var ps = mi.GetParameters();
                if (ps.Length == 1) mi.Invoke(instance, new object[] { val });
            }
        }

        // simple reference equality comparer for dictionary keys
        private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();
            public new bool Equals(object x, object y) => ReferenceEquals(x, y);
            public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }
}

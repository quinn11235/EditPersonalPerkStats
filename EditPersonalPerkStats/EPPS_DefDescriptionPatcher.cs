
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EditPersonalPerkStats
{
    internal static class EPPS_DefDescriptionPatcher
    {
        private static readonly HashSet<string> Targets = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Farsighted","Healer","Quarterback","Sniperist","Trooper",
            "Reckless","Bombardier","Cautious","Strongman","Thief",
            "Resourceful","Self-Defense","Close-Quarters Specialist","Biochemist"
        };

        public static int Apply(object cfg, Action<string> logInfo = null)
        {
            try
            {
                var gameUtl = FindType("PhoenixPoint.Common.Core.GameUtl");
                var defRepoT = FindType("Base.Defs.DefRepository");
                if (gameUtl == null || defRepoT == null) { logInfo?.Invoke("[EPPS] GameUtl/DefRepository types not found."); return 0; }

                var gameComp = gameUtl.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                      .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == "GameComponent" && m.GetParameters().Length == 0);
                if (gameComp == null) { logInfo?.Invoke("[EPPS] GameUtl.GameComponent<> not found."); return 0; }

                var repo = gameComp.MakeGenericMethod(defRepoT).Invoke(null, null);
                if (repo == null) { logInfo?.Invoke("[EPPS] DefRepository instance is null."); return 0; }

                var abilityDefT = FindType("PhoenixPoint.Common.Entities.Abilities.AbilityDef")
                               ?? FindType("PhoenixPoint.Tactical.Entities.Abilities.TacticalAbilityDef");
                if (abilityDefT == null) { logInfo?.Invoke("[EPPS] AbilityDef type not found."); return 0; }

                var getAllDefs = defRepoT.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                         .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == "GetAllDefs" && m.GetParameters().Length == 0);
                if (getAllDefs == null) { logInfo?.Invoke("[EPPS] DefRepository.GetAllDefs<> not found."); return 0; }

                var listObj = getAllDefs.MakeGenericMethod(abilityDefT).Invoke(repo, null) as IEnumerable;
                if (listObj == null) { logInfo?.Invoke("[EPPS] GetAllDefs<AbilityDef>() returned null."); return 0; }

                var veGetter = GetFieldOrGetter(abilityDefT, "ViewElementDef");
                var veType = listObj.Cast<object>().Select(a => GetValue(veGetter, a)).Where(v => v != null).Select(v => v.GetType()).FirstOrDefault();
                if (veType == null) { logInfo?.Invoke("[EPPS] Could not infer ViewElementDef type."); return 0; }

                var descSetter = GetFieldOrSetter(veType, "Description");
                var dispGetter = GetFieldOrGetter(veType, "DisplayName1");

                var ltbType = FindType("PhoenixPoint.Common.Localization.LocalizedTextBind")
                           ?? FindType("Base.View.LocalizedTextBind")
                           ?? FindType("Base.UI.LocalizedTextBind")
                           ?? FindType("LocalizedTextBind");
                if (ltbType == null) { logInfo?.Invoke("[EPPS] LocalizedTextBind type not found."); return 0; }
                var ltbCtor = ltbType.GetConstructor(new[] { typeof(string), typeof(bool) })
                           ?? ltbType.GetConstructor(new[] { typeof(string) });
                if (ltbCtor == null) { logInfo?.Invoke("[EPPS] LocalizedTextBind suitable ctor not found."); return 0; }

                int updated = 0;
                foreach (var a in listObj)
                {
                    var ve = GetValue(veGetter, a);
                    if (ve == null) continue;

                    var dispBind = GetValue(dispGetter, ve);
                    var dispName = LocalizeBind(dispBind);
                    if (string.IsNullOrEmpty(dispName)) continue;
                    if (!Targets.Contains(dispName)) continue;

                    string newText = EPPS_TextGen.BuildDescription(dispName, cfg);
                    if (string.IsNullOrEmpty(newText)) continue;

                    object ltb = ltbCtor.GetParameters().Length == 2
                               ? ltbCtor.Invoke(new object[] { newText, true })
                               : ltbCtor.Invoke(new object[] { newText });

                    SetValue(descSetter, ve, ltb);
                    updated++;
                }

                logInfo?.Invoke($"[EPPS] ViewElement descriptions updated: {updated}");
                return updated;
            }
            catch (Exception e)
            {
                logInfo?.Invoke("[EPPS] EPPS_DefDescriptionPatcher.Apply failed: " + e);
                return 0;
            }
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
            if (m is System.Reflection.FieldInfo fi) return fi.GetValue(instance);
            if (m is System.Reflection.MethodInfo mi) return mi.Invoke(instance, null);
            return null;
        }

        private static void SetValue(MemberInfo m, object instance, object val)
        {
            if (m is System.Reflection.FieldInfo fi) { fi.SetValue(instance, val); return; }
            if (m is System.Reflection.MethodInfo mi)
            {
                var pars = mi.GetParameters();
                if (pars.Length == 1) mi.Invoke(instance, new object[] { val });
            }
        }

        private static string LocalizeBind(object ltb)
        {
            if (ltb == null) return null;
            var t = ltb.GetType();
            var mi = t.GetMethod("LocalizeEnglish", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                  ?? t.GetMethod("Localize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null)
                  ?? t.GetMethod("ToString", BindingFlags.Instance | BindingFlags.Public);
            try { var o = mi != null ? mi.Invoke(ltb, null) : null; return o as string; } catch { return null; }
        }
    }
}

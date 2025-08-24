
using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace EditPersonalPerkStats
{
    internal static class EPPS_RowSetControllerPatch
    {
        private static Type _ltbType;
        private static ConstructorInfo _ltbCtor2, _ltbCtor1;
        private static bool _installed;

        // New overload: keep old call sites working
        public static void Install(Harmony h) => Install(h, EPPS_Main.Main?.Logger);

        public static void Install(Harmony h, PhoenixPoint.Modding.ModLogger logger)
        {
            if (_installed) return;

            try
            {
                var rowType = AppDomain.CurrentDomain.GetAssemblies()
                                .Select(a => { try { return a.GetType("PhoenixPoint.Common.View.ViewControllers.RowIconTextController", false); } catch { return null; } })
                                .FirstOrDefault(t => t != null)
                             ?? AppDomain.CurrentDomain.GetAssemblies()
                                .Select(a => { try { return a.GetType("PhoenixPoint.Common.UI.RowIconTextController", false); } catch { return null; } })
                                .FirstOrDefault(t => t != null);

                if (rowType == null) { logger?.LogWarning("RowIconTextController not found; skipping UI postfix."); return; }

                // Find SetController(Sprite, LocalizedTextBind, LocalizedTextBind)
                MethodInfo target = null;
                foreach (var m in rowType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (m.Name != "SetController") continue;
                    var ps = m.GetParameters();
                    if (ps.Length == 3 &&
                        ps[1].ParameterType.Name.EndsWith("LocalizedTextBind") &&
                        ps[2].ParameterType.Name.EndsWith("LocalizedTextBind"))
                    {
                        target = m; break;
                    }
                }
                if (target == null) { logger?.LogWarning("SetController(Sprite, LTB, LTB) not found."); return; }

                _ltbType = AppDomain.CurrentDomain.GetAssemblies()
                              .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
                              .FirstOrDefault(t => t.Name == "LocalizedTextBind");
                if (_ltbType != null)
                {
                    _ltbCtor2 = _ltbType.GetConstructor(new Type[] { typeof(string), typeof(bool) });
                    _ltbCtor1 = _ltbType.GetConstructor(new Type[] { typeof(string) });
                }

                var postfix = new HarmonyMethod(typeof(EPPS_RowSetControllerPatch).GetMethod(nameof(Postfix), BindingFlags.Static | BindingFlags.NonPublic));
                h.Patch(target, postfix: postfix);
                _installed = true;
                logger?.LogInfo("[EPPS] RowIconTextController.SetController postfix installed.");
            }
            catch (Exception e)
            {
                logger?.LogWarning("[EPPS] Row patch install failed: " + e);
            }
        }

        // instance method: (Sprite, LocalizedTextBind name, LocalizedTextBind desc)
        private static void Postfix(object __instance, object __0, ref object __1, ref object __2)
        {
            try
            {
                string disp = LocalizeBind(__1);
                if (string.IsNullOrEmpty(disp)) return;
                if (!IsTarget(disp)) return;

                string newText = EPPS_TextGen.BuildDescription(disp, EPPS_Main.Main?.EPPSConfig);
                if (string.IsNullOrEmpty(newText)) return;

                object newBind = null;
                if (_ltbType != null)
                {
                    if (_ltbCtor2 != null) newBind = _ltbCtor2.Invoke(new object[] { newText, true });
                    else if (_ltbCtor1 != null) newBind = _ltbCtor1.Invoke(new object[] { newText });
                }

                if (newBind != null)
                    __2 = newBind;
            }
            catch { /* never break UI */ }
        }

        private static bool IsTarget(string name)
        {
            string[] names = new string[] {
                "Farsighted","Healer","Quarterback","Sniperist","Trooper",
                "Reckless","Bombardier","Cautious","Strongman","Thief",
                "Resourceful","Self-Defense","Close-Quarters Specialist","Biochemist"
            };
            for (int i = 0; i < names.Length; i++)
                if (string.Equals(name, names[i], StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        private static string LocalizeBind(object ltb)
        {
            if (ltb == null) return null;
            var t = ltb.GetType();
            var mi = t.GetMethod("LocalizeEnglish", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                  ?? t.GetMethod("Localize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null)
                  ?? t.GetMethod("ToString", BindingFlags.Instance | BindingFlags.Public);
            try
            {
                var o = mi != null ? mi.Invoke(ltb, null) : null;
                return o as string;
            }
            catch { return null; }
        }
    }
}

using System;
using System.Linq;
using System.Reflection;
using Base.Defs;
using Base.Levels;

namespace EditPersonalPerkStats
{
    internal static class EPPS_Bootstrap
    {
        private static bool _wired;
        private static bool _done;

        public static void Wire(EPPS_Main main)
        {
            if (_wired) return;
            _wired = true;

            AppDomain.CurrentDomain.AssemblyLoad += OnAsmLoad;
            TryInit("ScanNow", main); // in case everything’s already loaded
        }

        public static void Unwire()
        {
            AppDomain.CurrentDomain.AssemblyLoad -= OnAsmLoad;
        }

        private static void OnAsmLoad(object sender, AssemblyLoadEventArgs e)
        {
            var main = EPPS_Main.Main;
            if (main == null) return;
            TryInit("AssemblyLoad:" + e.LoadedAssembly.GetName().Name, main);
        }

        public static void OnLevelStart(Level level)
        {
            var main = EPPS_Main.Main;
            if (main == null) return;
            TryInit("LevelStart:" + (level != null ? level.name : "<null>"), main);
        }

        private static void TryInit(string reason, EPPS_Main main)
        {
            if (_done || main == null) return;

            var gameUtl = Type.GetType("PhoenixPoint.Common.Core.GameUtl")
                       ?? AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
                            .FirstOrDefault(t => t.FullName == "PhoenixPoint.Common.Core.GameUtl");
            if (gameUtl == null) return;

            var defRepoT = typeof(DefRepository);
            var mi = gameUtl.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == "GameComponent" && m.GetParameters().Length == 0);
            if (mi == null) return;

            object repoObj = null;
            try { repoObj = mi.MakeGenericMethod(defRepoT).Invoke(null, null); } catch { }
            if (repoObj == null) return;

            try
            {
                main.Logger?.LogInfo("[EPPS] Late bootstrap via " + reason);

                try { EPPS_Perks.Change_Perks(); }
                catch (Exception e) { main.Logger?.LogWarning("[EPPS] Change_Perks late failed: " + e.Message); }

                try { EPPS_DefDescriptionPatcher.Apply(main.Config, s => main.Logger?.LogInfo(s)); }
                catch (Exception e) { main.Logger?.LogWarning("[EPPS] Description patch late failed: " + e.Message); }

                try { if (main.HarmonyInstance != null) EPPS_RowSetControllerPatch.Install(main.HarmonyInstance); }
                catch (Exception e) { main.Logger?.LogWarning("[EPPS] Row patch install late failed: " + e.Message); }

                _done = true;
                Unwire();
            }
            catch (Exception e)
            {
                main.Logger?.LogWarning("[EPPS] Late bootstrap error: " + e);
            }
        }
    }
}

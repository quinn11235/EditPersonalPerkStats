using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace EditPersonalPerkStats
{
    /// <summary>
    /// More aggressive patch to force Geoscape stat calculations to use current perk values
    /// </summary>
    internal static class EPPS_StatCalculationPatch
    {
        private static bool _installed = false;

        public static void Install(Harmony harmony)
        {
            if (_installed) return;

            try
            {
                // Try to find and patch common stat calculation patterns
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.GetName().Name.ToLower().Contains("assembly") || 
                               a.GetName().Name.ToLower().Contains("phoenix"))
                    .ToArray();

                bool patchApplied = false;

                foreach (var assembly in assemblies)
                {
                    var types = assembly.GetTypes()
                        .Where(t => t.Name.ToLower().Contains("tactical") && 
                                   t.Name.ToLower().Contains("actor") ||
                                   t.Name.ToLower().Contains("character") ||
                                   t.Name.ToLower().Contains("soldier"))
                        .ToArray();

                    foreach (var type in types)
                    {
                        // Look for property getters that might calculate stats
                        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(p => p.Name.ToLower().Contains("accuracy") ||
                                       p.Name.ToLower().Contains("perception") ||
                                       p.Name.ToLower().Contains("stealth"))
                            .ToArray();

                        foreach (var prop in properties)
                        {
                            try
                            {
                                if (prop.CanRead && prop.GetGetMethod(true) != null)
                                {
                                    var getter = prop.GetGetMethod(true);
                                    if (getter.IsVirtual || !getter.IsFinal)
                                    {
                                        var prefix = new HarmonyMethod(typeof(EPPS_StatCalculationPatch)
                                            .GetMethod(nameof(StatCalculationPrefix), 
                                                      BindingFlags.Static | BindingFlags.NonPublic));
                                        
                                        harmony.Patch(getter, prefix: prefix);
                                        
                                        EPPS_Main.Main?.Logger?.LogInfo($"[EPPS] Patched stat property: {type.Name}.{prop.Name}");
                                        patchApplied = true;
                                        
                                        // Only patch a few to avoid performance issues
                                        if (patchApplied) break;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Failed to patch {type.Name}.{prop.Name}: {e.Message}");
                            }
                        }
                        
                        if (patchApplied) break;
                    }
                    
                    if (patchApplied) break;
                }

                if (!patchApplied)
                {
                    // Fallback: try to patch any method that looks like it calculates stats
                    TryPatchStatMethods(harmony);
                }

                _installed = true;
                EPPS_Main.Main?.Logger?.LogInfo($"[EPPS] Stat calculation patch installed (success: {patchApplied})");
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Stat calculation patch failed: {e.Message}");
            }
        }

        private static void TryPatchStatMethods(Harmony harmony)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                
                foreach (var assembly in assemblies)
                {
                    try
                    {
                        var types = assembly.GetTypes()
                            .Where(t => !t.IsAbstract && 
                                       (t.Name.Contains("Actor") || t.Name.Contains("Character")))
                            .ToArray();

                        foreach (var type in types)
                        {
                            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                .Where(m => m.ReturnType == typeof(float) &&
                                           (m.Name.ToLower().Contains("getstat") ||
                                            m.Name.ToLower().Contains("calculatestat") ||
                                            m.Name.ToLower().Contains("accuracy") ||
                                            m.Name.ToLower().Contains("perception") ||
                                            m.Name.ToLower().Contains("stealth")))
                                .Take(3) // Limit to avoid too many patches
                                .ToArray();

                            foreach (var method in methods)
                            {
                                try
                                {
                                    var prefix = new HarmonyMethod(typeof(EPPS_StatCalculationPatch)
                                        .GetMethod(nameof(StatCalculationPrefix), 
                                                  BindingFlags.Static | BindingFlags.NonPublic));
                                    
                                    harmony.Patch(method, prefix: prefix);
                                    
                                    EPPS_Main.Main?.Logger?.LogInfo($"[EPPS] Patched stat method: {type.Name}.{method.Name}");
                                    return; // Success, exit
                                }
                                catch
                                {
                                    // Ignore individual patch failures
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Ignore assembly processing failures
                    }
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Fallback stat patching failed: {e.Message}");
            }
        }

        /// <summary>
        /// Prefix that ensures perks are updated before any stat calculation
        /// </summary>
        private static void StatCalculationPrefix()
        {
            try
            {
                // Ensure perks are current before any stat calculation
                var repo = EPPS_Main.RepoSafe();
                var config = EPPS_Main.Main?.EPPSConfig;
                
                if (repo != null && config != null)
                {
                    // Force perk updates to ensure current values are used
                    EPPS_Perks.Change_Perks();
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Stat calculation prefix error: {e.Message}");
            }
        }
    }
}
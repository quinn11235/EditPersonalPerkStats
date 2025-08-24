using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace EditPersonalPerkStats
{
    /// <summary>
    /// Patch to ensure Geoscape UI reflects configured perk values
    /// The issue: configured values show in ability tooltips but not in total stat calculations
    /// </summary>
    internal static class EPPS_GeoscapeStatsPatch
    {
        private static bool _patched = false;

        public static void Install(Harmony harmony)
        {
            if (_patched) return;

            try
            {
                // Try to patch specific UI methods that calculate or display character stats
                bool patchInstalled = false;

                // Look for common UI controller types that might handle character stat display
                var uiTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.Name.Contains("Character") && t.Name.Contains("UI") ||
                               t.Name.Contains("Personnel") && t.Name.Contains("Controller") ||
                               t.Name.Contains("Soldier") && t.Name.Contains("Info") ||
                               t.Name.Contains("Stat") && t.Name.Contains("Controller"))
                    .ToArray();

                foreach (var uiType in uiTypes)
                {
                    // Look for methods that might be responsible for calculating or displaying stats
                    var methods = uiType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        .Where(m => m.Name.ToLower().Contains("refresh") ||
                                   m.Name.ToLower().Contains("update") ||
                                   m.Name.ToLower().Contains("calculate") ||
                                   m.Name.ToLower().Contains("display"))
                        .ToArray();

                    foreach (var method in methods)
                    {
                        try
                        {
                            var postfix = new HarmonyMethod(typeof(EPPS_GeoscapeStatsPatch).GetMethod(nameof(EnsurePerksUpdated), 
                                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic));
                            harmony.Patch(method, postfix: postfix);
                            
                            EPPS_Main.Main?.Logger?.LogInfo($"[EPPS] Patched UI method: {uiType.Name}.{method.Name}");
                            patchInstalled = true;
                            
                            // Don't patch too many methods to avoid performance issues
                            if (patchInstalled) break;
                        }
                        catch (Exception e)
                        {
                            // Ignore individual patch failures, but log them
                            EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Failed to patch {uiType.Name}.{method.Name}: {e.Message}");
                        }
                    }
                    
                    if (patchInstalled) break;
                }

                if (!patchInstalled)
                {
                    // Fallback: Install a simple observer that checks for stat-related UI updates
                    StatUpdateObserver.Initialize();
                    EPPS_Main.Main?.Logger?.LogInfo("[EPPS] UI patches not found, installed stat update observer");
                }

                _patched = true;
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Geoscape stats patch failed: {e.Message}");
            }
        }

        /// <summary>
        /// Postfix method that ensures perk values are current before any stat calculations
        /// </summary>
        private static void EnsurePerksUpdated()
        {
            try
            {
                // Only update if we have a valid configuration
                if (EPPS_Main.Main?.EPPSConfig != null && EPPS_Main.RepoSafe() != null)
                {
                    EPPS_Perks.Change_Perks();
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Perk update in UI patch failed: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Lightweight observer that monitors for potential stat calculation updates
    /// </summary>
    internal sealed class StatUpdateObserver : MonoBehaviour
    {
        private static StatUpdateObserver _instance;
        private float _lastCheck;
        private const float CHECK_INTERVAL = 5.0f; // Check every 5 seconds

        public static void Initialize()
        {
            if (_instance != null) return;

            var go = new GameObject("EPPS_StatObserver");
            UnityEngine.Object.DontDestroyOnLoad(go);
            _instance = go.AddComponent<StatUpdateObserver>();
        }

        public static void Shutdown()
        {
            if (_instance == null) return;
            try { UnityEngine.Object.Destroy(_instance.gameObject); } catch { }
            _instance = null;
        }

        private void Update()
        {
            try
            {
                // Only check periodically to minimize performance impact
                if (Time.unscaledTime - _lastCheck < CHECK_INTERVAL)
                    return;

                _lastCheck = Time.unscaledTime;

                // Check if we're in a context where character stats might be displayed
                if (IsInStatsRelevantContext())
                {
                    // Ensure perk values are current
                    if (EPPS_Main.Main?.EPPSConfig != null && EPPS_Main.RepoSafe() != null)
                    {
                        EPPS_Perks.Change_Perks();
                    }
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Stat observer error: {e.Message}");
            }
        }

        private bool IsInStatsRelevantContext()
        {
            try
            {
                // Check if we're likely in a context where character stats are being displayed
                // This is a heuristic approach - look for active GameObjects that suggest character UI
                var characterUIObjects = Resources.FindObjectsOfTypeAll<GameObject>()
                    .Where(go => go.activeInHierarchy && 
                                (go.name.ToLower().Contains("character") ||
                                 go.name.ToLower().Contains("personnel") ||
                                 go.name.ToLower().Contains("soldier") ||
                                 go.name.ToLower().Contains("stats")))
                    .ToArray();

                return characterUIObjects.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }

    /// <summary>
    /// Public access point for the shutdown method
    /// </summary>
    public static class GeoscapeRefreshManager
    {
        public static void Shutdown()
        {
            StatUpdateObserver.Shutdown();
        }
    }
}
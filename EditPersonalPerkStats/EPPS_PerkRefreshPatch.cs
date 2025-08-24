using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace EditPersonalPerkStats
{
    /// <summary>
    /// Targeted patch to ensure perk values are refreshed when Geoscape character info is displayed
    /// This addresses the specific issue where configured perk values don't reflect in total stat calculations
    /// </summary>
    internal static class EPPS_PerkRefreshPatch
    {
        private static bool _installed = false;
        private static DateTime _lastPerkRefresh = DateTime.MinValue;
        private const double REFRESH_INTERVAL_SECONDS = 1.0; // Refresh at most once per second

        public static void Install(Harmony harmony)
        {
            if (_installed) return;

            try
            {
                // Install a more targeted approach using a GameObject that monitors for Geoscape activity
                var refreshManager = new GameObject("EPPS_PerkRefreshMonitor");
                UnityEngine.Object.DontDestroyOnLoad(refreshManager);
                refreshManager.AddComponent<PerkRefreshMonitor>();

                _installed = true;
                EPPS_Main.Main?.Logger?.LogInfo("[EPPS] Perk refresh monitor installed");
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Perk refresh patch failed: {e.Message}");
            }
        }

        /// <summary>
        /// Force a perk refresh if enough time has passed
        /// </summary>
        public static void RefreshPerksIfNeeded()
        {
            try
            {
                var now = DateTime.Now;
                if ((now - _lastPerkRefresh).TotalSeconds < REFRESH_INTERVAL_SECONDS)
                    return;

                _lastPerkRefresh = now;

                var repo = EPPS_Main.RepoSafe();
                var config = EPPS_Main.Main?.EPPSConfig;
                
                if (repo != null && config != null)
                {
                    EPPS_Perks.Change_Perks();
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Perk refresh error: {e.Message}");
            }
        }
    }

    /// <summary>
    /// MonoBehaviour that actively monitors for situations where perk refreshes might be needed
    /// </summary>
    internal class PerkRefreshMonitor : MonoBehaviour
    {
        private float _lastUpdate;
        private const float UPDATE_INTERVAL = 2.0f; // Check every 2 seconds
        private HashSet<string> _geoscapeScenes = new HashSet<string>
        {
            "geoscape", "phoenixbase", "personnel", "character", "soldier", "squad"
        };

        private void Update()
        {
            try
            {
                // Only check periodically
                if (Time.unscaledTime - _lastUpdate < UPDATE_INTERVAL)
                    return;

                _lastUpdate = Time.unscaledTime;

                // Check if we're in a context where character stats might be displayed
                if (IsInGeoscapeContext())
                {
                    EPPS_PerkRefreshPatch.RefreshPerksIfNeeded();
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Monitor update error: {e.Message}");
            }
        }

        private bool IsInGeoscapeContext()
        {
            try
            {
                // Check current scene
                var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                if (_geoscapeScenes.Any(scene => activeScene.name.ToLower().Contains(scene)))
                {
                    return true;
                }

                // Check for active UI objects that might display character stats
                var characterUIObjects = Resources.FindObjectsOfTypeAll<GameObject>()
                    .Where(go => go.activeInHierarchy && IsCharacterStatsUI(go.name))
                    .ToArray();

                return characterUIObjects.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private bool IsCharacterStatsUI(string objectName)
        {
            var lowerName = objectName.ToLower();
            return lowerName.Contains("character") ||
                   lowerName.Contains("personnel") ||
                   lowerName.Contains("soldier") ||
                   lowerName.Contains("stats") ||
                   lowerName.Contains("info") ||
                   lowerName.Contains("detail") ||
                   lowerName.Contains("roster");
        }

        private void OnDestroy()
        {
            // Cleanup if needed
        }
    }

    /// <summary>
    /// Cleanup methods for the perk refresh system
    /// </summary>
    internal static class PerkRefreshCleanup
    {
        public static void Shutdown()
        {
            try
            {
                var monitor = GameObject.FindObjectOfType<PerkRefreshMonitor>();
                if (monitor != null)
                {
                    UnityEngine.Object.Destroy(monitor.gameObject);
                }
            }
            catch (Exception e)
            {
                EPPS_Main.Main?.Logger?.LogWarning($"[EPPS] Shutdown error: {e.Message}");
            }
        }
    }
}
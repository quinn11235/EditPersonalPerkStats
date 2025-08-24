
using System;
using Base.Defs;
using UnityEngine;

namespace EditPersonalPerkStats
{
    internal sealed class EPPS_LateInit : MonoBehaviour
    {
        private static EPPS_LateInit _inst;
        private float _deadline;
        private float _nextLog;
        private bool _done;

        public static void Spawn()
        {
            if (_inst != null) return;
            var go = new GameObject("EPPS_LateInit");
            UnityEngine.Object.DontDestroyOnLoad(go);
            _inst = go.AddComponent<EPPS_LateInit>();
        }

        public static void Kill()
        {
            if (_inst == null) return;
            try { UnityEngine.Object.Destroy(_inst.gameObject); } catch { }
            _inst = null;
        }

        private void Awake()
        {
            // Give it more headroom; some systems are late in main menu
            _deadline = Time.unscaledTime + 120f;
            _nextLog = Time.unscaledTime + 5f;
            EditPersonalPerkStats.EPPS_Main.Main?.Logger?.LogInfo("[EPPS] LateInit awake (polling for Repo/UI up to 120s).");
        }

        private void Update()
        {
            if (_done) return;
            if (Time.unscaledTime > _deadline)
            {
                EditPersonalPerkStats.EPPS_Main.Main?.Logger?.LogWarning("[EPPS] LateInit timed out. Repo/UI not found.");
                _done = true;
                Kill();
                return;
            }

            // Throttle log noise
            if (Time.unscaledTime >= _nextLog)
            {
                EditPersonalPerkStats.EPPS_Main.Main?.Logger?.LogInfo("[EPPS] LateInit: still waiting for Repo/UI...");
                _nextLog = Time.unscaledTime + 10f;
            }

            string how;
            DefRepository repo = EPPS_RepoUtil.FindRepo(out how);
            if (repo == null) return;

            try
            {
                EPPS_Main.Main?.Logger?.LogInfo("[EPPS] LateInit running (repo ready via " + how + ").");

                if (EPPS_Main.Main?.HarmonyInstance != null)
                    EPPS_RowSetControllerPatch.Install(EPPS_Main.Main.HarmonyInstance);

                EPPS_DefDescriptionUpdater.Apply(EPPS_Main.Main?.EPPSConfig, s => EPPS_Main.Main?.Logger?.LogInfo(s));

                try { EPPS_Perks.Change_Perks(); } catch (Exception e) { EPPS_Main.Main?.Logger?.LogWarning("[EPPS] Change_Perks late failed: " + e.Message); }

                _done = true;
            }
            finally
            {
                Kill();
            }
        }
    }
}

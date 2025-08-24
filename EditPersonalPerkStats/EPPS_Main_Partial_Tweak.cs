
// Only the two lines changed: fully-qualified calls to the updater so
// the compiler can't get confused by partial namespaces.
using System;
using System.Linq;
using System.Reflection;
using Base.Defs;
using Base.Levels;
using HarmonyLib;
using PhoenixPoint.Modding;
using UnityEngine;

namespace EditPersonalPerkStats
{
    public partial class EPPS_Main // mark partial so it can merge if your EPPS_Main already exists
    {
        private void __EPPS_UpdateVE_OnEnable()
        {
            try
            {
                int n = EditPersonalPerkStats.EPPS_DefDescriptionUpdater.Apply(this.EPPSConfig, s => Logger.LogInfo(s));
                Logger.LogInfo($"[EPPS] Initial VE description update: {n}");
            }
            catch (Exception e)
            {
                Logger.LogWarning("[EPPS] Initial Apply failed: " + e.Message);
            }
        }

        private void __EPPS_UpdateVE_OnConfig()
        {
            try
            {
                int n = EditPersonalPerkStats.EPPS_DefDescriptionUpdater.Apply(this.EPPSConfig, s => Logger.LogInfo(s));
                Logger.LogInfo($"[EPPS] Config change VE update: {n}");
            }
            catch (Exception e)
            {
                Logger.LogWarning("[EPPS] Config change Apply failed: " + e.Message);
            }
        }
    }
}

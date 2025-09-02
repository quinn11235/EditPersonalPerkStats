using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Modding;
using HarmonyLib;
using System;

namespace Quinn11235.EditPersonalPerkStats
{
    public class EditPersonalPerkStatsMain : ModMain
    {
        public new static EditPersonalPerkStatsMain Instance { get; private set; }
        public static DefRepository Repo => GameUtl.GameComponent<DefRepository>();

        public override void OnModEnabled()
        {
            Instance = this;
            Logger.LogInfo("[EditPersonalPerkStats] Mod enabled");
            
            try
            {
                EditPersonalPerkStatsPerks.ApplyPerkConfigurations();
                EditPersonalPerkStatsDescriptions.UpdatePerkDescriptions();
                
                Logger.LogInfo("[EditPersonalPerkStats] Personal perk configurations applied successfully");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EditPersonalPerkStats] Failed to apply configurations: {e}");
            }
        }

        public override void OnConfigChanged()
        {
            try
            {
                EditPersonalPerkStatsPerks.ApplyPerkConfigurations();
                EditPersonalPerkStatsDescriptions.UpdatePerkDescriptions();
                ForceUIRefresh();
                
                Logger.LogInfo("[EditPersonalPerkStats] Configuration changes applied and descriptions updated");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EditPersonalPerkStats] Failed to apply configuration changes: {e}");
            }
        }

        public override void OnLevelStart(Base.Levels.Level level)
        {
            // Apply perk configurations when any level starts to ensure our values override other mods
            try
            {
                EditPersonalPerkStatsPerks.ApplyPerkConfigurations();
                Logger.LogInfo("[EditPersonalPerkStats] Re-applied perk configurations on level start");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EditPersonalPerkStats] Failed to re-apply configurations on level start: {e}");
            }
        }

        private void ForceUIRefresh()
        {
            try
            {
                I2.Loc.LocalizationManager.LocalizeAll(true);
                Logger.LogInfo("[EditPersonalPerkStats] Forced UI localization refresh");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EditPersonalPerkStats] Failed to force UI refresh: {e}");
            }
        }

        public override void OnModDisabled()
        {
            Instance = null;
            Logger.LogInfo("[EditPersonalPerkStats] Mod disabled");
        }
    }
}
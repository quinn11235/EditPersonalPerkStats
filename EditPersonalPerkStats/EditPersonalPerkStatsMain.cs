using Base.Core;
using Base.Defs;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using System;

namespace EditPersonalPerkStats
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
                // Apply TFTV-style perk configurations (core functionality)
                EditPersonalPerkStatsPerks.ApplyPerkConfigurations();
                
                // Update perk descriptions with configured values
                EditPersonalPerkStatsDescriptions.UpdatePerkDescriptions();
                
                // Note: UI stat display should work automatically with TFTV approach
                
                Logger.LogInfo("[EditPersonalPerkStats] Hybrid implementation applied successfully");
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
                // Reapply TFTV-style perk configurations
                EditPersonalPerkStatsPerks.ApplyPerkConfigurations();
                
                // Update descriptions with new configured values immediately
                EditPersonalPerkStatsDescriptions.UpdatePerkDescriptions();
                
                // Force UI refresh to show new descriptions without restart
                ForceUIRefresh();
                
                Logger.LogInfo("[EditPersonalPerkStats] Configuration changes applied and UI refreshed");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EditPersonalPerkStats] Failed to apply configuration changes: {e}");
            }
        }

        private void ForceUIRefresh()
        {
            try
            {
                // Force localization system to refresh all cached strings
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
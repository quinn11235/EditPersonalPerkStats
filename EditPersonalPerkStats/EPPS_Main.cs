using System;
using Base.Core;
using Base.Defs;
using Base.Levels;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;

namespace EditPersonalPerkStats
{
    public class EPPS_Main : ModMain
    {
        public new static EPPS_Main Main;
        public new static EPPS_Main Instance => Main;

        private Harmony _harmony;
        public Harmony HarmonyInstance => _harmony;

        public EPPS_Config EPPSConfig => (EPPS_Config)base.Config;

        public static DefRepository Repo => GameUtl.GameComponent<DefRepository>();
        public static DefRepository RepoSafe() => Repo;

        public override void OnModEnabled()
        {
            Main = this;
            Logger.LogInfo("[EPPS] Enabled.");

            _harmony = new Harmony("com.quinn11235.EditPersonalPerkStats");
            
            try
            {
                // Install all patches
                EPPS_RowSetControllerPatch.Install(_harmony);
                EPPS_GeoscapeStatsPatch.Install(_harmony);
                EPPS_StatCalculationPatch.Install(_harmony);
                EPPS_PerkRefreshPatch.Install(_harmony);

                // Apply perk changes
                EPPS_Perks.Change_Perks();

                // Apply description updates
                int n = EPPS_DefDescriptionUpdater.Apply(this.EPPSConfig, s => Logger.LogInfo(s));
                Logger.LogInfo($"[EPPS] Initial VE description update: {n}");

                Logger.LogInfo("[EPPS] Init complete.");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EPPS] Initialization failed: {e}");
            }
        }

        public override void OnLevelStart(Level level)
        {
            Logger.LogInfo("[EPPS] Level start: " + (level != null ? level.name : "<null>"));
            
            try
            {
                // Ensure patches are applied and perks are updated for new level
                EPPS_Perks.Change_Perks();
            }
            catch (Exception e)
            {
                Logger.LogWarning($"[EPPS] Level start perk update failed: {e.Message}");
            }
        }

        public override void OnConfigChanged()
        {
            try
            {
                // Reapply description updates with new config
                int n = EPPS_DefDescriptionUpdater.Apply(this.EPPSConfig, s => Logger.LogInfo(s));
                Logger.LogInfo($"[EPPS] Config change VE update: {n}");

                // Reinstall patches to ensure they use new config
                EPPS_RowSetControllerPatch.Install(_harmony);
                EPPS_GeoscapeStatsPatch.Install(_harmony);
                EPPS_StatCalculationPatch.Install(_harmony);
                EPPS_PerkRefreshPatch.Install(_harmony);

                // Apply perk changes with new config
                EPPS_Perks.Change_Perks();
            }
            catch (Exception e)
            {
                Logger.LogWarning($"[EPPS] Config change failed: {e.Message}");
            }
        }

        public override void OnModDisabled()
        {
            try
            {
                // Restore original descriptions
                EPPS_DefDescriptionUpdater.Reset(s => Logger.LogInfo(s));

                // Clean up managers
                EditPersonalPerkStats.GeoscapeRefreshManager.Shutdown();
                EditPersonalPerkStats.PerkRefreshCleanup.Shutdown();

                // Unpatch all
                if (_harmony != null)
                {
                    _harmony.UnpatchAll("com.quinn11235.EditPersonalPerkStats");
                    _harmony = null;
                }

                Logger.LogInfo("[EPPS] Disabled.");
            }
            catch (Exception e)
            {
                Logger.LogError($"[EPPS] Disable failed: {e}");
            }
            finally
            {
                Main = null;
            }
        }
    }
}
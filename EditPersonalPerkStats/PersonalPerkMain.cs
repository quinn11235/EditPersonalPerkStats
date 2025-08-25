using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Modding;
using HarmonyLib;
using System;

namespace EditPersonalPerkStats
{
    public class PersonalPerkMain : ModMain
    {
        internal static readonly DefRepository Repo = GameUtl.GameComponent<DefRepository>();
        public static PersonalPerkMain Main { get; private set; }

        public new PersonalPerkConfig Config => (PersonalPerkConfig)base.Config;

        public override bool CanSafelyDisable => true;

        public override void OnModEnabled()
        {
            Main = this;
            Logger.LogInfo($"Edit Personal Perk Stats - Refactored enabled.");
            
            try
            {
                HarmonyLib.Harmony harmony = (HarmonyLib.Harmony)HarmonyInstance;
                harmony.PatchAll(GetType().Assembly);
                Logger.LogInfo("Harmony patches applied successfully.");
                
                Logger.LogInfo("Personal perk modifications will be applied via tactical patches.");
                LogCurrentConfiguration();
            }
            catch (Exception e)
            {
                Logger.LogError($"Error enabling Edit Personal Perk Stats: {e}");
            }
        }

        public override void OnModDisabled()
        {
            try
            {
                HarmonyLib.Harmony harmony = (HarmonyLib.Harmony)HarmonyInstance;
                harmony.UnpatchAll(harmony.Id);
                Logger.LogInfo("Edit Personal Perk Stats - Refactored disabled.");
            }
            catch (Exception e)
            {
                Logger.LogError($"Error disabling Edit Personal Perk Stats: {e}");
            }
            
            Main = null;
        }

        public override void OnConfigChanged()
        {
            try
            {
                Logger.LogInfo("Configuration updated - changes will apply in tactical combat.");
                LogCurrentConfiguration();
            }
            catch (Exception e)
            {
                Logger.LogError($"Error handling config changes: {e}");
            }
        }

        private void LogCurrentConfiguration()
        {
            Logger.LogInfo("=== Current Perk Configuration ===");
            
            // Damage multipliers
            Logger.LogInfo($"Close Quarters Melee Damage: x{Config.CloseQuartersMelee}");
            Logger.LogInfo($"Close Quarters Shotgun Damage: x{Config.CloseQuartersShotgunDamage}");
            Logger.LogInfo($"Sniperist Damage: x{Config.SniperistDamage}");
            Logger.LogInfo($"Trooper Damage: x{Config.TrooperDamage}");
            Logger.LogInfo($"Strongman Heavy Weapon Damage: x{Config.StrongmanDamage}");
            Logger.LogInfo($"Self Defense Pistol Damage: x{Config.SelfDefensePistolDamage}");
            Logger.LogInfo($"Self Defense PDW Damage: x{Config.SelfDefensePDWDamage}");
            Logger.LogInfo($"Reckless Damage: x{Config.RecklessDamage}");
            Logger.LogInfo($"Bombardier Damage: x{Config.BombardierDamage}");
            
            // Accuracy bonuses
            Logger.LogInfo($"Close Quarters Shotgun Accuracy: +{Config.CloseQuartersShotgun}");
            Logger.LogInfo($"Trooper Accuracy: +{Config.TrooperAccuracy}");
            Logger.LogInfo($"Strongman Heavy Weapon Accuracy: +{Config.StrongmanAccuracy}");
            Logger.LogInfo($"Self Defense Pistol Accuracy: +{Config.SelfDefensePistolAccuracy}");
            Logger.LogInfo($"Self Defense PDW Accuracy: +{Config.SelfDefensePDWAccuracy}");
            Logger.LogInfo($"Cautious Accuracy: +{Config.CautiousAccuracy}");
            Logger.LogInfo($"Reckless Accuracy: {Config.RecklessAccuracy}");
            
            // Range multipliers
            Logger.LogInfo($"Quarterback Grenade Range: x{Config.QuarterbackRange}");
            Logger.LogInfo($"Bombardier Range: x{Config.BombardierRange}");
            
            // Attribute bonuses (applied via stat modifications would need different implementation)
            Logger.LogInfo($"Quarterback Speed: +{Config.QuarterbackSpeed}");
            Logger.LogInfo($"Resourceful Strength: +{Config.ResourcefulStrength}");
            Logger.LogInfo($"Farsighted Perception: +{Config.FarsightedPerception}");
            Logger.LogInfo($"Healer Willpower: +{Config.HealerWillpower}");
            Logger.LogInfo($"Thief Speed: +{Config.ThiefSpeed}");
            Logger.LogInfo($"Sniperist Willpower: {Config.SniperistWillpower}");
            Logger.LogInfo($"Strongman Strength: +{Config.StrongmanStrength}");
            Logger.LogInfo($"Strongman Perception: {Config.StrongmanPerception}");
            
            Logger.LogInfo("=== End Configuration ===");
        }
    }
}
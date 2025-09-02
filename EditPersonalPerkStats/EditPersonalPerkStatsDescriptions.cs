using System.Linq;
using Base.Defs;
using PhoenixPoint.Tactical.Entities.Abilities;
using System.IO;
using I2.Loc;

namespace Quinn11235.EditPersonalPerkStats
{
    internal static class EditPersonalPerkStatsDescriptions
    {
        private static readonly DefRepository Repo = EditPersonalPerkStatsMain.Repo;

        public static void UpdatePerkDescriptions()
        {
            var config = (EditPersonalPerkStatsConfig)EditPersonalPerkStatsMain.Instance.Config;
            
            try
            {
                // Always recreate dynamic CSV content with current configured values
                CreateDynamicLocalizationCSV(config);
                
                // Load the updated CSV into the localization system
                LoadLocalizationCSV();
                
                // Update ability localization keys
                UpdateAbilityLocalizationKeys();
                
                EditPersonalPerkStatsMain.Instance.Logger.LogInfo("[EditPersonalPerkStats] Perk descriptions updated successfully with current config values");
            }
            catch (System.Exception e)
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogError($"[EditPersonalPerkStats] Error updating perk descriptions: {e}");
            }
        }

        private static void CreateDynamicLocalizationCSV(EditPersonalPerkStatsConfig config)
        {
            // Use current directory since Assets folder is copied to output directory
            var localizationPath = Path.Combine("Assets", "Localization");
            Directory.CreateDirectory(localizationPath);
            
            var csvPath = Path.Combine(localizationPath, "EditPersonalPerkStats.csv");
            
            // Calculate percentage values for display using proper rounding
            int shotgunAccuracyPercent = (int)System.Math.Round(config.CloseQuartersShotgun * 100);
            int shotgunDamagePercent = (int)System.Math.Round((config.CloseQuartersShotgunDamage - 1) * 100);
            int meleeDamagePercent = (int)System.Math.Round((config.CloseQuartersMelee - 1) * 100);
            
            int heavyAccuracyPercent = (int)System.Math.Round(config.StrongmanAccuracy * 100);
            int heavyDamagePercent = (int)System.Math.Round((config.StrongmanDamage - 1) * 100);
            int perceptionPenalty = (int)System.Math.Round(System.Math.Abs(config.StrongmanPerception));
            int strengthBonus = (int)System.Math.Round(config.StrongmanStrength);
            
            int speedBonus = (int)System.Math.Round(config.QuarterbackSpeed);
            int rangePercent = (int)System.Math.Round((config.QuarterbackRange - 1) * 100);
            
            int resourcefulStrength = (int)System.Math.Round(config.ResourcefulStrength);
            int carryPercent = (int)System.Math.Round((config.ResourcefulCarry - 1) * 100);
            
            int willpowerPenalty = (int)System.Math.Round(System.Math.Abs(config.SniperistWillpower));
            int sniperistDamagePercent = (int)System.Math.Round((config.SniperistDamage - 1) * 100);
            
            int trooperAccuracyPercent = (int)System.Math.Round(config.TrooperAccuracy * 100);
            int trooperDamagePercent = (int)System.Math.Round((config.TrooperDamage - 1) * 100);
            
            int recklessDamagePercent = (int)System.Math.Round((config.RecklessDamage - 1) * 100);
            int recklessAccuracyPenalty = (int)System.Math.Round(System.Math.Abs(config.RecklessAccuracy * 100));
            
            int cautiousAccuracyPercent = (int)System.Math.Round(config.CautiousAccuracy * 100);
            int cautiousDamageReduction = (int)System.Math.Round((1 - config.CautiousDamage) * 100);
            
            int thiefSpeed = (int)System.Math.Round(config.ThiefSpeed);
            int thiefStealthPercent = (int)System.Math.Round(config.ThiefStealth * 100);
            
            // Additional perks calculations
            int farsightedPerception = (int)System.Math.Round(config.FarsightedPerception);
            int farsightedWillpower = (int)System.Math.Round(config.FarsightedWillpower);
            
            int healerWillpower = (int)System.Math.Round(config.HealerWillpower);
            int healerHealPercent = (int)System.Math.Round((config.HealerHeal - 1) * 100);
            
            int selfDefenseHearing = (int)System.Math.Round(config.SelfDefenseHearing);
            int selfDefensePistolAccuracy = (int)System.Math.Round(config.SelfDefensePistolAccuracy * 100);
            int selfDefensePistolDamage = (int)System.Math.Round((config.SelfDefensePistolDamage - 1) * 100);
            int selfDefensePDWAccuracy = (int)System.Math.Round(config.SelfDefensePDWAccuracy * 100);
            int selfDefensePDWDamage = (int)System.Math.Round((config.SelfDefensePDWDamage - 1) * 100);
            
            int bombardierDamage = (int)System.Math.Round((config.BombardierDamage - 1) * 100);
            int bombardierRange = (int)System.Math.Round((config.BombardierRange - 1) * 100);
            
            int biochemistViral = (int)System.Math.Round(config.BiochemistViral);

            // Build CSV content without quotes to avoid display issues - use semicolon separator instead
            var csvBuilder = new System.Text.StringBuilder();
            csvBuilder.AppendLine("Key;Type;Desc;English");
            csvBuilder.AppendLine($"EPPS_CLOSE_QUARTERS_DESC;Text;;Gain shotgun proficiency with +{shotgunAccuracyPercent}% accuracy, +{shotgunDamagePercent}% damage and melee weapon proficiency with +{meleeDamagePercent}% damage.");
            csvBuilder.AppendLine($"EPPS_STRONGMAN_DESC;Text;;Gain heavy weapons proficiency with +{heavyAccuracyPercent}% accuracy, +{heavyDamagePercent}% damage, -{perceptionPenalty} perception and +{strengthBonus} strength.");
            csvBuilder.AppendLine($"EPPS_QUARTERBACK_DESC;Text;;+{rangePercent}% bonus grenades range and +{speedBonus} speed.");
            csvBuilder.AppendLine($"EPPS_RESOURCEFUL_DESC;Text;;+{carryPercent}% bonus carry weight and +{resourcefulStrength} strength.");
            csvBuilder.AppendLine($"EPPS_SNIPERIST_DESC;Text;;Gain sniper rifle proficiency with +{sniperistDamagePercent}% damage and -{willpowerPenalty} willpower.");
            csvBuilder.AppendLine($"EPPS_TROOPER_DESC;Text;;Gain assault rifle proficiency with +{trooperDamagePercent}% damage and +{trooperAccuracyPercent}% accuracy.");
            csvBuilder.AppendLine($"EPPS_RECKLESS_DESC;Text;;+{recklessDamagePercent}% bonus damage dealt and -{recklessAccuracyPenalty}% accuracy.");
            csvBuilder.AppendLine($"EPPS_CAUTIOUS_DESC;Text;;+{cautiousAccuracyPercent}% bonus accuracy and -{cautiousDamageReduction}% damage dealt.");
            csvBuilder.AppendLine($"EPPS_THIEF_DESC;Text;;+{thiefStealthPercent}% bonus stealth and +{thiefSpeed} speed.");
            csvBuilder.AppendLine($"EPPS_FARSIGHTED_DESC;Text;;Additional +{farsightedWillpower} to willpower and +{farsightedPerception} perception.");
            csvBuilder.AppendLine($"EPPS_HEALER_DESC;Text;;+{healerHealPercent}% bonus healing and +{healerWillpower} willpower.");
            csvBuilder.AppendLine($"EPPS_SELFDEFENSE_DESC;Text;;Gain PDW and handgun proficiency with +{selfDefensePDWDamage}% damage, +{selfDefensePistolAccuracy}% accuracy and +{selfDefenseHearing} tiles hearing range.");
            csvBuilder.AppendLine($"EPPS_BOMBARDIER_DESC;Text;;Gain mounted weapon proficiency with +{bombardierRange}% range and +{bombardierDamage}% damage.");
            csvBuilder.AppendLine($"EPPS_BIOCHEMIST_DESC;Text;;All attacks that damage a target also inflict {biochemistViral} Virus Damage virus damage (per bullet).");
            
            string csvContent = csvBuilder.ToString();
            
            File.WriteAllText(csvPath, csvContent);
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Created dynamic localization CSV at {csvPath}");
        }

        private static void LoadLocalizationCSV()
        {
            try
            {
                // Use relative path since Assets folder is copied to output directory
                var localizationPath = Path.Combine("Assets", "Localization");
                var csvPath = Path.Combine(localizationPath, "EditPersonalPerkStats.csv");
                
                string csvContent = File.ReadAllText(csvPath);
                if (!csvContent.EndsWith("\n"))
                {
                    csvContent += "\n";
                }
                
                var sourceToChange = LocalizationManager.Sources[0]; // Use first language source
                if (sourceToChange != null)
                {
                    int numBefore = sourceToChange.mTerms.Count;
                    sourceToChange.Import_CSV(string.Empty, csvContent, eSpreadsheetUpdateMode.AddNewTerms, ';'); // Use semicolon separator
                    LocalizationManager.LocalizeAll(true); // Force localization refresh
                    int numAfter = sourceToChange.mTerms.Count;
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Added {numAfter - numBefore} localization terms");
                }
            }
            catch (System.Exception e)
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogError($"[EditPersonalPerkStats] Failed to load localization CSV: {e}");
            }
        }

        private static void UpdateAbilityLocalizationKeys()
        {
            // Update Close Quarters Specialist
            var closeQuarters = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuartersSpecialist_AbilityDef"));
            if (closeQuarters?.ViewElementDef?.Description != null)
            {
                closeQuarters.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_CLOSE_QUARTERS_DESC");
            }

            // Update Strongman
            var strongman = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Strongman_AbilityDef"));
            if (strongman?.ViewElementDef?.Description != null)
            {
                strongman.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_STRONGMAN_DESC");
            }

            // Update Quarterback
            var quarterback = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Pitcher_AbilityDef"));
            if (quarterback?.ViewElementDef?.Description != null)
            {
                quarterback.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_QUARTERBACK_DESC");
            }

            // Update Resourceful
            var resourceful = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Resourceful_AbilityDef"));
            if (resourceful?.ViewElementDef?.Description != null)
            {
                resourceful.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_RESOURCEFUL_DESC");
            }

            // Update Sniperist
            var sniperist = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Focused_AbilityDef"));
            if (sniperist?.ViewElementDef?.Description != null)
            {
                sniperist.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_SNIPERIST_DESC");
            }

            // Update Trooper
            var trooper = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("GoodShot_AbilityDef"));
            if (trooper?.ViewElementDef?.Description != null)
            {
                trooper.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_TROOPER_DESC");
            }

            // Update Reckless
            var reckless = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Reckless_AbilityDef"));
            if (reckless?.ViewElementDef?.Description != null)
            {
                reckless.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_RECKLESS_DESC");
            }

            // Update Cautious
            var cautious = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Cautious_AbilityDef"));
            if (cautious?.ViewElementDef?.Description != null)
            {
                cautious.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_CAUTIOUS_DESC");
            }

            // Update Thief
            var thief = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Thief_AbilityDef"));
            if (thief?.ViewElementDef?.Description != null)
            {
                thief.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_THIEF_DESC");
            }

            // Update Farsighted (Brainiac)
            var farsighted = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Brainiac_AbilityDef"));
            if (farsighted?.ViewElementDef?.Description != null)
            {
                farsighted.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_FARSIGHTED_DESC");
            }

            // Update Healer (Helpful)
            var healer = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Helpful_AbilityDef"));
            if (healer?.ViewElementDef?.Description != null)
            {
                healer.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_HEALER_DESC");
            }

            // Update Self Defense Specialist
            var selfDefense = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("SelfDefenseSpecialist_AbilityDef"));
            if (selfDefense?.ViewElementDef?.Description != null)
            {
                selfDefense.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_SELFDEFENSE_DESC");
            }

            // Update Bombardier (Crafty)
            var bombardier = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Crafty_AbilityDef"));
            if (bombardier?.ViewElementDef?.Description != null)
            {
                bombardier.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_BOMBARDIER_DESC");
            }

            // Update Biochemist
            var biochemist = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("BioChemist_AbilityDef"));
            if (biochemist?.ViewElementDef?.Description != null)
            {
                biochemist.ViewElementDef.Description = new Base.UI.LocalizedTextBind("EPPS_BIOCHEMIST_DESC");
            }
        }
    }
}
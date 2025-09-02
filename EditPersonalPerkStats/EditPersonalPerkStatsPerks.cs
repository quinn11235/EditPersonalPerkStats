using System.Collections.Generic;
using System.Linq;
using Base.Defs;
using Base.Entities.Statuses;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Equipments;
using UnityEngine;
namespace Quinn11235.EditPersonalPerkStats
{
    internal static class EditPersonalPerkStatsPerks
    {
        private static readonly DefRepository Repo = EditPersonalPerkStatsMain.Repo;

        public static void ApplyPerkConfigurations()
        {
            var config = (EditPersonalPerkStatsConfig)EditPersonalPerkStatsMain.Instance.Config;
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Config object: {config?.GetType()?.FullName ?? "NULL"}");
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Config RecklessDamage default check: {config?.RecklessDamage ?? -999f}");
            ConfigureAllPerks(config);
        }

        private static void ConfigureAllPerks(EditPersonalPerkStatsConfig config)
        {
            try
            {
                // Configure each perk using TFTV-style StatModifications arrays
                ConfigureQuarterback(config);
                ConfigureResourceful(config);
                ConfigureFarsighted(config);
                ConfigureHealer(config);
                ConfigureThief(config);
                ConfigureSniperist(config);
                ConfigureStrongman(config);
                ConfigureSelfDefense(config);
                ConfigureReckless(config);
                ConfigureCautious(config);
                ConfigureTrooper(config);
                ConfigureBombardier(config);
                ConfigureCloseQuarters(config);
                ConfigureBiochemist(config);
                
                EditPersonalPerkStatsMain.Instance.Logger.LogInfo("[EditPersonalPerkStats] All perks configured successfully");
            }
            catch (System.Exception e)
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogError($"[EditPersonalPerkStats] Error configuring perks: {e}");
            }
        }

        #region Individual Perk Configuration Methods

        private static void ConfigureQuarterback(EditPersonalPerkStatsConfig config)
        {
            var quarterback = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Pitcher_AbilityDef"));
            if (quarterback == null) return;

            // Create new StatModifications array for Speed bonus
            quarterback.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Speed,
                    Modification = StatModificationType.Add,
                    Value = config.QuarterbackSpeed
                }
            };

            // Configure grenade range bonus
            if (quarterback.ItemTagStatModifications?.Length > 0)
            {
                quarterback.ItemTagStatModifications[0].EquipmentStatModification.Value = config.QuarterbackRange;
            }
        }

        private static void ConfigureResourceful(EditPersonalPerkStatsConfig config)
        {
            var resourceful = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Resourceful_AbilityDef"));
            if (resourceful == null) return;

            // Create new StatModifications array for Strength and Carry Weight bonuses
            resourceful.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Endurance,
                    Modification = StatModificationType.Add,
                    Value = config.ResourcefulStrength
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Endurance,
                    Modification = StatModificationType.AddMax,
                    Value = config.ResourcefulMaxStrength
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.CarryWeight,
                    Modification = StatModificationType.Multiply,
                    Value = config.ResourcefulCarry
                }
            };
        }

        private static void ConfigureFarsighted(EditPersonalPerkStatsConfig config)
        {
            var farsighted = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Brainiac_AbilityDef"));
            if (farsighted == null) return;

            // Create new StatModifications array for Perception and Willpower bonuses
            farsighted.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Perception,
                    Modification = StatModificationType.Add,
                    Value = config.FarsightedPerception
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Willpower,
                    Modification = StatModificationType.Add,
                    Value = config.FarsightedWillpower
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Willpower,
                    Modification = StatModificationType.AddMax,
                    Value = config.FarsightedMaxWillpower
                }
            };
        }

        private static void ConfigureHealer(EditPersonalPerkStatsConfig config)
        {
            var healer = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Helpful_AbilityDef"));
            if (healer == null) return;

            // Create new StatModifications array for Willpower and Healing bonuses
            healer.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Willpower,
                    Modification = StatModificationType.Add,
                    Value = config.HealerWillpower
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Willpower,
                    Modification = StatModificationType.AddMax,
                    Value = config.HealerMaxWillpower
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.BonusHealValue,
                    Modification = StatModificationType.Multiply,
                    Value = config.HealerHeal
                }
            };
        }

        private static void ConfigureThief(EditPersonalPerkStatsConfig config)
        {
            var thief = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Thief_AbilityDef"));
            if (thief == null) return;

            // Create new StatModifications array for Speed and Stealth bonuses
            thief.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Speed,
                    Modification = StatModificationType.Add,
                    Value = config.ThiefSpeed
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Stealth,
                    Modification = StatModificationType.Add,
                    Value = config.ThiefStealth
                }
            };
        }

        private static void ConfigureSniperist(EditPersonalPerkStatsConfig config)
        {
            var sniperist = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Focused_AbilityDef"));
            if (sniperist == null) return;

            // Create new StatModifications array for Willpower penalty
            sniperist.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Willpower,
                    Modification = StatModificationType.Add,
                    Value = config.SniperistWillpower // This should be negative
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Willpower,
                    Modification = StatModificationType.AddMax,
                    Value = config.SniperistMaxWillpower // This should be negative
                }
            };

            // CRITICAL: Don't replace ItemTagStatModifications - update existing sniper rifle proficiency
            var sniperTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("SniperRifleItem_TagDef"));
            
            if (sniperist.ItemTagStatModifications != null && sniperTagDef != null)
            {
                // Update existing sniper rifle damage modification (preserve proficiency)
                var sniperDamageMod = sniperist.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == sniperTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                if (sniperDamageMod != null)
                {
                    sniperDamageMod.EquipmentStatModification.Value = config.SniperistDamage;
                }
            }
        }

        private static void ConfigureStrongman(EditPersonalPerkStatsConfig config)
        {
            var strongman = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Strongman_AbilityDef"));
            if (strongman == null) 
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogWarning("[EditPersonalPerkStats] Strongman_AbilityDef not found!");
                return;
            }

            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Configuring Strongman with damage multiplier: {config.StrongmanDamage}");

            // Create new StatModifications array for Perception penalty and Strength bonus
            strongman.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Perception,
                    Modification = StatModificationType.Add,
                    Value = config.StrongmanPerception // This should be negative
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Endurance,
                    Modification = StatModificationType.Add,
                    Value = config.StrongmanStrength
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Endurance,
                    Modification = StatModificationType.AddMax,
                    Value = config.StrongmanMaxStrength
                }
            };

            // Debug: Check what ItemTagStatModifications exist in base game
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Strongman ItemTagStatModifications count: {strongman.ItemTagStatModifications?.Length ?? 0}");
            if (strongman.ItemTagStatModifications != null)
            {
                for (int i = 0; i < strongman.ItemTagStatModifications.Length; i++)
                {
                    var mod = strongman.ItemTagStatModifications[i];
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] [{i}] Tag: {mod.ItemTag?.name}, Stat: {mod.EquipmentStatModification.TargetStat}, Modification: {mod.EquipmentStatModification.Modification}, Value: {mod.EquipmentStatModification.Value}");
                }
            }
            
            // Also check if there are already BonusAttackDamage modifications for heavy weapons  
            var heavyTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("HeavyItem_TagDef"));
            var existingHeavyDamageMods = strongman.ItemTagStatModifications
                ?.Where(mod => mod.ItemTag == heavyTagDef && 
                              mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage)
                ?.ToList();
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Existing heavy weapon damage mods: {existingHeavyDamageMods?.Count ?? 0}");
            
            // Log each damage mod in detail
            if (existingHeavyDamageMods?.Count > 0)
            {
                for (int i = 0; i < existingHeavyDamageMods.Count; i++)
                {
                    var mod = existingHeavyDamageMods[i];
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Damage mod [{i}]: Value={mod.EquipmentStatModification.Value}, Modification={mod.EquipmentStatModification.Modification}");
                }
            }

            // CRITICAL: Don't replace ItemTagStatModifications - update existing heavy weapon proficiency
            
            if (strongman.ItemTagStatModifications != null && heavyTagDef != null)
            {
                // Update existing heavy weapon accuracy modification (preserve proficiency)
                var heavyAccuracyMod = strongman.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == heavyTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.Accuracy);
                if (heavyAccuracyMod != null)
                {
                    heavyAccuracyMod.EquipmentStatModification.Value = config.StrongmanAccuracy;
                }

                // Check if we already have damage bonuses from previous runs - if so, just update the first one
                var existingDamageBonuses = strongman.ItemTagStatModifications
                    .Where(mod => mod.ItemTag == heavyTagDef && 
                                mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage)
                    .ToList();
                
                if (existingDamageBonuses.Count > 0)
                {
                    // Update the first existing bonus and remove any duplicates
                    var firstBonus = existingDamageBonuses[0];
                    firstBonus.EquipmentStatModification.Value = config.StrongmanDamage;
                    firstBonus.EquipmentStatModification.Modification = StatModificationType.Multiply;
                    
                    // Remove duplicate bonuses
                    var modsList = strongman.ItemTagStatModifications.ToList();
                    for (int i = 1; i < existingDamageBonuses.Count; i++)
                    {
                        modsList.Remove(existingDamageBonuses[i]);
                    }
                    strongman.ItemTagStatModifications = modsList.ToArray();
                    
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Updated existing damage bonus to {config.StrongmanDamage}, removed {existingDamageBonuses.Count - 1} duplicates");
                }
                else
                {
                    // No existing damage bonus, add one
                    var modsList = strongman.ItemTagStatModifications.ToList();
                    modsList.Add(new EquipmentItemTagStatModification()
                    {
                        ItemTag = heavyTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.BonusAttackDamage,
                            Modification = StatModificationType.Multiply,
                            Value = config.StrongmanDamage
                        }
                    });
                    strongman.ItemTagStatModifications = modsList.ToArray();
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Added new damage bonus: {config.StrongmanDamage}");
                }
                
                EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Final ItemTagStatModifications count: {strongman.ItemTagStatModifications.Length}");
            }
            else
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogWarning("[EditPersonalPerkStats] Strongman has no ItemTagStatModifications or heavy tag not found");
            }
        }

        private static void ConfigureSelfDefense(EditPersonalPerkStatsConfig config)
        {
            var selfDefense = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("SelfDefenseSpecialist_AbilityDef"));
            if (selfDefense == null) return;

            // Create new StatModifications array for Hearing Range
            selfDefense.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.HearingRange,
                    Modification = StatModificationType.Add,
                    Value = config.SelfDefenseHearing
                }
            };

            // CRITICAL: Don't replace ItemTagStatModifications - update existing handgun/PDW proficiencies
            var pistolTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("HandgunItem_TagDef"));
            var pdwTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("PDWItem_TagDef"));
            
            if (selfDefense.ItemTagStatModifications != null)
            {
                // Update pistol modifications (preserve proficiency)
                if (pistolTagDef != null)
                {
                    // Update existing pistol accuracy modification
                    var pistolAccuracyMod = selfDefense.ItemTagStatModifications
                        .FirstOrDefault(mod => mod.ItemTag == pistolTagDef && 
                                             mod.EquipmentStatModification.TargetStat == StatModificationTarget.Accuracy);
                    if (pistolAccuracyMod != null)
                    {
                        pistolAccuracyMod.EquipmentStatModification.Value = config.SelfDefensePistolAccuracy;
                    }

                    // Add or update pistol damage bonus
                    var pistolDamageMod = selfDefense.ItemTagStatModifications
                        .FirstOrDefault(mod => mod.ItemTag == pistolTagDef && 
                                             mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                    
                    if (pistolDamageMod == null)
                    {
                        // Add new damage bonus while preserving existing modifications
                        var modsList = selfDefense.ItemTagStatModifications.ToList();
                        modsList.Add(new EquipmentItemTagStatModification()
                        {
                            ItemTag = pistolTagDef,
                            EquipmentStatModification = new ItemStatModification()
                            {
                                TargetStat = StatModificationTarget.BonusAttackDamage,
                                Modification = StatModificationType.Multiply,
                                Value = config.SelfDefensePistolDamage
                            }
                        });
                        selfDefense.ItemTagStatModifications = modsList.ToArray();
                    }
                    else
                    {
                        pistolDamageMod.EquipmentStatModification.Value = config.SelfDefensePistolDamage;
                    }
                }

                // Update PDW modifications (preserve proficiency)
                if (pdwTagDef != null)
                {
                    // Update existing PDW accuracy modification
                    var pdwAccuracyMod = selfDefense.ItemTagStatModifications
                        .FirstOrDefault(mod => mod.ItemTag == pdwTagDef && 
                                             mod.EquipmentStatModification.TargetStat == StatModificationTarget.Accuracy);
                    if (pdwAccuracyMod != null)
                    {
                        pdwAccuracyMod.EquipmentStatModification.Value = config.SelfDefensePDWAccuracy;
                    }

                    // Add or update PDW damage bonus
                    var pdwDamageMod = selfDefense.ItemTagStatModifications
                        .FirstOrDefault(mod => mod.ItemTag == pdwTagDef && 
                                             mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                    
                    if (pdwDamageMod == null)
                    {
                        // Add new damage bonus while preserving existing modifications
                        var modsList = selfDefense.ItemTagStatModifications.ToList();
                        modsList.Add(new EquipmentItemTagStatModification()
                        {
                            ItemTag = pdwTagDef,
                            EquipmentStatModification = new ItemStatModification()
                            {
                                TargetStat = StatModificationTarget.BonusAttackDamage,
                                Modification = StatModificationType.Multiply,
                                Value = config.SelfDefensePDWDamage
                            }
                        });
                        selfDefense.ItemTagStatModifications = modsList.ToArray();
                    }
                    else
                    {
                        pdwDamageMod.EquipmentStatModification.Value = config.SelfDefensePDWDamage;
                    }
                }
            }
        }

        private static void ConfigureReckless(EditPersonalPerkStatsConfig config)
        {
            var reckless = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Reckless_AbilityDef"));
            if (reckless == null)
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogWarning("[EditPersonalPerkStats] Reckless_AbilityDef not found!");
                return;
            }

            // CRITICAL DEBUG: Log the entire StatModifications array structure
            if (reckless.StatModifications?.Length > 0)
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Reckless StatModifications array has {reckless.StatModifications.Length} entries:");
                for (int i = 0; i < reckless.StatModifications.Length; i++)
                {
                    var mod = reckless.StatModifications[i];
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats]   [{i}] TargetStat: {mod.TargetStat}, Type: {mod.Modification}, Value: {mod.Value}");
                }
            }

            // CRITICAL DEBUG: Log config object and source
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Config object type: {config.GetType().FullName}");
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Config source: {EditPersonalPerkStatsMain.Instance.Config.GetType().FullName}");
            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Config values: RecklessDamage = {config.RecklessDamage}, RecklessAccuracy = {config.RecklessAccuracy}");

            // CRITICAL: Use direct array access to modify existing StatModifications (like the working mod)
            if (reckless.StatModifications?.Length >= 2)
            {
                reckless.StatModifications[0].Value = config.RecklessDamage;   // Damage multiplier
                reckless.StatModifications[1].Value = config.RecklessAccuracy; // Accuracy penalty
                
                EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Applied: [{0}] = {config.RecklessDamage}, [{1}] = {config.RecklessAccuracy}");
            }
            else
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogWarning($"[EditPersonalPerkStats] StatModifications array too short! Length = {reckless.StatModifications?.Length ?? 0}");
            }
        }

        private static void ConfigureCautious(EditPersonalPerkStatsConfig config)
        {
            var cautious = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Cautious_AbilityDef"));
            if (cautious == null) return;

            // Create new StatModifications array for accuracy bonus and damage penalty
            cautious.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Accuracy,
                    Modification = StatModificationType.Add,
                    Value = config.CautiousAccuracy
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.BonusAttackDamage,
                    Modification = StatModificationType.Multiply,
                    Value = config.CautiousDamage
                }
            };
        }

        private static void ConfigureTrooper(EditPersonalPerkStatsConfig config)
        {
            var trooper = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("GoodShot_AbilityDef"));
            if (trooper == null) return;

            // CRITICAL: Don't replace ItemTagStatModifications - update existing assault rifle proficiency
            var rifleTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("AssaultRifleItem_TagDef"));
            
            if (trooper.ItemTagStatModifications != null && rifleTagDef != null)
            {
                // Update existing assault rifle accuracy modification (preserve proficiency)
                var rifleAccuracyMod = trooper.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == rifleTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.Accuracy);
                if (rifleAccuracyMod != null)
                {
                    rifleAccuracyMod.EquipmentStatModification.Value = config.TrooperAccuracy;
                }

                // Add assault rifle damage bonus while preserving existing proficiencies
                var rifleDamageMod = trooper.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == rifleTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                
                if (rifleDamageMod == null)
                {
                    // Add new damage bonus while preserving existing modifications
                    var modsList = trooper.ItemTagStatModifications.ToList();
                    modsList.Add(new EquipmentItemTagStatModification()
                    {
                        ItemTag = rifleTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.BonusAttackDamage,
                            Modification = StatModificationType.Multiply,
                            Value = config.TrooperDamage
                        }
                    });
                    trooper.ItemTagStatModifications = modsList.ToArray();
                }
                else
                {
                    rifleDamageMod.EquipmentStatModification.Value = config.TrooperDamage;
                }
            }
        }

        private static void ConfigureBombardier(EditPersonalPerkStatsConfig config)
        {
            var bombardier = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Crafty_AbilityDef"));
            if (bombardier == null) return;

            // CRITICAL: Don't replace ItemTagStatModifications - update existing mounted weapon proficiency
            var mountedTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("MountedWeaponItem_TagDef"));
            
            if (bombardier.ItemTagStatModifications != null && mountedTagDef != null)
            {
                // Update existing mounted weapon damage modification (preserve proficiency)
                var mountedDamageMod = bombardier.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == mountedTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                if (mountedDamageMod != null)
                {
                    mountedDamageMod.EquipmentStatModification.Value = config.BombardierDamage;
                }

                // Update existing mounted weapon range modification (preserve proficiency)
                var mountedRangeMod = bombardier.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == mountedTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackRange);
                if (mountedRangeMod != null)
                {
                    mountedRangeMod.EquipmentStatModification.Value = config.BombardierRange;
                }
            }
        }

        private static void ConfigureCloseQuarters(EditPersonalPerkStatsConfig config)
        {
            var closeQuarters = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuartersSpecialist_AbilityDef"));
            if (closeQuarters == null) 
            {
                EditPersonalPerkStatsMain.Instance.Logger.LogWarning("[EditPersonalPerkStats] CloseQuartersSpecialist_AbilityDef not found!");
                return;
            }

            EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Configuring Close Quarters with shotgun damage multiplier: {config.CloseQuartersShotgunDamage}");


            // IMPORTANT: Don't completely replace ItemTagStatModifications - this breaks weapon proficiencies!
            // Instead, find and update existing entries or add new ones while preserving proficiency structure
            var meleeTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("MeleeWeapon_TagDef"));
            var shotgunTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("ShotgunItem_TagDef"));

            if (closeQuarters.ItemTagStatModifications != null && closeQuarters.ItemTagStatModifications.Length > 0)
            {
                // Update existing melee weapon modification (preserve proficiency)
                var meleeModification = closeQuarters.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == meleeTagDef);
                if (meleeModification != null)
                {
                    meleeModification.EquipmentStatModification.Value = config.CloseQuartersMelee;
                }

                // Update existing shotgun accuracy modification (preserve proficiency)
                var shotgunModification = closeQuarters.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == shotgunTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.Accuracy);
                if (shotgunModification != null)
                {
                    shotgunModification.EquipmentStatModification.Value = config.CloseQuartersShotgun;
                }

                // Check if we already have damage bonuses from previous runs - if so, just update the first one
                var existingDamageBonuses = closeQuarters.ItemTagStatModifications
                    .Where(mod => mod.ItemTag == shotgunTagDef && 
                                mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage)
                    .ToList();
                
                if (existingDamageBonuses.Count > 0)
                {
                    // Update the first existing bonus and remove any duplicates
                    var firstBonus = existingDamageBonuses[0];
                    firstBonus.EquipmentStatModification.Value = config.CloseQuartersShotgunDamage;
                    firstBonus.EquipmentStatModification.Modification = StatModificationType.Multiply;
                    
                    // Remove duplicate bonuses
                    var modsList = closeQuarters.ItemTagStatModifications.ToList();
                    for (int i = 1; i < existingDamageBonuses.Count; i++)
                    {
                        modsList.Remove(existingDamageBonuses[i]);
                    }
                    closeQuarters.ItemTagStatModifications = modsList.ToArray();
                    
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Updated existing shotgun damage bonus to {config.CloseQuartersShotgunDamage}, removed {existingDamageBonuses.Count - 1} duplicates");
                }
                else
                {
                    // No existing damage bonus, add one
                    var modsList = closeQuarters.ItemTagStatModifications.ToList();
                    modsList.Add(new EquipmentItemTagStatModification()
                    {
                        ItemTag = shotgunTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.BonusAttackDamage,
                            Modification = StatModificationType.Multiply,
                            Value = config.CloseQuartersShotgunDamage
                        }
                    });
                    closeQuarters.ItemTagStatModifications = modsList.ToArray();
                    EditPersonalPerkStatsMain.Instance.Logger.LogInfo($"[EditPersonalPerkStats] Added new shotgun damage bonus: {config.CloseQuartersShotgunDamage}");
                }
            }
        }

        private static void ConfigureBiochemist(EditPersonalPerkStatsConfig config)
        {
            var biochemist = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("BioChemist_AbilityDef"));
            if (biochemist == null) return;

            // Configure viral damage bonus
            if (biochemist.DamageKeywordPairs?.Length > 0)
            {
                biochemist.DamageKeywordPairs[0].Value = config.BiochemistViral;
            }
        }

        #endregion
    }
}
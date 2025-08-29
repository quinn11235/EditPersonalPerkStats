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
namespace EditPersonalPerkStats
{
    internal static class EditPersonalPerkStatsPerks
    {
        private static readonly DefRepository Repo = EditPersonalPerkStatsMain.Repo;

        public static void ApplyPerkConfigurations()
        {
            var config = (EditPersonalPerkStatsConfig)EditPersonalPerkStatsMain.Instance.Config;
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
            if (strongman == null) return;

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

            // CRITICAL: Don't replace ItemTagStatModifications - update existing heavy weapon proficiency
            var heavyTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Equals("HeavyItem_TagDef"));
            
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

                // Add heavy weapon damage bonus while preserving existing proficiencies
                var heavyDamageMod = strongman.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == heavyTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                
                if (heavyDamageMod == null)
                {
                    // Add new damage bonus while preserving existing modifications
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
                }
                else
                {
                    heavyDamageMod.EquipmentStatModification.Value = config.StrongmanDamage;
                }
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
            if (reckless == null) return;

            // Create new StatModifications array for general damage bonus and accuracy penalty
            reckless.StatModifications = new ItemStatModification[]
            {
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.BonusAttackDamage,
                    Modification = StatModificationType.Multiply,
                    Value = config.RecklessDamage
                },
                new ItemStatModification()
                {
                    TargetStat = StatModificationTarget.Accuracy,
                    Modification = StatModificationType.Add,
                    Value = config.RecklessAccuracy // This should be negative
                }
            };
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
            if (closeQuarters == null) return;

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

                // Add shotgun damage bonus if it doesn't exist (while preserving existing proficiencies)
                var shotgunDamageModification = closeQuarters.ItemTagStatModifications
                    .FirstOrDefault(mod => mod.ItemTag == shotgunTagDef && 
                                         mod.EquipmentStatModification.TargetStat == StatModificationTarget.BonusAttackDamage);
                
                if (shotgunDamageModification == null && shotgunTagDef != null)
                {
                    // Add new shotgun damage bonus while preserving existing modifications
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
                }
                else if (shotgunDamageModification != null)
                {
                    shotgunDamageModification.EquipmentStatModification.Value = config.CloseQuartersShotgunDamage;
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
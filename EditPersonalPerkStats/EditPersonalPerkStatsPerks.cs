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

            // Configure sniper weapon damage bonus
            if (sniperist.ItemTagStatModifications?.Length > 0)
            {
                sniperist.ItemTagStatModifications[0].EquipmentStatModification.Value = config.SniperistDamage;
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

            // Configure heavy weapon bonuses - ADD heavy weapon damage bonus
            var heavyTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Contains("HeavyWeapon") || tag.name.Contains("Heavy"));
            if (heavyTagDef != null)
            {
                strongman.ItemTagStatModifications = new EquipmentItemTagStatModification[]
                {
                    new EquipmentItemTagStatModification()
                    {
                        ItemTag = heavyTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.Accuracy,
                            Modification = StatModificationType.Add,
                            Value = config.StrongmanAccuracy
                        }
                    },
                    new EquipmentItemTagStatModification()
                    {
                        ItemTag = heavyTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.BonusAttackDamage,
                            Modification = StatModificationType.Multiply,
                            Value = config.StrongmanDamage
                        }
                    }
                };
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

            // Configure pistol and PDW bonuses
            var pistolTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Contains("Handgun") || tag.name.Contains("Pistol"));
            var pdwTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Contains("PDW") || tag.name.Contains("SMG"));
            
            var itemTagMods = new System.Collections.Generic.List<EquipmentItemTagStatModification>();

            if (pistolTagDef != null)
            {
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = pistolTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.BonusAttackDamage,
                        Modification = StatModificationType.Multiply,
                        Value = config.SelfDefensePistolDamage
                    }
                });
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = pistolTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Accuracy,
                        Modification = StatModificationType.Add,
                        Value = config.SelfDefensePistolAccuracy
                    }
                });
            }

            if (pdwTagDef != null)
            {
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = pdwTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.BonusAttackDamage,
                        Modification = StatModificationType.Multiply,
                        Value = config.SelfDefensePDWDamage
                    }
                });
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = pdwTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Accuracy,
                        Modification = StatModificationType.Add,
                        Value = config.SelfDefensePDWAccuracy
                    }
                });
            }

            selfDefense.ItemTagStatModifications = itemTagMods.ToArray();
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

            // Configure assault rifle bonuses
            var rifleTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Contains("AssaultRifle") || tag.name.Contains("Rifle"));
            if (rifleTagDef != null)
            {
                trooper.ItemTagStatModifications = new EquipmentItemTagStatModification[]
                {
                    new EquipmentItemTagStatModification()
                    {
                        ItemTag = rifleTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.Accuracy,
                            Modification = StatModificationType.Add,
                            Value = config.TrooperAccuracy
                        }
                    },
                    new EquipmentItemTagStatModification()
                    {
                        ItemTag = rifleTagDef,
                        EquipmentStatModification = new ItemStatModification()
                        {
                            TargetStat = StatModificationTarget.BonusAttackDamage,
                            Modification = StatModificationType.Multiply,
                            Value = config.TrooperDamage
                        }
                    }
                };
            }
        }

        private static void ConfigureBombardier(EditPersonalPerkStatsConfig config)
        {
            var bombardier = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Crafty_AbilityDef"));
            if (bombardier == null) return;

            // Configure mounted weapon bonuses
            if (bombardier.ItemTagStatModifications?.Length >= 2)
            {
                bombardier.ItemTagStatModifications[0].EquipmentStatModification.Value = config.BombardierDamage;
                bombardier.ItemTagStatModifications[1].EquipmentStatModification.Value = config.BombardierRange;
            }
        }

        private static void ConfigureCloseQuarters(EditPersonalPerkStatsConfig config)
        {
            var closeQuarters = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuartersSpecialist_AbilityDef"));
            if (closeQuarters == null) return;

            // Configure melee and shotgun bonuses - ADD shotgun damage bonus
            var meleeTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Contains("MeleeWeapon") || tag.name.Contains("Melee"));
            var shotgunTagDef = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(tag => tag.name.Contains("Shotgun"));

            var itemTagMods = new System.Collections.Generic.List<EquipmentItemTagStatModification>();

            if (meleeTagDef != null)
            {
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = meleeTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.BonusAttackDamage,
                        Modification = StatModificationType.Multiply,
                        Value = config.CloseQuartersMelee
                    }
                });
            }

            if (shotgunTagDef != null)
            {
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = shotgunTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.Accuracy,
                        Modification = StatModificationType.Add,
                        Value = config.CloseQuartersShotgun
                    }
                });
                itemTagMods.Add(new EquipmentItemTagStatModification()
                {
                    ItemTag = shotgunTagDef,
                    EquipmentStatModification = new ItemStatModification()
                    {
                        TargetStat = StatModificationTarget.BonusAttackDamage,
                        Modification = StatModificationType.Multiply,
                        Value = config.CloseQuartersShotgunDamage
                    }
                });
            }

            closeQuarters.ItemTagStatModifications = itemTagMods.ToArray();
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
using System.Linq;
using Base.Defs;
using PhoenixPoint.Tactical.Entities.Abilities;
using UnityEngine;
using ModMain = EditPersonalPerkStats.EPPS_Main;

namespace EditPersonalPerkStats
{
    internal class EPPS_Perks
    {
        private static readonly DefRepository Repo = EPPS_Main.Repo;

        public static void Change_Perks()
        {
            var Config = (EPPS_Config)EPPS_Main.Main.Config;

            PassiveModifierAbilityDef bio = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("BioChemist_AbilityDef"));
            PassiveModifierAbilityDef obj = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Cautious_AbilityDef"));
            PassiveModifierAbilityDef close = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuartersSpecialist_AbilityDef"));
            PassiveModifierAbilityDef far = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Brainiac_AbilityDef"));
            PassiveModifierAbilityDef heal = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Helpful_AbilityDef"));
            PassiveModifierAbilityDef pitch = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Pitcher_AbilityDef"));
            PassiveModifierAbilityDef reck = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Reckless_AbilityDef"));
            PassiveModifierAbilityDef res = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Resourceful_AbilityDef"));
            PassiveModifierAbilityDef self = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("SelfDefenseSpecialist_AbilityDef"));
            PassiveModifierAbilityDef sniper = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Focused_AbilityDef"));
            PassiveModifierAbilityDef strong = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Strongman_AbilityDef"));
            PassiveModifierAbilityDef thief = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Thief_AbilityDef"));
            PassiveModifierAbilityDef trooper = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("GoodShot_AbilityDef"));
            PassiveModifierAbilityDef bomb = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(a => a.name.Equals("Crafty_AbilityDef"));

            // Apply config
            bio.DamageKeywordPairs[0].Value = Config.BiochemistViral;
            obj.StatModifications[0].Value = Config.CautiousDamage;
            obj.StatModifications[1].Value = Config.CautiousAccuracy;
            close.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.CloseQuartersMelee;
            close.ItemTagStatModifications[1].EquipmentStatModification.Value = Config.CloseQuartersShotgun;
            close.ItemTagStatModifications[2].EquipmentStatModification.Value = Config.CloseQuartersShotgunDamage;
            far.StatModifications[0].Value = Config.FarsightedMaxWill;
            far.StatModifications[1].Value = Config.FarsightedWill;
            far.StatModifications[2].Value = Config.FarsightedPerception;
            far.StatModifications[3].Value = Config.FarsightedWill;
            far.StatModifications[4].Value = Config.FarsightedMaxWill;
            heal.StatModifications[0].Value = Config.HealerWillMax;
            heal.StatModifications[1].Value = Config.HealerWill;
            heal.StatModifications[2].Value = Config.HealerHeal;
            heal.StatModifications[3].Value = Config.HealerWill;
            heal.StatModifications[4].Value = Config.HealerWillMax;
            pitch.StatModifications[0].Value = Config.QuarterbackSpeed;
            pitch.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.QuarterbackRange;
            reck.StatModifications[0].Value = Config.RecklessDamage;
            reck.StatModifications[1].Value = Config.RecklessAccuracy;
            res.StatModifications[0].Value = Config.ResourcefulStrngthMax;
            res.StatModifications[1].Value = Config.ResourcefulStrngth;
            res.StatModifications[2].Value = Config.ResourcefulCarry;
            self.StatModifications[0].Value = Config.SelfDefenseHearing;
            self.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.SelfDefensePistolDamage;
            self.ItemTagStatModifications[1].EquipmentStatModification.Value = Config.SelfDefensePistolAccuracy;
            self.ItemTagStatModifications[2].EquipmentStatModification.Value = Config.SelfDefensePDWDamage;
            self.ItemTagStatModifications[3].EquipmentStatModification.Value = Config.SelfDefensePDWAccuracyv;
            sniper.StatModifications[0].Value = Config.SniperistWill;
            sniper.StatModifications[1].Value = Config.SniperistWillMax;
            sniper.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.SniperistDamage;
            strong.StatModifications[0].Value = Config.StrongmanPerception;
            strong.StatModifications[1].Value = Config.StrongmanStrengthMax;
            strong.StatModifications[2].Value = Config.StrongmanStrength;
            strong.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.StrongmanWeapon;
            strong.ItemTagStatModifications[1].EquipmentStatModification.Value = Config.StrongmanWeaponDamage;
            thief.StatModifications[0].Value = Config.ThiefStealth;
            thief.StatModifications[1].Value = Config.ThiefSpeed;
            trooper.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.TrooperAccuracy;
            trooper.ItemTagStatModifications[1].EquipmentStatModification.Value = Config.TrooperDamage;
            bomb.ItemTagStatModifications[0].EquipmentStatModification.Value = Config.BombardierDamage;
            bomb.ItemTagStatModifications[1].EquipmentStatModification.Value = Config.BombardierRange;
        }
    }
}
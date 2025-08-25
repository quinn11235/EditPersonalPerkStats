using HarmonyLib;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Weapons;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using System;
using System.Linq;

namespace EditPersonalPerkStats
{
    // Patch for weapon damage calculations in tactical
    [HarmonyPatch(typeof(Weapon), "GetDamagePayload")]
    public static class Weapon_GetDamagePayload_Patch
    {
        public static void Postfix(Weapon __instance, ref DamagePayload __result)
        {
            try
            {
                if (PersonalPerkMain.Main?.Config == null) return;

                var config = PersonalPerkMain.Main.Config;
                var actor = __instance.TacticalActor;
                if (actor == null) return;

                var weaponDef = __instance.WeaponDef;
                if (weaponDef == null) return;

                // Apply damage modifications based on weapon names and perks
                // Note: This is a simplified approach - in full implementation you'd check actual perks on soldier
                ApplyDamageModifications(ref __result, config, weaponDef);
            }
            catch (Exception e)
            {
                PersonalPerkMain.Main?.Logger?.LogError($"Error in weapon damage patch: {e}");
            }
        }

        private static void ApplyDamageModifications(ref DamagePayload damage, PersonalPerkConfig config, PhoenixPoint.Tactical.Entities.Weapons.WeaponDef weaponDef)
        {
            var weaponName = weaponDef.name.ToLower();

            // Close Quarters Specialist - Melee weapons
            if (weaponName.Contains("melee") || weaponName.Contains("sword") || weaponName.Contains("hammer") || 
                weaponName.Contains("fist") || weaponName.Contains("blade"))
            {
                damage = MultiplyDamagePayload(damage, config.CloseQuartersMelee);
            }
            
            // Close Quarters Specialist - Shotgun weapons  
            else if (weaponName.Contains("shotgun") || weaponName.Contains("scatter"))
            {
                damage = MultiplyDamagePayload(damage, config.CloseQuartersShotgunDamage);
            }
            
            // Sniperist - Sniper weapons
            else if (weaponName.Contains("sniper") || weaponName.Contains("longrange"))
            {
                damage = MultiplyDamagePayload(damage, config.SniperistDamage);
            }
            
            // Trooper - Assault rifle weapons
            else if (weaponName.Contains("assaultrifle") || weaponName.Contains("rifle"))
            {
                damage = MultiplyDamagePayload(damage, config.TrooperDamage);
            }
            
            // Strongman - Heavy weapons
            else if (weaponName.Contains("heavy") || weaponName.Contains("cannon") || weaponName.Contains("launcher") ||
                     weaponName.Contains("mg") || weaponName.Contains("machinegun"))
            {
                damage = MultiplyDamagePayload(damage, config.StrongmanDamage);
            }
            
            // Self Defense - Pistol weapons
            else if (weaponName.Contains("pistol") || weaponName.Contains("handgun"))
            {
                damage = MultiplyDamagePayload(damage, config.SelfDefensePistolDamage);
            }
            
            // Self Defense - PDW weapons
            else if (weaponName.Contains("pdw") || weaponName.Contains("smg") || weaponName.Contains("submachine"))
            {
                damage = MultiplyDamagePayload(damage, config.SelfDefensePDWDamage);
            }
            
            // Bombardier - Mounted weapons (vehicles)
            else if (weaponName.Contains("mounted") || weaponName.Contains("turret") || weaponName.Contains("vehicle"))
            {
                damage = MultiplyDamagePayload(damage, config.BombardierDamage);
            }
        }

        private static DamagePayload MultiplyDamagePayload(DamagePayload damage, float multiplier)
        {
            // Modify damage payload in place
            if (damage?.DamageKeywords != null)
            {
                for (int i = 0; i < damage.DamageKeywords.Count; i++)
                {
                    if (damage.DamageKeywords[i] != null)
                    {
                        var keyword = damage.DamageKeywords[i];
                        keyword.Value *= multiplier;
                        damage.DamageKeywords[i] = keyword;
                    }
                }
            }
            
            return damage;
        }
    }

    // Patch for weapon accuracy calculations
    [HarmonyPatch(typeof(Weapon), "GetAccuracy")]
    public static class Weapon_GetAccuracy_Patch
    {
        public static void Postfix(Weapon __instance, ref float __result)
        {
            try
            {
                if (PersonalPerkMain.Main?.Config == null) return;

                var config = PersonalPerkMain.Main.Config;
                var actor = __instance.TacticalActor;
                if (actor == null) return;

                var weaponDef = __instance.WeaponDef;
                if (weaponDef == null) return;

                // Apply accuracy modifications based on weapon names
                // Note: This is a simplified approach - in full implementation you'd check actual perks on soldier
                ApplyAccuracyModifications(ref __result, config, weaponDef, actor);
            }
            catch (Exception e)
            {
                PersonalPerkMain.Main?.Logger?.LogError($"Error in weapon accuracy patch: {e}");
            }
        }

        private static void ApplyAccuracyModifications(ref float accuracy, PersonalPerkConfig config, PhoenixPoint.Tactical.Entities.Weapons.WeaponDef weaponDef, TacticalActor actor)
        {
            var weaponName = weaponDef.name.ToLower();

            // Close Quarters Specialist - Shotgun accuracy
            if (weaponName.Contains("shotgun") || weaponName.Contains("scatter"))
            {
                accuracy += config.CloseQuartersShotgun;
            }
            
            // Trooper - Assault rifle accuracy
            else if (weaponName.Contains("assaultrifle") || weaponName.Contains("rifle"))
            {
                accuracy += config.TrooperAccuracy;
            }
            
            // Strongman - Heavy weapon accuracy
            else if (weaponName.Contains("heavy") || weaponName.Contains("cannon") || weaponName.Contains("launcher") ||
                     weaponName.Contains("mg") || weaponName.Contains("machinegun"))
            {
                accuracy += config.StrongmanAccuracy;
            }
            
            // Self Defense - Pistol accuracy
            else if (weaponName.Contains("pistol") || weaponName.Contains("handgun"))
            {
                accuracy += config.SelfDefensePistolAccuracy;
            }
            
            // Self Defense - PDW accuracy
            else if (weaponName.Contains("pdw") || weaponName.Contains("smg") || weaponName.Contains("submachine"))
            {
                accuracy += config.SelfDefensePDWAccuracy;
            }
            
            // Cautious - Accuracy bonus when in cover (always applied for simplicity)
            // Note: Checking actual cover status would require more complex implementation
            accuracy += config.CautiousAccuracy;
            
            // Reckless - Accuracy penalty (always applied for simplicity)
            accuracy += config.RecklessAccuracy; // This is negative, so it reduces accuracy
        }
    }

    // Patch for grenade range (Quarterback perk)
    [HarmonyPatch(typeof(Weapon), "GetRange")]
    public static class Weapon_GetRange_Patch
    {
        public static void Postfix(Weapon __instance, ref float __result)
        {
            try
            {
                if (PersonalPerkMain.Main?.Config == null) return;

                var config = PersonalPerkMain.Main.Config;
                var weaponDef = __instance.WeaponDef;
                if (weaponDef == null) return;

                var weaponName = weaponDef.name.ToLower();

                // Quarterback - Grenade and throwing weapon range
                if (weaponName.Contains("grenade") || weaponName.Contains("thrown") || weaponName.Contains("explosive"))
                {
                    __result *= config.QuarterbackRange;
                }
                
                // Bombardier - Mounted weapon range
                else if (weaponName.Contains("mounted") || weaponName.Contains("turret") || weaponName.Contains("vehicle"))
                {
                    __result *= config.BombardierRange;
                }
            }
            catch (Exception e)
            {
                PersonalPerkMain.Main?.Logger?.LogError($"Error in weapon range patch: {e}");
            }
        }
    }
}
using PhoenixPoint.Modding;

namespace EditPersonalPerkStats
{
    public class EditPersonalPerkStatsConfig : ModConfig
    {
        // Quarterback Perk
        [ConfigField("Quarterback Speed Bonus", "Speed bonus for throwing grenades further")]
        public float QuarterbackSpeed = 2f;
        
        [ConfigField("Quarterback Grenade Range Multiplier", "Range multiplier for grenades (25% = 1.25)")]
        public float QuarterbackRange = 1.25f;

        // Resourceful Perk
        [ConfigField("Resourceful Strength Bonus", "Current strength bonus")]
        public float ResourcefulStrength = 2f;
        
        [ConfigField("Resourceful Max Strength Bonus", "Maximum strength bonus")]
        public float ResourcefulMaxStrength = 2f;
        
        [ConfigField("Resourceful Carry Weight Multiplier", "Carry weight multiplier (25% = 1.25)")]
        public float ResourcefulCarry = 1.25f;

        // Farsighted Perk  
        [ConfigField("Farsighted Perception Bonus", "Perception bonus")]
        public float FarsightedPerception = 10f;
        
        [ConfigField("Farsighted Willpower Bonus", "Current willpower bonus")]
        public float FarsightedWillpower = 2f;
        
        [ConfigField("Farsighted Max Willpower Bonus", "Maximum willpower bonus")]
        public float FarsightedMaxWillpower = 2f;

        // Healer Perk
        [ConfigField("Healer Willpower Bonus", "Current willpower bonus")]
        public float HealerWillpower = 2f;
        
        [ConfigField("Healer Max Willpower Bonus", "Maximum willpower bonus")]
        public float HealerMaxWillpower = 2f;
        
        [ConfigField("Healer Heal Multiplier", "Healing effectiveness multiplier (30% = 1.3)")]
        public float HealerHeal = 1.3f;

        // Thief Perk
        [ConfigField("Thief Speed Bonus", "Speed bonus")]
        public float ThiefSpeed = 1f;
        
        [ConfigField("Thief Stealth Bonus", "Stealth bonus (25% = 0.25)")]
        public float ThiefStealth = 0.25f;

        // Sniperist Perk
        [ConfigField("Sniperist Willpower Penalty", "Current willpower penalty (negative value)")]
        public float SniperistWillpower = -4f;
        
        [ConfigField("Sniperist Max Willpower Penalty", "Maximum willpower penalty (negative value)")]
        public float SniperistMaxWillpower = -4f;
        
        [ConfigField("Sniperist Sniper Damage Multiplier", "Sniper weapon damage multiplier (25% = 1.25)")]
        public float SniperistDamage = 1.25f;

        // Strongman Perk
        [ConfigField("Strongman Perception Penalty", "Perception penalty (negative value)")]
        public float StrongmanPerception = -5f;
        
        [ConfigField("Strongman Strength Bonus", "Current strength bonus")]
        public float StrongmanStrength = 2f;
        
        [ConfigField("Strongman Max Strength Bonus", "Maximum strength bonus")]
        public float StrongmanMaxStrength = 2f;
        
        [ConfigField("Strongman Heavy Weapon Accuracy Bonus", "Heavy weapon accuracy bonus (20% = 0.2)")]
        public float StrongmanAccuracy = 0.2f;
        
        [ConfigField("Strongman Heavy Weapon Damage Multiplier", "Heavy weapon damage multiplier (10% = 1.1)")]
        public float StrongmanDamage = 1.1f;

        // Self Defense Specialist Perk
        [ConfigField("Self Defense Hearing Range Bonus", "Hearing range bonus")]
        public float SelfDefenseHearing = 10f;
        
        [ConfigField("Self Defense Pistol Accuracy Bonus", "Pistol accuracy bonus (20% = 0.2)")]
        public float SelfDefensePistolAccuracy = 0.2f;
        
        [ConfigField("Self Defense Pistol Damage Multiplier", "Pistol damage multiplier (10% = 1.1)")]
        public float SelfDefensePistolDamage = 1.1f;
        
        [ConfigField("Self Defense PDW Accuracy Bonus", "PDW accuracy bonus (20% = 0.2)")]
        public float SelfDefensePDWAccuracy = 0.2f;
        
        [ConfigField("Self Defense PDW Damage Multiplier", "PDW damage multiplier (10% = 1.1)")]
        public float SelfDefensePDWDamage = 1.1f;

        // Reckless Perk
        [ConfigField("Reckless Damage Multiplier", "General damage multiplier (10% = 1.1)")]
        public float RecklessDamage = 1.1f;
        
        [ConfigField("Reckless Accuracy Penalty", "Accuracy penalty (negative value, -5% = -0.05)")]
        public float RecklessAccuracy = -0.05f;

        // Cautious Perk
        [ConfigField("Cautious Accuracy Bonus", "Accuracy bonus (20% = 0.2)")]
        public float CautiousAccuracy = 0.2f;
        
        [ConfigField("Cautious Damage Multiplier", "Damage multiplier penalty (-10% = 0.9)")]
        public float CautiousDamage = 0.9f;

        // Trooper Perk
        [ConfigField("Trooper Assault Rifle Accuracy Bonus", "Assault rifle accuracy bonus (20% = 0.2)")]
        public float TrooperAccuracy = 0.2f;
        
        [ConfigField("Trooper Assault Rifle Damage Multiplier", "Assault rifle damage multiplier (10% = 1.1)")]
        public float TrooperDamage = 1.1f;

        // Bombardier Perk
        [ConfigField("Bombardier Damage Multiplier", "Mounted weapon damage multiplier (10% = 1.1)")]
        public float BombardierDamage = 1.1f;
        
        [ConfigField("Bombardier Range Multiplier", "Mounted weapon range multiplier (20% = 1.2)")]
        public float BombardierRange = 1.2f;

        // Close Quarters Specialist Perk
        [ConfigField("Close Quarters Melee Damage Multiplier", "Melee weapon damage multiplier (20% = 1.2)")]
        public float CloseQuartersMelee = 1.2f;
        
        [ConfigField("Close Quarters Shotgun Accuracy Bonus", "Shotgun accuracy bonus (20% = 0.2)")]
        public float CloseQuartersShotgun = 0.2f;
        
        [ConfigField("Close Quarters Shotgun Damage Multiplier", "Shotgun damage multiplier (10% = 1.1)")]
        public float CloseQuartersShotgunDamage = 1.1f;

        // Biochemist Perk
        [ConfigField("Biochemist Viral Damage Bonus", "Viral damage bonus")]
        public float BiochemistViral = 1f;

    }
}
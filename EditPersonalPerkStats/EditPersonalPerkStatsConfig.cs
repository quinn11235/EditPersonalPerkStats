using PhoenixPoint.Modding;

namespace Quinn11235.EditPersonalPerkStats
{
    public class EditPersonalPerkStatsConfig : ModConfig
    {
        // Biochemist
        [ConfigField("Biochemist Viral Damage Bonus", "Additional viral damage")]
        public float BiochemistViral = 1f;

        // Bombardier  
        [ConfigField("Bombardier Range Multiplier", "Mounted weapon range multiplier (1.2 = 20% increase)")]
        public float BombardierRange = 1.2f;
        
        [ConfigField("Bombardier Damage Multiplier", "Mounted weapon damage multiplier (1.1 = 10% increase)")]
        public float BombardierDamage = 1.1f;

        // Cautious
        [ConfigField("Cautious Accuracy Bonus", "Accuracy bonus (0.2 = 20%)")]
        public float CautiousAccuracy = 0.2f;
        
        [ConfigField("Cautious Damage Multiplier", "Damage multiplier penalty (0.9 = -10%)")]
        public float CautiousDamage = 0.9f;

        // Close Quarters Specialist
        [ConfigField("Close Quarters Melee Damage Multiplier", "Melee damage multiplier (1.2 = 20% increase)")]
        public float CloseQuartersMelee = 1.2f;
        
        [ConfigField("Close Quarters Shotgun Accuracy Bonus", "Shotgun accuracy bonus (0.2 = 20%)")]
        public float CloseQuartersShotgun = 0.2f;
        
        [ConfigField("Close Quarters Shotgun Damage Multiplier", "Shotgun damage multiplier (1.1 = 10% increase)")]
        public float CloseQuartersShotgunDamage = 1.1f;

        // Farsighted
        [ConfigField("Farsighted Perception Bonus", "Perception attribute bonus")]
        public float FarsightedPerception = 10f;
        
        [ConfigField("Farsighted Willpower Bonus", "Willpower attribute bonus")]
        public float FarsightedWillpower = 2f;
        
        [ConfigField("Farsighted Max Willpower Bonus", "Max willpower attribute bonus")]
        public float FarsightedMaxWillpower = 2f;

        // Healer
        [ConfigField("Healer Willpower Bonus", "Willpower attribute bonus")]
        public float HealerWillpower = 2f;
        
        [ConfigField("Healer Max Willpower Bonus", "Max willpower attribute bonus")]
        public float HealerMaxWillpower = 2f;
        
        [ConfigField("Healer Healing Multiplier", "Healing effectiveness multiplier (1.3 = 30% increase)")]
        public float HealerHeal = 1.3f;

        // Quarterback
        [ConfigField("Quarterback Speed Bonus", "Speed attribute bonus")]
        public float QuarterbackSpeed = 2f;
        
        [ConfigField("Quarterback Grenade Range Multiplier", "Throwing range multiplier (1.25 = 25% increase)")]
        public float QuarterbackRange = 1.25f;

        // Reckless
        [ConfigField("Reckless Damage Multiplier", "Damage multiplier (1.15 = 15% increase, base game is 1.1)")]
        public float RecklessDamage = 1.15f;
        
        [ConfigField("Reckless Accuracy Penalty", "Accuracy penalty (-0.05 = -5%)")]
        public float RecklessAccuracy = -0.05f;

        // Resourceful
        [ConfigField("Resourceful Carry Weight Multiplier", "Carry capacity multiplier (1.25 = 25% increase)")]
        public float ResourcefulCarry = 1.25f;
        
        [ConfigField("Resourceful Strength Bonus", "Strength attribute bonus")]
        public float ResourcefulStrength = 2f;
        
        [ConfigField("Resourceful Max Strength Bonus", "Max strength attribute bonus")]
        public float ResourcefulMaxStrength = 2f;

        // Self Defense Specialist
        [ConfigField("Self Defense Hearing Range Bonus", "Hearing range bonus")]
        public float SelfDefenseHearing = 10f;
        
        [ConfigField("Self Defense Pistol Accuracy Bonus", "Pistol accuracy bonus (0.2 = 20%)")]
        public float SelfDefensePistolAccuracy = 0.2f;
        
        [ConfigField("Self Defense Pistol Damage Multiplier", "Pistol damage multiplier (1.1 = 10% increase)")]
        public float SelfDefensePistolDamage = 1.1f;
        
        [ConfigField("Self Defense PDW Accuracy Bonus", "PDW accuracy bonus (0.2 = 20%)")]
        public float SelfDefensePDWAccuracy = 0.2f;
        
        [ConfigField("Self Defense PDW Damage Multiplier", "PDW damage multiplier (1.1 = 10% increase)")]
        public float SelfDefensePDWDamage = 1.1f;

        // Sniperist
        [ConfigField("Sniperist Willpower Penalty", "Willpower attribute penalty")]
        public float SniperistWillpower = -4f;
        
        [ConfigField("Sniperist Max Willpower Penalty", "Max willpower attribute penalty")]
        public float SniperistMaxWillpower = -4f;
        
        [ConfigField("Sniperist Sniper Damage Multiplier", "Sniper weapon damage multiplier (1.25 = 25% increase)")]
        public float SniperistDamage = 1.25f;

        // Strongman
        [ConfigField("Strongman Perception Penalty", "Perception attribute penalty")]
        public float StrongmanPerception = -5f;
        
        [ConfigField("Strongman Strength Bonus", "Strength attribute bonus")]
        public float StrongmanStrength = 2f;
        
        [ConfigField("Strongman Max Strength Bonus", "Max strength attribute bonus")]
        public float StrongmanMaxStrength = 2f;
        
        [ConfigField("Strongman Heavy Weapon Accuracy Bonus", "Heavy weapon accuracy bonus (0.2 = 20%)")]
        public float StrongmanAccuracy = 0.2f;
        
        [ConfigField("Strongman Heavy Weapon Damage Multiplier", "Heavy weapon damage multiplier (1.1 = 10% increase)")]
        public float StrongmanDamage = 1.1f;

        // Thief
        [ConfigField("Thief Speed Bonus", "Speed attribute bonus")]
        public float ThiefSpeed = 1f;
        
        [ConfigField("Thief Stealth Bonus", "Stealth bonus (0.25 = 25%)")]
        public float ThiefStealth = 0.25f;

        // Trooper
        [ConfigField("Trooper Assault Rifle Accuracy Bonus", "Assault rifle accuracy bonus (0.2 = 20%)")]
        public float TrooperAccuracy = 0.2f;
        
        [ConfigField("Trooper Assault Rifle Damage Multiplier", "Assault rifle damage multiplier (1.1 = 10% increase)")]
        public float TrooperDamage = 1.1f;
    }
}
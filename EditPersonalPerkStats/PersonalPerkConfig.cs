using PhoenixPoint.Modding;

namespace EditPersonalPerkStats
{
    public class PersonalPerkConfig : ModConfig
    {
        // Biochemist
        [ConfigField(text: "Biochemist Viral Damage Bonus", description: "Additional viral damage")]
        public float BiochemistViral = 1f;

        // Bombardier  
        [ConfigField(text: "Bombardier Range Multiplier", description: "Mounted weapon range multiplier (1.2 = 20% increase)")]
        public float BombardierRange = 1.2f;
        
        [ConfigField(text: "Bombardier Damage Multiplier", description: "Mounted weapon damage multiplier (1.1 = 10% increase)")]
        public float BombardierDamage = 1.1f;

        // Cautious
        [ConfigField(text: "Cautious Accuracy Bonus", description: "Accuracy bonus when full cover (0.2 = 20%)")]
        public float CautiousAccuracy = 0.2f;
        
        [ConfigField(text: "Cautious Damage Reduction", description: "Damage multiplier penalty (0.9 = -10%)")]
        public float CautiousDamage = 0.9f;

        // Close Quarters Specialist
        [ConfigField(text: "Close Quarters Melee Damage Multiplier", description: "Melee damage multiplier (1.2 = 20% increase)")]
        public float CloseQuartersMelee = 1.2f;
        
        [ConfigField(text: "Close Quarters Shotgun Accuracy Bonus", description: "Shotgun accuracy bonus (0.2 = 20%)")]
        public float CloseQuartersShotgun = 0.2f;
        
        [ConfigField(text: "Close Quarters Shotgun Damage Multiplier", description: "Shotgun damage multiplier (1.1 = 10% increase)")]
        public float CloseQuartersShotgunDamage = 1.1f;

        // Quarterback
        [ConfigField(text: "Quarterback Speed Bonus", description: "Speed attribute bonus")]
        public float QuarterbackSpeed = 2f;
        
        [ConfigField(text: "Quarterback Grenade Range Multiplier", description: "Throwing range multiplier (1.25 = 25% increase)")]
        public float QuarterbackRange = 1.25f;

        // Reckless
        [ConfigField(text: "Reckless Damage Multiplier", description: "Damage multiplier (1.1 = 10% increase)")]
        public float RecklessDamage = 1.1f;
        
        [ConfigField(text: "Reckless Accuracy Penalty", description: "Accuracy penalty (-0.1 = -10%)")]
        public float RecklessAccuracy = -0.1f;

        // Resourceful
        [ConfigField(text: "Resourceful Carry Weight Multiplier", description: "Carry capacity multiplier (1.25 = 25% increase)")]
        public float ResourcefulCarry = 1.25f;
        
        [ConfigField(text: "Resourceful Strength Bonus", description: "Strength attribute bonus")]
        public float ResourcefulStrength = 2f;
        
        [ConfigField(text: "Resourceful Max Strength Bonus", description: "Max strength attribute bonus")]
        public float ResourcefulMaxStrength = 2f;

        // Farsighted
        [ConfigField(text: "Farsighted Perception Bonus", description: "Perception attribute bonus")]
        public float FarsightedPerception = 10f;
        
        [ConfigField(text: "Farsighted Willpower Bonus", description: "Willpower attribute bonus")]
        public float FarsightedWillpower = 2f;
        
        [ConfigField(text: "Farsighted Max Willpower Bonus", description: "Max willpower attribute bonus")]
        public float FarsightedMaxWillpower = 2f;

        // Healer
        [ConfigField(text: "Healer Willpower Bonus", description: "Willpower attribute bonus")]
        public float HealerWillpower = 2f;
        
        [ConfigField(text: "Healer Max Willpower Bonus", description: "Max willpower attribute bonus")]
        public float HealerMaxWillpower = 2f;
        
        [ConfigField(text: "Healer Healing Multiplier", description: "Healing effectiveness multiplier (1.3 = 30% increase)")]
        public float HealerHeal = 1.3f;

        // Thief
        [ConfigField(text: "Thief Speed Bonus", description: "Speed attribute bonus")]
        public float ThiefSpeed = 1f;
        
        [ConfigField(text: "Thief Stealth Bonus", description: "Stealth bonus (0.25 = 25%)")]
        public float ThiefStealth = 0.25f;

        // Self Defense Specialist
        [ConfigField(text: "Self Defense Hearing Range Bonus", description: "Hearing range bonus")]
        public float SelfDefenseHearing = 10f;
        
        [ConfigField(text: "Self Defense Pistol Accuracy Bonus", description: "Pistol accuracy bonus (0.2 = 20%)")]
        public float SelfDefensePistolAccuracy = 0.2f;
        
        [ConfigField(text: "Self Defense Pistol Damage Multiplier", description: "Pistol damage multiplier (1.1 = 10% increase)")]
        public float SelfDefensePistolDamage = 1.1f;
        
        [ConfigField(text: "Self Defense PDW Accuracy Bonus", description: "PDW accuracy bonus (0.2 = 20%)")]
        public float SelfDefensePDWAccuracy = 0.2f;
        
        [ConfigField(text: "Self Defense PDW Damage Multiplier", description: "PDW damage multiplier (1.1 = 10% increase)")]
        public float SelfDefensePDWDamage = 1.1f;

        // Sniperist
        [ConfigField(text: "Sniperist Willpower Penalty", description: "Willpower attribute penalty")]
        public float SniperistWillpower = -4f;
        
        [ConfigField(text: "Sniperist Max Willpower Penalty", description: "Max willpower attribute penalty")]
        public float SniperistMaxWillpower = -4f;
        
        [ConfigField(text: "Sniperist Sniper Damage Multiplier", description: "Sniper weapon damage multiplier (1.25 = 25% increase)")]
        public float SniperistDamage = 1.25f;

        // Strongman
        [ConfigField(text: "Strongman Perception Penalty", description: "Perception attribute penalty")]
        public float StrongmanPerception = -5f;
        
        [ConfigField(text: "Strongman Strength Bonus", description: "Strength attribute bonus")]
        public float StrongmanStrength = 2f;
        
        [ConfigField(text: "Strongman Max Strength Bonus", description: "Max strength attribute bonus")]
        public float StrongmanMaxStrength = 2f;
        
        [ConfigField(text: "Strongman Heavy Weapon Accuracy Bonus", description: "Heavy weapon accuracy bonus (0.2 = 20%)")]
        public float StrongmanAccuracy = 0.2f;
        
        [ConfigField(text: "Strongman Heavy Weapon Damage Multiplier", description: "Heavy weapon damage multiplier (1.1 = 10% increase)")]
        public float StrongmanDamage = 1.1f;

        // Trooper
        [ConfigField(text: "Trooper Assault Rifle Accuracy Bonus", description: "Assault rifle accuracy bonus (0.2 = 20%)")]
        public float TrooperAccuracy = 0.2f;
        
        [ConfigField(text: "Trooper Assault Rifle Damage Multiplier", description: "Assault rifle damage multiplier (1.1 = 10% increase)")]
        public float TrooperDamage = 1.1f;
    }
}
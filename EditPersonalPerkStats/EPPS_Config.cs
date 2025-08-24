using PhoenixPoint.Modding;

namespace EditPersonalPerkStats
{
    public class EPPS_Config : ModConfig
    {
        [ConfigField("Biochemist Viral Adder", "")]
        public float BiochemistViral = 1f;

        [ConfigField("Bombardier Mounted Range Multiplier", "20% = 1.2")]
        public float BombardierRange = 1.2f;

        [ConfigField("Bombardier Mounted Damage Multiplier", "10% = 1.1")]
        public float BombardierDamage = 1.1f;

        [ConfigField("Cautious Accuracy Adder", "20% = 0.2")]
        public float CautiousAccuracy = 0.2f;

        [ConfigField("Cautious Damage Multiplier", "-10% = 0.9")]
        public float CautiousDamage = 0.9f;

        [ConfigField("Close Quarters Specialist Melee Damage Multiplier", "20% = 1.2")]
        public float CloseQuartersMelee = 1.2f;

        [ConfigField("Close Quarters Specialist Shotgun Accuracy Adder", "20% = 0.2")]
        public float CloseQuartersShotgun = 0.2f;

        [ConfigField("Close Quarters Specialist Shotgun Damage Multiplier", "10% = 1.1")]
        public float CloseQuartersShotgunDamage = 1.1f;

        [ConfigField("Quarterback Speed Adder", "")]
        public float QuarterbackSpeed = 2f;

        [ConfigField("Quarterback Grenade Range Multiplier", "20% = 1.2")]
        public float QuarterbackRange = 1.25f;

        [ConfigField("Reckless Damage Multiplier", "10% = 1.1")]
        public float RecklessDamage = 1.1f;

        [ConfigField("Reckless Accuracy Adder", "-10% = -0.1")]
        public float RecklessAccuracy = -0.1f;

        [ConfigField("Resourceful Carry Weight Multiplier", "25% = 1.25")]
        public float ResourcefulCarry = 1.25f;

        [ConfigField("Resourceful Strength Adder", "")]
        public float ResourcefulStrngth = 2f;

        [ConfigField("Resourceful Max Strength Adder", "")]
        public float ResourcefulStrngthMax = 2f;

        [ConfigField("Farsighted Perception Adder", "")]
        public float FarsightedPerception = 10f;

        [ConfigField("Farsighted Willpower Adder", "")]
        public float FarsightedWill = 2f;

        [ConfigField("Farsighted Max Willpower Adder", "")]
        public float FarsightedMaxWill = 2f;

        [ConfigField("Healer Willpower Adder", "")]
        public float HealerWill = 2f;

        [ConfigField("Healer Max Willpower Adder", "")]
        public float HealerWillMax = 2f;

        [ConfigField("Healer Heal Multiplier", "30% = 1.3")]
        public float HealerHeal = 1.3f;

        [ConfigField("Thief Speed Adder", "")]
        public float ThiefSpeed = 1f;

        [ConfigField("Thief Stealth Adder", "25% = 0.25")]
        public float ThiefStealth = 0.25f;

        [ConfigField("Self Defense Specialist Hearing Range Adder", "")]
        public float SelfDefenseHearing = 10f;

        [ConfigField("Self Defense Specialist Pistol Accuracy Adder", "20% = 0.2")]
        public float SelfDefensePistolAccuracy = 0.2f;

        [ConfigField("Self Defense Specialist Pistol Damage Multiplier", "10% = 1.1")]
        public float SelfDefensePistolDamage = 1.1f;

        [ConfigField("Self Defense Specialist PDW Accuracy Adder", "20% = 0.2")]
        public float SelfDefensePDWAccuracyv = 0.2f;

        [ConfigField("Self Defense Specialist PDW Damage Multiplier", "10% = 1.1")]
        public float SelfDefensePDWDamage = 1.1f;

        [ConfigField("Sniperist Willpower Adder", "-4")]
        public float SniperistWill = -4f;

        [ConfigField("Sniperist Max Willpower Adder", "")]
        public float SniperistWillMax = -4f;

        [ConfigField("Sniperist Sniper Damage Multiplier", "25% = 1.25")]
        public float SniperistDamage = 1.25f;

        [ConfigField("Strongman Perception Adder", "")]
        public float StrongmanPerception = -5f;

        [ConfigField("Strongman Strength Adder", "")]
        public float StrongmanStrength = 2f;

        [ConfigField("Strongman Max Strength Adder", "")]
        public float StrongmanStrengthMax = 2f;

        [ConfigField("Strongman Heavy Weapon Accuracy Adder", "20% = 0.2")]
        public float StrongmanWeapon = 0.2f;

        [ConfigField("Strongman Heavy Weapon Damage Multiplier", "10% = 1.1")]
        public float StrongmanWeaponDamage = 1.1f;

        [ConfigField("Trooper Assault Rifle Accuracy Adder", "20% = 0.2")]
        public float TrooperAccuracy = 0.2f;

        [ConfigField("Trooper Assault Rifle Damage Multiplier", "10% = 1.1")]
        public float TrooperDamage = 1.1f;
    }
}

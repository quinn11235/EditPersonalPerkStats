
using System;
using System.Reflection;

namespace EditPersonalPerkStats
{
    internal static class EPPS_TextGen
    {
        // Get a numeric config value (double/float/int), or default.
        private static double GetNumber(object cfg, double @default, params string[] names)
        {
            if (cfg == null) return @default;
            var t = cfg.GetType();
            foreach (var n in names)
            {
                var p = t.GetProperty(n, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                if (p != null)
                {
                    try
                    {
                        var val = p.GetValue(cfg, null);
                        if (val is double dd) return dd;
                        if (val is float  ff) return (double)ff;
                        if (val is int    ii) return (double)ii;
                    } catch {}
                }
                var f = t.GetField(n, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                if (f != null)
                {
                    try
                    {
                        var val = f.GetValue(cfg);
                        if (val is double dd2) return dd2;
                        if (val is float  ff2) return (double)ff2;
                        if (val is int    ii2) return (double)ii2;
                    } catch {}
                }
            }
            return @default;
        }

        // Robust % formatter:
        // - 1.1 -> +10%
        // - 0.9 -> -10%
        // - 0.25 -> +25%
        // - 25   -> +25%
        // - -10  -> -10%
        private static string Pct(double v, bool forceSign = true)
        {
            double av = Math.Abs(v);
            double pct;
            if (v == 0.0)
            {
                pct = 0.0;
            }
            else if (av >= 0.5 && av <= 1.5)
            {
                // Likely multiplier (0.9, 1.25, etc.)
                pct = (v - 1.0) * 100.0;
            }
            else if (av < 0.5)
            {
                // Likely fractional percent (0.25 -> 25%)
                pct = v * 100.0;
            }
            else
            {
                // Already a percent (10 -> 10%)
                pct = v;
            }

            int ip = (int)Math.Round(pct);
            string sign = forceSign ? (ip >= 0 ? "+" : "") : "";
            return sign + ip.ToString(System.Globalization.CultureInfo.InvariantCulture) + "%";
        }

        private static string IntDelta(double v, string label, bool forceSign = true)
        {
            int iv = (int)Math.Round(v);
            string sign = forceSign ? (iv >= 0 ? "+" : "") : "";
            return sign + iv.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + label;
        }

        private static string Tiles(double v)
        {
            int iv = (int)Math.Round(v);
            string sign = (iv >= 0 ? "+" : "");
            return sign + iv.ToString(System.Globalization.CultureInfo.InvariantCulture) + " tiles";
        }

        public static string BuildDescription(string perkName, object cfg)
        {
            if (string.IsNullOrEmpty(perkName)) return null;
            switch (perkName.Trim().ToLowerInvariant())
            {
                case "trooper":
                    var trDmg = GetNumber(cfg, 10, "TrooperDamage","TrooperDamagePercent","TrooperDamageBonus","TrooperDamageMultiplier");
                    var trAcc = GetNumber(cfg, 20, "TrooperAccuracy","TrooperAccuracyPercent","TrooperAccuracyBonus","TrooperAccuracyMultiplier");
                    return $"Gain Assault Rifle proficiency with {Pct(trDmg)} damage and {Pct(trAcc)} accuracy";

                case "sniperist":
                case "focused":
                    var snDmg = GetNumber(cfg, 25, "SniperistDamage","SniperistDamagePercent","SniperistDamageBonus","SniperistDamageMultiplier");
                    var snWp  = GetNumber(cfg, -4, "SniperistWill","SniperistWillDelta");
                    return $"Gain Sniper Rifle proficiency with {Pct(snDmg)} damage and {IntDelta(snWp, "Willpower")}";

                case "bombardier":
                    var boRng = GetNumber(cfg, 20, "BombardierRange","BombardierRangePercent","BombardierRangeBonus","BombardierRangeMultiplier");
                    var boDmg = GetNumber(cfg, 10, "BombardierDamage","BombardierDamagePercent","BombardierDamageBonus","BombardierDamageMultiplier");
                    return $"Gain mounted weapon proficiency with {Pct(boRng)} range and {Pct(boDmg)} damage";

                case "healer":
                case "helpful":
                    var hlHeal = GetNumber(cfg, 30, "HealerHealing","HealerHealingPercent","HealerHealingBonus","HealerHealingMultiplier");
                    var hlWp   = GetNumber(cfg, 2,  "HealerWill","HealerWillDelta");
                    return $"{Pct(hlHeal)} bonus healing and {IntDelta(hlWp, "Willpower")}";

                case "quarterback":
                case "pitcher":
                    var qbRange = GetNumber(cfg, 25, "QuarterbackGrenadeRange","GrenadeRange","QuarterbackRange","QuarterbackRangePercent","QuarterbackRangeMultiplier");
                    var qbSpd   = GetNumber(cfg, 2,  "QuarterbackSpeed","QuarterbackSpeedDelta");
                    return $"{Pct(qbRange)} bonus Grenade range and {IntDelta(qbSpd, "Speed")}";

                case "farsighted":
                case "brainiak":
                case "brainiac":
                    var faWp  = GetNumber(cfg, 2,  "FarsightedWill","FarsightedWillDelta");
                    var faPer = GetNumber(cfg, 10, "FarsightedPerception","FarsightedPerceptionDelta");
                    return $"Additional {IntDelta(faWp, "to Willpower")} and {IntDelta(faPer, "Perception range")}";

                case "thief":
                    var thStealth = GetNumber(cfg, 25, "ThiefStealth","ThiefStealthPercent","ThiefStealthBonus","ThiefStealthMultiplier");
                    var thSpeed   = GetNumber(cfg, 1,  "ThiefSpeed","ThiefSpeedDelta");
                    return $"{Pct(thStealth)} bonus stealth and {IntDelta(thSpeed, "Speed")}";

                case "resourceful":
                    var reCarry = GetNumber(cfg, 25, "ResourcefulCarry","ResourcefulCarryPercent","ResourcefulCarryBonus");
                    var reStr   = GetNumber(cfg, 2,  "ResourcefulStrength","ResourcefulStrengthDelta");
                    return $"{Pct(reCarry)} bonus carry weight and {IntDelta(reStr, "Strength")}";

                case "strongman":
                    var stAcc = GetNumber(cfg, 20, "StrongmanWeapon","StrongmanAccuracy","StrongmanAccuracyPercent","StrongmanAccuracyBonus","StrongmanAccuracyMultiplier");
                    var stDmg = GetNumber(cfg, 10, "StrongmanWeaponDamage","StrongmanDamage","StrongmanDamagePercent","StrongmanDamageBonus","StrongmanDamageMultiplier");
                    var stStr = GetNumber(cfg, 2,  "StrongmanStrength","StrongmanStrengthDelta");
                    var stPer = GetNumber(cfg, -5, "StrongmanPerception","StrongmanPerceptionDelta");
                    return $"Gain heavy weapons proficiency with {Pct(stAcc)} accuracy, {Pct(stDmg)} damage and {IntDelta(stPer, "perception")}, {IntDelta(stStr, "strength")}";

                case "cautious":
                    var caAcc = GetNumber(cfg, 20, "CautiousAccuracy","CautiousAccuracyPercent","CautiousAccuracyBonus","CautiousAccuracyMultiplier");
                    var caDmg = GetNumber(cfg, -10,"CautiousDamage","CautiousDamagePercent","CautiousDamageBonus","CautiousDamageMultiplier");
                    return $"{Pct(caAcc)} bonus accuracy and {Pct(caDmg)} damage dealt";

                case "reckless":
                    var rkDmg = GetNumber(cfg, 10, "RecklessDamage","RecklessDamagePercent","RecklessDamageBonus","RecklessDamageMultiplier");
                    var rkAcc = GetNumber(cfg, -10,"RecklessAccuracy","RecklessAccuracyPercent","RecklessAccuracyPenalty","RecklessAccuracyMultiplier");
                    return $"{Pct(rkDmg)} bonus damage dealt and {Pct(rkAcc)} accuracy";

                case "close-quarters specialist":
                case "close quarters specialist":
                case "close-quarters":
                case "close quarters":
                    var cqShotAcc = GetNumber(cfg, 20, "CloseQuartersShotgun","CQS_ShotgunAccuracy","CQS_ShotgunAccuracyPercent","CQS_ShotgunAccuracyMultiplier");
                    var cqShotDmg = GetNumber(cfg, 10, "CloseQuartersShotgunDamage","CQS_ShotgunDamage","CQS_ShotgunDamagePercent","CQS_ShotgunDamageMultiplier");
                    var cqMeleeDmg= GetNumber(cfg, 20, "CloseQuartersMelee","CQS_MeleeDamage","CQS_MeleeDamagePercent","CQS_MeleeDamageMultiplier");
                    return $"Gain shotgun proficiency with {Pct(cqShotAcc)} accuracy and {Pct(cqShotDmg)} damage and melee weapon proficiency with {Pct(cqMeleeDmg)} damage";

                case "self-defense specialist":
                case "self defence specialist":
                case "self-defense":
                case "self defence":
                    var sdDmg = GetNumber(cfg, 10, "SDS_Damage","SelfDefenseDamage","SelfDefenseDamagePercent","SDS_DamageMultiplier");
                    var sdAcc = GetNumber(cfg, 10, "SDS_Accuracy","SelfDefenseAccuracy","SelfDefenseAccuracyPercent","SDS_AccuracyMultiplier");
                    var sdHear= GetNumber(cfg, 10, "SDS_Hearing","SelfDefenseHearingTiles","SelfDefenseHearing");
                    return $"Gain PDW and Handgun proficiency with {Pct(sdDmg)} damage, {Pct(sdAcc)} accuracy and {Tiles(sdHear)} hearing range";

                case "biochemist":
                    var bioVirus = GetNumber(cfg, 1, "BiochemistVirus","BiochemistVirusPerHit");
                    return $"All attacks that damage a target inflict {(int)Math.Round(bioVirus)} Virus Damage (per bullet)";

                default:
                    return null;
            }
        }
    }
}

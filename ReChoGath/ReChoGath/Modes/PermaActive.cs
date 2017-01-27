using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReChoGath.Utils;

namespace ReChoGath.Modes
{
    public static class PermaActive
    {
        private static bool chance(int chance)
        {
            return (Other.GetRandom.Next(0, 100) <= chance);
        }

        public static void Execute()
        {
            #region Auto Q
            if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.Another.Q.AlwaysStun") && SpellManager.Q.IsReady())
            {
                foreach (var e in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.IsAlive() && h.IsInRange(Player.Instance, SpellManager.Q.Range) && (h.HasBuffOfType(BuffType.Stun) || h.HasBuffOfType(BuffType.Knockup) || h.HasBuffOfType(BuffType.Fear))).OrderByDescending(h => h.Health))
                {
                    SpellManager.Q.Cast(SpellManager.Q.GetPrediction(e).CastPosition);
                    break;
                }
            }
            #endregion
            #region Flash R
            if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.FlashRAuto") && SpellManager.R.IsReady() && SpellManager.Flash.IsReady())
            {
                foreach (var e in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.IsAlive() && !h.IsInvulnerable && h.IsInRange(Player.Instance, SpellManager.R.Range + SpellManager.Flash.Range - 50)))
                {
                    if (e.Distance(Player.Instance) > SpellManager.Flash.Range && e.CountEnemyChampionsInRange(550) <= 1)
                    {
                        Other.FlashR(e);
                        break;
                    }
                }
            }
            #endregion
            #region KillSteal
            foreach (var e in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.IsAlive() && h.IsInRange(Player.Instance.Position, SpellManager.Q.Range) && !h.IsInvulnerable))
            {
                float health = Prediction.Health.GetPrediction(e, 500);
                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.Q") && SpellManager.Q.IsReady() && health <= Damage.GetQDamage(e))
                {
                    SpellManager.Q.Cast(SpellManager.Q.GetPrediction(e).CastPosition);
                    break;  
                }

                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.W") && SpellManager.W.IsReady() && health <= Damage.GetWDamage(e) && e.IsInRange(Player.Instance, SpellManager.W.Range))
                {
                    SpellManager.W.Cast(SpellManager.W.GetPrediction(e).CastPosition);
                    break;
                }

                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.R") && SpellManager.R.IsReady() && health <= Damage.GetRDamage(e) && e.IsInRange(Player.Instance, SpellManager.R.Range))
                {
                    SpellManager.R.Cast(e);
                    break;
                }
            }
            #endregion
            #region Auto harass
            var target = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Mixed);
            if (target == null) return;
            if (Player.Instance.Position.IsUnderEnemyTurret() || Player.Instance.Position.IsGrass() && Player.Instance.CountAllyChampionsInRange(150) >= 1 && !target.Position.IsGrass()) return; // anti trap destroyer Fappa

            if (chance(Config.Harass.Menu.GetSliderValue("Config.AutoHarass.Q.Chance")) && Config.Harass.Menu.GetCheckBoxValue("Config.AutoHarass.Q.Status") && SpellManager.Q.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.Mana"))
            {
                var predition = SpellManager.Q.GetPrediction(target);
                if (predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.HitChance"))
                    SpellManager.Q.Cast(predition.CastPosition);
            }

            if (chance(Config.Harass.Menu.GetSliderValue("Config.AutoHarass.W.Chance")) && Config.Harass.Menu.GetCheckBoxValue("Config.AutoHarass.W.Status") && SpellManager.W.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.W.Mana"))
            {
                var predition = SpellManager.W.GetPrediction(target);
                if (predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.E.HitChance"))
                    SpellManager.E.Cast(predition.CastPosition);
            }
            #endregion
            #region Jungle steal
            if (SpellManager.R.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.R.Status") && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.R.Steal"))
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, SpellManager.Q.Range);
                if (monsters == null || !monsters.Any()) return;

                foreach (var e in monsters.Where(t => t.IsInRange(Player.Instance, SpellManager.R.Range) && Other.BigMonsters.Contains(t.BaseSkinName)))
                {
                    if (e.Health + 3 <= Damage.GetRDamage(e) && Config.Farm.Menu.GetCheckBoxValue($"Config.Farm.R.Monster.{e.BaseSkinName}"))
                        SpellManager.R.Cast(e);
                }
            }
            #endregion
        }
    }
}

using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReAhri.Utils;

namespace ReAhri.Modes
{
    public static class PermaActive
    {
        private static bool chance(int chance)
        {
            return (Other.GetRandom.Next(0, 100) <= chance);
        }

        public static void Execute()
        {
            #region KillSteal
            foreach (var e in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.IsAlive() && h.IsInRange(Player.Instance.Position, SpellManager.Q.Range) && !h.IsInvulnerable))
            {
                int time = (int)(Player.Instance.Position.Distance(e) / SpellManager.Q.Speed) * 1000;
                float health = Prediction.Health.GetPrediction(e, time);
                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.Q") && SpellManager.Q.IsReady() && health <= Damage.GetQDamage(e))
                {
                    SpellManager.Q.Cast(SpellManager.Q.GetPrediction(e).CastPosition);
                    break;  
                }

                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.W") && SpellManager.W.IsReady() && e.TotalShieldHealth() <= Damage.GetWDamage(e) && e.IsInRange(Player.Instance, SpellManager.W.Range))
                {
                    SpellManager.W.Cast();
                    break;
                }

                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.E") && SpellManager.E.IsReady() && health <= Damage.GetEDamage(e))
                {
                    var prediction = SpellManager.Q.GetPrediction(e);
                    if (!prediction.Collision) SpellManager.E.Cast(prediction.CastPosition);
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
                if (predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.HitChance") * 33)
                    SpellManager.Q.Cast(predition.CastPosition);
            }

            if (chance(Config.Harass.Menu.GetSliderValue("Config.AutoHarass.E.Chance")) && Config.Harass.Menu.GetCheckBoxValue("Config.AutoHarass.E.Status") && SpellManager.E.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.E.Mana"))
            {
                var predition = SpellManager.E.GetPrediction(target);
                if (!predition.Collision && predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.E.HitChance") * 33)
                    SpellManager.E.Cast(predition.CastPosition);
            }
            #endregion
        }
    }
}

using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReChoGath.Utils;

namespace ReChoGath.Modes
{
    public static class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies, DamageType.Magical);
            if (target == null) return;

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.Q.Status") && SpellManager.Q.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.Mana"))
            {
                var predition = SpellManager.Q.GetPrediction(target);
                if (predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.HitChance"))
                    SpellManager.Q.Cast(predition.CastPosition);
            }

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.W.Status") && SpellManager.W.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.W.Mana") && target.IsInRange(Player.Instance, SpellManager.W.Range))
            {
                var predition = SpellManager.W.GetPrediction(target);
                if (predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.W.HitChance"))
                    SpellManager.W.Cast(predition.CastPosition);
            }

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.E.Status"))
            {
                if (target.IsInRange(Player.Instance.Position, Player.Instance.GetAutoAttackRange() * 3))
                    Other.SetSpikes(true);
            }
        }
    }
}

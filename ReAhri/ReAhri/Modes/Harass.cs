using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReAhri.Utils;

namespace ReAhri.Modes
{
    public static class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies, DamageType.Magical);
            if (target == null) return;

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.Q.Status") && SpellManager.E.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.Mana"))
            {
                var predition = SpellManager.E.GetPrediction(target);
                if (predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.Q.HitChance") * 33)
                    SpellManager.E.Cast(predition.CastPosition);
            }

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.W.Status") && SpellManager.W.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.W.Mana"))
            {
                if (target.IsInRange(Player.Instance.Position, SpellManager.W.Range))
                    SpellManager.W.Cast();
            }

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.E.Status") && SpellManager.E.IsReady() && Player.Instance.ManaPercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.E.Mana"))
            {
                var predition = SpellManager.E.GetPrediction(target);
                if (!predition.Collision && predition.HitChancePercent >= Config.Harass.Menu.GetSliderValue("Config.Harass.E.HitChance") * 33)
                    SpellManager.E.Cast(predition.CastPosition);
            }
        }
    }
}

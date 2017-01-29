using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Collections.Generic;
using ReChoGath.Utils;
using System.Linq;

namespace ReChoGath.Modes
{
    public static class Combo
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Mixed, Player.Instance.Position);
            if (target == null || target.IsInvulnerable)
                return;

            if (SpellManager.Q.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.Q.Status"))
            {
                var prediction = SpellManager.Q.GetPrediction(target);
                if (prediction.HitChancePercent >= Config.Combo.Menu.GetSliderValue("Config.Combo.Q.HitChance"))
                    SpellManager.Q.Cast(prediction.CastPosition);
            }

            if (SpellManager.W.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.W.Status"))
            {
                if (target.IsInRange(Player.Instance, SpellManager.W.Range))
                {
                    var prediction = SpellManager.W.GetPrediction(target);
                    if (prediction.HitChancePercent >= Config.Combo.Menu.GetSliderValue("Config.Combo.W.HitChance"))
                        SpellManager.W.Cast(prediction.CastPosition);
                }
            }

            if (SpellManager.E.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.E.Status"))
            {
                if (target.IsInRange(Player.Instance, SpellManager.W.Range)) Other.SetSpikes(true);
            }

            if (SpellManager.R.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Status") && target.IsInRange(Player.Instance, SpellManager.R.Range) && !target.HasSpellshield())
            {
                if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Killable") && target.TotalShieldHealth() + 5 > Damage.GetRDamage(target)) return;
                SpellManager.R.Cast(target);
            }
        }
    }

}
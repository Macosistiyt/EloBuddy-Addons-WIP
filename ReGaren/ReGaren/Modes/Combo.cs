using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Collections.Generic;
using ReGaren.Utils;
using System.Linq;

namespace ReGaren.Modes
{
    public static class Combo
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(SpellManager.R.Range, DamageType.Mixed, Player.Instance.Position);
            if (target == null || target.IsInvulnerable)
                return;

            bool HasQCasted = false;
            if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.Q.Status") && SpellManager.Q.IsReady() && !Player.Instance.HasBuff("GarenE"))
            {
                if ((Player.Instance.Distance(target) / (Player.Instance.MoveSpeed * 1.3f)) * 1000 <= 3500)
                {
                    HasQCasted = true;
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                    Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, target), Config.Combo.Menu.GetSliderValue("Config.Combo.Q.Delay"));
                }
            }

            if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.W.Status") && SpellManager.W.IsReady())
            {
                if (target.IsInRange(Player.Instance, Config.Combo.Menu.GetSliderValue("Config.Combo.W.Distance")))
                    SpellManager.W.Cast();
            }

            if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.E.Status") && SpellManager.E.IsReady() && !(Player.Instance.HasBuff("GarenE") || Player.Instance.HasBuff("GarenQ") || HasQCasted) && !Player.Instance.HasBuff("GarenQ"))
            {
                if (SpellManager.Q.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.E.AfterQAA")) return;
                if (Player.Instance.IsInRange(target, SpellManager.E.Range))
                {
                    SpellManager.E.Cast();
                }
            }

            if (SpellManager.R.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Status") && target.IsInRange(Player.Instance, SpellManager.R.Range))
            {
                if (Config.Combo.Menu.GetCheckBoxValue($"Config.Combo.R.Use.{target.ChampionName}") && !target.HasSpellshield() && target.Health + Config.Combo.Menu.GetSliderValue("Config.Combo.R.Substract") <= Damage.GetRDamage(target))
                {
                    SpellManager.R.Cast(target);
                }
            }
        }
    }
    
}
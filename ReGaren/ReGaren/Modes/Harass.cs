using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReGaren.Utils;

namespace ReGaren.Modes
{
    public static class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(EntityManager.Heroes.Enemies.Where(e => e.IsInRange(Player.Instance, SpellManager.E.Range)), DamageType.Magical);
            if (target == null) return;

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.Q.Status") && SpellManager.Q.IsReady() && !Player.Instance.HasBuff("GarenE"))
            {
                if (Player.Instance.IsInRange(target, SpellManager.Q.Range))
                {
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                    Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, target), Config.Misc.Menu.GetSliderValue("Config.Harass.Q.Delay"));
                }
            }

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.W.Status") && SpellManager.W.IsReady())
            {
                if (target.IsInRange(Player.Instance, Config.Harass.Menu.GetSliderValue("Config.Harass.W.Distance")))
                    SpellManager.W.Cast();
            }

            if (Config.Harass.Menu.GetCheckBoxValue("Config.Harass.E.Status") && SpellManager.E.IsReady() && !Player.Instance.HasBuff("GarenE"))
            {
                if (SpellManager.Q.IsReady() && Config.Harass.Menu.GetCheckBoxValue("Config.Harass.E.AfterQAA")) return;
                if (Player.Instance.IsInRange(target, SpellManager.E.Range))
                {
                    SpellManager.E.Cast();
                }
            }
        }
    }
}

using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReChoGath.Utils;

namespace ReChoGath.Modes
{
    public static class LastHit
    {
        public static void Execute()
        {
            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status"))
                Other.SetSpikes(true);
        }

        public static void OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (SpellManager.W.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.W.Unkillable") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.W.Mana"))
                    if (target.IsInRange(target, SpellManager.W.Range)) SpellManager.W.Cast();

                int time = (int)(Player.Instance.Position.Distance(target) / SpellManager.Q.Speed) * 1000;
                float health = Prediction.Health.GetPrediction(target, time);

                if (SpellManager.Q.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Unkillable") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Mana"))
                    if (health <= Damage.GetQDamage(target)) SpellManager.Q.Cast(SpellManager.Q.GetPrediction(target).CastPosition);
            }
        }
    }
}

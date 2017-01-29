using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReGaren.Utils;

namespace ReGaren.Modes
{
    public static class LastHit
    {
        public static void Execute()
        {
            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.LastHit") && SpellManager.Q.IsReady())
            {
                foreach (var e in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy).Where(m => m.IsValid && m.IsInRange(Player.Instance, SpellManager.Q.Range * 1.5f)).OrderByDescending(m => m.Health))
                {
                    if (e.Health <= Damage.GetQDamage(e))
                    {
                        SpellManager.Q.Cast();
                        Orbwalker.ResetAutoAttack();
                        Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, e), Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Delay"));
                        break;
                    }
                }
            }
        }

        public static void OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            if (!Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Unkillable") || Player.Instance.HasBuff("GarenE")) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (SpellManager.Q.IsReady() && target.CountEnemyChampionsInRange(550) <= 1 && Player.Instance.HealthPercent >= 30)
                {
                    if (target.IsInRange(Player.Instance, Player.Instance.GetAutoAttackRange()))
                    {
                        SpellManager.Q.Cast();
                        Orbwalker.ResetAutoAttack();
                        Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, target), Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Delay"));
                    }
                }
            }
        }
    }
}

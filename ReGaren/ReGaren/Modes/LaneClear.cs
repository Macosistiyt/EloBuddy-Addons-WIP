using EloBuddy;
using EloBuddy.SDK;
using ReGaren.Utils;
using System.Linq;

namespace ReGaren.Modes
{
    public static class LaneClear
    {
        public static void Execute()
        {
            bool HasQCasted = Player.Instance.HasBuff("GarenQ") ? true : false;
            if (SpellManager.Q.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") && !Player.Instance.HasBuff("GarenE"))
            {
                var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, SpellManager.Q.Range * 1.5f);
                if (minions.Any(h => h.Health <= Damage.GetQDamage(h)))
                {
                    HasQCasted = true;
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                    Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, minions.FirstOrDefault(h => h.Health <= Damage.GetQDamage(h))), Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Delay"));
                }
                else
                {
                    if (minions.OrderByDescending(h => h.Health).FirstOrDefault() != null)
                    {
                        HasQCasted = true;
                        SpellManager.Q.Cast();
                        Orbwalker.ResetAutoAttack();
                        Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, minions.OrderByDescending(h => h.Health).FirstOrDefault()), Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Delay"));
                    }
                } 
            }

            if (SpellManager.E.IsReady() && !(Player.Instance.HasBuff("GarenE") || Player.Instance.HasBuff("GarenQ") || HasQCasted) && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status"))
            {
                if (SpellManager.Q.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.AfterQAA")) return;
                if (Player.Instance.CountEnemyMinionsInRange((int)SpellManager.E.Range) >= Config.Farm.Menu.GetSliderValue("Config.Farm.E.Near"))
                {
                    SpellManager.E.Cast();
                }
            }
        }
    }
}

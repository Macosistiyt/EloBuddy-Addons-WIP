using EloBuddy;
using EloBuddy.SDK;
using ReGaren.Utils;
using System;
using System.Linq;

namespace ReGaren.Modes
{
    class JungleClear
    {
        public static void Execute()
        {
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, SpellManager.E.Range);
            if (monsters == null || !monsters.Any()) return;

            bool HasQCasted = Player.Instance.HasBuff("GarenQ") ? true : false;
            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") && SpellManager.Q.IsReady() && !Player.Instance.HasBuff("GarenE"))
            {
                HasQCasted = true;
                SpellManager.Q.Cast();
                Orbwalker.ResetAutoAttack();
                Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, monsters.OrderByDescending(h => h.Health).FirstOrDefault()), Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Delay"));
            }

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status") && !(Player.Instance.HasBuff("GarenE") || Player.Instance.HasBuff("GarenQ") || HasQCasted) && SpellManager.E.IsReady())
            {
                if ((SpellManager.Q.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.AfterQAA")) || (!Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Ignore") && monsters.Count(e => e.IsInRange(Player.Instance, SpellManager.E.Range)) < Config.Farm.Menu.GetSliderValue("Config.Farm.E.Near"))) return;
                SpellManager.E.Cast();
            }
        }
    }
}

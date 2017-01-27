using EloBuddy;
using EloBuddy.SDK;
using ReWarwick.Utils;
using System;
using System.Linq;

namespace ReWarwick.Modes
{
    class JungleClear
    {
        public static void Execute()
        {
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, SpellManager.Q.Range);
            if (monsters == null || !monsters.Any()) return;

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Mana") && SpellManager.Q.IsReady())
            {
                var target = monsters.OrderByDescending(h => h.Health).FirstOrDefault();
                SpellManager.Q.Cast(target);
            }

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.E.Mana") && SpellManager.E.IsReady())
            {
                if (monsters.Count() >= 2 || Player.Instance.HealthPercent <= 40)
                    SpellManager.E.Cast();
            }
        }
    }
}

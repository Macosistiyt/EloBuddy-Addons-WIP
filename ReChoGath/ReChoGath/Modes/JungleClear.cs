using EloBuddy;
using EloBuddy.SDK;
using ReChoGath.Utils;
using System;
using System.Linq;

namespace ReChoGath.Modes
{
    class JungleClear
    {
        public static void Execute()
        {
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, SpellManager.Q.Range);
            if (monsters == null || !monsters.Any()) return;

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") || Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Mana") && SpellManager.Q.IsReady())
            {
                var target = SpellManager.Q.GetBestCircularCastPosition(monsters);
                if (!Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Ignore"))
                {
                    if (target.HitNumber >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Hit"))
                        SpellManager.Q.Cast(target.CastPosition);
                }
                else
                    SpellManager.Q.Cast(target.CastPosition);
            }

            if (SpellManager.W.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.W.Status") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.W.Mana"))
            {
                var minions = SpellManager.W.GetBestConeCastPosition(monsters);
                if (minions.HitNumber >= Config.Farm.Menu.GetSliderValue("Config.Farm.W.Hit"))
                    SpellManager.W.Cast(minions.CastPosition);
            }

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status")) Other.SetSpikes(true);

            if (SpellManager.R.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.R.Status"))
            {
                foreach (var e in monsters.Where(t => t.IsInRange(Player.Instance, SpellManager.R.Range) && Other.BigMonsters.Contains(t.BaseSkinName)))
                {
                    if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.R.Ignore") && Other.GetFeastStacks() < 6)
                        if (e.Health + 3 <= Damage.GetRDamage(e))
                            SpellManager.R.Cast(e);

                    if (e.Health + 3 <= Damage.GetRDamage(e) && Config.Farm.Menu.GetCheckBoxValue($"Config.Farm.R.Monster.{e.BaseSkinName}"))
                        SpellManager.R.Cast(e);
                }
            }
        }
    }
}

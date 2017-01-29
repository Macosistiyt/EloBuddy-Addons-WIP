using EloBuddy;
using EloBuddy.SDK;
using ReChoGath.Utils;
using System;
using System.Linq;

namespace ReChoGath.Modes
{
    public static class LaneClear
    {
        public static void Execute()
        {
            if (SpellManager.Q.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Mana"))
            {
                var minions = SpellManager.Q.GetBestCircularCastPosition(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, SpellManager.Q.Range));
                if (minions.HitNumber >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Hit"))
                    SpellManager.Q.Cast(minions.CastPosition);
            }

            if (SpellManager.W.IsReady() && Config.Farm.Menu.GetCheckBoxValue("Config.Farm.W.Status") && Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.W.Mana"))
            {
                var minions = SpellManager.W.GetBestLinearCastPosition(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, SpellManager.W.Range));
                if (minions.HitNumber >= Config.Farm.Menu.GetSliderValue("Config.Farm.W.Hit"))
                    SpellManager.W.Cast(minions.CastPosition);
            }

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status") && Player.Instance.CountEnemyMinionsInRange(Player.Instance.GetAutoAttackRange() * 3) > 0)
                Other.SetSpikes(true);
        }
    }
}

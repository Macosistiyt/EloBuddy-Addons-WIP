using EloBuddy;
using EloBuddy.SDK;
using ReAhri.Utils;

namespace ReAhri.Modes
{
    public static class LaneClear
    {
        public static void Execute()
        {
            if (!Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") || Player.Instance.ManaPercent < Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Mana") || !SpellManager.Q.IsReady()) return;

            var minions = SpellManager.Q.GetBestLinearCastPosition(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.ServerPosition, SpellManager.Q.Range));
            if (minions.HitNumber >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Hit"))
                SpellManager.Q.Cast(minions.CastPosition);
        }
    }
}

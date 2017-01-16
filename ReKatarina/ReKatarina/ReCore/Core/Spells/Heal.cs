using EloBuddy;
using System.Linq;
using EloBuddy.SDK;
using ReKatarina.ReCore.ConfigList;
using ReKatarina.ReCore.Managers;
using ReKatarina.ReCore.Utility;

namespace ReKatarina.ReCore.Core.Spells
{
    class Heal : ISpell
    {
        public void Execute()
        {
            var enemies = Player.Instance.CountEnemyChampionsInRange(1500);
            if (MenuHelper.GetCheckBoxValue(Protector.Menu, "healDangerous"))
            {
                if (enemies > 0 && Player.Instance.IsInDanger(MenuHelper.GetSliderValue(Protector.Menu, "healMe")))
                    SummonerManager.Heal.Cast();

                foreach (var d in EloBuddy.SDK.EntityManager.Heroes.Allies.Where(a => !a.IsMe && a.IsAlive() && !a.IsInvulnerable && a.IsInDanger(MenuHelper.GetSliderValue(Protector.Menu, "healAlly")) && MenuHelper.GetCheckBoxValue(Protector.Menu, $"useHealOn{a.ChampionName}")))
                    SummonerManager.Heal.Cast();
            }
            else
                if (Player.Instance.HealthPercent <= MenuHelper.GetSliderValue(Protector.Menu, "healMe"))
                    SummonerManager.Heal.Cast();
        }

        public bool ShouldGetExecuted()
        {
            if (!SummonerManager.Heal.IsReady() || !MenuHelper.GetCheckBoxValue(Protector.Menu, "enableHeal"))
                return false;
            return false;
        }

        public void OnDraw()
        {

        }

        public void OnEndScene()
        {

        }
    }
}

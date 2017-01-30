using EloBuddy;
using EloBuddy.SDK;
using ReCORE.ReCore.Config;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;

namespace ReCORE.ReCore.Core.Spells
{
    class Barrier : ISpell
    {
        public void Execute()
        {
            if (Player.Instance.HealthPercent > MenuHelper.GetSliderValue(Summoners.Menu, "Summoners.Barrier.Health"))
                return;

            var enemies = Player.Instance.CountEnemyChampionsInRange(300);
            if (MenuHelper.GetCheckBoxValue(Summoners.Menu, "Summoners.Barrier.Dangerous"))
            {
                if (enemies > 0 && Player.Instance.IsInDanger(MenuHelper.GetSliderValue(Summoners.Menu, "Summoners.Barrier.Health")))
                    SummnerManager.Barrier.Cast();
            }
            else
                SummnerManager.Barrier.Cast();
        }

        public bool ShouldGetExecuted()
        {
            if (!SummnerManager.Barrier.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "Summoners.Barrier.Status"))
                return false;
            return true;
        }

        public void OnDraw()
        {
            
        }

        public void OnEndScene()
        {
            
        }
    }
}

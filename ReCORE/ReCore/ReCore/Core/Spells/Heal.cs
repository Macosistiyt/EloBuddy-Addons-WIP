using EloBuddy;
using System.Linq;
using EloBuddy.SDK;
using ReCORE.ReCore.Config;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;

namespace ReCORE.ReCore.Core.Spells
{
    class Heal : ISpell
    {
        public void Execute()
        {
            var enemies = Player.Instance.CountEnemyChampionsInRange(MenuHelper.GetSliderValue(Config.Settings.Menu, "Settings.Range"));
            if (MenuHelper.GetCheckBoxValue(Protector.Menu, "Protector.Heal.Dangerous"))
            {
                if (enemies > 0 && Player.Instance.IsInDanger(MenuHelper.GetSliderValue(Protector.Menu, "Protector.Heal.Health.Me")))
                    SummnerManager.Heal.Cast();

                foreach (var d in EloBuddy.SDK.EntityManager.Heroes.Allies.Where(a => !a.IsMe && a.IsAlive() && !a.IsInvulnerable && a.IsInRange(Player.Instance, SummnerManager.Heal.Range) && a.IsInDanger(MenuHelper.GetSliderValue(Protector.Menu, "Protector.Heal.Health.Ally")) && MenuHelper.GetCheckBoxValue(Protector.Menu, $"Protector.Heal.Use.{a.ChampionName}")))
                    SummnerManager.Heal.Cast();
            }
            else
                if (Player.Instance.HealthPercent <= MenuHelper.GetSliderValue(Protector.Menu, "Protector.Heal.Health.Me"))
                    SummnerManager.Heal.Cast();
        }

        public bool ShouldGetExecuted()
        {
            if (!SummnerManager.Heal.IsReady() || !MenuHelper.GetCheckBoxValue(Protector.Menu, "Protector.Heal.Status"))
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

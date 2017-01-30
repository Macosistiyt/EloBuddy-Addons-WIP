using EloBuddy;
using System.Linq;
using EloBuddy.SDK;
using ReCORE.ReCore.Config;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;

namespace ReCORE.ReCore.Core.Spells
{
    class Exhaust : ISpell
    {
        public void Execute()
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var enemy = EloBuddy.SDK.EntityManager.Heroes.Enemies.
                    Where(e =>
                        !e.IsDead &&
                        e.IsInRange(Player.Instance, SummnerManager.Exhaust.Range) &&
                        e.TotalShieldHealth() <= MenuHelper.GetSliderValue(Summoners.Menu, "Summoners.Exhaust.Health"));
                SummnerManager.Exhaust.Cast(enemy.FirstOrDefault());
            }
        }

        public bool ShouldGetExecuted()
        {
            if (!SummnerManager.Exhaust.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "Summoners.Exhaust.Status"))
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

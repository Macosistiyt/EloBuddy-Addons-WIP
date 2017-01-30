using EloBuddy;
using System.Linq;
using EloBuddy.SDK;
using ReCORE.ReCore.Config;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;

namespace ReCORE.ReCore.Core.Spells
{
    class Ignite : ISpell
    {
        public void Execute()
        {
            if (Summoners.Menu.GetCheckBoxValue("Summoners.Ignite.KillSteal"))
            {
                Obj_AI_Base ks = EloBuddy.SDK.EntityManager.Heroes.Enemies.FirstOrDefault(p =>
                                Prediction.Health.GetPrediction(p, Game.Ping) <= Managers.EntityManager.GetIgniteDamage() &&
                                p.IsValidTarget(SummnerManager.Ignite.Range));
                if (ks != null && ks.IsValid)
                    SummnerManager.Ignite.Cast(ks);
            }

            Obj_AI_Base target = TargetSelector.GetTarget(SummnerManager.Ignite.Range, DamageType.True);
            if (target == null || !target.IsValid) return;
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if (target.HealthPercent <= Summoners.Menu.GetSliderValue("Summoners.Ignite.Health"))
                SummnerManager.Ignite.Cast(target);
        }

        public bool ShouldGetExecuted()
        {
            if (!SummnerManager.Ignite.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "Summoners.Ignite.Status"))
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

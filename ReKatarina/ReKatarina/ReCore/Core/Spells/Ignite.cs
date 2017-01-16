using EloBuddy;
using System.Linq;
using EloBuddy.SDK;
using ReKatarina.ReCore.ConfigList;
using ReKatarina.ReCore.Managers;
using ReKatarina.ReCore.Utility;

namespace ReKatarina.ReCore.Core.Spells
{
    class Ignite : ISpell
    {
        public void Execute()
        {
            if (Summoners.Menu.GetCheckBoxValue("KsWithIgnite"))
            {
                Obj_AI_Base ks = EloBuddy.SDK.EntityManager.Heroes.Enemies.FirstOrDefault(p =>
                                Prediction.Health.GetPrediction(p, Game.Ping) <= ReKatarina.ReCore.Managers.EntityManager.GetIgniteDamage() &&
                                p.IsValidTarget(SummonerManager.Ignite.Range));
                if (ks != null && ks.IsValid)
                    SummonerManager.Ignite.Cast(ks);
            }
            Obj_AI_Base target = TargetSelector.GetTarget(SummonerManager.Ignite.Range, DamageType.True);
            if (target == null || !target.IsValid) return;
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) return;
            if (target.HealthPercent <= Summoners.Menu.GetSliderValue("igniteChampsHp"))
                SummonerManager.Ignite.Cast(target);
        }

        public bool ShouldGetExecuted()
        {
            if (!SummonerManager.Ignite.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "enableIgnite"))
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

using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using ReCORE.ReCore.Config;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;
using System;
using System.Collections.Generic;

namespace ReCORE.ReCore.Core.Spells
{
    class Snowball : ISpell
    {
        public void Execute()
        {
            Obj_AI_Base target = TargetSelector.GetTarget(SummnerManager.Snowball.Range, DamageType.True);
            if (target == null || !target.IsValid()) return;
            var prediction = SummnerManager.Snowball.GetPrediction(target);
            if (prediction.HitChancePercent >= 75)
                SummnerManager.Snowball.Cast(prediction.CastPosition);
        }

        public bool ShouldGetExecuted()
        {
            if (!SummnerManager.Snowball.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "Summoners.Snowball.Status") || SummnerManager.Snowball.Name.ToLower().Contains("snowballfollowupcast"))
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

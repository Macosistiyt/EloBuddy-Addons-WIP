﻿using EloBuddy;
using EloBuddy.SDK;
using ReKatarina.ReCore.ConfigList;
using ReKatarina.ReCore.Managers;
using ReKatarina.ReCore.Utility;

namespace ReKatarina.ReCore.Core.Spells
{
    class Barrier : ISpell
    {
        public void Execute()
        {
            if (Player.Instance.HealthPercent > MenuHelper.GetSliderValue(Summoners.Menu, "barrierHp"))
                return;

            var enemies = Player.Instance.CountEnemyChampionsInRange(300);
            if (MenuHelper.GetCheckBoxValue(Summoners.Menu, "barrierDangerous"))
            {
                if (enemies > 0 && Player.Instance.IsInDanger(MenuHelper.GetSliderValue(Summoners.Menu, "barrierHp")))
                    SummonerManager.Barrier.Cast();
            }
            else
                SummonerManager.Barrier.Cast();
        }

        public bool ShouldGetExecuted()
        {
            if (!SummonerManager.Barrier.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "enableBarrier"))
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

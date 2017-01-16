﻿using EloBuddy;
using System.Linq;
using EloBuddy.SDK;
using ReKatarina.ReCore.ConfigList;
using ReKatarina.ReCore.Managers;
using ReKatarina.ReCore.Utility;

namespace ReKatarina.ReCore.Core.Spells
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
                        e.IsInRange(e, SummonerManager.Exhaust.Range) &&
                        e.TotalShieldHealth() <= MenuHelper.GetSliderValue(Summoners.Menu, "exhaustHp"));
                SummonerManager.Exhaust.Cast(enemy.FirstOrDefault());
            }
        }

        public bool ShouldGetExecuted()
        {
            if (!SummonerManager.Exhaust.IsReady() || !MenuHelper.GetCheckBoxValue(Summoners.Menu, "enableExhaust"))
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

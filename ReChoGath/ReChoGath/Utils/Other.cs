using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReChoGath.Utils
{
    static class Other
    {
        public static readonly Random GetRandom = new Random();
        public static int GetAditionalDelay()
        {
            return GetRandom.Next(50, Config.Misc.Menu.GetSliderValue("Misc.Humanizer.RandomDelay"));
        }

        public static string[] BigMonsters =
        {
            "TT_Spiderboss", "TTNGolem", "TTNWolf", "TTNWraith",
            "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Red", "SRU_Krug", "Sru_Crab", "SRU_Baron", "SRU_RiftHerald",
            "SRU_Dragon_Air", "SRU_Dragon_Water", "SRU_Dragon_Fire", "SRU_Dragon_Elder", "SRU_Dragon_Earth"
        };

        public static void SetSpikes(bool new_status)
        {
            switch (new_status)
            {
                case true:
                    if (!Player.Instance.HasBuff("VorpalSpikes")) SpellManager.E.Cast();
                    break;
                case false:
                    if (Player.Instance.HasBuff("VorpalSpikes")) SpellManager.E.Cast();
                    break;
            }
        }

        public static int GetFeastStacks()
        {
            if (Player.Instance.HasBuff("Feast")) return Player.Instance.GetBuff("Feast").Count;
            return 0;
        }

        public static void FlashR(Obj_AI_Base target) // best combo btw Kappa
        {
            Console.WriteLine(Damage.GetRDamage(target));
            if (!SpellManager.PlayerHasFlash || !SpellManager.Flash.IsReady() || !SpellManager.R.IsReady() || target.TotalShieldHealth() + 5 > Damage.GetRDamage(target)) return;

            var position = Player.Instance.Position.Extend(target, SpellManager.Flash.Range).To3D();
            if ((position.IsUnderEnemyTurret() && !Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.UnderTurret")) || position.IsWall()) return;

            SpellManager.Flash.Cast(position);
            Core.DelayAction(() => SpellManager.R.Cast(target), 250);
        }
    }
}

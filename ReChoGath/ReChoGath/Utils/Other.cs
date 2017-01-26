using EloBuddy;
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

        public static int GetFeastStacks() // TODO
        {
            return 0;
        }
    }
}

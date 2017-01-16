using ReKatarina.ReCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReKatarina.ReCore.Utility
{
    class ItemsList
    {
        public static List<IItem> modules = new List<IItem>();
        public static void Initialize()
        {
            if (Managers.SummonerManager.PlayerHasBarrier) modules.Add(new Core.Items.OffensiveItems());
            if (Managers.SummonerManager.PlayerHasCleanse) modules.Add(new Core.Items.DeffensiveItems());
            if (Managers.SummonerManager.PlayerHasExhaust) modules.Add(new Core.Items.ConsumerItems());
        }
    }
}

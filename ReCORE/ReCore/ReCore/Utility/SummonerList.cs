using ReCORE.ReCore.Core;
using System.Collections.Generic;

namespace ReCORE.ReCore.Utility
{
    class SummonerList
    {
        public static List<ISpell> modules = new List<ISpell>();
        public static void Initialize()
        {
            if (Managers.SummnerManager.PlayerHasBarrier) modules.Add(new Core.Spells.Barrier());
            if (Managers.SummnerManager.PlayerHasCleanse) modules.Add(new Core.Spells.Cleanse());
            if (Managers.SummnerManager.PlayerHasExhaust) modules.Add(new Core.Spells.Exhaust());
            if (Managers.SummnerManager.PlayerHasHeal) modules.Add(new Core.Spells.Heal());
            if (Managers.SummnerManager.PlayerHasIgnite) modules.Add(new Core.Spells.Ignite());
            if (Managers.SummnerManager.PlayerHasSmite) modules.Add(new Core.Spells.Smite());
            if (Managers.SummnerManager.PlayerHasSnowball) modules.Add(new Core.Spells.Snowball());
        }
    }
}

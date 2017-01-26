using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReChoGath.ReCore.Utility;

namespace ReChoGath.ReCore.Core
{
    class SummonerUpdater
    {
        public static void Update()
        {
            foreach (var module in SummonerList.modules.Where(module => module.ShouldGetExecuted()))
            {
                module.Execute();
            }
        }
    }
}

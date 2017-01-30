using EloBuddy;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCORE.ReCore.Core
{
    class ItemUpdater
    {
        public static void Update()
        {
            foreach (var module in ItemsList.modules)
            {
                module.Execute();
            }
        }
    }
}

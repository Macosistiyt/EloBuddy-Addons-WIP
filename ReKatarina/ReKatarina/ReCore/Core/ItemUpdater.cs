using EloBuddy;
using ReKatarina.ReCore.Managers;
using ReKatarina.ReCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReKatarina.ReCore.Core
{
    class ItemUpdater
    {
        public static void Update()
        {
            if (MenuHelper.GetCheckBoxValue(ConfigList.Settings.Menu, "Settings.PreventCanceling") && !Player.Instance.ShouldUseItem()) return;
            foreach (var module in ItemsList.modules)
            {
                module.Execute();
            }
        }
    }
}

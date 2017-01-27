using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReWarwick.Utils;

namespace ReWarwick.Config
{
    public static class Farm
    {
        public static readonly Menu Menu;

        static Farm()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Farm");
            Menu.AddGroupLabel("Farm settings");

            Menu.AddGroupLabel("Q settings");
            Menu.CreateCheckBox("Use in lane clear / jungle clear", "Config.Farm.Q.Status");
            Menu.CreateCheckBox("Use in last hit mode", "Config.Farm.Q.LastHit");
            Menu.CreateCheckBox("Use on unkillable minion", "Config.Farm.Q.Unkillable");
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.Q.Mana", 45, 1, 100);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in lane clear / jungle clear", "Config.Farm.E.Status");
            Menu.CreateSlider("Use only if creeps near >= {0}", "Config.Farm.E.Near", 3, 1, 5);
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.E.Mana", 45, 1, 100);
        }

        public static void Initialize()
        {
        }
    }
}
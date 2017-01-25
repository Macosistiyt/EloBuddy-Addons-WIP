using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReAhri.Utils;

namespace ReAhri.Config
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
            Menu.CreateCheckBox("Use on unkillable minion", "Config.Farm.Q.Unkillable", false);
            Menu.CreateSlider("Use only if will hit >= {0} units", "Config.Farm.Q.Hit", 3, 1, 5);
            Menu.CreateCheckBox("Ignore hit count in jungle", "Config.Farm.Q.Ignore");
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.Q.Mana", 45, 1, 100);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use on unkillable minion", "Config.Farm.W.Unkillable");
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.W.Mana", 45, 1, 100);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in jungle clear", "Config.Farm.E.Status");
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.E.Mana", 45, 1, 100);
        }

        public static void Initialize()
        {
        }
    }
}
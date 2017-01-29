using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReGaren.Utils;

namespace ReGaren.Config
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
            Menu.CreateSlider("Delay (ms) between Q -> AA", "Config.Farm.Q.Delay", 100, 0, 300);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in lane clear / jungle clear", "Config.Farm.E.Status");
            Menu.CreateSlider("Use only if creeps near >= {0}", "Config.Farm.E.Near", 3, 1, 5);
            Menu.CreateCheckBox("Ignore creeps count in jungle clear", "Config.Farm.E.Ignore");
            Menu.CreateCheckBox("Use always after Q -> AA", "Config.Farm.E.AfterQAA");
        }

        public static void Initialize()
        {
        }
    }
}
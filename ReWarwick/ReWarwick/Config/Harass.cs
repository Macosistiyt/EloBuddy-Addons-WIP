using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReWarwick.Utils;

namespace ReWarwick.Config
{
    public static class Harass
    {
        public static readonly Menu Menu;

        static Harass()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Harass");
            Menu.AddGroupLabel("Harass settings");

            Menu.AddGroupLabel("Q settings");
            Menu.CreateCheckBox("Use in harass", "Config.Harass.Q.Status");
            Menu.CreateCheckBox("Use in auto harass", "Config.AutoHarass.Q.Status");
            Menu.CreateSlider("Auto harass usage chance", "Config.AutoHarass.Q.Chance", 65, 1, 100);
            Menu.AddLabel("100% = always, 0% = never.", 15);
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Harass.Q.Mana", 45, 1, 100);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in harass", "Config.Harass.E.Status");
            Menu.CreateCheckBox("Use in auto harass", "Config.AutoHarass.E.Status");
            Menu.CreateCheckBox("Use instantly E2", "Config.Harass.E.After", false);
            Menu.CreateSlider("Auto harass usage chance", "Config.AutoHarass.E.Chance", 35, 1, 100);
            Menu.AddLabel("100% = always, 0% = never", 15);
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Harass.E.Mana", 45, 1, 100);

            Menu.CreateSlider("Enable Auto-Harass only when my health >= {0}%", "Config.AutoHarass.Health", 65, 1, 100);
            Menu.CreateSlider("Enable Auto-Harass only when enemies near <= {0}", "Config.AutoHarass.Enemies", 1, 1, 5);
        }

        public static void Initialize()
        {
        }
    }
}
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReGaren.Utils;

namespace ReGaren.Config
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
            Menu.CreateSlider("Delay (ms) between Q -> AA", "Config.Harass.Q.Delay", 100, 0, 300);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use in harass", "Config.Harass.W.Status");
            Menu.CreateSlider("Use when target distance <= {0} units", "Config.Harass.W.Distance", 525, 200, 875);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in harass", "Config.Harass.E.Status");
            Menu.CreateCheckBox("Use always after Q -> AA", "Config.Harass.E.AfterQAA");
        }

        public static void Initialize()
        {
        }
    }
}
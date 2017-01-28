using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReChoGath.Utils;

namespace ReChoGath.Config
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
            Menu.CreateSlider("Use only if hit chance >= {0}", "Config.Harass.Q.HitChance", 70, 1, 100);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use in harass", "Config.Harass.W.Status");
            Menu.CreateCheckBox("Use in auto harass", "Config.AutoHarass.W.Status");
            Menu.CreateSlider("Auto harass usage chance", "Config.AutoHarass.W.Chance", 30, 1, 100);
            Menu.AddLabel("100% = always, 0% = never.", 15);
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Harass.W.Mana", 45, 1, 100);
            Menu.CreateSlider("Use only if hit chance >= {0}", "Config.Harass.W.HitChance", 85, 1, 100);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in harass", "Config.Harass.E.Status");
        }

        public static void Initialize()
        {
        }
    }
}
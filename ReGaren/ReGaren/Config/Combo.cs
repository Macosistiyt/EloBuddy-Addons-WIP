using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReGaren.Utils;

namespace ReGaren.Config
{
    public static class Combo
    {
        public static readonly Menu Menu;

        static Combo()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Combo");
            Menu.AddGroupLabel("Combo settings");

            Menu.AddGroupLabel("Q settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.Q.Status");
            Menu.CreateSlider("Delay (ms) between Q -> AA", "Config.Combo.Q.Delay", 100, 0, 300);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.W.Status");
            Menu.CreateSlider("Use when target distance <= {0} units", "Config.Combo.W.Distance", 525, 200, 875);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.E.Status");
            Menu.CreateCheckBox("Use always after Q -> AA", "Config.Combo.E.AfterQAA");

            Menu.AddGroupLabel("R settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.R.Status");
            Menu.CreateSlider("Subtract {0} from damage calculations", "Config.Combo.R.Substract", 5, 0, 25);
            Menu.AddLabel("Whitelist :");
            foreach (var e in EntityManager.Heroes.Enemies)
            {
                Menu.CreateCheckBox($"Use on {e.ChampionName}", $"Config.Combo.R.Use.{e.ChampionName}");
            }
        }

        public static void Initialize()
        {
        }
    }
}
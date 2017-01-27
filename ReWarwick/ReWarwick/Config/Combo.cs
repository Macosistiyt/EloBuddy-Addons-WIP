using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReWarwick.Utils;

namespace ReWarwick.Config
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

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.E.Status");
            Menu.CreateCheckBox("Use instantly E2", "Config.Combo.E.After", false);

            Menu.AddGroupLabel("R settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.R.Status");
            Menu.CreateCheckBox("Allow jump under enemy turret", "Config.Combo.R.Dive", false);
            Menu.AddLabel("Whitelist :");
            foreach (var e in EntityManager.Heroes.Enemies)
            {
                Menu.CreateCheckBox($"Use on {e.ChampionName}", $"Config.Combo.R.Use.{e.ChampionName}");
            }
            Menu.CreateCheckBox("Ignore whitelist when force", "Config.Combo.R.IgnoreForce");
            Menu.CreateKeyBind("Force R usage", "Config.Combo.R.Force", 85, KeyBind.UnboundKey, KeyBind.BindTypes.HoldActive);
            Menu.CreateSlider("Use only when hit chance >= {0}%", "Config.Combo.R.HitChance", 80, 1, 100);
            Menu.CreateSlider("Use automatically only when target health >= {0}%", "Config.Combo.R.TargetHealth", 40, 1, 100);
        }

        public static void Initialize()
        {
        }
    }
}
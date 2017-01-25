using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReAhri.Utils;

namespace ReAhri.Config
{
    public static class Combo
    {
        public static readonly Menu Menu;

        static Combo()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Combo");
            Menu.AddGroupLabel("Combo settings");

            Menu.CreateComboBox("Select combo mode", "Config.Combo.Mode", new System.Collections.Generic.List<string>() { "Auto", "EQWR", "QEWR" }, 0);

            Menu.AddGroupLabel("Q settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.Q.Status");
            Menu.CreateSlider("Use only if hit chance >= {0}", "Config.Combo.Q.HitChance", 2, 1, 3);
            Menu.AddLabel("1 = low (33%), 2 = medium (66%), 3 = high (99%)", 15);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.W.Status");

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.E.Status");
            Menu.CreateSlider("Use only if hit chance >= {0}", "Config.Combo.E.HitChance", 2, 1, 3);
            Menu.AddLabel("1 = low (33%), 2 = medium (66%), 3 = high (99%).", 15);

            Menu.AddGroupLabel("R settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.R.Status");
            Menu.CreateCheckBox("Use always to mouse position", "Config.Combo.R.Mouse", false);
            Menu.CreateCheckBox("Allow jump under enemy turret", "Config.Combo.R.Dive", false);
            Menu.CreateSlider("Use only if target health >= {0}%", "Config.Combo.R.Health", 35, 1, 100);
            Menu.CreateKeyBind("Force R usage", "Config.Combo.R.Force", 85, KeyBind.UnboundKey, KeyBind.BindTypes.HoldActive);
        }

        public static void Initialize()
        {
        }
    }
}
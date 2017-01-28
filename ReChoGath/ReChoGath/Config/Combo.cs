using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReChoGath.Utils;

namespace ReChoGath.Config
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
            Menu.CreateSlider("Use only if hit chance >= {0}%", "Config.Combo.Q.HitChance", 65, 1, 100);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.W.Status");
            Menu.CreateSlider("Use only if hit chance >= {0}%", "Config.Combo.W.HitChance", 65, 1, 100);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.E.Status");

            Menu.AddGroupLabel("R settings");
            Menu.CreateCheckBox("Use in combo", "Config.Combo.R.Status");
            Menu.CreateCheckBox("Use only when killable", "Config.Combo.R.Killable");
            Menu.CreateCheckBox("Enable Flash + R automatically if killable", "Config.Combo.R.FlashRAuto");
            Menu.CreateCheckBox("Allow flash under enemy turret", "Config.Combo.R.UnderTurret", false);
            Menu.CreateKeyBind("Flash + R key", "Config.Combo.R.FlashR", 85, KeyBind.UnboundKey, KeyBind.BindTypes.HoldActive, false);
        }

        public static void Initialize()
        {
        }
    }
}
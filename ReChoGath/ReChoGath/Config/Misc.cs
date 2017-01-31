using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReChoGath.Utils;

namespace ReChoGath.Config
{
    public static class Misc
    {
        public static readonly Menu Menu;

        static Misc()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Misc");
            Menu.AddGroupLabel("Misc settings");

            Menu.AddGroupLabel("Kill Steal");
            Menu.CreateCheckBox("KillSteal with Q", "Config.Misc.KillSteal.Q");
            Menu.CreateCheckBox("KillSteal with W", "Config.Misc.KillSteal.W");
            Menu.CreateCheckBox("KillSteal with R", "Config.Misc.KillSteal.R");

            Menu.AddGroupLabel("Skin manager");
            Menu.CreateCheckBox("Enable skin changer", "Misc.Skin.Status");
            Menu.CreateSlider("Select your skin", "Misc.Skin.Id", 0, 0, 7);

            Menu.AddGroupLabel("Humanizer");
            Menu.CreateCheckBox("Enable humanizer", "Misc.Humanizer.Status", false);
            Menu.CreateSlider("Select your delay between spells in (ms).", "Misc.Humanizer.Delay", 200, 50, 500);
            Menu.CreateSlider("Additional random delay in (ms).", "Misc.Humanizer.RandomDelay", 75, 50, 100);

            Menu.AddGroupLabel("Another settings");
            Menu.CreateCheckBox("Auto Q on immobile targets", "Config.Misc.Another.Q.AlwaysStun");
            Menu.CreateCheckBox("Enable Anti-Gapcloser", "Config.Misc.Another.Gapcloser");
            Menu.CreateCheckBox("Enable Interrupter", "Config.Misc.Another.Interrupter");
            Menu.CreateSlider("Anti-Gapcloser / Interrupter cast delay", "Config.Misc.Another.Delay", 0, 0, 500);

            #region Disable / enable skin changer
            if (Menu.Get<CheckBox>("Misc.Skin.Status").CurrentValue)
                Player.Instance.SetSkinId(Menu.GetSliderValue("Misc.Skin.Id"));

            Menu.Get<CheckBox>("Misc.Skin.Status").OnValueChange += (ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args) =>
            {
                if (args.NewValue == false) Player.Instance.SetSkinId(0);
                else Player.Instance.SetSkinId(Menu.GetSliderValue("Misc.Skin.Id"));
            };

            Menu.Get<Slider>("Misc.Skin.Id").OnValueChange += (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args) =>
            {
                if (Menu.GetCheckBoxValue("Misc.Skin.Status")) Player.Instance.SetSkinId(Menu.GetSliderValue("Misc.Skin.Id"));
            };
            #endregion
        }


        public static void Initialize()
        {
        }
    }
}
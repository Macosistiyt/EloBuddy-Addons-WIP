using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReKatarina.ReCore.Utility;

namespace ReKatarina.ReCore.ConfigList
{
    public static class Settings
    {
        public static readonly Menu Menu;

        static Settings()
        {
            Menu = Loader.Menu.AddSubMenu("Settings");
            Menu.AddGroupLabel("Settings");
            Menu.CreateCheckBox("Enable chat info", "Settings.Chat.Status");
            Menu.CreateSlider("Delay between ticks [{0}ms]", "Settings.Tick", 50, 25, 500);
        }

        public static void Initialize()
        {
        }
    }
}
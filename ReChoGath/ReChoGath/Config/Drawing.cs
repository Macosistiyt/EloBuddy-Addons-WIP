using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReChoGath.Utils;

namespace ReChoGath.Config
{
    public static class Drawing
    {
        public static readonly Menu Menu;

        static Drawing()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Drawing");
            Menu.AddGroupLabel("Drawing settings");

            Menu.CreateCheckBox("Draw Q range", "Config.Drawing.Q", true);
            Menu.CreateCheckBox("Draw W range", "Config.Drawing.W", false);
            Menu.CreateCheckBox("Draw R range", "Config.Drawing.R", false);
            Menu.CreateCheckBox("Draw damage indicator", "Config.Drawing.Indicator", true);
        }

        public static void Initialize()
        {
        }
    }
}
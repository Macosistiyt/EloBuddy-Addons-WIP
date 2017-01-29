using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReGaren.Utils;

namespace ReGaren.Config
{
    public static class Drawing
    {
        public static readonly Menu Menu;

        static Drawing()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Drawing");
            Menu.AddGroupLabel("Drawing settings");

            Menu.CreateCheckBox("Draw E range", "Config.Drawing.E", false);
            Menu.CreateCheckBox("Draw R range", "Config.Drawing.R", true);
            Menu.CreateCheckBox("Draw damage indicator", "Config.Drawing.Indicator", true);
        }

        public static void Initialize()
        {
        }
    }
}
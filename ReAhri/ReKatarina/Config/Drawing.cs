using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReAhri.Utils;

namespace ReAhri.Config
{
    public static class Drawing
    {
        public static readonly Menu Menu;

        static Drawing()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Drawing");
            Menu.AddGroupLabel("Drawing settings");

            Menu.CreateCheckBox("Draw Q range", "Config.Drawing.Q", false);
            Menu.CreateCheckBox("Draw Q position", "Config.Drawing.Q.Position", true);
            Menu.CreateCheckBox("Draw W range", "Config.Drawing.W", true);
            Menu.CreateCheckBox("Draw E range", "Config.Drawing.E", false);
            Menu.CreateCheckBox("Draw R range", "Config.Drawing.R", false);
            Menu.CreateCheckBox("Draw damage indicator", "Config.Drawing.Indicator", true);
        }

        public static void Initialize()
        {
        }
    }
}
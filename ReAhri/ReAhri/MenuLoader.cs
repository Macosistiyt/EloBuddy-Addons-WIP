using EloBuddy.SDK.Menu;
using ReAhri.Config;

namespace ReAhri
{
    public static class MenuLoader
    {
        public static readonly Menu Menu;

        static MenuLoader()
        {
            Menu = MainMenu.AddMenu("ReAhri", "ReAhri");
            Menu.AddGroupLabel("Welcome to ReAhri!");
            Utility.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Utility
        {
            static Utility()
            {
                // Menu
                Combo.Initialize();
                Drawing.Initialize();
                Farm.Initialize();
                Harass.Initialize();
                Misc.Initialize();
            }

            public static void Initialize()
            {
            }
        }
    }
}
using EloBuddy.SDK.Menu;
using ReChoGath.Config;

namespace ReChoGath
{
    public static class MenuLoader
    {
        public static readonly Menu Menu;

        static MenuLoader()
        {
            Menu = MainMenu.AddMenu("ReChoGath", "ReChoGath");
            Menu.AddGroupLabel("Welcome to ReChoGath!");
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
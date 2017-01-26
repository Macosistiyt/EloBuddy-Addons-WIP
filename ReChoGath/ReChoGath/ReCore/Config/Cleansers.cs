using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReChoGath.ReCore.Utility;

namespace ReChoGath.ReCore.Config
{
    public static class Cleansers
    {
        private static readonly Menu Menu;

        static Cleansers()
        {
            Menu = Loader.Menu.AddSubMenu("Cleaners");
            Menu.AddGroupLabel("Cleaners settings");
        }

        public static void Initialize()
        {
        }
    }
}
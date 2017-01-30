using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReCORE.ReCore.Utility;

namespace ReCORE.ReCore.Config
{
    public static class Stealer
    {
        private static readonly Menu Menu;

        static Stealer()
        {
            Menu = Loader.Menu.AddSubMenu("Stealers");
            Menu.AddGroupLabel("Stealers settings");
        }

        public static void Initialize()
        {
        }
    }
}
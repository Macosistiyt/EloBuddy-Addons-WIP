using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ReKatarina.ReCore.ConfigList
{
    public static class CItems
    {
        private static readonly Menu Menu;

        static CItems()
        {
            Menu = Loader.Menu.AddSubMenu("Consumer items");
            Menu.AddGroupLabel("Consumer items settings");
        }

        public static void Initialize()
        {
        }
    }
}
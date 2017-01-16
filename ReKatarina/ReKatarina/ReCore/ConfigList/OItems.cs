using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace ReKatarina.ReCore.ConfigList
{
    public static class OItems
    {
        private static readonly Menu Menu;

        static OItems()
        {
            Menu = Loader.Menu.AddSubMenu("Offensive items");
            Menu.AddGroupLabel("Offensive items settings");

            foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsMe))
            {
                Menu.Add("idkWhatIsThat", new CheckBox("Use on " + ally.ChampionName + "(" + ally.Name + ")"));
            }
        }

        public static void Initialize()
        {
        }
    }
}
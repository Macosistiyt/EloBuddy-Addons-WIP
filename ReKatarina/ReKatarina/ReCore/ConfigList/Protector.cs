using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using System.Linq;
using EloBuddy.SDK.Menu.Values;
using ReKatarina.ReCore.Utility;

namespace ReKatarina.ReCore.ConfigList
{
    public static class Protector
    {
        public static readonly Menu Menu;

        static Protector()
        {
            Menu = Loader.Menu.AddSubMenu("Protector");
            Menu.AddGroupLabel("Protector settings");

            if (Managers.SummonerManager.PlayerHasHeal)
            {
                Menu.CreateCheckBox("Enable heal", "enableHeal");
                Menu.AddSeparator();
                Menu.CreateCheckBox("Heal only dangerous", "healDangerous");
                Menu.CreateSlider("Heal if my HP <= {0}%", "healMe", 10);
                Menu.CreateSlider("Heal if ally HP <= {0}%", "healAlly", 10);
                Menu.AddSeparator();
                Menu.AddLabel("Whitelist");
                foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsMe))
                    Menu.CreateCheckBox(ally.ChampionName + " (" + ally.Name + ")", "useHealOn" + ally.ChampionName);
            }
            else
            {
                Menu.AddSeparator();
                Menu.AddLabel("You don't have any spell to protect your allies.");
            }

        }

        public static void Initialize()
        {
        }
    }
}
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReChoGath.Utils;

namespace ReChoGath.Config
{
    public static class Farm
    {
        public static readonly Menu Menu;

        static Farm()
        {
            Menu = MenuLoader.Menu.AddSubMenu("Farm");
            Menu.AddGroupLabel("Farm settings");

            Menu.AddGroupLabel("Q settings");
            Menu.CreateCheckBox("Use in lane clear / jungle clear", "Config.Farm.Q.Status");
            Menu.CreateCheckBox("Use on unkillable minion", "Config.Farm.Q.Unkillable", false);
            Menu.CreateSlider("Use only if will hit >= {0} units", "Config.Farm.Q.Hit", 3, 1, 5);
            Menu.CreateCheckBox("Ignore hit count in jungle", "Config.Farm.Q.Ignore");
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.Q.Mana", 45, 1, 100);

            Menu.AddGroupLabel("W settings");
            Menu.CreateCheckBox("Use in lane clear / jungle clear", "Config.Farm.W.Status");
            Menu.CreateCheckBox("Use on unkillable minion", "Config.Farm.W.Unkillable", false);
            Menu.CreateSlider("Use only if will hit >= {0} units", "Config.Farm.W.Hit", 3, 1, 5);
            Menu.CreateCheckBox("Ignore hit count in jungle", "Config.Farm.W.Ignore");
            Menu.CreateSlider("Use only if mana percent >= {0}%", "Config.Farm.W.Mana", 45, 1, 100);

            Menu.AddGroupLabel("E settings");
            Menu.CreateCheckBox("Use in lane clear / jungle clear", "Config.Farm.E.Status");

            Menu.AddGroupLabel("R settings");
            Menu.CreateCheckBox("Use in jungle clear", "Config.Farm.R.Status");
            Menu.CreateCheckBox("Use to jungle steal", "Config.Farm.R.Steal");
            Menu.CreateCheckBox("Ignore monster type if stacks < 6", "Config.Farm.R.Ignore");
            #region Monsters
            switch (Game.MapId)
            {
                case GameMapId.SummonersRift:
                    Menu.AddLabel("Epic monsters");
                    Menu.CreateCheckBox("Eat Baron", "Config.Farm.R.Monster." + "SRU_Baron");
                    Menu.CreateCheckBox("Eat Herald", "Config.Farm.R.Monster." + "SRU_RiftHerald");
                    Menu.CreateCheckBox("Eat Cloud Drake", "Config.Farm.R.Monster." + "SRU_Dragon_Air");
                    Menu.CreateCheckBox("Eat Infernal Drake", "Config.Farm.R.Monster." + "SRU_Dragon_Fire");
                    Menu.CreateCheckBox("Eat Mountain Drake", "Config.Farm.R.Monster." + "SRU_Dragon_Earth");
                    Menu.CreateCheckBox("Eat Ocean Drake", "Config.Farm.R.Monster." + "SRU_Dragon_Water");
                    Menu.CreateCheckBox("Eat Elder Drake", "Config.Farm.R.Monster." + "SRU_Dragon_Elder");

                    Menu.AddLabel("Normal monsters");
                    Menu.CreateCheckBox("Eat Red", "Config.Farm.R.Monster." + "SRU_Red");
                    Menu.CreateCheckBox("Eat Blue", "Config.Farm.R.Monster." + "SRU_Blue");

                    Menu.AddLabel("Other monsters");
                    Menu.CreateCheckBox("Eat Gromp", "Config.Farm.R.Monster." + "SRU_Gromp", false);
                    Menu.CreateCheckBox("Eat Murkwolf", "Config.Farm.R.Monster." + "SRU_Murkwolf", false);
                    Menu.CreateCheckBox("Eat Razorbeak", "Config.Farm.R.Monster." + "SRU_Razorbeak", false);
                    Menu.CreateCheckBox("Eat Krug", "Config.Farm.R.Monster." + "SRU_Krug", false);
                    Menu.CreateCheckBox("Eat Crab", "Config.Farm.R.Monster." + "Sru_Crab", false);
                    break;
                
                case GameMapId.TwistedTreeline:
                    Menu.AddLabel("Epic monsters");
                    Menu.CreateCheckBox("Eat Vilemaw", "Config.Farm.R.Monster." + "TT_Spiderboss");

                    Menu.AddLabel("Normal monsters");
                    Menu.CreateCheckBox("Eat Golem", "Config.Farm.R.Monster." + "TTNGolem");
                    Menu.CreateCheckBox("Eat Wolf", "Config.Farm.R.Monster." + "TTNWolf");
                    Menu.CreateCheckBox("Eat Wraith", "Config.Farm.R.Monster." + "TTNWraith");
                    break;
            }
            #endregion
        }

        public static void Initialize()
        {
        }
    }
}
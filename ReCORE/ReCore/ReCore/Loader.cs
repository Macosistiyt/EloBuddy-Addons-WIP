using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using ReCORE.ReCore.Config;
using ReCORE.ReCore.Managers;
using ReCORE.ReCore.Utility;
using System;

namespace ReCORE.ReCore
{
    class Loader
    {
        public static readonly Menu Menu;
        public static System.Version AssVersion { get { return System.Version.Parse("7.1.4"); } }

        static Loader()
        {
            Menu = MainMenu.AddMenu("ReCore", "ReCore");
            Menu.AddGroupLabel("Special thanks to MarioGK for his Mario's Lib.");

            SummnerManager.Initialize();
            ItemManager.Initialize();
            UtilsManager.Initialize();
            SummonerList.Initialize();
            ItemsList.Initialize();
            Utility.Initialize();
            DangerManager.Initialize();

            Game.OnTick += OnTick;
            Game.OnUpdate += OnTick;
            Drawing.OnDraw += Core.DrawingsUpdater.OnDraw;
            Drawing.OnEndScene += Core.DrawingsUpdater.OnEndScene;

            Menu.AddGroupLabel("Welcome to ReCore v." + AssVersion + " [BETA].");
            Chat.Print("<font color='#FFFFFF'>ReCore v." + AssVersion + "</font> <font color='#CF2942'>[BETA]</font> <font color='#FFFFFF'>has been loaded.</font>");
        }

        private static void OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead || Player.Instance.IsRecalling() || !TickLimiter.Check())
                return;

            Core.SummonerUpdater.Update();
            Core.ItemUpdater.Update();
            Core.UtilsUpdater.Update();
        }

        public static void Initialize()
        {
        }

        public static class Utility
        {
            static Utility()
            {
                // Menu
                Summoners.Initialize();
                if (UtilsManager.IsSupported) Stealer.Initialize();
                OItems.Initialize();
                DItems.Initialize();
                CItems.Initialize();
                Protector.Initialize();
                Cleansers.Initialize();
                Settings.Initialize();
            }

            public static void Initialize()
            {
            }
        }
    }
}

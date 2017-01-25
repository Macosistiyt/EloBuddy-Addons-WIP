using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using ReAhri.Modes;
using ReAhri.ReCore;
using System;
using SharpDX;
using ReAhri.Utils;
using System.Linq;

namespace ReAhri
{
    class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Ahri") return;
            
            //VersionChecker.Check();
            Loader.Initialize(); // ReCore BETA
            Humanizer.Initialize();
            MenuLoader.Initialize();
            Drawing.OnDraw += OnDraw;
            Game.OnTick += OnTick;
            Game.OnUpdate += OnTick;
            Orbwalker.OnUnkillableMinion += LastHit.OnUnkillableMinion;
            Drawing.OnEndScene += OnEndScene;

            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;

            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;

            Chat.Print("<font color='#FFFFFF'>ReAhri v." + VersionChecker.AssVersion + " has been loaded.</font>");
        }

        private static void OnEndScene(EventArgs args)
        {
            if (Player.Instance.IsDead || !Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.Indicator"))
                return;

            Indicator.Execute();
        }

        public static void OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead || Player.Instance.IsRecalling()) 
                return;

            PermaActive.Execute();
            var flags = Orbwalker.ActiveModesFlags;
            #region Flags checker
            if (flags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                try
                {
                    Combo.Execute();
                }
                catch (Exception e) 
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
            if (flags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                try
                {
                    Harass.Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
            if (flags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                try
                {
                    LastHit.Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
            if (flags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                try
                {
                    LaneClear.Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
            if (flags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                try
                {
                    JungleClear.Execute();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
            if (Config.Combo.Menu.GetKeyBindValue("Config.Combo.R.Force"))
            {
                if (SpellManager.R.IsReady() || Player.Instance.HasBuff("AhriTumble"))
                {
                    var position = Player.Instance.Position.Distance(Game.CursorPos) < SpellManager.R.Range ? Game.CursorPos : Player.Instance.Position.Extend(Game.CursorPos, SpellManager.R.Range).To3D();
                    SpellManager.R.Cast(position);
                }
            }
            #endregion
        }

        public static MissileClient QOrbMissile;
        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var hero = sender as MissileClient;
            if (hero != null && (hero.SData.Name == "AhriOrbMissile" || hero.SData.Name == "AhriOrbReturn")) QOrbMissile = null;
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var hero = sender as MissileClient;
            if (hero != null && (hero.SData.Name == "AhriOrbMissile" || hero.SData.Name == "AhriOrbReturn")) QOrbMissile = hero;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!Config.Misc.Menu.GetCheckBoxValue("Config.Misc.Another.Gapcloser") || !SpellManager.E.IsReady() || !sender.IsValidTarget(SpellManager.E.Range) || (e.End.Distance(Player.Instance) > 400)) return;
            if (SpellManager.E.GetPrediction(sender).Collision) return;

            Core.DelayAction(() => SpellManager.E.Cast(sender), Config.Misc.Menu.GetSliderValue("Config.Misc.Another.Delay"));
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!Config.Misc.Menu.GetCheckBoxValue("Config.Misc.Another.Interrupter") || !SpellManager.E.IsReady() || !sender.IsValidTarget(SpellManager.E.Range)) return;
            if (SpellManager.E.GetPrediction(sender).Collision) return;

            Core.DelayAction(() => SpellManager.E.Cast(sender), Config.Misc.Menu.GetSliderValue("Config.Misc.Another.Delay"));
        }

        private static void OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            foreach (var spell in SpellManager.AllSpells)
            {
                switch (spell.Slot)
                {
                    case SpellSlot.Q:
                        if (!Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.Q")) continue;
                        break;
                    case SpellSlot.W:
                        if (!Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.W")) continue;
                        break;
                    case SpellSlot.E:
                        if (!Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.E")) continue;
                        break;
                    case SpellSlot.R:
                        if (!Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.R")) continue;
                        break;
                }
                Circle.Draw(spell.GetColor(), spell.Range, Player.Instance);
            }

            if (Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.Q.Position"))
            {
                var position = QOrbMissile?.Position;
                if (position == null) return;

                var polygon = new Geometry.Polygon.Rectangle(Player.Instance.Position.To2D(), position.Value.To2D(), SpellManager.E.Width);
                polygon.Draw(System.Drawing.Color.Blue, 3);
            }
        }
    }
}

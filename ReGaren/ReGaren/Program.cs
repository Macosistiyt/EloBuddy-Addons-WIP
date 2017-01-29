using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using ReGaren.Modes;
using ReGaren.ReCore;
using System;
using ReGaren.Utils;

namespace ReGaren
{
    class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Garen") return;
            
            VersionChecker.Check();
            Loader.Initialize(); // ReCore BETA
            Humanizer.Initialize();
            MenuLoader.Initialize();
            Drawing.OnDraw += OnDraw;
            Game.OnTick += OnTick;
            Game.OnUpdate += OnTick;
            Orbwalker.OnUnkillableMinion += LastHit.OnUnkillableMinion;
            Drawing.OnEndScene += OnEndScene;

            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;

            Chat.Print("<font color='#FFFFFF'>ReGaren v." + VersionChecker.AssVersion + " has been loaded.</font>");
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
            #endregion
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!SpellManager.Q.IsReady() || !Config.Misc.Menu.GetCheckBoxValue("Config.Misc.Another.Interrupter") || !sender.IsValidTarget(Player.Instance.GetAutoAttackRange())) return;

            SpellManager.Q.Cast();
            Orbwalker.ResetAutoAttack();
            Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, sender), Config.Misc.Menu.GetSliderValue("Config.Misc.Another.Delay"));
        }

        private static void OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            foreach (var spell in SpellManager.AllSpells)
            {
                switch (spell.Slot)
                {
                    case SpellSlot.E:
                        if (!Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.E")) continue;
                        break;
                    case SpellSlot.R:
                        if (!Config.Drawing.Menu.GetCheckBoxValue("Config.Drawing.R")) continue;
                        break;
                    default: continue;
                }
                Circle.Draw(spell.GetColor(), spell.Range, Player.Instance);
            }
        }
    }
}

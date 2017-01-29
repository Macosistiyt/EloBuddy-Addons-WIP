using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;
using EloBuddy.SDK.Enumerations;
using ReGaren.Utils;

namespace ReGaren.Modes
{
    public static class PermaActive
    {
        private static bool chance(int chance)
        {
            return (Other.GetRandom.Next(0, 100) <= chance);
        }

        private static int LastRemove = Core.GameTickCount;
        public static void Execute()
        {
            #region Auto slow remover
            if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.Another.Slow") && Player.Instance.HasBuffOfType(BuffType.Slow))
                if (Core.GameTickCount - LastRemove >= SpellManager.Q.Handle.Cooldown * 1000)
                    Core.DelayAction(() => SpellManager.Q.Cast(), Config.Misc.Menu.GetSliderValue("Config.Misc.Another.SlowDelay"));
            #endregion
            #region KillSteal
            foreach (var e in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.IsAlive() && h.IsInRange(Player.Instance.Position, SpellManager.R.Range) && !h.IsInvulnerable && !h.HasSpellshield()))
            {
                float health = Prediction.Health.GetPrediction(e, 500);
                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.Q") && SpellManager.Q.IsReady() && Player.Instance.IsInAutoAttackRange(e) && health <= Damage.GetQDamage(e))
                {
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                    Core.DelayAction(() => Player.IssueOrder(GameObjectOrder.AttackTo, e), 100);
                    break;  
                }

                if (Config.Misc.Menu.GetCheckBoxValue("Config.Misc.KillSteal.R") && SpellManager.R.IsReady() && health + Config.Combo.Menu.GetSliderValue("Config.Combo.R.Substract") <= Damage.GetRDamage(e))
                {
                    SpellManager.R.Cast(e);
                    break;
                }
            }
            #endregion
        }
    }
}

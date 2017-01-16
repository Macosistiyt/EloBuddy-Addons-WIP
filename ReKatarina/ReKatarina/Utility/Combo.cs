using System;
using EloBuddy;
using EloBuddy.SDK;

namespace ReKatarina.Utility
{
    public static class Combo
    {
        private static bool shouldOrbToDagger = false;

        public static void Execute()
        {
            if (shouldOrbToDagger)
            {
                if (SpellManager.E.IsReady()) SpellManager.E.Cast(Dagger.GetClosestDagger());
                else Orbwalker.OrbwalkTo(Dagger.GetClosestDagger());
            }
            else if (shouldOrbToDagger && Damage.HasRBuff()) shouldOrbToDagger = false;

            var target = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Mixed, Player.Instance.Position);
            if (target == null || target.IsInvulnerable)
                return;

            if (Damage.HasRBuff())
            {
                if (Player.Instance.CountEnemyChampionsInRange(SpellManager.R.Range) <= 0)
                {
                    Damage.UnfreezePlayer();
                }
                if (SpellManager.R.IsReady() && Player.Instance.CountEnemyChampionsInRange(SpellManager.E.Range) > 0)
                {
                    SpellManager.E.Cast(target.Position);
                    Core.DelayAction(() => SpellManager.R.Cast(), 125);
                    Core.DelayAction(() => Damage.FreezePlayer(), 125);
                }
                return;
            }

            switch (ConfigList.Combo.ComboStyle)
            {
                case 1:
                    Combo1(target);
                    break;
                case 2:
                    Combo2(target);
                    break;
                case 3:
                    Combo3(target);
                    break;
                default:
                    Combo4(target);
                    break;
            }
        }

        private static void Combo1(AIHeroClient t)
        {
            if (SpellManager.Q.CanCast(t) && ConfigList.Combo.ComboQ)
            {
                SpellManager.Q.Cast(t);
            }

            if (SpellManager.E.IsReady() && t.IsInRange(Player.Instance.Position, SpellManager.E.Range) && ConfigList.Combo.ComboE)
            {
                SpellManager.E.Cast(t.Position);
            }

            if (SpellManager.W.IsLearned && !SpellManager.W.IsOnCooldown && ConfigList.Combo.ComboW)
            {
                if (t.IsInRange(Player.Instance.Position, SpellManager.W.Range))
                    SpellManager.W.Cast();
            }

            if (SpellManager.R.IsLearned && !SpellManager.R.IsOnCooldown && ConfigList.Combo.ComboR)
            {
                if (Player.Instance.CountEnemyChampionsInRange(ConfigList.Combo.MaxRCastRange) < ConfigList.Combo.MinToUseR) return;
                if (Damage.GetQDamage(t) + Damage.GetWDamage(t) + Damage.GetEDamage(t) + Player.Instance.GetAutoAttackDamage(t, true) >= t.TotalShieldHealth()) return;
                SpellManager.R.Cast();
                Damage.FreezePlayer();
            }

            if (!Damage.HasRBuff())
            {
                shouldOrbToDagger = true;
            }
        }
        private static void Combo2(AIHeroClient t)
        {
            if (SpellManager.Q.CanCast(t) && ConfigList.Combo.ComboQ)
            {
                SpellManager.Q.Cast(t);
            }

            if (SpellManager.E.IsReady() && t.IsInRange(Player.Instance.Position, SpellManager.E.Range) && ConfigList.Combo.ComboE)
            {
                SpellManager.E.Cast(t.Position);
            }

            if (!Damage.HasRBuff())
            {
                shouldOrbToDagger = true;
            }

            if (!shouldOrbToDagger && !Damage.HasRBuff() && Player.Instance.IsInAutoAttackRange(t))
            {
                Player.IssueOrder(GameObjectOrder.AttackUnit, t);
            }
        }
        private static void Combo3(AIHeroClient t)
        {
            if (SpellManager.E.IsReady() && t.IsInRange(Player.Instance.Position, SpellManager.E.Range) && ConfigList.Combo.ComboE)
            {
                SpellManager.E.Cast(t.Position);
            }

            if (SpellManager.Q.IsReady() && t.IsInRange(Player.Instance.Position, SpellManager.Q.Range) && ConfigList.Combo.ComboQ)
            {
                SpellManager.Q.Cast(t);
            }

            if (SpellManager.E.IsReady() && Player.Instance.IsInRange(Dagger.GetClosestDagger(), SpellManager.E.Range) && ConfigList.Combo.ComboE)
            {
                SpellManager.E.Cast(Dagger.GetClosestDagger());
            }
        }
        private static void Combo4(AIHeroClient t)
        {
            if (SpellManager.Q.CanCast(t) && ConfigList.Combo.ComboQ)
            {
                SpellManager.Q.Cast(t);
            }

            if (SpellManager.E.IsReady() && t.IsInRange(Player.Instance.Position, SpellManager.E.Range) && ConfigList.Combo.ComboE)
            {
                var d = Dagger.GetClosestDagger();
                if (t.Position.IsInRange(d, SpellManager.W.Range)) SpellManager.E.Cast(d);
                else
                    if (Player.Instance.Distance(t) >= SpellManager.W.Range)
                        SpellManager.E.Cast(t.Position);
            }

            if (SpellManager.W.IsLearned && !SpellManager.W.IsOnCooldown && ConfigList.Combo.ComboW)
            {
                if (t.IsInRange(Player.Instance.Position, SpellManager.W.Range))
                    SpellManager.W.Cast();
            }

            if (SpellManager.R.IsLearned && !SpellManager.R.IsOnCooldown && ConfigList.Combo.ComboR)
            {
                if (Player.Instance.CountEnemyChampionsInRange(ConfigList.Combo.MaxRCastRange) < ConfigList.Combo.MinToUseR) return;
                if (Damage.GetQDamage(t) + Damage.GetWDamage(t) + Damage.GetEDamage(t) + Player.Instance.GetAutoAttackDamage(t, true) >= t.TotalShieldHealth()) return;
                SpellManager.R.Cast();
                Damage.FreezePlayer();
            }
        }
    }
}
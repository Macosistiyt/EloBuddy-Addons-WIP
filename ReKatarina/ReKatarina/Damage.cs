using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Linq;

namespace ReKatarina
{
    static class Extensions
    {
        public static bool IsUnderEnemyTurret(this Vector3 d)
        {
            return EntityManager.Turrets.Enemies.Any((Obj_AI_Turret turret) => turret.IsInRange(d, turret.GetAutoAttackRange(null)));
        }
    }

    class Damage
    {
        public static readonly Random getrandom = new Random();
        public static double GetQDamage(Obj_AI_Base target)
        {
            if (SpellManager.Q.IsReady())
                return Player.Instance.GetSpellDamage(target, SpellSlot.Q, DamageLibrary.SpellStages.Default);
            return 0;
        }

        public static double GetWDamage(Obj_AI_Base target)
        {
            if (SpellManager.W.IsLearned)
                return Player.Instance.GetSpellDamage(target, SpellSlot.W, DamageLibrary.SpellStages.Default);
            return 0;
        }

        public static double GetEDamage(Obj_AI_Base target)
        {
            if (SpellManager.E.IsReady())
                return Player.Instance.GetSpellDamage(target, SpellSlot.E, DamageLibrary.SpellStages.Default);
            return 0;
        }

        public static double GetRDamage(Obj_AI_Base target)
        {
            if (SpellManager.R.IsReady())
                return Player.Instance.GetSpellDamage(target, SpellSlot.R, DamageLibrary.SpellStages.Default);
            return 0;
        }

        public static double GetTotalDamage(Obj_AI_Base target)
        {
            var damage = 0.0;
            damage += GetQDamage(target);
            damage += GetWDamage(target);
            damage += GetEDamage(target);
            damage += GetRDamage(target);
            damage += Player.Instance.GetAutoAttackDamage(target, true);
            return damage;
        }

        public static int GetAditionalDelay()
        {
            return getrandom.Next(50, (ConfigList.Misc.GetMaxAditDelay));
        }

        public static bool HasRBuff()
        {
            return SpellManager.CastingUlt;
        }

        public static void FreezePlayer()
        {
            Orbwalker.DisableAttacking = true;
            Orbwalker.DisableMovement = true;
            SpellManager.CastingUlt = true;
            SpellManager.LastUltCast = Core.GameTickCount;
        }

        public static void UnfreezePlayer()
        {
            Orbwalker.DisableAttacking = false;
            Orbwalker.DisableMovement = false;
            SpellManager.CastingUlt = false;
        }
    }
}

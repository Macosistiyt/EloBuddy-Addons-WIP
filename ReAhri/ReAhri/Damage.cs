using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using ReAhri.Utils;

namespace ReAhri
{
    class Damage
    {
        public static double GetQDamage(Obj_AI_Base target)
        {
            if (!SpellManager.Q.IsReady() || SpellManager.Q.Level <= 0) return 0;
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 40, 65, 90, 115, 140 }[SpellManager.Q.Level] + (0.35f * Player.Instance.TotalMagicalDamage) + (new float[] { 0, 40, 65, 90, 115, 140 }[SpellManager.Q.Level] / 2)));
        }

        public static double GetWDamage(Obj_AI_Base target)
        {
            if (SpellManager.W.IsReady())
                return Player.Instance.GetSpellDamage(target, SpellSlot.W, DamageLibrary.SpellStages.Default);
            return 0;
        }

        public static double GetEDamage(Obj_AI_Base target)
        {
            if (!SpellManager.E.IsReady() || SpellManager.E.Level <= 0) return 0;

            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 60, 95, 130, 165, 200 }[SpellManager.E.Level] + (0.5f * Player.Instance.TotalMagicalDamage)));
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
    }
}

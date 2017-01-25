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
            int[] damages = { 40, 65, 90, 115, 140 };

            return (damages[SpellManager.Q.Level - 1] + (0.35 * Player.Instance.TotalMagicalDamage)) * ((100 - target.PercentMagicReduction) / 100) + damages[SpellManager.Q.Level - 1] / 2;
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
            int[] damages = { 60, 95, 130, 165, 200 };

            return (damages[SpellManager.E.Level - 1] + (0.5 * Player.Instance.TotalMagicalDamage)) * ((100 - target.PercentMagicReduction) / 100);
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

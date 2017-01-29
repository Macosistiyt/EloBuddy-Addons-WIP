using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using ReGaren.Utils;

namespace ReGaren
{
    class Damage
    {
        private static Dictionary<int, int> MaxESpins = new Dictionary<int, int>() { { 1, 5 }, { 4, 6 }, { 7, 7 }, { 10, 8 }, { 13, 9 }, { 16, 10 } };

        public static double GetQDamage(Obj_AI_Base target)
        {
            if (SpellManager.Q.IsReady() || Player.Instance.HasBuff("GarenQ"))
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                    (new float[] { 0, 30, 45, 60, 75, 90 }[SpellManager.Q.Level] + 1.4f * Player.Instance.TotalAttackDamage));
            return 0;
        }

        public static double GetEDamage(Obj_AI_Base target)
        {
            if (SpellManager.E.IsReady())
            {
                int spins = MaxESpins.Where(e => Player.Instance.Level <= e.Key).FirstOrDefault().Value;
                if (target.CountEnemyChampionsInRange(250) > 0)
                    return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                        ((new float[] { 0, 14, 18, 22, 26, 30 }[SpellManager.E.Level] + new float[] { 0, 0.34f, 0.35f, 0.36f, 0.37f, 0.38f }[SpellManager.E.Level] * Player.Instance.TotalAttackDamage) * spins));
                else
                    return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                        ((new float[] { 0, 18.62f, 23.94f, 29.26f, 34.58f, 39.9f }[SpellManager.E.Level] + new float[] { 0, 0.4522f, 0.4655f, 0.4788f, 0.4921f, 0.5054f }[SpellManager.E.Level] * Player.Instance.TotalAttackDamage) * spins));
            }
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
            damage += GetEDamage(target);
            damage += GetRDamage(target);
            damage += Player.Instance.GetAutoAttackDamage(target, true);
            return damage;
        }
    }
}

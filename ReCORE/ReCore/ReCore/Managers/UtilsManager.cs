using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCORE.ReCore.Managers
{
    class UtilsManager
    {
        public static bool IsSupported = false;
        private static List<Champion> SupportedChampions = new List<Champion>()
        {
            Champion.Ashe,
            Champion.Draven,
            Champion.Ezreal,
            Champion.Jinx
        };

        public static Dictionary<Champion, StealerInfo> StealerInfos = new Dictionary<Champion, StealerInfo>()
        {
            [Champion.Ashe] = new StealerInfo(1600, 250, 130),
            [Champion.Draven] = new StealerInfo(2000, 500, 150),
            [Champion.Ezreal] = new StealerInfo(2000, 1000, 150),
            [Champion.Jinx] = new StealerInfo(1700, 600, 140, 1700, 2500)
        };

        public static List<Vector2> MonsterPos = new List<Vector2>()
        {
            new Vector2(0, 0), // TODO, INSERT BARON POS
            new Vector2(0, 0)  // TODO, INSERT DRAKE POS
        };

        public static void Initialize()
        {
            if (!SupportedChampions.Contains(Player.Instance.Hero)) return;
            IsSupported = true;
        }
    }

    public class StealerInfo
    {
        public float Speed { get; }
        public float MinimumSpeed { get; }
        public float MaximumSpeed { get; }
        public float CastTime { get; }
        public int Width { get; }

        public StealerInfo(float speed, float castTime, int width)
        {
            Speed = speed;
            CastTime = castTime;
            Width = width;
        }

        public StealerInfo(float speed, float castTime, int width, float minimumSpeed, float maximumSpeed)
        {
            Speed = speed;
            CastTime = castTime;
            Width = width;
            MinimumSpeed = minimumSpeed;
            MaximumSpeed = maximumSpeed;
        }
    }

    public class Utils
    {
        public static float GetTravelTime(StealerInfo stealer, Vector3 point)
        {
            switch (Player.Instance.Hero)
            {
                case Champion.Ashe:
                case Champion.Draven:
                case Champion.Ezreal:
                {
                    return (stealer.CastTime + (Player.Instance.Position.Distance(point) / stealer.Speed * 1000));
                }

                case Champion.Jinx:
                {
                    var distance = Player.Instance.Position.Distance(point);
                    if (distance <= 1700) return (stealer.CastTime + (Player.Instance.Position.Distance(point) / stealer.Speed * 1000));
                    return (((1700 / stealer.Speed + stealer.CastTime) + (distance - 1700) / 2230) * 1000);
                }

                default:
                    Console.WriteLine("ReCORE.ReCore.Managers :: Utils :: GetTravelTime - Error.");
                    break;
            }
            return float.MaxValue;
        }

        public static Vector3 GetCastPosition(StealerInfo stealer, Obj_AI_Base target)
        {
            switch (Player.Instance.Hero)
            {
                case Champion.Ashe:
                case Champion.Jinx:
                {
                    var closest_enemy = EloBuddy.SDK.EntityManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsInRange(target, 250)).OrderByDescending(e => e.Distance(target)).FirstOrDefault();
                    if (closest_enemy == null) return new Vector3();
                    return closest_enemy.Position;
                }

                case Champion.Draven:
                case Champion.Ezreal:
                {
                    return target.Position;
                }

                default:
                    Console.WriteLine("ReCORE.ReCore.Managers :: Utils :: GetCastPosition - Error.");
                    break;
            }

            return new Vector3();
        }
    }

    public static class DamageLib
    {
        public static int GetStealDamage(Obj_AI_Base target)
        {
            switch (Player.Instance.Hero)
            {
                case Champion.Ashe: return GetAsheDamage(target);
                case Champion.Draven: return GetDravenDamage(target);
                case Champion.Ezreal: return GetEzrealDamage(target);
                case Champion.Jinx: return GetJinxDamage(target);
            }
            return 0;
        }

        #region Champion damage
        private static int GetEzrealDamage(Obj_AI_Base target)
        {
            int level = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level;
            var base_damage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                                                                   (new float[] { 0, 350, 500, 650 }[level] +
                                                                   (Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage) +
                                                                   (.9f * Player.Instance.TotalMagicalDamage)));

            var total_damage = base_damage;
            var polygon = new Geometry.Polygon.Rectangle(Player.Instance.Position, target.Position, 160);
            foreach (var e in EloBuddy.SDK.EntityManager.Heroes.Enemies.Where(h => !h.IsDead))
                if (polygon.IsInside(e))
                    total_damage *= .9f;

            if (base_damage * .3f > total_damage) return (int)(base_damage * .3f);
            return (int)total_damage;
        }

        private static int GetJinxDamage(Obj_AI_Base target)
        {
            int level = 0;
            var base_damage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                                                                    (new float[] { 250, 350, 450 }[level] +
                                                                    (new float[] { 25, 30, 35 }[level] / 100 * (target.MaxHealth - target.Health)) +
                                                                    (1 * Player.Instance.FlatPhysicalDamageMod)) * .8f);
            return (int)base_damage;
        }

        private static int GetDravenDamage(Obj_AI_Base target)
        {
            int level = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level;
            var base_damage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                                                                   (new float[] { 0, 175, 275, 375 }[level] +
                                                                   ((Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage) * 1.1f)));

            var total_damage = base_damage;
            var polygon = new Geometry.Polygon.Rectangle(Player.Instance.Position, target.Position, 150);
            foreach (var e in EloBuddy.SDK.EntityManager.Heroes.Enemies.Where(h => !h.IsDead))
                if (polygon.IsInside(e))
                    total_damage *= .92f;

            if (base_damage * .4f > total_damage) return (int)(base_damage * .4f);
            return (int)total_damage;
        }

        private static int GetAsheDamage(Obj_AI_Base target)
        {
            int level = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level;
            var base_damage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical,
                                                                   (new float[] { 0, 200, 400, 600 }[level] + Player.Instance.TotalMagicalDamage) * .5f);

            var polygon = new Geometry.Polygon.Rectangle(Player.Instance.Position, target.Position, 160);
            foreach (var e in EloBuddy.SDK.EntityManager.Heroes.Enemies.Where(h => !h.IsDead))
                if (polygon.IsInside(e))
                    return 0;

            return (int)base_damage;
        }
        #endregion
    }
}

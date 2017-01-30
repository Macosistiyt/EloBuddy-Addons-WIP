using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
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
        public static List<Champion> SupportedChampions = new List<Champion>()
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
            return 0;
        }

        private static int GetJinxDamage(Obj_AI_Base target)
        {
            return 0;
        }

        private static int GetDravenDamage(Obj_AI_Base target)
        {
            return 0;
        }

        private static int GetAsheDamage(Obj_AI_Base target)
        {
            return 0;
        }
        #endregion
    }
}

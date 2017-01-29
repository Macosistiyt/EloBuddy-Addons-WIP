using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReGaren.Utils
{
    static class Extensions
    {
        public static bool IsUnderEnemyTurret(this Vector3 d)
        {
            return EntityManager.Turrets.Enemies.Any((Obj_AI_Turret turret) => turret.IsInRange(d, turret.GetAutoAttackRange(null)) && turret.IsAlive());
        }
    }
}

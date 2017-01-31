using EloBuddy;
using EloBuddy.SDK;
using ReCORE.ReCore.Managers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCORE.ReCore.Core
{
    class UtilsUpdater
    {
        public static void Update()
        {
            if (!UtilsManager.IsSupported || !Player.Instance.Spellbook.GetSpell(SpellSlot.R).IsReady) return;

            var target = new Obj_AI_Base();
            foreach (var p in UtilsManager.MonsterPos)
                target = EloBuddy.SDK.EntityManager.MinionsAndMonsters.GetJungleMonsters(p.To3D(), 800).FirstOrDefault();

            if (target == null || target.IsDead) return;
            int damage = DamageLib.GetStealDamage(target);
            Vector3 position = Utils.GetCastPosition(UtilsManager.StealerInfos[Player.Instance.Hero], target);
            int travel_time = (int)Utils.GetTravelTime(UtilsManager.StealerInfos[Player.Instance.Hero], position);

            if (damage >= Prediction.Health.GetPrediction(target, travel_time))
            {
                Player.Instance.Spellbook.CastSpell(SpellSlot.R, position);
            }
        }
    }
}

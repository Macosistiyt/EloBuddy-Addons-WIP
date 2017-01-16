using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace ReKatarina
{
    class Dagger
    {
        public static IEnumerable<Obj_AI_Base> GetDaggers()
        {
            return ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "HiddenMinion" && a.IsValid && a.Health == 100);
        }

        public static Vector3 GetClosestDagger()
        {
            var dagger = GetDaggers();
            if (dagger.Count() <= 0 || dagger.Any() || dagger == null) return new Vector3();
            return GetDaggers().Where(p => p.Distance(Player.Instance) >= 125).OrderBy(p => p.Distance(Player.Instance.Position)).FirstOrDefault().Position;
        }
    }
}

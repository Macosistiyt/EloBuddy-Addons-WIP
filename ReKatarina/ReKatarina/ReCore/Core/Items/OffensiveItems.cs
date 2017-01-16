using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReKatarina.ReCore.Core.Items
{
    class OffensiveItems : IItem
    {
        public void Execute()
        {
            var target = TargetSelector.GetTarget(700.0f, DamageType.Mixed, Player.Instance.Position);
            foreach (var item in Player.Instance.InventoryItems)
            {
                switch (item.Id)
                {
                    case ItemId.Corrupting_Potion:
                    case ItemId.Health_Potion:
                        if (Player.Instance.HealthPercent <= 75)
                            item.Cast();
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnDraw()
        {
            return;
        }

        public void OnEndScene()
        {
            return;
        }
    }
}

using EloBuddy;
using EloBuddy.SDK;
using ReAhri.Utils;
using System.Linq;

namespace ReAhri.Modes
{
    class JungleClear
    {
        public static void Execute()
        {
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, SpellManager.Q.Range);
            if (monsters == null || !monsters.Any()) return;

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Status") || Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Mana") && SpellManager.Q.IsReady())
            {
                var target = SpellManager.Q.GetBestLinearCastPosition(monsters);
                if (!Config.Farm.Menu.GetCheckBoxValue("Config.Farm.Q.Ignore"))
                {
                    if (target.HitNumber >= Config.Farm.Menu.GetSliderValue("Config.Farm.Q.Hit"))
                        SpellManager.Q.Cast(target.CastPosition);
                }
                else
                    SpellManager.Q.Cast(target.CastPosition);
            }

            if (Config.Farm.Menu.GetCheckBoxValue("Config.Farm.E.Status") || Player.Instance.ManaPercent >= Config.Farm.Menu.GetSliderValue("Config.Farm.E.Mana") && SpellManager.E.IsReady())
            {
                var target = monsters.FirstOrDefault(e => Other.BigMonsters.Contains(e.BaseSkinName));
                if (target == null) return;

                var prediction = SpellManager.E.GetPrediction(target);
                if (!prediction.Collision && prediction.HitChance >= EloBuddy.SDK.Enumerations.HitChance.High) SpellManager.E.Cast(prediction.CastPosition);
            }
        }
    }
}

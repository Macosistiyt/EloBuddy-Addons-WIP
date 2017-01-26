using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using System.Collections.Generic;

namespace ReChoGath
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        public static List<Spell.SpellBase> AllSpells { get; private set; }
        public static Dictionary<SpellSlot, Color> ColorTranslation { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, 750, int.MaxValue, 175);
            W = new Spell.Skillshot(SpellSlot.W, 650, SkillShotType.Cone, 250, 1750, 100);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 175);

            AllSpells = new List<Spell.SpellBase>(new Spell.SpellBase[] { Q, W, E, R });
            ColorTranslation = new Dictionary<SpellSlot, Color>
            {
                { SpellSlot.Q, Color.LimeGreen.ToArgb(150) },
                { SpellSlot.W, Color.CornflowerBlue.ToArgb(150) },
                { SpellSlot.E, Color.YellowGreen.ToArgb(150) },
                { SpellSlot.R, Color.OrangeRed.ToArgb(150) }
            };
        }

        public static void Initialize()
        {
        }

        public static Color ToArgb(this Color color, byte a) // by Hellsing 
        {
            return new ColorBGRA(color.R, color.G, color.B, a);
        }

        public static Color GetColor(this Spell.SpellBase spell) // by Hellsing 
        {
            return ColorTranslation.ContainsKey(spell.Slot) ? ColorTranslation[spell.Slot] : Color.Wheat;
        }
    }
}
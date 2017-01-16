using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ReKatarina.ConfigList
{
    public static class Flee
    {
        public static readonly Menu Menu;
        private static readonly CheckBox _JumpToAlly;
        private static readonly CheckBox _JumpToDagger;
        private static readonly CheckBox _JumpToAllyMinion;
        private static readonly CheckBox _JumpToEnemyMinion;
        private static readonly CheckBox _JumpToMonster;
        private static readonly Slider _JumpToMonsterHP;
        private static readonly Slider _JumpCursorRange;

        public static bool JumpToAlly { get { return _JumpToAlly.CurrentValue; } }
        public static bool JumpToDagger { get { return _JumpToDagger.CurrentValue; } }
        public static bool JumpToAllyMinion { get { return _JumpToAllyMinion.CurrentValue; } }
        public static bool JumpToEnemyMinion { get { return _JumpToEnemyMinion.CurrentValue; } }
        public static bool JumpToMonster { get { return _JumpToMonster.CurrentValue; } }
        public static int JumpToMonsterHP { get { return _JumpToMonsterHP.CurrentValue; } }
        public static int JumpCursorRange { get { return _JumpCursorRange.CurrentValue; } }
        

        static Flee()
        {
            Menu = Config.Menu.AddSubMenu("Flee");
            Menu.AddGroupLabel("Flee settings");
            _JumpToAlly = Menu.Add("Flee.Ally", new CheckBox("Enable jump to ally."));
            _JumpToDagger = Menu.Add("Flee.Dagger", new CheckBox("Enable jump to dagger."));
            _JumpToAllyMinion = Menu.Add("Flee.AllyMinion", new CheckBox("Enable jump to ally minion."));
            _JumpToEnemyMinion = Menu.Add("Flee.EnemyMinion", new CheckBox("Enable jump to enemy minion."));
            _JumpToMonster = Menu.Add("Flee.Monster", new CheckBox("Enable jump to monster."));
            _JumpToMonsterHP = Menu.Add("Flee.MonsterHP", new Slider("Enable jump to monster when HP > {0}%", 15, 1, 100));
            _JumpCursorRange = Menu.Add("Flee.CursorRange", new Slider("Jump to cursor target detect range. ", 200, 100, 250));
        }

        public static void Initialize()
        {
        }
    }
}
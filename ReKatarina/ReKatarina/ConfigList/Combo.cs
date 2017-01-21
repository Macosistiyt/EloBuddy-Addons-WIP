using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ReKatarina.ConfigList
{
    public static class Combo
    {
        private static readonly Menu Menu;
        private static readonly CheckBox _ComboQ;
        private static readonly CheckBox _ComboW;
        private static readonly CheckBox _ComboE;
        private static readonly CheckBox _ComboR;
        private static readonly Slider _MinToUseR;
        private static readonly Slider _MaxRCastRange;
        private static readonly Slider _MinHPToGoUnderTower;
        private static readonly CheckBox _GoUnderTower;
        private static readonly Slider _ComboSaver;
        private static readonly ComboBox _ComboStyle;

        public static bool ComboQ
        {
            get { return _ComboQ.CurrentValue; }
        }
        public static bool ComboW
        {
            get { return _ComboW.CurrentValue; }
        }
        public static bool ComboE
        {
            get { return _ComboE.CurrentValue; }
        }
        public static bool ComboR
        {
            get { return _ComboR.CurrentValue; }
        }
        public static int MinToUseR
        {
            get { return _MinToUseR.CurrentValue; }
        }
        public static int MaxRCastRange
        {
            get { return _MaxRCastRange.CurrentValue; }
        }
        public static bool GoUnderTower
        {
            get { return _GoUnderTower.CurrentValue; }
        }
        public static int ComboSaver
        {
            get { return _ComboSaver.CurrentValue; }
        }
        public static int MinHPToGoUnderTower
        {
            get { return _MinHPToGoUnderTower.CurrentValue; }
        }
        public static int ComboStyle
        {
            get { return _ComboStyle.CurrentValue; }
        }

        static Combo()
        {
            Menu = Config.Menu.AddSubMenu("Combo");
            Menu.AddGroupLabel("Combo settings");
            _ComboStyle = Menu.Add("Combo.Style", new ComboBox("Select combo style", 3, "Auto", "EWQR", "EQWR", "QEWR"));
            _ComboQ = Menu.Add("Combo.UseQ", new CheckBox("Use Q in combo"));
            _ComboW = Menu.Add("Combo.UseW", new CheckBox("Use W in combo"));
            _ComboE = Menu.Add("Combo.UseE", new CheckBox("Use E in combo"));
            _ComboR = Menu.Add("Combo.UseR", new CheckBox("Use R in combo"));

            Menu.AddGroupLabel("R settings");
            _MinToUseR = Menu.Add("Combo.R.Minimum", new Slider("Minimum enemies to use R.", 1, 1, 5));
            _MaxRCastRange = Menu.Add("Combo.R.MaxRange", new Slider("Maximum R range to cast.", (int)SpellManager.W.Range, (int)SpellManager.W.Range, (int)SpellManager.R.Range));

            Menu.AddGroupLabel("Another settings");
            _ComboSaver = Menu.Add("Combo.E.Saver", new Slider("Don't go in if my health <= {0}% and QW is not ready.", 15, 1, 100));
            _GoUnderTower = Menu.Add("Combo.E.Turret", new CheckBox("Allow E jump under enemy turret.", false));
            _MinHPToGoUnderTower = Menu.Add("Combo.R.Turret.MinHP", new Slider("My health must be >= {0}% to allow enter under enemy tower.", 35, 1, 100));
        }

        public static void Initialize()
        {
        }
    }
}
using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Collections.Generic;
using ReChoGath.Utils;
using System.Linq;

namespace ReChoGath.Modes
{
    public static class Combo
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(SpellManager.E.Range, DamageType.Mixed, Player.Instance.Position);
            if (target == null || target.IsInvulnerable)
                return;

            switch (Config.Combo.Menu.GetComboBoxValue("Config.Combo.Mode"))
            {
                case 1:
                    Combo1(target);
                    break;
                case 2:
                    Combo2(target);
                    break;
                default:
                    Combo2(target);
                    break;
            }
        }
        private static void Combo1(Obj_AI_Base target) // EQWR
        {
            if (SpellManager.E.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.E.Status"))
            {
                var prediction = SpellManager.E.GetPrediction(target);
                if (!prediction.Collision && prediction.HitChancePercent >= Config.Combo.Menu.GetSliderValue("Config.Combo.E.HitChance") * 33)
                    SpellManager.E.Cast(prediction.CastPosition);
            }

            if (SpellManager.Q.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.Q.Status"))
            {
                var prediction = SpellManager.Q.GetPrediction(target);
                if (prediction.HitChancePercent >= Config.Combo.Menu.GetSliderValue("Config.Combo.Q.HitChance") * 33)
                    SpellManager.Q.Cast(prediction.CastPosition);
            }

            if (SpellManager.W.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.W.Status"))
            {
                if (target.Position.IsInRange(Player.Instance.Position, SpellManager.W.Range)) SpellManager.W.Cast();
            }

            if (SpellManager.R.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Status"))
            {
                if (!Player.HasBuff("AhriTumble") && target.HealthPercent < Config.Combo.Menu.GetSliderValue("Config.Combo.R.Health")) return;

                if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Mouse"))
                {
                    if (target.HealthPercent >= 50 && Game.CursorPos.Extend(target, SpellManager.R.Range).To3D().IsUnderEnemyTurret() && !Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Dive")) return;
                    SpellManager.R.Cast(Game.CursorPos.Extend(target, SpellManager.R.Range).To3D());
                }
                else
                {
                    var RPos = GetBestRPos(target);
                    if ((target.HealthPercent >= 50 || Player.Instance.HealthPercent < 35) && RPos.IsUnderEnemyTurret() && !Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Dive")) return;
                    SpellManager.R.Cast(Player.Instance.Position.Extend(RPos, SpellManager.R.Range).To3D());
                }
            }
        }
        private static void Combo2(Obj_AI_Base target) // QEWR
        {
            if (SpellManager.Q.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.Q.Status"))
            {
                var prediction = SpellManager.Q.GetPrediction(target);
                if (prediction.HitChancePercent >= Config.Combo.Menu.GetSliderValue("Config.Combo.Q.HitChance") * 33)
                    SpellManager.Q.Cast(prediction.CastPosition);
            }

            if (SpellManager.E.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.E.Status"))
            {
                var prediction = SpellManager.E.GetPrediction(target);
                if (!prediction.Collision && prediction.HitChancePercent >= Config.Combo.Menu.GetSliderValue("Config.Combo.E.HitChance") * 33)
                    SpellManager.E.Cast(prediction.CastPosition);
            }

            if (SpellManager.W.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.W.Status"))
            {
                if (target.Position.IsInRange(Player.Instance.Position, SpellManager.W.Range)) SpellManager.W.Cast();
            }

            if (SpellManager.R.IsReady() && Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Status"))
            {
                if (!Player.HasBuff("AhriTumble") && target.HealthPercent < Config.Combo.Menu.GetSliderValue("Config.Combo.R.Health")) return;

                if (Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Mouse"))
                {
                    if (target.HealthPercent >= 50 && Game.CursorPos.Extend(target, SpellManager.R.Range).To3D().IsUnderEnemyTurret() && !Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Dive")) return;
                    SpellManager.R.Cast(Game.CursorPos.Extend(target, SpellManager.R.Range).To3D());
                }
                else
                {
                    var RPos = GetBestRPos(target);
                    if ((target.HealthPercent >= 50 || Player.Instance.HealthPercent < 35) && RPos.IsUnderEnemyTurret() && !Config.Combo.Menu.GetCheckBoxValue("Config.Combo.R.Dive")) return;
                    SpellManager.R.Cast(Player.Instance.Position.Extend(RPos, SpellManager.R.Range).To3D());
                }
            }
        }

        static public Vector3 GetBestRPos(Obj_AI_Base target) // With catch Q
        {
            if (target == null) return new Vector3();
            if (!Player.Instance.IsInRange(target, SpellManager.E.Range)) return new Vector3();

            if (Program.QOrbMissile?.Name == "AhriOrbReturn")
            {
                var polygon = new Geometry.Polygon.Rectangle(Player.Instance.Position, Program.QOrbMissile.Position, SpellManager.E.Width);
                if (polygon.IsInside(target.Position)) return new Vector3();

                if (Player.Instance.Distance(Program.QOrbMissile.Position) <= Player.Instance.Distance(target))
                {
                    return GetBestRPosWithoutCatch(target);
                }
                else
                {
                    for (int i = 50; i < 600; i += 50)
                    {
                        var position = Player.Instance.Position.Extend(target, i);
                        polygon = new Geometry.Polygon.Rectangle(position.To3D(), Program.QOrbMissile.Position, SpellManager.E.Width);
                        if (polygon.IsInside(target.Position)) return position.To3D();
                    }
                    return GetBestRPosWithoutCatch(target);
                }
            }
            else
            {
                return GetBestRPosWithoutCatch(target);
            }
        }
        static public Vector3 GetBestRPosWithoutCatch(Obj_AI_Base target) // With catch Q
        {
            if (!Player.Instance.IsInRange(target, SpellManager.E.Range)) return new Vector3();

            for (int i = 450; i > 0; i -= 50)
            {
                for (int j = 50; j < 600; j += 50)
                {
                    foreach (var e in CircleCircleIntersection(Player.Instance.Position.To2D(), i, target.Position.To2D(), j))
                    {
                        if (!e.IsWall() && IsCollision(target.Position.To2D(), e.To2D(), SpellManager.E.Width)) return e;
                    }
                }
            }
            return new Vector3();
        }
        private static List<Vector3> CircleCircleIntersection(Vector2 circle0, float radius0, Vector2 circle1, float radius1)
        {
            float dx = circle0.X - circle1.X;
            float dy = circle0.Y - circle1.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            List<Vector3> intersections = new List<Vector3>();

            if (dist > radius0 + radius1 || dist < Math.Abs(radius0 - radius1) || (dist == 0) && (radius0 == radius1))
            {
                intersections.Add(Vector3.Zero);
                intersections.Add(Vector3.Zero);
            }
            else
            {
                double a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Math.Sqrt(radius0 * radius0 - a * a);

                double cx2 = circle0.X + a * (circle1.X - circle0.X) / dist;
                double cy2 = circle0.Y + a * (circle1.Y - circle0.Y) / dist;

                intersections.Add(new Vector2((float)(cx2 + h * (circle1.Y - circle0.Y) / dist), (float)(cy2 - h * (circle1.X - circle0.X) / dist)).To3D());
                intersections.Add(new Vector2((float)(cx2 - h * (circle1.Y - circle0.Y) / dist), (float)(cy2 + h * (circle1.X - circle0.X) / dist)).To3D());
            }
            return intersections;
        }
        static bool IsCollision(Vector2 start, Vector2 end, float width)
        {
            Geometry.Polygon.Rectangle r = new Geometry.Polygon.Rectangle(start, end, width);
            foreach (Obj_AI_Base aiBase in ObjectManager.Get<Obj_AI_Base>().Where(x => !x.IsAlly && x.IsValid && (x is AIHeroClient || x is Obj_AI_Minion)))
            {
                if (r.IsInside(aiBase.Position) || r.Points.Any(x => x.Distance(aiBase.Position) <= aiBase.BoundingRadius))
                    return true;
            }
            return false;
        }
    }

}
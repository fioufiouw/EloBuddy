using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using Geometry = EloBuddy.SDK.Geometry;

using SharpDX;
using SharpDX.Direct3D9;

using Color1 = SharpDX.Color;
using Color = System.Drawing.Color;

namespace Talon
{
    internal class Draw
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            var _ComboTarget = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);

            Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 0),
                Color.White, "Talon by Buddy Enabled", 4);

            if (Program.DrawMenu["DrawW"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._W.IsReady())
                {
                    new Circle() {BorderWidth = 3, Color = Color.LimeGreen, Radius = Program._W.Range}.Draw(
                        Program._Player.Position);
                }
                if (!Program._W.IsReady())
                {
                    new Circle() {BorderWidth = 3, Color = Color.Red, Radius = Program._W.Range}.Draw(
                        Program._Player.Position);
                }
            }

            if (Program.DrawMenu["DrawE"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._E.IsReady())
                {
                    new Circle() {BorderWidth = 3, Color = Color.LimeGreen, Radius = Program._E.Range}.Draw(
                        Program._Player.Position);
                }
                if (!Program._E.IsReady())
                {
                    new Circle() {BorderWidth = 3, Color = Color.Red, Radius = Program._E.Range}.Draw(
                        Program._Player.Position);
                }
            }

            if (Program.DrawMenu["DrawR"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._R.IsReady())
                {
                    new Circle() {BorderWidth = 3, Color = Color.LimeGreen, Radius = Program._R.Range}.Draw(
                        Program._Player.Position);
                }
                if (!Program._R.IsReady())
                {
                    new Circle() {BorderWidth = 3, Color = Color.Red, Radius = Program._R.Range}.Draw(
                        Program._Player.Position);
                }
            }

            if (_ComboTarget != null)
            {
                if (_ComboTarget.IsValidTarget())
                {
                    if (Program.DrawMenu["DrawComboLine"].Cast<CheckBox>().CurrentValue)
                    {
                        Drawing.DrawLine(ObjectManager.Player.Position.WorldToScreen(), _ComboTarget.Position.WorldToScreen(), 1, Color.White);
                    }
                    if (Program.DrawMenu["DrawComboCircle"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Damage.AvailableComboDamage() >= _ComboTarget.Health)
                            new Circle() {BorderWidth = 2, Color = Color.Lime, Radius = 35}.Draw(_ComboTarget.Position);

                        if (Damage.AvailableComboDamage() < _ComboTarget.Health)
                            new Circle() { BorderWidth = 2, Color = Color.Red, Radius = 35 }.Draw(_ComboTarget.Position);
                    }
                    if (Program.DrawMenu["DrawText"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Damage.ComboDamage() > _ComboTarget.Health)
                        {
                            Drawing.DrawText(Drawing.WorldToScreen(_ComboTarget.Position) - new Vector2(0, -1),
                                Color.Red, "Combo + Target = GG", 4);
                        }
                    }
                    if (Program._R.IsLearned)
                    {
                        if (Program.LogicMenu["DrawRCastCircle"].Cast<CheckBox>().CurrentValue)
                        {
                            new Circle()
                            {
                                BorderWidth = 1,
                                Color = Color.White,
                                Radius =
                                    Logic.RCastRange() +
                                    Program.LogicMenu["DrawRCastBufferRange"].Cast<Slider>().CurrentValue
                            }.Draw(
                                _ComboTarget.Position);
                        }
                    }
                }
            }
            if (Program.DrawMenu["DrawText"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._Q.IsReady() && Program._W.IsReady() && Program._E.IsReady() && Program._R.IsReady())
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 17), Color.Lime,
                        "Full Combo is Ready", 2);
                }

                else
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 17), Color.Red,
                        "Full Combo is NOT Ready", 2);
                }
            }
            if (Program.DrawMenu["DrawMana"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 34), Color.White,
                    Damage.ComboManaUsage().ToString(), 2);
            }
        }
    }
}
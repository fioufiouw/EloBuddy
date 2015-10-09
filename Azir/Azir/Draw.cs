using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Menu.Values;

using SharpDX;
using Color = System.Drawing.Color;

namespace Azir
{
    internal class Draw
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            var _ComboTarget = TargetSelector.GetTarget(1500, DamageType.Magical);
            
            if (_ComboTarget != null && _ComboTarget.IsValid)
            {
                if (_ComboTarget.Health <= Damage.GetComboDamage(_ComboTarget))
                {
                    new Circle() {BorderWidth = 1, Color = Color.Lime, Radius = _ComboTarget.BoundingRadius}.Draw(
                        _ComboTarget.Position);
                }
                else
                {
                    new Circle() {BorderWidth = 1, Color = Color.Red, Radius = _ComboTarget.BoundingRadius}.Draw(
                        _ComboTarget.Position);
                }
            }
            if (Orbwalker.ValidAzirSoldiers.Count > 0)
            {
                if (Program.DrawMenu["dDrawQ"].Cast<CheckBox>().CurrentValue)
                {
                    if (Program._Q.IsReady())
                    {
                        new Circle() {BorderWidth = 1, Color = Color.Lime, Radius = Program._Q.Range}.Draw(
                            Program._Player.Position);
                    }
                    else
                    {
                        new Circle() {BorderWidth = 1, Color = Color.Red, Radius = Program._Q.Range}.Draw(
                            Program._Player.Position);
                    }
                }
                foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                {
                    new Circle() {BorderWidth = 1, Color = Color.FromArgb(150, Color.Yellow), Radius = Orbwalker.AzirSoldierAutoAttackRange}.Draw(
                        soldier.Position);
                }
            }
            if (Program.DrawMenu["dDrawW"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._W.IsReady())
                {
                    new Circle() {BorderWidth = 1, Color = Color.Lime, Radius = Program._W.Range}.Draw(
                        Program._Player.Position);
                }
                else
                {
                    new Circle() {BorderWidth = 1, Color = Color.Red, Radius = Program._W.Range}.Draw(
                        Program._Player.Position);
                }
            }

            if (Orbwalker.ValidAzirSoldiers.Count > 0)
            {
                if (Program.DrawMenu["dDrawE"].Cast<CheckBox>().CurrentValue)
                {
                    if (Program._Q.IsReady())
                    {
                        new Circle() {BorderWidth = 1, Color = Color.Lime, Radius = Program._E.Range}.Draw(
                            Program._Player.Position);
                    }
                    else
                    {
                        new Circle() {BorderWidth = 1, Color = Color.Red, Radius = Program._E.Range}.Draw(
                            Program._Player.Position);
                    }
                }
            }
            if (Program.DrawMenu["dDrawR"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._Q.IsReady())
                {
                    new Circle() {BorderWidth = 1, Color = Color.Lime, Radius = Program._R.Range + 25}.Draw(
                        Program._Player.Position);
                }
                else
                {
                    new Circle() {BorderWidth = 1, Color = Color.Red, Radius = Program._R.Range + 25}.Draw(
                        Program._Player.Position);
                }
            }
            if (_ComboTarget != null && _ComboTarget.IsValid)
            {
                if (Program.DrawMenu["dDrawLines"].Cast<CheckBox>().CurrentValue)
                {
                    Drawing.DrawLine(ObjectManager.Player.Position.WorldToScreen(),
                        _ComboTarget.Position.WorldToScreen(), 1,
                        Color.White);

                    foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                    {
                        Drawing.DrawLine(soldier.Position.WorldToScreen(),
                            _ComboTarget.Position.WorldToScreen(), 1,
                            Color.FromArgb(150, System.Drawing.Color.Yellow));
                    }
                }
            }
            if (Program.DrawMenu["dDrawWRange"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                {
                    if (soldier != null && soldier.IsValid)
                    {
                        new Circle() {BorderWidth = 1, Color = Color.FromArgb(150, System.Drawing.Color.Yellow), Radius = 825}.Draw(soldier.Position);
                    }
                }
            }
            if (Program.DrawMenu["dDrawWCommandRange"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                {
                    if (soldier != null && soldier.IsValid)
                    {
                        new Circle() {BorderWidth = 1, Color = Color.FromArgb(150, System.Drawing.Color.Yellow), Radius = 800}.Draw(soldier.Position);
                    }
                }
            }
            if (Program.DrawMenu["dDrawText"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._Q.IsReady() && Program._W.IsReady() && Program._E.IsReady() && Program._R.IsReady())
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 0), Color.White, "Full Combo Is Ready", 2);
                }
                else
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 0), Color.White, "Full Combo Is NOT Ready", 2);
                }
                if (Program.DrawMenu["dDrawManaUsage"].Cast<CheckBox>().CurrentValue)
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(0, 10), Color.LightBlue, Mana.GetComboMana().ToString(), 2);
                }
            }
        }
    }
}

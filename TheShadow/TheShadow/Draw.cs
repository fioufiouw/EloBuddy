using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using TheShadow;
using Color = System.Drawing.Color;

namespace TheShadow
{
    internal class Draw
    {
        public static void OnDraw(EventArgs args)
        {

            var target = TargetSelector.GetTarget(MainShadow._R.Range + 350, DamageType.Physical);

            if (MainShadow.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue == true)
            {
                if (MainShadow._Q.IsReady())
                    new Circle() {Color = Color.Lime, Radius = MainShadow._Q.Range}.Draw(ObjectManager.Player.Position);

                if (MainShadow._Q.IsOnCooldown)
                    new Circle() {Color = Color.Orange, Radius = MainShadow._Q.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._Q.IsLearned)
                    new Circle() {Color = Color.Red, Radius = MainShadow._Q.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._Q.IsReady() && !MainShadow._Q.IsOnCooldown && MainShadow._Q.IsLearned)
                    new Circle() {Color = Color.Purple, Radius = MainShadow._Q.Range}.Draw(ObjectManager.Player.Position);
            }

            if (MainShadow.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue == true)
            {
                if (MainShadow._W.IsReady())
                    new Circle() {Color = Color.Lime, Radius = MainShadow._W.Range}.Draw(ObjectManager.Player.Position);

                if (MainShadow._W.IsOnCooldown)
                    new Circle() {Color = Color.Orange, Radius = MainShadow._W.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._W.IsLearned)
                    new Circle() {Color = Color.Red, Radius = MainShadow._W.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._W.IsReady() && !MainShadow._W.IsOnCooldown && MainShadow._W.IsLearned)
                    new Circle() {Color = Color.Purple, Radius = MainShadow._W.Range}.Draw(ObjectManager.Player.Position);
            }

            if (MainShadow.DrawMenu["drawE"].Cast<CheckBox>().CurrentValue == true)
            {
                if (MainShadow._E.IsReady())
                    new Circle() {Color = Color.Lime, Radius = MainShadow._E.Range}.Draw(ObjectManager.Player.Position);

                if (MainShadow._E.IsOnCooldown)
                    new Circle() {Color = Color.Orange, Radius = MainShadow._E.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._E.IsLearned)
                    new Circle() {Color = Color.Red, Radius = MainShadow._E.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._E.IsReady() && !MainShadow._E.IsOnCooldown && MainShadow._E.IsLearned)
                    new Circle() {Color = Color.Purple, Radius = MainShadow._E.Range}.Draw(ObjectManager.Player.Position);
            }

            if (MainShadow.DrawMenu["drawR"].Cast<CheckBox>().CurrentValue == true)
            {
                if (MainShadow._R.IsReady())
                    new Circle() {Color = Color.Lime, Radius = MainShadow._R.Range}.Draw(ObjectManager.Player.Position);

                if (MainShadow._R.IsOnCooldown)
                    new Circle() {Color = Color.Orange, Radius = MainShadow._R.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._R.IsLearned)
                    new Circle() {Color = Color.Red, Radius = MainShadow._R.Range}.Draw(ObjectManager.Player.Position);

                if (!MainShadow._R.IsReady() && !MainShadow._R.IsOnCooldown && MainShadow._R.IsLearned)
                    new Circle() {Color = Color.Purple, Radius = MainShadow._R.Range}.Draw(ObjectManager.Player.Position);
            }

            if (MainShadow.DrawMenu["drawFlash"].Cast<CheckBox>().CurrentValue == true)
                new Circle() {Color = Color.Yellow, Radius = 425}.Draw(ObjectManager.Player.Position);

            if (MainShadow.DrawMenu["drawText"].Cast<CheckBox>().CurrentValue == true)
            {
                if (MainShadow._Q.IsReady() && MainShadow._W.IsReady() && MainShadow._E.IsReady() &&
                    MainShadow._R.IsReady())
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -30),
                        Color.GhostWhite, "Full Combo is Ready", 2);
                }
                else
                {
                    Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, -30),
                        Color.GhostWhite, "Full Combo is NOT Ready", 2);
                }
                if (target.IsValid && !target.IsDead && !target.IsZombie)
                {
                    if (DamageCalc.dmgCalc() > target.Health)
                    {
                        Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(30, 100),
                            Color.GhostWhite, "Target + Combo = RIP", 2);
                    }
                }
            }
        }
    }
}
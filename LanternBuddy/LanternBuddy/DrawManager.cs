using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace LanternBuddy
{
    internal class DrawManager
    {
        public static void Init()
        {
            Drawing.OnDraw += DrawingOnOnDraw;
        }

        private static void DrawingOnOnDraw(EventArgs args)
        {

            if (Program.PrioritizedAlly == null || !Program.W.IsReady())
                return;
            if (!Config.Menu["enable"].Cast<KeyBind>().CurrentValue)
                return;
            if (!Config.Menu["use"].Cast<KeyBind>().CurrentValue)
                return;
            if (Config.Menu["minmana"].Cast<Slider>().CurrentValue > Player.Instance.ManaPercent)
                return;
            if (Config.HumanizerMenu["castOnScreen"].Cast<CheckBox>().CurrentValue && !Program.PrioritizedAlly.IsHPBarRendered)
                return;

            var lineColor = Config.GetColor(Config.DrawMenu["lineA"].Cast<Slider>().CurrentValue,
                Config.DrawMenu["lineR"].Cast<Slider>().CurrentValue,
                Config.DrawMenu["lineG"].Cast<Slider>().CurrentValue,
                Config.DrawMenu["lineB"].Cast<Slider>().CurrentValue);
            var circleColor = Config.GetColor(Config.DrawMenu["circleA"].Cast<Slider>().CurrentValue,
                Config.DrawMenu["circleR"].Cast<Slider>().CurrentValue,
                Config.DrawMenu["circleG"].Cast<Slider>().CurrentValue,
                Config.DrawMenu["circleB"].Cast<Slider>().CurrentValue);

            if (Config.DrawMenu["drawLine"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.DrawLine(Drawing.ScreenToWorld(Player.Instance.ServerPosition.To2D()).To2D(), Drawing.ScreenToWorld(Program.PrioritizedAlly.ServerPosition.To2D()).To2D(), 4, lineColor);

                Drawing.DrawLine(Drawing.ScreenToWorld(Game.CursorPos2D).To2D(), Drawing.ScreenToWorld(Program.PrioritizedAlly.ServerPosition.To2D()).To2D(), 4, lineColor);
            }
            if (Config.DrawMenu["drawTargetCircle"].Cast<CheckBox>().CurrentValue)
            {
                new Circle
                {
                    BorderWidth = 5,
                    Color = circleColor,
                    Radius = Program.PrioritizedAlly.BoundingRadius + 25
                }.Draw(Program.PrioritizedAlly.ServerPosition);
            }
            if (Config.DrawMenu["drawCheckDist"].Cast<CheckBox>().CurrentValue)
            {
                new Circle
                {
                    BorderWidth = 1,
                    Color = circleColor,
                    Radius = Config.Menu["checkDist"].Cast<Slider>().CurrentValue
                }.Draw(Player.Instance.ServerPosition);
            }
        }
    }
}
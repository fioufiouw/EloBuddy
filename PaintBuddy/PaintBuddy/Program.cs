using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;

namespace PaintBuddy
{
    internal class Program
    {
        public static List<Paint> Paints = new List<Paint>();

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Config.Generate.GenerateMenu();

            Game.OnUpdate += GameOnOnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            foreach (var paint in Paints)
            {
                if (!paint.IsGlow)
                {
                    new Circle
                    {
                        BorderWidth = 2,
                        Color = paint.Color,
                        Radius = paint.Radius
                    }.Draw(paint.Location);
                }
                else
                {
                    Drawing.DrawCircle(paint.Location, paint.Radius, paint.Color);
                }
            }
        }

        private static void GameOnOnUpdate(EventArgs args)
        {
            if (Config.Get.Remove)
                Paints.Clear();

            if (Config.Get.Remove)
                return;

            if (Config.Get.Draw)
            {
                Add();
            }
        }

        private static void Add()
        {
            Paints.Add(new Paint
            {
                Color = Config.Get.Color,
                Location = Game.CursorPos,
                Radius = Config.Get.Radius,
                IsGlow = Config.Get.IsGlow
            });
        }
    }
}
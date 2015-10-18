using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
using EloBuddy;

using SharpDX;

namespace KataBuddy.Drawings
{
    class Text
    {
        private static AIHeroClient playerClient = ObjectManager.Player;
        public static void DrawStartUp(bool value)
        {
            Drawing.DrawText(Drawing.WorldToScreen(playerClient.Position) - new Vector2(0, 0), Color.White, "Buddy Series Katarina Acitve", 1);
        }

        public static void DrawDamage(bool value)
        {
            var target = "soontmgettargetm8";
            //Drawing.DrawText(Drawing.WorldToScreen(playerClient.Position) - new Vector2(0, 0), Color.White, "Buddy Series Katarina Acitve", 1); targetposnotimplement rip
        }
    }
}

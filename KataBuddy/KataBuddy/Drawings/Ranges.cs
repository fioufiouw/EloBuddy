using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using Color = System.Drawing.Color;

namespace KataBuddy.Drawings
{
    class Ranges
    {
        private static AIHeroClient playerClient = ObjectManager.Player;
        public static void DrawQ(bool value)
        {
            if (value == true)
            {
                if (Spells.Q.IsReady())
                {
                    new Circle() {BorderWidth = 2, Color = Color.LimeGreen, Radius = Spells.Q.Range}.Draw(
                        playerClient.Position);
                }
                else
                {
                    new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.Q.Range }.Draw(
                        playerClient.Position);
                }
            }
        }

        public static void DrawW(bool value)
        {
            if (value == true)
            {
                if (Spells.W.IsReady())
                {
                    new Circle() { BorderWidth = 2, Color = Color.LimeGreen, Radius = Spells.W.Range }.Draw(
                        playerClient.Position);
                }
                else
                {
                    new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.W.Range }.Draw(
                        playerClient.Position);
                }
            }
        }

        public static void DrawE(bool value)
        {
            if (value == true)
            {
                if (Spells.E.IsReady())
                {
                    new Circle() { BorderWidth = 2, Color = Color.LimeGreen, Radius = Spells.E.Range }.Draw(
                        playerClient.Position);
                }
                else
                {
                    new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.E.Range }.Draw(
                        playerClient.Position);
                }
            }
        }
        public static void DrawR(bool value)
        {
            if (value == true)
            {
                if (Spells.R.IsReady())
                {
                    new Circle() { BorderWidth = 2, Color = Color.LimeGreen, Radius = Spells.R.Range }.Draw(
                        playerClient.Position);
                }
                else
                {
                    new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.R.Range }.Draw(
                        playerClient.Position);
                }
            }
        }
    }
}

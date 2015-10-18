using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu.Values;

namespace KataBuddy.Utils
{
    class DrawingManager
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            DrawRanges();
            DrawText();
            DrawLines();
            DrawMisc();
        }

        private static void DrawRanges()
        {
            if (Drawings._Menu.DrawMenu["dDrawQ"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Ranges.DrawQ(true);
            }
            else
            {
                Drawings.Ranges.DrawQ(false);
            }


            if (Drawings._Menu.DrawMenu["dDrawW"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Ranges.DrawW(true);
            }
            else
            {
                Drawings.Ranges.DrawW(false);
            }


            if (Drawings._Menu.DrawMenu["dDrawE"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Ranges.DrawE(true);
            }
            else
            {
                Drawings.Ranges.DrawE(false);
            }


            if (Drawings._Menu.DrawMenu["dDrawR"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Ranges.DrawR(true);
            }
            else
            {
                Drawings.Ranges.DrawR(false);
            }
        }

        private static void DrawText()
        {
            if (Drawings._Menu.DrawMenu["dDrawText"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Text.DrawStartUp(true);
                //Drawings.Text.DrawDamage(true);
            }
            else
            {
                Drawings.Text.DrawStartUp(true);
                //Drawings.Text.DrawDamage(true);
            }
        }

        private static void DrawLines()
        {
            if (Drawings._Menu.DrawMenu["dDrawLines"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Lines.DrawTargetLine(true);
            }
            else
            {
                Drawings.Lines.DrawTargetLine(false);
            }
            //Drawings.Lines.DrawPredictedBounce(true);
        }

        private static void DrawMisc()
        {
            if (Drawings._Menu.DrawMenu["dDrawTarget"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Misc.DrawComboTargetCircle(true);
            }
            if (!Drawings._Menu.DrawMenu["dDrawTarget"].Cast<CheckBox>().CurrentValue)
            {
                Drawings.Misc.DrawComboTargetCircle(false);
            }
        }
    }
}

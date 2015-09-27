using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

using SharpDX;
using SharpDX.Direct3D9;

using Color1 = SharpDX.Color;
using Color = System.Drawing.Color;
namespace Talon
{
    class Draw
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (Program.DrawMenu["DrawW"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._W.IsReady())
                {
                    new Circle() { BorderWidth = 3, Color = Color.LimeGreen, Radius = Program._W.Range }.Draw(Program._Player.Position);
                }
                if (!Program._W.IsReady())
                {
                    new Circle() { BorderWidth = 3, Color = Color.Red, Radius = Program._W.Range }.Draw(Program._Player.Position);
                }
            }

            if (Program.DrawMenu["DrawE"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._E.IsReady())
                {
                    new Circle() { BorderWidth = 3, Color = Color.LimeGreen, Radius = Program._E.Range }.Draw(Program._Player.Position);
                }
                if (!Program._E.IsReady())
                {
                    new Circle() { BorderWidth = 3, Color = Color.Red, Radius = Program._E.Range }.Draw(Program._Player.Position);
                }
            }

            if (Program.DrawMenu["DrawR"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._R.IsReady())
                {
                    new Circle() { BorderWidth = 3, Color = Color.LimeGreen, Radius = Program._R.Range }.Draw(Program._Player.Position);
                }
                if (!Program._R.IsReady())
                {
                    new Circle() { BorderWidth = 3, Color = Color.Red, Radius = Program._R.Range }.Draw(Program._Player.Position);
                }
            }

        }
    }
}

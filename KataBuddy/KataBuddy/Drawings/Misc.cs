using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;

using Color = System.Drawing.Color;
namespace KataBuddy.Drawings
{
    class Misc
    {
        public static void DrawComboTargetCircle(bool value)
        {
            var ComboTarget = TargetSelector.GetTarget(1250, DamageType.Magical);

            if (value == true)
            {
                if (ComboTarget != null && ComboTarget.IsValid)
                {
                    new Circle()
                    {
                        BorderWidth = 1,
                        Color = Color.LimeGreen,
                        Radius = ComboTarget.BoundingRadius
                    }.Draw(
                        ComboTarget.Position);
                }
            }
        }
    }
}

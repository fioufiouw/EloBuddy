using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace KataBuddy.States
{
    class Harass
    {
        public static void QInit()
        {
            var Target = TargetSelector.GetTarget(1000, DamageType.Magical);

            if (Target != null && Target.IsValid)
            {
                if (Spells.Q.IsReady() && Spells.Q.IsInRange(Target))
                {
                    Spells.Q.Cast(Target);
                }
            }
        }
        public static void WInit()
        {
            var Target = TargetSelector.GetTarget(1000, DamageType.Magical);

            if (Target != null && Target.IsValid)
            {
                if (Spells.W.IsReady() && Spells.W.IsInRange(Target))
                {
                    Spells.W.Cast(Target);
                }
            }
        }
    }
}

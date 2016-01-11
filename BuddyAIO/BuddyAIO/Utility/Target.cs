using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;
namespace BuddyAIO.Utility
{
    class Target
    {
        private static AIHeroClient target(float range, DamageType? type)
        {
            var damagetype = type ?? default(DamageType);
            return TargetSelector.GetTarget(range, damagetype);
        }
        public static AIHeroClient GetTarget(Orbwalker.ActiveModes? Mode = null, DamageType? type = null)
        {
            switch (Mode)
            {
                default:
                    return GetTargetByType(1000, null);
                case Orbwalker.ActiveModes.Combo:
                    return GetTargetByType(1200, type);
                case Orbwalker.ActiveModes.Flee:
                    return GetTargetByType(650, type);
                case Orbwalker.ActiveModes.Harass:
                    return GetTargetByType(900, type);
                case Orbwalker.ActiveModes.JungleClear:
                    return GetTargetByType(750, type);
                case Orbwalker.ActiveModes.LaneClear:
                    return GetTargetByType(750, type);
                case Orbwalker.ActiveModes.LastHit:
                    return GetTargetByType(700, type);
            }
        }
        private static AIHeroClient GetTargetByType(float range, DamageType? type)
        {
            if (type == null)
            {
                return target(range, DamageType.Mixed);
            }
            return target(range, type);
        }
    }
}

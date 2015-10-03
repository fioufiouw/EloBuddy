using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Talon
{
    class Logic
    {
        public static float RCastRange()
        {
            var _ComboTarget = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);

            float _MoveSpeed = ((Program._Player.MoveSpeed * 25) / 10) - (_ComboTarget.MoveSpeed / 2);

            return _MoveSpeed;
        }
    }
}

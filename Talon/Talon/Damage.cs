using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;

namespace Talon
{
    class Damage
    {
        public static float WDamage()
        {
            float CurrentAD = (60f / 100f) * Program._Player.TotalAttackDamage;
            float WBaseDamage = 0;
            if (!Program._W.IsLearned)
                CurrentAD = 0;

            if (Program._W.Level == 1)
                WBaseDamage = 30;

            if (Program._W.Level == 2)
                WBaseDamage = 55;

            if (Program._W.Level == 3)
                WBaseDamage = 80;

            if (Program._W.Level == 4)
                WBaseDamage = 105;

            if (Program._W.Level == 5)
                WBaseDamage = 130;

            return WBaseDamage + CurrentAD;
        }

        public static float QDamage()
        {
            float CurrentAD = (30f / 100f) * Program._Player.TotalAttackDamage;
            float TotalAD = Program._Player.TotalAttackDamage;
            float QBaseDamage = 0;

            if (!Program._Q.IsLearned)
                CurrentAD = 0;

            if (Program._Q.Level == 1)
                QBaseDamage = 30;

            if (Program._Q.Level == 2)
                QBaseDamage = 60;

            if (Program._Q.Level == 3)
                QBaseDamage = 90;

            if (Program._Q.Level == 4)
                QBaseDamage = 120;

            if (Program._Q.Level == 5)
                QBaseDamage = 150;

            return QBaseDamage + CurrentAD + TotalAD;
        }
    }
}

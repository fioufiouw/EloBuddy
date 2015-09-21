using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Rendering;
using SharpDX;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Enumerations;
using Color = System.Drawing.Color;

namespace TheShadow
{
    public class DamageCalc
    {
        static public float dmgCalc()
        {
            float QDmgCalc = 0;
            float EDmgCalc = 0;

            float zedAD = ObjectManager.Player.TotalAttackDamage;
            int WLvl = MainShadow._W.Level;
            if (WLvl == 1)
                zedAD = (5 / 100) * ObjectManager.Player.TotalAttackDamage;

            if (WLvl == 2)
                zedAD = (10 / 100) * ObjectManager.Player.TotalAttackDamage;

            if (WLvl == 3)
                zedAD = (15 / 100) * ObjectManager.Player.TotalAttackDamage;

            if (WLvl == 4)
                zedAD = (20 / 100) * ObjectManager.Player.TotalAttackDamage;

            if (WLvl == 5)
                zedAD = (25 / 100) * ObjectManager.Player.TotalAttackDamage;


            int QLvl = MainShadow._Q.Level;
            if (QLvl == 1)
                QDmgCalc = 75 + zedAD;

            if (QLvl == 2)
                QDmgCalc = 115 + zedAD;

            if (QLvl == 3)
                QDmgCalc = 155 + zedAD;

            if (QLvl == 4)
                QDmgCalc = 195 + zedAD;

            if (QLvl == 5)
                QDmgCalc = 235 + zedAD;


            int ELvl = MainShadow._E.Level;
            if (ELvl == 1)
                EDmgCalc = 60 + (80 / 100) * zedAD;

            if (ELvl == 2)
                EDmgCalc = 90 + (80 / 100) * zedAD;

            if (ELvl == 3)
                EDmgCalc = 120 + (80 / 100) * zedAD;

            if (ELvl == 4)
                EDmgCalc = 150 + (80 / 100) * zedAD;

            if (ELvl == 5)
                EDmgCalc = 180 + (80 / 100) * zedAD;

            float WDmgCalc = 0;
            float RDmgCalc = zedAD;
            int RLvl = MainShadow._R.Level;
            if (RLvl == 1)
                RDmgCalc = RDmgCalc + (20 / 100) * (RDmgCalc + QDmgCalc + EDmgCalc + WDmgCalc);

            if (RLvl == 2)
                RDmgCalc = RDmgCalc + (35 / 100) * (RDmgCalc + QDmgCalc + EDmgCalc + WDmgCalc);

            if (RLvl == 3)
                RDmgCalc = RDmgCalc + (50 / 100) * (RDmgCalc + QDmgCalc + EDmgCalc + WDmgCalc);


            float totalDamageDeal;

            WDmgCalc = QDmgCalc + EDmgCalc;
            totalDamageDeal = QDmgCalc + EDmgCalc + RDmgCalc + WDmgCalc;
            return totalDamageDeal;
        }
    }
}
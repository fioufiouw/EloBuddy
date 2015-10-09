using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Azir
{
    class Mana
    {
        public static float GetComboMana()
        {
            var QMana = 0f;
            var WMana = 0f;
            var EMana = 0f;
            var RMana = 0f;

            if (Program._Q.IsReady())
            {
                QMana = 70;
            }
            if (Program._W.IsReady())
            {
                WMana = 40;
            }
            if (Program._E.IsReady())
            {
                EMana = 60;
            }
            if (Program._R.IsReady())
            {
                RMana = 100;
            }
            return QMana + WMana + EMana + RMana;
        }

        public static void ManaManager()
        {
            if (Program._Player.ManaPercent <= Program.ManaMenu["mUsePot"].Cast<Slider>().CurrentValue &&
                Program.Menu["mUseItems"].Cast<CheckBox>().CurrentValue)
            {
                if (!Program._Player.HasBuff("FlaskOfCrystalWater"))
                {
                    if (Program._ManaPot.IsOwned() && Program._ManaPot.IsReady())
                    {
                        Program._ManaPot.Cast();
                    }
                }
            }
        }
    }
}

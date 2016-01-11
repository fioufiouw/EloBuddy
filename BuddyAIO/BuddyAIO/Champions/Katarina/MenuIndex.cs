using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using BuddyAIO.Utility.Config;

namespace BuddyAIO.Champions.Katarina
{
    class MenuIndex
    {
        public class NonMenu
        {
            private Modules.Misc.UltManager ultmanager = new Modules.Misc.UltManager();

            public bool IsUlting
            {
                get { return ultmanager.IsUlting; }
            }
        }
        public class Harass
        {
            Modules.States.Harass harass = new Modules.States.Harass();

            public bool UseQ
            {
                get { return harass.mMenu.GetCheckBoxValue("useq"); }
            }
            public bool UseW
            {
                get { return harass.mMenu.GetCheckBoxValue("usew"); }
            }
            public bool UseE
            {
                get { return harass.mMenu.GetCheckBoxValue("usee"); }
            }
        }
        public class Combo
        {
            private Modules.States.Combo combo = new Modules.States.Combo();

            public int ComboMode
            {
                get { return combo.mMenu.GetSliderValue("combomode"); }
            }
            public bool UseQ
            {
                get { return combo.mMenu.GetCheckBoxValue("useq"); }
            }
            public bool UseW
            {
                get { return combo.mMenu.GetCheckBoxValue("usew"); }
            }
            public bool UseE
            {
                get { return combo.mMenu.GetCheckBoxValue("usee"); }
            }
            public bool UseR
            {
                get { return combo.mMenu.GetCheckBoxValue("user"); }
            }
        }
        public class Flee
        {
            private Modules.Misc.WardJump wardjump = new Modules.Misc.WardJump();

            public bool UseE
            {
                get { return wardjump.mMenu.GetCheckBoxValue("usee"); }
            }
            public bool WardJump
            {
                get { return wardjump.mMenu.GetCheckBoxValue("wardjump"); }
            }
        }
        public class LastHit
        {
            private Modules.States.LastHit lasthit = new Modules.States.LastHit();

            public bool SmartFarm
            {
                get { return lasthit.mMenu.GetCheckBoxValue("smartfarm"); }
            }
            public bool UseQ
            {
                get { return lasthit.mMenu.GetCheckBoxValue("useq"); }
            }
            public bool UseW
            {
                get { return lasthit.mMenu.GetCheckBoxValue("usew"); }
            }
            public bool UseE
            {
                get { return lasthit.mMenu.GetCheckBoxValue("usee"); }
            }
        }
        public class LaneClear
        {
            private Modules.States.LaneClear laneclear = new Modules.States.LaneClear();

            public bool DangerLow
            {
                get { return laneclear.mMenu.GetCheckBoxValue("dangerlow"); }
            }
            public bool UseQ
            {
                get { return laneclear.mMenu.GetCheckBoxValue("useq"); }
            }
            public bool UseW
            {
                get { return laneclear.mMenu.GetCheckBoxValue("usew"); }
            }
            public bool UseE
            {
                get { return laneclear.mMenu.GetCheckBoxValue("usee"); }
            }
        }
        public class Misc
        {
            private Modules.Misc.AutoW autow = new Modules.Misc.AutoW();

            public bool AutoW
            {
                get { return autow.mMenu.GetCheckBoxValue("autow"); }
            }
        }
        public class Drawings
        {
            private Modules.Misc.DrawSpells drawspells = new Modules.Misc.DrawSpells();
            public bool DrawQ
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawq"); }
            }
            public bool DrawW
            {
                get { return drawspells.mMenu.GetCheckBoxValue("draww"); }
            }
            public bool DrawE
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawe"); }
            }
            public bool DrawR
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawr"); }
            }
            public bool DrawFlash
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawflash"); }
            }
            public bool DrawIgnite
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawignite"); }
            }
            public bool DrawPredictedQ
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drwapredictedq"); }
            }
            public bool DrawDamageIndicator
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawdamageindicator"); }
            }
            public bool DrawDamageIndicatorPercentage
            {
                get { return drawspells.mMenu.GetCheckBoxValue("drawdamageindicatorpercentage"); }
            }
        }
    }
}

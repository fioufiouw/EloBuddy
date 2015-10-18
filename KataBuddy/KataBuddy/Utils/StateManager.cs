using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace KataBuddy.Utils
{
    class StateManager
    {
        public static void GetState()
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                States.Combo.Init();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                if (Drawings._Menu.HarassMenu["hUseQ"].Cast<CheckBox>().CurrentValue)
                {
                    States.Harass.QInit();
                }
                if (Drawings._Menu.HarassMenu["hUseW"].Cast<CheckBox>().CurrentValue)
                {
                    States.Harass.WInit();
                }
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (Drawings._Menu.FarmMenu["fLcUseQ"].Cast<CheckBox>().CurrentValue)
                {
                    States.LaneClear.QInit();
                }
                if (Drawings._Menu.FarmMenu["fLcUseW"].Cast<CheckBox>().CurrentValue)
                {
                    States.LaneClear.WInit();
                }
                if (Drawings._Menu.FarmMenu["fLcUseE"].Cast<CheckBox>().CurrentValue)
                {
                    States.LaneClear.EInit();
                }
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                if (Drawings._Menu.FarmMenu["fLhUseQ"].Cast<CheckBox>().CurrentValue)
                {
                    States.LastHit.QInit();
                }
                if (Drawings._Menu.FarmMenu["fLhUseW"].Cast<CheckBox>().CurrentValue)
                {
                    States.LastHit.WInit();
                }
                if (Drawings._Menu.FarmMenu["fLhUseE"].Cast<CheckBox>().CurrentValue)
                {
                    States.LastHit.EInit();
                }
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                if (Drawings._Menu.FleeMenu["flMinionJump"].Cast<CheckBox>().CurrentValue)
                {
                    States.Flee.MinionJumpInit();
                }
            }
        }
    }
}

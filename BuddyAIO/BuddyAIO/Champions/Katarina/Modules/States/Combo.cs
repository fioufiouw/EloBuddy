using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;

namespace BuddyAIO.Champions.Katarina.Modules.States
{
    class Combo : IModule
    {
        private static AIHeroClient Target
        {
            get { return Utility.Target.GetTarget(Orbwalker.ActiveModes.Combo, DamageType.Magical); }
        }
        public EloBuddy.SDK.Menu.Menu mMenu;
        public override void MenuCreate()
        {
            mMenu = Menu.AddSubMenu("Combo", "combo");
            mMenu.AddStringList("combomode", "Mode: ", new string[3] { "QEWR", "EQWR", "ERWQ"}, 1);
            mMenu.AddCheckBox("useq", "Use Q");
            mMenu.AddCheckBox("usew", "Use W");
            mMenu.AddCheckBox("usee", "Use E", false);
        }
        public override void OnLoad()
        {

        }
        public override ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
        public new bool ShouldDo()
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                return true;
            return false;
        }
        public override void Do()
        {
            MenuIndex.Combo combo = new MenuIndex.Combo();

            if (!Target.IsValid || Target == null)
                return;

            switch (combo.ComboMode)
            {
                #region Case 1
                case 1:
                    if (Spells.Q.IsReady() && Spells.Q.IsInRange(Target) && combo.UseQ)
                    {
                        Spells.Q.Cast(Target);
                    }
                    if (Spells.E.IsReady() && Spells.E.IsInRange(Target) && combo.UseE)
                    {
                        Spells.E.Cast(Target);
                    }
                    if (Spells.W.IsReady() && Spells.W.IsInRange(Target) && combo.UseW)
                    {
                        Spells.W.Cast(Target);
                    }
                    if (Spells.R.IsReady() && Spells.R.IsInRange(Target) && combo.UseR)
                    {
                        Spells.R.Cast(Target);
                    }
                    break;
                #endregion
                #region Case 2
                case 2:
                    if (Spells.E.IsReady() && Spells.E.IsInRange(Target) && combo.UseE)
                    {
                        Spells.E.Cast(Target);
                    }
                    if (Spells.Q.IsReady() && Spells.Q.IsInRange(Target) && combo.UseQ)
                    {
                        Spells.Q.Cast(Target);
                    }
                    if (Spells.W.IsReady() && Spells.W.IsInRange(Target) && combo.UseW)
                    {
                        Spells.W.Cast(Target);
                    }
                    if (Spells.R.IsReady() && Spells.R.IsInRange(Target) && combo.UseR)
                    {
                        Spells.R.Cast(Target);
                    }
                    break;
                #endregion
                #region Case 3
                case 3:
                    if (Spells.E.IsReady() && Spells.E.IsInRange(Target) && combo.UseE)
                    {
                        Spells.E.Cast(Target);
                    }
                    if (Spells.R.IsReady() && Spells.R.IsInRange(Target) && combo.UseR)
                    {
                        Spells.R.Cast(Target);
                    }
                    if (Spells.W.IsReady() && Spells.W.IsInRange(Target) && combo.UseW)
                    {
                        Core.DelayAction(() => Spells.W.Cast(Target), 250);
                    }
                    if (Spells.Q.IsReady() && Spells.Q.IsInRange(Target) && combo.UseQ)
                    {
                        Spells.Q.Cast(Target);
                    }
                    break;
                    #endregion
            }
        }
    }
}

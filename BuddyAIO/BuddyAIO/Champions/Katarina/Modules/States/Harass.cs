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
    class Harass : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        private static AIHeroClient Target
        {
            get { return Utility.Target.GetTarget(Orbwalker.ActiveModes.Harass, DamageType.Magical); }
        }
        void IModule.MenuCreate()
        {
            mMenu = Menu.AddSubMenu("Harass", "harass");
            mMenu.AddCheckBox("useq", "Use Q");
            mMenu.AddCheckBox("usew", "Use W");
            mMenu.AddCheckBox("usee", "Use E", false);
        }

        void IModule.OnLoad()
        {

        }
        bool IModule.ShouldDo()
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
                return true;
            return false;
        }
        void IModule.Do()
        {
            MenuIndex.Harass harass = new MenuIndex.Harass();

            if (!Target.IsValid || Target == null)
                return;

            if (Spells.Q.IsReady() && Spells.Q.IsInRange(Target) && harass.UseQ)
            {
                Spells.Q.Cast(Target);
            }
            if (Spells.W.IsReady() && Spells.W.IsInRange(Target) && harass.UseW)
            {
                Spells.W.Cast(Target);
            }
            if (Spells.E.IsReady() && Spells.E.IsInRange(Target) && harass.UseE)
            {
                Spells.E.Cast(Target);
            }
        }

        ModuleType IModule.GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
    }
}

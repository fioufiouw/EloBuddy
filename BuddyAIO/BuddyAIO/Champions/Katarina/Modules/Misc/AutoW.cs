using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class AutoW : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        private bool Wardjump;
        public override void MenuCreate()
        {
            mMenu = Menu.AddSubMenu("Misc", "misc");
            mMenu.AddCheckBox("autow", "Auto W");
        }
        public override ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
        public new bool ShouldDo()
        {
            MenuIndex.Misc misc = new MenuIndex.Misc();
            var x = misc.AutoW;

            if (x)
                return true;
            return false;
        }

        public override void Do()
        {
            MenuIndex.NonMenu nonmenu = new MenuIndex.NonMenu();
            foreach (var hero in EntityManager.Heroes.Enemies.Where(h => Spells.W.IsInRange(h)))
            {
                if (hero.IsValid && hero != null && Spells.W.IsReady() && !nonmenu.IsUlting)
                {
                    Spells.W.Cast();
                }
            }
        }
    }
}

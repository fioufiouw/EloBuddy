using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class UltManager : IModule
    {
        public bool IsUlting;
        void IModule.OnLoad()
        {

        }
        ModuleType IModule.GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
        bool IModule.ShouldDo()
        {
            if (Player.Instance.HasBuff("katarinarsound"))
            {
                IsUlting = true;
                return true;
            }

            Orbwalker.DisableMovement = false;
            Orbwalker.DisableAttacking = false;

            return false;
        }
        void IModule.Do()
        {
            Orbwalker.DisableMovement = true;
            Orbwalker.DisableAttacking = true;
        }

        void IModule.MenuCreate()
        {

        }
    }
}

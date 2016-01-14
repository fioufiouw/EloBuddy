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

        public override ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
        public new bool ShouldDo()
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
        public override void Do()
        {
            Orbwalker.DisableMovement = true;
            Orbwalker.DisableAttacking = true;
        }

        public override void MenuCreate()
        {

        }
    }
}

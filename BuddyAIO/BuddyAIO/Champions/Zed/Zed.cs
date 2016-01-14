using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using BuddyAIO.Champions.Zed.Spells;

namespace BuddyAIO.Champions.Zed
{
    class Zed : IChampion
    {
        public override Champion Hero()
        {
            return Champion.Katarina;
        }

        public override void OnLoad()
        {
            SpellsGet.Get();

            foreach (var module in Index.ModuleList)
            {
                module.OnLoad();
                module.MenuCreate();
            }
        }
        public override void OnUpdate()
        {
            foreach (var module in Index.ModuleList.Where(m => m.GetModuleType() == ModuleType.OnUpdate))
            {
                if (module.ShouldDo == true)
                {
                    module.Do();
                }
            }
        }
        public override void AfterAA()
        {
            foreach (var module in Index.ModuleList.Where(m => m.GetModuleType() == ModuleType.AfterAA))
            {
                if (module.ShouldDo == true)
                {
                    module.Do();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace BuddyAIO.Champions.Katarina
{
    class Katarina : IChampion
    {
        Champion IChampion.Hero()
        {
            return Champion.Katarina;
        }

        void IChampion.OnLoad()
        {
            SpellsGet.Get();

            foreach (var module in Index.ModuleList)
            {
                module.OnLoad();
                module.MenuCreate();
            }
        }
        void IChampion.OnUpdate()
        {
            foreach (var module in Index.ModuleList.Where(m => m.GetModuleType() == ModuleType.OnUpdate))
            {
                if (module.ShouldDo() == true)
                {
                    module.Do();
                }
            }
        }
        void IChampion.AfterAA()
        {
            foreach (var module in Index.ModuleList.Where(m => m.GetModuleType() == ModuleType.AfterAA))
            {
                if (module.ShouldDo() == true)
                {
                    module.Do();
                }
            }
        }
    }
}

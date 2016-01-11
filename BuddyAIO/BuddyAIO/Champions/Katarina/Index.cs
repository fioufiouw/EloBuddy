using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Champions.Katarina.Modules.Misc;
using BuddyAIO.Champions.Katarina.Modules.States;

namespace BuddyAIO.Champions.Katarina
{
    class Index
    {
        public static List<IModule> ModuleList = new List<IModule>()
        {
            new UltManager(),
            new WardJump(),
            new Combo(),
            new Harass(),
            new LastHit(),
            new LaneClear(),
            new DrawSpells(),
            new DrawPredictedQ(),
            new DamageIndicator(),
            new AutoW()
        };
    }
}

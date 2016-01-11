using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;

namespace BuddyAIO.Champions.Katarina
{
    class SpellsGet
    {
        public static void Get()
        {
            Spells.Q = new Spell.Targeted(EloBuddy.SpellSlot.Q, 675);
            Spells.W = new Spell.Active(EloBuddy.SpellSlot.W, 375);
            Spells.E = new Spell.Targeted(EloBuddy.SpellSlot.E, 700);
            Spells.R = new Spell.Active(EloBuddy.SpellSlot.R, 550);
        }
    }
}

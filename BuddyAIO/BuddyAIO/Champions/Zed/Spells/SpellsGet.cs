using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;
using EloBuddy.SDK.Enumerations;

namespace BuddyAIO.Champions.Zed.Spells
{
    class SpellsGet
    {
        internal static void Get()
        {
            Spells.Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear);
            Spells.W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Linear);
            Spells.E = new Spell.Active(SpellSlot.E, 290);
            Spells.R = new Spell.Targeted(SpellSlot.R, 625);
        }
    }
}

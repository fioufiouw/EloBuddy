using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace KataBuddy
{
    class Spells
    {
        public static Spell.Targeted Q, E, Ignite;
        public static Spell.Skillshot W, R;
        private static AIHeroClient playerClient = ObjectManager.Player;
        public static void GetSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 675);
            W = new Spell.Skillshot(SpellSlot.W, 400, SkillShotType.Circular);
            E = new Spell.Targeted(SpellSlot.E, 700);
            R = new Spell.Skillshot(SpellSlot.R, 550, SkillShotType.Circular);

            var slot = playerClient.GetSpellSlotFromName("summonerdot");

            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }
        }

        public static bool IgniteCheck()
        {
            var slot = playerClient.GetSpellSlotFromName("summonerdot");

            if (slot != SpellSlot.Unknown)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

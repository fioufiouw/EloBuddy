using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
namespace BuddyAIO.Utility
{
    class Damage
    {
        public static float GetRawComboDamage(Obj_AI_Base target)
        {
            float damage = 0;
            var player = Player.Instance;
            if (player.Spellbook.CanUseSpell(SpellSlot.Q) == SpellState.Ready)
                damage += player.GetSpellDamage(target, SpellSlot.Q);
            if (player.Spellbook.CanUseSpell(SpellSlot.W) == SpellState.Ready)
                damage += player.GetSpellDamage(target, SpellSlot.W);
            if (player.Spellbook.CanUseSpell(SpellSlot.E) == SpellState.Ready)
                damage += player.GetSpellDamage(target, SpellSlot.E);
            if (player.Spellbook.CanUseSpell(SpellSlot.R) != SpellState.Cooldown || Player.Instance.Spellbook.CanUseSpell(SpellSlot.R) != SpellState.NotAvailable)
                damage += player.GetSpellDamage(target, SpellSlot.R);

            return damage;
        }
        public static float GetComboDamage(Obj_AI_Base target)
        {
            float damage = GetRawComboDamage(target);
            Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, GetRawComboDamage(target), true, true);

            return damage;
        }
    }
}

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
    class DamageLib
    {
        #region Spell Damage
        private static AIHeroClient playerClient = ObjectManager.Player;
        public static float GetComboDamage(Obj_AI_Base target)
        {
            var damage = 0f;
            
            if (Spells.Q.IsReady())
            {
                damage += playerClient.GetSpellDamage(target, SpellSlot.Q);
            }
            if (Spells.W.IsReady())
            {
                damage += playerClient.GetSpellDamage(target, SpellSlot.W);
            }
            if (Spells.E.IsReady())
            {
                damage += playerClient.GetSpellDamage(target, SpellSlot.E);
            }
            if (Spells.R.IsReady())
            {
                damage += playerClient.GetSpellDamage(target, SpellSlot.R);
            }

            damage += GetIgniteDamage(target);
            damage += GetLudensEchoDamage(target);

            return damage;
        }

        public static float GetIgniteDamage(Obj_AI_Base target)
        {
            var damage = 0f;
            if (Spells.IgniteCheck() == true)
            {
                if (Spells.Ignite != null && Spells.Ignite.IsReady())
                {
                    damage += 50 + (20 * playerClient.Level);
                }
            }
            return GetCalculatedDamage(target, DamageType.True, damage);
        }

        private static float GetCalculatedDamage(Obj_AI_Base target, DamageType type, float rawdamage)
        {
            var damage = 0f;
            damage += target.CalculateDamageOnUnit(target, type, rawdamage);

            return damage;
        }
        #endregion

        #region Item Damage

        public static float GetLudensEchoDamage(Obj_AI_Base target)
        {
            var damage = 0f;
            var ludenbuff = playerClient.GetBuffCount("itemmagicshankcharge");
            var AP = playerClient.FlatMagicDamageMod;

            if (ludenbuff == 100)
            {
                damage += 100 + ((10/100f)*AP);
            }
            return GetCalculatedDamage(target, DamageType.Magical, damage);
        }

        #endregion
    }
}

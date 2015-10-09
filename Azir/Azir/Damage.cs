using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Azir
{
    internal class Damage
    {
        /*public static float AvailableComboDamage()
        {
            var QDamage = 0f;
            var WDamage = 0f;
            var EDamage = 0f;
            var RDamage = 0f;

            var _ComboTarget = TargetSelector.GetTarget(1500, EloBuddy.DamageType.Magical);

            if (Program._Q.IsReady())
                QDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.Q);
            if (Program._W.IsReady())
                WDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.W);
            if (Program._E.IsReady())
                EDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.E);
            if (Program._R.IsReady())
                RDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.R);

            return QDamage + (WDamage * 2) + EDamage + RDamage;
        }*/

        public static float GetComboDamage(Obj_AI_Base target)
        {
            var damage = 0d;
            if (Program._Q.IsReady())
            {
                damage += GetQDamage(target);
            }

            if (Program._E.IsReady())
            {
                damage += GetEDamage(target);
            }

            if (Program._R.IsReady())
            {
                damage += GetEDamage(target);
            }

            if (Program._Ignite != null && Program._Ignite.IsReady())
            {
                damage += Program._Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
            }

            damage += GetSoldierDamage(target);

            return (float) damage;
        }

        public static float GetSoldierDamage(Obj_AI_Base target)
        {
            var damage = 45f;
            float calcdamage;
            var AP = Program._Player.FlatMagicDamageMod;
            foreach (var soldier in Orbwalker.ValidAzirSoldiers)
            {
                if (soldier.IsValid && soldier != null)
                {
                    if (Orbwalker.ValidAzirSoldiers.Count == 2)
                    {
                        damage += (((Program._Player.Level * 5) + ((60f / 100f) * AP)) * 2);
                    }
                    if (Orbwalker.ValidAzirSoldiers.Count == 1)
                    {
                        damage += (((Program._Player.Level * 5) + ((60f / 100f) * AP)));
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                damage = (Program.Menu["mDamageBuffer"].Cast<Slider>().CurrentValue/100f)*damage;
            }

            calcdamage = target.CalculateDamageOnUnit(target, DamageType.Magical, damage);
            return calcdamage;
        }

        public static float GetQDamage(Obj_AI_Base target)
        {
            var damage = 45f;
            float calcdamage;
            var AP = Program._Player.FlatMagicDamageMod;
            foreach (var soldier in Orbwalker.ValidAzirSoldiers)
            {
                if (Orbwalker.ValidAzirSoldiers.Count == 2)
                {
                    damage += ((Program._Q.Level * 20) + ((50f / 100f) * AP) * 2);
                }
                if (Orbwalker.ValidAzirSoldiers.Count == 1)
                {
                    damage += ((Program._Q.Level * 20) + ((50f / 100f) * AP));
                }
                if (Orbwalker.ValidAzirSoldiers.Count == 0)
                {
                    damage = 0;
                }
            }
            calcdamage = target.CalculateDamageOnUnit(target, DamageType.Magical, damage);
            return calcdamage;
        }

        public static float GetEDamage(Obj_AI_Base target)
        {
            var damage = 30f;
            float calcdamage;
            var AP = Program._Player.FlatMagicDamageMod;
            foreach (var soldier in Orbwalker.ValidAzirSoldiers)
            {
                if (Orbwalker.ValidAzirSoldiers.Count > 0)
                {
                    damage += ((Program._E.Level * 30) + ((60f / 100f) * AP));
                }
                if (Orbwalker.ValidAzirSoldiers.Count == 0)
                {
                    damage = 0;
                }
            }
            calcdamage = target.CalculateDamageOnUnit(target, DamageType.Magical, damage);
            return calcdamage;
        }

        public static float GetRDamage(Obj_AI_Base target)
        {
            var damage = 75f;
            float calcdamage;
            var AP = Program._Player.FlatMagicDamageMod;

            damage += ((Program._R.Level * 75) + ((60f/100f) * AP));

            calcdamage = target.CalculateDamageOnUnit(target, DamageType.Magical, damage);
            return calcdamage;
        }

        public static float GetKSSoldierDamage(Obj_AI_Base target)
        {
            var damage = 45f;
            float calcdamage;
            var AP = Program._Player.FlatMagicDamageMod;

            damage += (((Program._Player.Level*5) + ((60f/100f)*AP)));

            calcdamage = target.CalculateDamageOnUnit(target, DamageType.Magical, damage);
            return calcdamage;
        }

        public static float GetKSQDamage(Obj_AI_Base target)
        {
            var damage = 45f;
            float calcdamage;
            var AP = Program._Player.FlatMagicDamageMod;

            damage += ((Program._Q.Level*20) + ((50f/100f)*AP));

            calcdamage = target.CalculateDamageOnUnit(target, DamageType.Magical, damage);
            return calcdamage;
        }
    }
}

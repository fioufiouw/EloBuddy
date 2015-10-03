using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;

using SharpDX;
using Damages = EloBuddy.SDK.DamageLibrary;

namespace Talon
{
    class Damage
    {
        public static float ComboDamage()
        {
            var HydraDamage = 0f;
            var BilgeDamage = 0f;
            var TiamatDamage = 0f;

            var _ComboTarget = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);

            var QDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.Q);
            var WDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.W);
            var EDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.E);
            var RDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.R);

            if (Program.ItemMenu["useHydra"].Cast<CheckBox>().CurrentValue && Program._hydra.IsOwned() &&
                Program.ComboMenu["cUseHydra"].Cast<CheckBox>().CurrentValue)
            {
                HydraDamage = Program._Player.GetItemDamage(_ComboTarget, ItemId.Ravenous_Hydra_Melee_Only);
            }
            if (Program.ItemMenu["useBilge"].Cast<CheckBox>().CurrentValue && Program._bilge.IsOwned() &&
                Program.ComboMenu["cUseBilge"].Cast<CheckBox>().CurrentValue)
            {
                BilgeDamage = Program._Player.GetItemDamage(_ComboTarget, ItemId.Bilgewater_Cutlass);
            }
            if (Program.ItemMenu["useTiamat"].Cast<CheckBox>().CurrentValue && Program._tiamat.IsOwned() &&
                Program.ComboMenu["cUseTiamat"].Cast<CheckBox>().CurrentValue)
            {
                TiamatDamage = Program._Player.GetItemDamage(_ComboTarget, ItemId.Tiamat_Melee_Only);
            }
            return QDamage + WDamage + EDamage + RDamage + HydraDamage + BilgeDamage + TiamatDamage;
        }

        public static float AvailableComboDamage()
        {
            var HydraDamage = 0f;
            var BilgeDamage = 0f;
            var TiamatDamage = 0f;

            var QDamage = 0f;
            var WDamage = 0f;
            var EDamage = 0f;
            var RDamage = 0f;

            var _ComboTarget = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);

            if (Program._Q.IsReady())
                QDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.Q);
            if (Program._W.IsReady())
                WDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.W);
            if (Program._E.IsReady())
                EDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.E);
            if (Program._R.IsReady())
                RDamage = Program._Player.GetSpellDamage(_ComboTarget, SpellSlot.R);

            if (Program.ItemMenu["useHydra"].Cast<CheckBox>().CurrentValue && Program._hydra.IsOwned() &&
                Program.ComboMenu["cUseHydra"].Cast<CheckBox>().CurrentValue && Program._hydra.IsReady())
            {
                HydraDamage = Program._Player.GetItemDamage(_ComboTarget, ItemId.Ravenous_Hydra_Melee_Only);
            }
            if (Program.ItemMenu["useBilge"].Cast<CheckBox>().CurrentValue && Program._bilge.IsOwned() &&
                Program.ComboMenu["cUseBilge"].Cast<CheckBox>().CurrentValue && Program._bilge.IsReady())
            {
                BilgeDamage = Program._Player.GetItemDamage(_ComboTarget, ItemId.Bilgewater_Cutlass);
            }
            if (Program.ItemMenu["useTiamat"].Cast<CheckBox>().CurrentValue && Program._tiamat.IsOwned() &&
                Program.ComboMenu["cUseTiamat"].Cast<CheckBox>().CurrentValue && Program._tiamat.IsReady())
            {
                TiamatDamage = Program._Player.GetItemDamage(_ComboTarget, ItemId.Tiamat_Melee_Only);
            }
            return QDamage + WDamage + EDamage + RDamage + HydraDamage + BilgeDamage + TiamatDamage;
        }

        public static float ComboManaUsage()
        {
            float QMana = 0;
            float WMana = 0;
            float EMana = 0;
            float RMana = 0;

            if (Program._Q.Level == 1)
            {
                QMana = 40;
            }
            if (Program._Q.Level == 2)
            {
                QMana = 45;
            }
            if (Program._Q.Level == 3)
            {
                QMana = 50;
            }
            if (Program._Q.Level == 4)
            {
                QMana = 55;
            }
            if (Program._Q.Level == 5)
            {
                QMana = 60;
            }

            if (Program._W.Level == 1)
            {
                WMana = 60;
            }
            if (Program._W.Level == 2)
            {
                WMana = 65;
            }
            if (Program._W.Level == 3)
            {
                WMana = 70;
            }
            if (Program._W.Level == 4)
            {
                WMana = 75;
            }
            if (Program._W.Level == 5)
            {
                WMana = 80;
            }

            if (Program._E.Level == 1)
            {
                EMana = 35;
            }
            if (Program._E.Level == 2)
            {
                EMana = 40;
            }
            if (Program._E.Level == 3)
            {
                EMana = 45;
            }
            if (Program._E.Level == 4)
            {
                EMana = 50;
            }
            if (Program._E.Level == 5)
            {
                EMana = 55;
            }

            if (Program._R.Level == 1)
            {
                RMana = 80;
            }
            if (Program._R.Level == 2)
            {
                RMana = 90;
            }
            if (Program._Q.Level == 3)
            {
                RMana = 100;
            }

            return QMana + WMana + EMana + RMana;
        }

        public static float AvailableComboManaUsage()
        {
            float QMana = 0;
            float WMana = 0;
            float EMana = 0;
            float RMana = 0;

            if (Program._Q.IsReady())
            {
                if (Program._Q.Level == 1)
                {
                    QMana = 40;
                }
                if (Program._Q.Level == 2)
                {
                    QMana = 45;
                }
                if (Program._Q.Level == 3)
                {
                    QMana = 50;
                }
                if (Program._Q.Level == 4)
                {
                    QMana = 55;
                }
                if (Program._Q.Level == 5)
                {
                    QMana = 60;
                }
            }

            if (Program._W.IsReady())
            {
                if (Program._W.Level == 1)
                {
                    WMana = 60;
                }
                if (Program._W.Level == 2)
                {
                    WMana = 65;
                }
                if (Program._W.Level == 3)
                {
                    WMana = 70;
                }
                if (Program._W.Level == 4)
                {
                    WMana = 75;
                }
                if (Program._W.Level == 5)
                {
                    WMana = 80;
                }
            }

            if (Program._E.IsReady())
            {
                if (Program._E.Level == 1)
                {
                    EMana = 35;
                }
                if (Program._E.Level == 2)
                {
                    EMana = 40;
                }
                if (Program._E.Level == 3)
                {
                    EMana = 45;
                }
                if (Program._E.Level == 4)
                {
                    EMana = 50;
                }
                if (Program._E.Level == 5)
                {
                    EMana = 55;
                }
            }

            if (Program._R.IsReady())
            {
                if (Program._R.Level == 1)
                {
                    RMana = 80;
                }
                if (Program._R.Level == 2)
                {
                    RMana = 90;
                }
                if (Program._Q.Level == 3)
                {
                    RMana = 100;
                }
            }

            return QMana + WMana + EMana + RMana;
        }
    }
}

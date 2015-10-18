using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace KataBuddy.States
{
    internal class Combo
    {
        private static AIHeroClient playerClient = ObjectManager.Player;
        private static Obj_AI_Base Target = TargetSelector.GetTarget(1250, DamageType.Magical);

        public static void Init()
        {
            if (Target != null)
            {
                if (Target.IsValid && !Target.IsDead)
                {
                    CastQ();

                    CastW();

                    CastE();

                    CastR();
                }
            }
        }

        private static void CastQ()
        {
            if (Drawings._Menu.ComboMenu["cUseQ"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                Spells.Q.IsInRange(Target))
            {
                if (Drawings._Menu.Menu["mDbg"].Cast<CheckBox>().CurrentValue)
                    Chat.Print("Castq");

                Spells.Q.Cast(Target);
            }
        }

        private static void CastW()
        {
            if (Drawings._Menu.ComboMenu["cUseW"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() &&
                Spells.W.IsInRange(Target))
            {
                if (Drawings._Menu.Menu["mDbg"].Cast<CheckBox>().CurrentValue)
                    Chat.Print("Castw");

                Spells.W.Cast(Target);
            }
        }

        private static void CastE()
        {
            if (Drawings._Menu.ComboMenu["cUseE"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() &&
                Spells.E.IsInRange(Target))
            {
                if (Drawings._Menu.Menu["mDbg"].Cast<CheckBox>().CurrentValue)
                    Chat.Print("Caste");

                Spells.E.Cast(Target);
            }
        }

        private static void CastR()
        {
            if (Drawings._Menu.ComboMenu["cUseR"].Cast<CheckBox>().CurrentValue)
            {
                if (Spells.R.IsInRange(Target))
                {
                    if (Spells.R.IsReady())
                    {
                        if (Drawings._Menu.Menu["mDbg"].Cast<CheckBox>().CurrentValue)
                            Chat.Print("Castr");

                        Spells.R.Cast(Target);
                    }
                }
            }
        }
    }
}

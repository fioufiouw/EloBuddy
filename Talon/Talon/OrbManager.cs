using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Properties;

namespace Talon
{
    internal class OrbManager
    {

        public static void Combo()
        {
            var _Target = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);

            if (_Target != null)
            {
                if (_Target.IsValid && !_Target.IsZombie)
                {
                    if (Program.ComboMenu["cUseR"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Program._R.IsReady())
                        {
                            Program._R.Cast(Program._Player);
                        }
                    }

                    if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Orbwalker.CanAutoAttack)
                        {
                            Player.IssueOrder(GameObjectOrder.AttackUnit, _Target);
                        }
                    }

                    if (Program.ComboMenu["cUseW"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Program._W.IsReady())
                        {
                            Program._W.Cast(_Target);
                        }
                    }

                    if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Orbwalker.CanAutoAttack)
                        {
                            Player.IssueOrder(GameObjectOrder.AttackUnit, _Target);
                        }
                    }

                    if (Program.ComboMenu["cUseE"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Program._E.IsReady())
                        {
                            Program._E.Cast(_Target);
                        }
                    }

                    if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Orbwalker.CanAutoAttack)
                        {
                            Player.IssueOrder(GameObjectOrder.AttackUnit, _Target);
                        }
                    }

                    if (Program.ComboMenu["cUseQ"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Program._Q.IsReady())
                        {
                            Program._Q.Cast();
                        }
                    }

                    if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Orbwalker.CanAutoAttack)
                        {
                            Player.IssueOrder(GameObjectOrder.AttackUnit, _Target);
                        }
                    }
                }
            }
        }

        public static void Harass()
        {
            var _Target = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);
            if (_Target != null)
            {
                if (Program.HarassMenu["hUseE"].Cast<CheckBox>().CurrentValue)
                {
                    if (_Target.IsValid && !_Target.IsZombie && Program._E.IsReady() && Program._E.IsInRange(_Target))
                    {
                        Program._E.Cast(_Target);
                    }
                }

                if (Program.HarassMenu["hUseW"].Cast<CheckBox>().CurrentValue)
                {
                    if (_Target.IsValid && !_Target.IsZombie && Program._W.IsReady() && Program._W.IsInRange(_Target))
                    {
                        Program._W.Cast(_Target);
                    }
                }
            }
        }

        public static void Flee()
        {
            if (Program.FleeMenu["fUseE"].Cast<CheckBox>().CurrentValue)
            {
                var targetminion = ObjectManager.Get<Obj_AI_Minion>()
                    .Where(m => m.Distance(Program._MousePos) < Program._MousePos.Distance(m));

                foreach (var minion in targetminion)
                {
                    if (Program._E.IsReady())
                    {
                        Program._E.Cast(minion);
                    }
                }
            }

            if (Program.FleeMenu["fUseR"].Cast<CheckBox>().CurrentValue)
            {
                Program._R.Cast(Program._Player);
            }
        }
        
        public static void LastHit()
        {
            if (Program.FarmMenu["fLHUseW"].Cast<CheckBox>().CurrentValue)
            {
                if (Orbwalker.IsAutoAttacking)
                    return;
                Orbwalker.ForcedTarget = null;
                
                var killableminions = ObjectManager.Get<Obj_AI_Minion>()
                    .Where(m => m.Health < Damage.WDamage()).ToList();
                
                foreach (var minion in killableminions)
                {
                    if (Program._W.IsReady() && minion.Health <= Damage.WDamage())
                    {
                        Chat.Print(Damage.WDamage());
                        Program._W.Cast(minion);
                    }
                }
            }
            if (Program.FarmMenu["fLHUseQ"].Cast<CheckBox>().CurrentValue == true)
            {
                if (Orbwalker.IsAutoAttacking)
                    return;
                Orbwalker.ForcedTarget = null;

                var killableminions2 = ObjectManager.Get<Obj_AI_Minion>()
                    .Where(m => m.Health < Damage.QDamage()).ToList();

                foreach (var minion in killableminions2)
                {
                    if (Program._Q.IsReady())
                    {
                        Program._Q.Cast(minion);
                        Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    }
                }
            }
        }

        /*public static void LaneClear()
        {
            var _EligibleMinions = ObjectManager.Get<Obj_AI_Base>()
                    .Where(m => m.IsValid && !m.IsDead).ToList();

            if (Orbwalker.IsAutoAttacking)
                return;
            Orbwalker.ForcedTarget = null;

            if (Program.FarmMenu["fLHUseW"].Cast<CheckBox>().CurrentValue)
            {
                if (Orbwalker.minion);
            }
            if (Program.FarmMenu["fLHUseQ"].Cast<CheckBox>().CurrentValue == true)
            {
                if (Orbwalker.IsAutoAttacking)
                    return;
                Orbwalker.ForcedTarget = null;

                var killableminions2 = ObjectManager.Get<Obj_AI_Minion>()
                    .Where(m => m.Health < Damage.QDamage()).ToList();

                foreach (var minion in killableminions2)
                {
                    if (Program._Q.IsReady())
                    {
                        Program._Q.Cast(minion);
                        Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    }
                }
            }
        }*/

    }
}

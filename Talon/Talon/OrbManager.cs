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
using Geometry = EloBuddy.SDK.Geometry;

namespace Talon
{
    internal class OrbManager
    {
        private static readonly float _Health = Program._Player.Health;
        public static void Combo()
        {
            var _Target = TargetSelector.GetTarget(Program._R.Range + 350, EloBuddy.DamageType.Physical);

            if (_Target != null)
            {
                if (_Target.IsValid && !_Target.IsZombie)
                {
                    if (Program.ItemMenu["useYoumuu"].Cast<CheckBox>().CurrentValue &&
                        Program.ComboMenu["cUseYoumuu"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program._youmuu.IsOwned() && Program._youmuu.IsReady())
                            Program._youmuu.Cast();
                    }
                    if (Program.ItemMenu["useBilge"].Cast<CheckBox>().CurrentValue &&
                        Program.ComboMenu["cUseBilge"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program._bilge.IsOwned() && Program._bilge.IsReady())
                            Program._bilge.Cast(_Target);
                    }
                    if (Program.ItemMenu["useHydra"].Cast<CheckBox>().CurrentValue &&
                        Program.ComboMenu["cUseHydra"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program._hydra.IsOwned() && Program._hydra.IsReady() && Program._hydra.IsInRange(_Target))
                            Program._hydra.Cast();
                    }
                    if (Program.ItemMenu["useTiamat"].Cast<CheckBox>().CurrentValue &&
                        Program.ComboMenu["cUseTiamat"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program._tiamat.IsOwned() && Program._tiamat.IsReady() && Program._tiamat.IsInRange(_Target))
                            Program._tiamat.Cast();
                    }

                    if (Program.ItemMenu["useBotrk"].Cast<CheckBox>().CurrentValue &&
                        Program.ComboMenu["cUseBotrk"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program._botrk.IsOwned() && Program._botrk.IsReady() && Program._botrk.IsInRange(_Target))
                            Program._botrk.Cast(_Target);
                    }
                    if (Program.ComboMenu["cUseR"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Program._R.IsReady())
                        {
                            Program._R.Cast(Program._Player);
                        }
                    }
                    if (Program.ComboMenu["cUseW"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsValid && Program._W.IsReady())
                        {
                            Program._W.Cast(_Target);
                        }
                    }
                    if (Program._E != null)
                    {
                        if (Program.ComboMenu["cUseE"].Cast<CheckBox>().CurrentValue &&
                            Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue &&
                            Program.LogicMenu["eUseHealthSlider"].Cast<Slider>().CurrentValue < Program._Player.Health)
                        {
                            if (_Target.IsValid && Program._E.IsReady())
                            {
                                Program._E.Cast(_Target);
                            }
                        }
                    }
                    
                    if (Program._Q != null)
                    {
                        if (Program.ComboMenu["cUseQ"].Cast<CheckBox>().CurrentValue)
                        {
                            if (_Target.IsValid && Program._Q.IsReady())
                            {
                                Program._Q.Cast();
                            }
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
                if (Program.HarassMenu["hUseE"].Cast<CheckBox>().CurrentValue &&
                    Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue &&
                    Program.LogicMenu["eUseHealthSlider"].Cast<Slider>().CurrentValue < _Health)
                {
                    if (_Target.IsValid && !_Target.IsZombie && Program._E.IsReady() && Program._E.IsInRange(_Target))
                    {
                        Program._E.Cast(_Target);
                    }
                }

                if (Program.HarassMenu["hUseE"].Cast<CheckBox>().CurrentValue &&
                    Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue == false)
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
            if (Program.FleeMenu["fUseE"].Cast<CheckBox>().CurrentValue &&
                Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue &&
                Program.LogicMenu["eUseHealthSlider"].Cast<Slider>().CurrentValue < _Health)
            {
                var targetminion = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .OrderBy(m => m.Distance(Program._MousePos));

                foreach (var minion in targetminion)
                {
                    if (Program._E.IsReady())
                    {
                        Program._E.Cast(minion);
                    }
                }
            }

            if (Program.FleeMenu["fUseYoumuu"].Cast<CheckBox>().CurrentValue &&
                Program.ItemMenu["useYoumuu"].Cast<CheckBox>().CurrentValue)
            {
                Program._youmuu.Cast();
            }

            if (Program.FleeMenu["fUseR"].Cast<CheckBox>().CurrentValue)
            {
                Program._R.Cast(Program._Player);
            }

            if (Program.FleeMenu["fUseE"].Cast<CheckBox>().CurrentValue &&
                Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue == false)
            {
                var targetminion = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .OrderBy(m => m.Distance(Program._MousePos));

                foreach (var minion in targetminion)
                {
                    if (Program._E.IsReady())
                    {
                        Program._E.Cast(minion);
                    }
                }
            }

            if (Program.FleeMenu["fUseYoumuu"].Cast<CheckBox>().CurrentValue &&
                Program.ItemMenu["useYoumuu"].Cast<CheckBox>().CurrentValue)
            {
                Program._youmuu.Cast();
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

                var killableminions = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .Where(m => m.Health < Program._Player.GetSpellDamage(m, SpellSlot.W)).ToList();

                foreach (var minion in killableminions)
                {
                    if (Program._W.IsInRange(minion) && Program._W.IsReady() &&
                        minion.Health <= Program._Player.GetSpellDamage(minion, SpellSlot.W))
                    {
                        Program._W.Cast(minion);
                    }
                }
            }
            if (Program.FarmMenu["fLHUseQ"].Cast<CheckBox>().CurrentValue == true)
            {
                if (Orbwalker.IsAutoAttacking)
                    return;
                Orbwalker.ForcedTarget = null;

                var killableminions2 = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .Where(m => m.Health < Program._Player.GetSpellDamage(m, SpellSlot.Q)).ToList();

                foreach (var minion in killableminions2)
                {
                    if (Program._Q.IsReady())
                    {
                        Program._Q.Cast();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    }
                }
            }
        }

        public static void KillSteal()
        {
            var _Target = TargetSelector.GetTarget(Program._E.Range + 600, EloBuddy.DamageType.Physical);

            if (Program.KSMenu["ksUseE"].Cast<CheckBox>().CurrentValue &&
                Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue &&
                Program.LogicMenu["eUseHealthSlider"].Cast<Slider>().CurrentValue < _Health &&
                Program.KSMenu["ksUseQ"].Cast<CheckBox>().CurrentValue)
            {
                if (_Target != null)
                {
                    if (_Target.Health <
                        (Program._Player.GetSpellDamage(_Target, SpellSlot.Q) +
                         Program._Player.GetAutoAttackDamage(_Target)))
                    {
                        if ((Program._Q.IsReady() && Program._E.IsReady()) &&
                            Program._E.IsInRange(_Target) && _Target.IsValid && !_Target.IsZombie)
                        {
                            Program._E.Cast(_Target);
                            Program._Q.Cast();
                            Orbwalker.ForcedTarget = _Target;
                        }
                    }
                }
            }

            if (Program.KSMenu["ksUseE"].Cast<CheckBox>().CurrentValue &&
                Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue == false &&
                Program.KSMenu["ksUseQ"].Cast<CheckBox>().CurrentValue)
            {
                if (_Target != null)
                {
                    if (_Target.Health <
                        (Program._Player.GetSpellDamage(_Target, SpellSlot.Q) +
                         Program._Player.GetAutoAttackDamage(_Target)))
                    {
                        if ((Program._Q.IsReady() && Program._E.IsReady()) &&
                            Program._E.IsInRange(_Target) && _Target.IsValid && !_Target.IsZombie)
                        {
                            Program._E.Cast(_Target);
                            Program._Q.Cast();
                            Orbwalker.ForcedTarget = _Target;
                        }
                    }
                }
            }

            if (Program.KSMenu["ksUseQ"].Cast<CheckBox>().CurrentValue)
            {

                if (_Target != null)
                {
                    if (_Target.Health < ObjectManager.Player.GetSpellDamage(_Target, SpellSlot.Q)
                        && _Target.IsValid && !_Target.IsZombie)
                    {
                        if (Orbwalker.IsAutoAttacking)
                            return;

                        Program._Q.Cast();
                        Orbwalker.ForcedTarget = _Target;
                    }
                }
            }

            if (Program.KSMenu["ksUseW"].Cast<CheckBox>().CurrentValue)
            {
                if (_Target != null)
                {
                    if (_Target.Health < ObjectManager.Player.GetSpellDamage(_Target, SpellSlot.W))
                    {
                        if (Program._W.IsReady())
                        {
                            if (Program._W.IsInRange(_Target))
                            {
                                Program._W.Cast(_Target);
                            }

                            else
                            {
                                if (Program.KSMenu["ksUseE"].Cast<CheckBox>().CurrentValue &&
                                    Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue &&
                                    Program.LogicMenu["eUseHealthSlider"].Cast<Slider>().CurrentValue < _Health &&
                                    Program.KSMenu["ksUseW"].Cast<CheckBox>().CurrentValue)
                                {
                                    if (_Target != null)
                                    {
                                        if (_Target.Health < ObjectManager.Player.GetSpellDamage(_Target, SpellSlot.W) &&
                                            Program._W.IsInRange(_Target) && _Target.IsValid && !_Target.IsZombie)
                                        {
                                            Program._E.Cast(_Target);
                                            Program._W.Cast(_Target);
                                        }
                                    }
                                }

                                if (Program.KSMenu["ksUseE"].Cast<CheckBox>().CurrentValue &&
                                    Program.LogicMenu["eUseHealthCheck"].Cast<CheckBox>().CurrentValue == false &&
                                    Program.KSMenu["ksUseW"].Cast<CheckBox>().CurrentValue)
                                {
                                    if (_Target != null)
                                    {
                                        if (_Target.Health < ObjectManager.Player.GetSpellDamage(_Target, SpellSlot.W) &&
                                            Program._W.IsInRange(_Target) && _Target.IsValid && !_Target.IsZombie &&
                                            Program._E.IsReady() && Program._E.IsInRange(_Target))
                                        {
                                            Program._E.Cast(_Target);
                                            Program._W.Cast(_Target);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void LaneClear()
        {
            if (Program.ManaMenu["waveClearMana"].Cast<Slider>().CurrentValue > Program._Player.Mana)
            {
                int _ChoiceW = 0;
                int _ChoiceQ = 0;

                var _EligibleMinionsW = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .Where(m => m.Health < Program._Player.GetSpellDamage(m, SpellSlot.W)).ToList();

                var _EligibleMinionsQ = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .Where(m => m.Health < Program._Player.GetSpellDamage(m, SpellSlot.W)).ToList();

                if (Orbwalker.IsAutoAttacking)
                {
                    return;
                }
                Orbwalker.ForcedTarget = null;

                if (Program.FarmMenu["fLCUseW"].Cast<CheckBox>().CurrentValue)
                {
                    foreach (var minion in _EligibleMinionsW)
                    {
                        if (Program._W.IsInRange(minion) && Program._W.IsReady())
                        {
                            if (Orbwalker.CanBeLastHitted(minion))
                            {
                                _ChoiceW = 3;
                            }

                            if (minion.Health <= Program._Player.GetSpellDamage(minion, SpellSlot.W))
                            {
                                _ChoiceW = 1;
                            }

                            else
                            {
                                _ChoiceW = 2;
                            }
                        }

                        switch (_ChoiceW)
                        {
                            case 0:
                                //Nothing is valid; Cannot do anything.
                                break;

                            case 1:
                                //We can cast W to execute minion.
                                Program._W.Cast(minion);
                                break;

                            case 2:
                                //We can use free time to try to deal as much damage as possible.
                                Program._W.Cast(minion);
                                break;

                            case 3:
                                //Minion can be lasthitted; We can leave the work up to the Orbwalker.
                                break;
                        }
                    }
                }
                if (Program.FarmMenu["fLCUseQ"].Cast<CheckBox>().CurrentValue)
                {
                    foreach (var minion in _EligibleMinionsQ)
                    {
                        if (Program._Q.IsInRange(minion) && Program._Q.IsReady())
                        {
                            if (minion.Health <=
                                (Program._Player.GetSpellDamage(minion, SpellSlot.W) +
                                 ObjectManager.Player.TotalAttackDamage))
                            {
                                _ChoiceQ = 2;
                            }

                            else
                            {
                                _ChoiceQ = 1;

                            }
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Properties;
using Slider = EloBuddy.SDK.Menu.Values.Slider;
using TargetSelector = EloBuddy.SDK.TargetSelector;

namespace Azir
{
    class OrbManager
    {
        public static void AutoHealthPot()
        {
            if (Program.Menu["mUseHealthPot"].Cast<Slider>().CurrentValue >= Program._Player.HealthPercent &&
                Program.Menu["mUseItems"].Cast<CheckBox>().CurrentValue)
            {
                if (!Program._Player.HasBuff("RegenerationPotion"))
                {
                    if (Program._HealthPot.IsOwned() && Program._HealthPot.IsReady())
                    {
                        Program._HealthPot.Cast();
                    }
                }
            }
        }
        public static void AutoIgnite()
        {
            if (Program._Ignite != null && Program._Ignite.IsReady())
            {
                var _IgniteTarget = TargetSelector.GetTarget(Program._Ignite.Range, DamageType.True);

                if (Program.Menu["mAutoIgnite"].Cast<CheckBox>().CurrentValue)
                {
                    if (_IgniteTarget != null && _IgniteTarget.IsValid)
                    {
                        if (Program._Ignite.IsInRange(_IgniteTarget))
                        {
                            if (_IgniteTarget.Health <
                                Damage.GetIgniteDamage(_IgniteTarget))
                            {
                                Program._Ignite.Cast(_IgniteTarget);
                            }
                        }
                    }
                }
            }
        }
        public static void Combo()
        {
            var Choice = 0;

            //Cast W = 1
            //Cast Q = 2
            //Cast E = 3
            //Cast R = 4
            //AA     = 5
            var _Target = TargetSelector.GetTarget(1500, DamageType.Magical);

            if (Program._Ignite != null && Program._Ignite.IsReady())
            {
                var _IgniteTarget = TargetSelector.GetTarget(Program._Ignite.Range, DamageType.True);

                if (_IgniteTarget != null && _IgniteTarget.IsValid)
                {
                    if (Program._Ignite.IsInRange(_Target))
                    {
                        if (_IgniteTarget.Health <
                           Damage.GetIgniteDamage(_Target))
                        {
                            Program._Ignite.Cast(_IgniteTarget);
                        }
                    }
                }
            }
            if (_Target != null && _Target.IsValid)
            {
                if (Program.ComboMenu["cUseW"].Cast<CheckBox>().CurrentValue && Program._W.IsReady() &&
                    Program._W.IsInRange(_Target))
                {
                    Choice = 1;
                }
                if (Program.ComboMenu["cUseQ"].Cast<CheckBox>().CurrentValue && Program._Q.IsReady() &&
                    Program._Q.IsInRange(_Target) && Orbwalker.ValidAzirSoldiers.Count > 0)
                {
                    Choice = 2;
                }
                if (Program.ComboMenu["cUseEGC"].Cast<CheckBox>().CurrentValue && Program._E.IsReady() &&
                    !Program._Q.IsInRange(_Target))
                {
                    Choice = 3;
                }
                if (Program.ComboMenu["cUseE"].Cast<CheckBox>().CurrentValue && Program._E.IsReady() &&
                    Program._E.IsInRange(_Target) && Orbwalker.ValidAzirSoldiers.Count > 0)
                {
                    Choice = 3;
                }
                if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue && Orbwalker.CanAutoAttack)
                {
                    Choice = 5;
                }
                if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue && Orbwalker.CanAutoAttack)
                {
                    Choice = 5;
                }
                //if (Program.ComboMenu["cUseE"].Cast<CheckBox>().CurrentValue && Program._E.IsReady() &&
                //    Program._E.IsInRange(_Target))
                //{
                //    Choice = 3;
                //}
                if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue && Orbwalker.CanAutoAttack)
                {
                    Choice = 5;
                }
                if (Program.ComboMenu["cUseAA"].Cast<CheckBox>().CurrentValue && Orbwalker.CanAutoAttack)
                {
                    Choice = 5;
                }
                if (Program.ComboMenu["cUseR"].Cast<CheckBox>().CurrentValue && Program._R.IsReady() &&
                    Program._R.IsInRange(_Target))
                {
                    Choice = 4;
                }

                switch (Choice)
                {
                    case 1:
                    {
                        if (!Orbwalker.IsAutoAttacking)
                        {
                            Program._W.Cast(_Target);
                        }
                        break;
                    }
                    case 2:
                    {
                        if (!Orbwalker.IsAutoAttacking)
                        {
                            Program._Q.Cast(_Target);
                        }
                        break;
                    }
                    case 5:
                    {
                        if (!Orbwalker.IsAutoAttacking && Orbwalker.ValidAzirSoldiers.Count > 0)
                        {
                            Orbwalker.ForcedTarget = _Target;
                        }
                        break;
                    }
                    case 3:
                    {
                        if (!Orbwalker.IsAutoAttacking)
                        {
                            Program._E.Cast(_Target);
                        }
                        break;
                    }
                    case 4:
                    {
                        if (!Orbwalker.IsAutoAttacking)
                        {
                            Program._R.Cast(_Target);
                        }
                        break;
                    }
                }
            }
        }

        public static void LastHit()
        {
            if (Orbwalker.ValidAzirSoldiers.Count >= 1)
                Orbwalker.DisableAttacking = true;

            var MinionsW = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy);

            var MinionsQ = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                .Where(m => m.Health <= Damage.GetSoldierDamage(m));

            if (Orbwalker.IsAutoAttacking)
                return;

            foreach (var minion1 in MinionsW)
            {
                var GobalMinions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program._Player.Position)
                    .Where(m => m.HealthPercent <= Program.FarmMenu["fLHUseWHealth"].Cast<Slider>().CurrentValue)
                    .OrderBy(m => m.Distance(minion1));

                if (Program.FarmMenu["fLHUseW"].Cast<CheckBox>().CurrentValue)
                {
                    foreach (var minion3 in GobalMinions)
                    {
                        if (Program._W.IsInRange(minion3) && Program._W.IsReady())
                        {
                            Program._W.Cast(minion3);
                        }
                    }
                    if (Program._Player.IsInAutoAttackRange(minion1) &&
                        minion1.Health <= Damage.GetSoldierDamage(minion1))
                    {
                        Player.IssueOrder(GameObjectOrder.AttackUnit, minion1);
                    }
                }
            }


            if (Program.FarmMenu["fLHUseQ"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var minion2 in MinionsQ)
                {
                    if (Program._Q.IsInRange(minion2) && Orbwalker.ValidAzirSoldiers.Count >= 1 &&
                        Program._Q.IsReady())
                    {
                        Program._Q.Cast(minion2);
                    }
                }
            }
        }

        public static void LaneClear()
        {
            if (Orbwalker.IsAutoAttacking)
            {
                return;
            }

            var LasthittableMinions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                .Where(m => m.Health <= Damage.GetSoldierDamage(m));

            foreach (var minion in LasthittableMinions)
            {
                var GobalMinions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program._Player.Position)
                    .Where(m => m.HealthPercent <= Program.FarmMenu["fLCUseWHealth"].Cast<Slider>().CurrentValue)
                    .OrderBy(m => m.Distance(minion));

                if (minion.HealthPercent <= Program.FarmMenu["fLCWaitHealth"].Cast<Slider>().CurrentValue)
                {
                    Orbwalker.DisableAttacking = true;
                }
                else
                {
                    Orbwalker.DisableAttacking = false;
                }
                if (Program.FarmMenu["fLCUseW"].Cast<CheckBox>().CurrentValue)
                {
                    foreach (var wminion in GobalMinions)
                    {
                        if (Program._W.IsReady() && Program._W.IsInRange(minion))
                        {
                            Program._W.Cast(minion);
                        }
                    }
                    if (minion.Health <= Damage.GetSoldierDamage(minion))
                    {
                            Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    }
                }
            }
        }

        public static void Flee()
        {
            var _RTarget = TargetSelector.GetTarget(1500, DamageType.Magical);

                if (Program.FleeMenu["fUseWE"].Cast<CheckBox>().CurrentValue)
                {
                    if (Program._W.IsReady())
                    {
                        Program._W.Cast(Game.CursorPos);
                    }
                    if (Program.FleeMenu["fUseQ"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program._Q.IsReady() && Orbwalker.ValidAzirSoldiers.Count > 0)
                        {
                            Program._Q.Cast(Game.CursorPos);
                        }
                }
                    if (Program._E.IsReady())
                    {
                        Program._E.Cast(Game.CursorPos);
                    }
                }

            if (_RTarget != null && _RTarget.IsValid)
            {
                if (Program.FleeMenu["fUseR"].Cast<CheckBox>().CurrentValue)
                {
                    if (Program._Player.Health <= Program.FleeMenu["fUseRHealth"].Cast<Slider>().CurrentValue &&
                        Program._R.IsReady() && Program._R.IsInRange(_RTarget))
                    {
                        Program._R.Cast(_RTarget);
                    }
                }
            }
        }

        public static void Harass()
        {
            var _Target = TargetSelector.GetTarget(1500, DamageType.Magical);

            if (Orbwalker.IsAutoAttacking)
                return;

            if (_Target != null && _Target.IsValid)
            {
                if (Program.HarassMenu["hUseW"].Cast<CheckBox>().CurrentValue)
                {
                    Program._W.Cast(_Target);

                    if (Program.HarassMenu["hUseQ"].Cast<CheckBox>().CurrentValue && !Orbwalker.IsAutoAttacking &&
                        Program._Q.IsReady() && Program._Q.IsInRange(_Target))
                    {
                        Program._Q.Cast(_Target);
                    }

                    if (Program.HarassMenu["hUseAA"].Cast<CheckBox>().CurrentValue)
                    {
                        if (_Target.IsInAutoAttackRange(Program._Player))
                            Player.IssueOrder(GameObjectOrder.AttackUnit, _Target);
                    }
                }
            }
        }

        public static void ItemManager()
        {
            if (Program.ItemMenu["iUseZhonyas"].Cast<CheckBox>().CurrentValue)
            {
                if (Program._Player.Health < Program.ItemMenu["iUseZhonyasHealth"].Cast<Slider>().CurrentValue)
                {
                    if (Program._Zhonyas.IsReady() && !Program._Player.IsDead && Program._Zhonyas.IsOwned())
                    {
                        Program._Zhonyas.Cast();
                    }
                }
            }
        }

        public static void KillSteal()
        {
            var _Target = TargetSelector.GetTarget(1500, DamageType.Magical);

            if (_Target != null && _Target.IsValid)
            {
                if (Program.KSMenu["ksUseWQ"].Cast<CheckBox>().CurrentValue)
                {
                    if (_Target.Health < Damage.GetKSQDamage(_Target))
                    {
                        if (Program._W.IsReady() && Program._Q.IsReady())
                        {
                            Program._W.Cast(_Target);

                            if (Program._Q.IsInRange(_Target))
                            {
                                Program._Q.Cast(_Target);
                            }
                            else
                            {
                                if (Program.KSMenu["ksUseE"].Cast<CheckBox>().CurrentValue)
                                {
                                    foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                                    {
                                        Program._E.Cast(soldier);
                                    }
                                    if (Program._Q.IsInRange(_Target))
                                    {
                                        Program._Q.Cast(_Target);
                                    }
                                }
                            }
                        }
                    }
                }

                if (Program.KSMenu["ksUseR"].Cast<CheckBox>().CurrentValue)
                {
                    if (_Target.Health < Damage.GetRDamage(_Target))
                    {
                        if (Program._R.IsReady())
                        {
                            if (Program._R.IsInRange(_Target))
                            {
                                Program._R.Cast(_Target);
                            }
                            else
                            {
                                if (Program.KSMenu["ksUseE"].Cast<CheckBox>().CurrentValue)
                                {
                                    foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                                    {
                                        Program._E.Cast(soldier);
                                    }
                                    if (Program._R.IsInRange(_Target))
                                    {
                                        Program._R.Cast(_Target);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

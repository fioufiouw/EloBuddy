using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace TheShadow
{
    internal class OrbManager
    {
        public static void Combo()
        {
            var target = TargetSelector.GetTarget(1000, EloBuddy.DamageType.Physical);
            if (MainShadow.ComboMenu["fullcombo"].Cast<CheckBox>().CurrentValue == true && MainShadow._Q.IsReady() &&
                MainShadow._W.IsReady() && MainShadow._E.IsReady() && MainShadow._R.IsReady())
            {
                if (MainShadow.ComboMenu["useR"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._R.IsInRange(target))
                {
                    MainShadow._R.Cast(target);
                }

                if (MainShadow.ComboMenu["useW"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._W.IsInRange(target))
                {
                    MainShadow._W.Cast(target);
                }

                if (MainShadow.ComboMenu["useE"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._E.IsInRange(target))
                    MainShadow._E.Cast(target);

                if (MainShadow.ComboMenu["useQ"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._Q.IsInRange(target))
                    MainShadow._Q.Cast(target);
            }

            if (MainShadow.ComboMenu["fullcombo"].Cast<CheckBox>().CurrentValue == false)
            {
                if (MainShadow.ComboMenu["useR"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._R.IsInRange(target))
                {
                    MainShadow._R.Cast(target);
                }

                if (MainShadow.ComboMenu["useW"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._W.IsInRange(target))
                {
                    MainShadow._W.Cast(target);
                }

                if (MainShadow.ComboMenu["useE"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._E.IsInRange(target))
                    MainShadow._E.Cast(target);

                if (MainShadow.ComboMenu["useQ"].Cast<CheckBox>().CurrentValue == true &&
                    MainShadow._Q.IsInRange(target))
                    MainShadow._Q.Cast(target);
            }

        }

        public static void Harass()
        {
            bool wUsed = false;
            var target = TargetSelector.GetTarget(1000, EloBuddy.DamageType.Physical);

            if (target.IsValid && !target.IsDead)
            {
                if (MainShadow.HarassMenu["hUseW"].Cast<CheckBox>().CurrentValue == true)
                {
                    if (MainShadow.sender.IsMe && MainShadow.buff.Buff.DisplayName == "Living Shadow")
                        wUsed = true;
                    else
                        wUsed = false;

                    if (MainShadow._W.IsInRange(target) && wUsed == false)
                    {
                        MainShadow._W.Cast(target);
                        wUsed = true;
                    }
                }

                if (MainShadow.HarassMenu["hUseQ"].Cast<CheckBox>().CurrentValue == true)
                {
                    if (MainShadow._Q.IsInRange(target))
                    {
                        MainShadow._Q.Cast(target);

                    }
                }

                if (MainShadow.HarassMenu["hUseE"].Cast<CheckBox>().CurrentValue == true)
                {
                    if (MainShadow._E.IsInRange(target))
                    {
                        MainShadow._E.Cast(target);

                    }
                }
            }
        }

        public static void Flee()
        {
            if (MainShadow._W.IsReady())
                MainShadow._W.Cast(MainShadow.mousePos);
            else
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
        }

    /*static public void WaveClear()
        {
            if (Orbwalker.IsAutoAttacking) { return; }
            Orbwalker.ForcedTarget = null;

            var source =
                ObjectManager.Get<Obj_AI_Minion>()
                    .Where(a => a.IsEnemy && a.Distance(ObjectManager.Player) < CustomRange())
                    .OrderBy(a => a.Health)
                    .FirstOrDefault();

            if (MainShadow.HarassMenu["fwUseE"].Cast<CheckBox>().CurrentValue == true)
            {
                if (Orbwalker.IsAutoAttacking || !source.IsValidTarget(ObjectManager.Player.GetAutoAttackRange(source)) || !MainShadow._Q.IsReady()) return;
                MainShadow._Q.Cast(source);
            }
            if (MainShadow.HarassMenu["fwUseE"].Cast<CheckBox>().CurrentValue == true)
            {
                if (!MainShadow._E.IsReady()) return;
                MainShadow._E.Cast(source);
            }

        }

        static public void LastHit()
        {
            if (Orbwalker.IsAutoAttacking) return;
            Orbwalker.ForcedTarget = null;
            var source =
                ObjectManager.Get<Obj_AI_Minion>()
                    .Where(a => a.IsEnemy && a.Distance(ObjectManager.Player) < CustomRange())
                    .OrderBy(a => a.Health)
                    .FirstOrDefault();
            if (MainShadow.HarassMenu["flUseQ"].Cast<CheckBox>().CurrentValue == true)
            {
                if (Orbwalker.IsAutoAttacking || !source.IsValidTarget(ObjectManager.Player.GetAutoAttackRange(source)) || !MainShadow._Q.IsReady()) return;
                MainShadow._Q.Cast(source);
            }

            if (MainShadow.HarassMenu["flUseE"].Cast<CheckBox>().CurrentValue == true)
            {
                if (Orbwalker.IsAutoAttacking || !source.IsValidTarget(ObjectManager.Player.GetAutoAttackRange(source)) || !MainShadow._E.IsReady()) return;
                MainShadow._E.Cast(source);
            }
        }*/
    }
}

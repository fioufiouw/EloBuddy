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
    class OrbManager
    {

        public static float CustomRange()
        {
            if (MainShadow._Q.IsReady())
                return MainShadow._Q.Range;

            if (MainShadow._E.IsReady())
                return MainShadow._E.Range;

            return ObjectManager.Player.GetAutoAttackRange() + 50;

        }
        static public void Combo()
        {
            bool wUsed1 = false;
            bool rUsed1 = false;
            bool wUsed2 = false;
            bool rUsed2 = false;

            if (MainShadow._W.IsOnCooldown)
                wUsed1 = false;

            if (MainShadow._R.IsOnCooldown)
                rUsed1 = false;


            var target = TargetSelector.GetTarget(MainShadow._R.Range + 350, EloBuddy.DamageType.Physical);
            if (MainShadow.ComboMenu["fullcombo"].Cast<CheckBox>().CurrentValue == true && MainShadow._Q.IsReady() && MainShadow._W.IsReady() && MainShadow._E.IsReady() && MainShadow._R.IsReady())
            {
                if (MainShadow.ComboMenu["useR"].Cast<CheckBox>().CurrentValue == true && rUsed1 == false && MainShadow._R.IsInRange(target))
                {
                    MainShadow._R.Cast(target);
                    rUsed1 = true;
                }

                if (MainShadow.ComboMenu["useW"].Cast<CheckBox>().CurrentValue == true && wUsed1 == false && MainShadow._W.IsInRange(target))
                {
                    MainShadow._W.Cast(target);
                    wUsed1 = true;
                }

                if (MainShadow.ComboMenu["useE"].Cast<CheckBox>().CurrentValue == true && MainShadow._E.IsInRange(target))
                    MainShadow._E.Cast(target);

                if (MainShadow.ComboMenu["useQ"].Cast<CheckBox>().CurrentValue == true && MainShadow._Q.IsInRange(target))
                    MainShadow._Q.Cast(target);
            }

            if (MainShadow.ComboMenu["fullcombo"].Cast<CheckBox>().CurrentValue == false)
            {
                if (MainShadow.ComboMenu["useR"].Cast<CheckBox>().CurrentValue == true && rUsed2 == false && MainShadow._R.IsInRange(target))
                {
                    MainShadow._R.Cast(target);
                    rUsed2 = true;
                }

                if (MainShadow.ComboMenu["useW"].Cast<CheckBox>().CurrentValue == true && wUsed2 == false && MainShadow._W.IsInRange(target))
                {
                    MainShadow._W.Cast(target);
                    wUsed2 = true;
                }

                if (MainShadow.ComboMenu["useE"].Cast<CheckBox>().CurrentValue == true && MainShadow._E.IsInRange(target))
                    MainShadow._E.Cast(target);

                if (MainShadow.ComboMenu["useQ"].Cast<CheckBox>().CurrentValue == true && MainShadow._Q.IsInRange(target))
                    MainShadow._Q.Cast(target);
            }

        }

        static public void Harass()
        {
            var target = TargetSelector.GetTarget(MainShadow._R.Range + 350, EloBuddy.DamageType.Physical);

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

        static public void Flee()
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;

namespace BuddyAIO.Champions.Katarina.Modules.States
{
    class LastHit : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        void IModule.MenuCreate()
        {
            mMenu = Menu.AddSubMenu("LastHit", "lasthit");
            mMenu.AddCheckBox("useq", "Use Q");
            mMenu.AddCheckBox("usew", "Use W");
            mMenu.AddCheckBox("usee", "Use E", false);
            mMenu.AddSeparator(10);
            mMenu.AddLabel("Only casts spells if minion is unkillable by AA");
            mMenu.AddCheckBox("smartfarm", "Smart Farm");
        }

        void IModule.OnLoad()
        {

        }
        ModuleType IModule.GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
        bool IModule.ShouldDo()
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LastHit)
            {
                return true;
            }
            return false;
        }
        void IModule.Do()
        {
            MenuIndex.LastHit lasthit = new MenuIndex.LastHit();
            foreach (var minion in EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => Player.Instance.IsInRange(m, 900)))
            {
                if (lasthit.SmartFarm)
                {
                    foreach (var unkillableminion in Orbwalker.UnLasthittableMinions)
                    {
                        if (unkillableminion.IsValid && unkillableminion != null && Spells.W.IsReady() && Spells.W.IsInRange(unkillableminion))
                        {
                            Spells.W.Cast();
                        }
                        else
                        {
                            if (unkillableminion.IsValid && unkillableminion != null && Spells.Q.IsReady() && Spells.Q.IsInRange(unkillableminion))
                            {
                                Spells.Q.Cast(unkillableminion);
                            }
                            else
                            {
                                if (unkillableminion.IsValid && unkillableminion != null && Spells.E.IsReady() && Spells.E.IsInRange(unkillableminion))
                                {
                                    Spells.E.Cast(unkillableminion);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Prediction.Health.GetPrediction(minion, Spells.Q.CastDelay + Game.Ping / 3) <= Player.Instance.GetSpellDamage(minion as Obj_AI_Base, SpellSlot.Q))
                    {
                        if (minion.IsValid && minion != null && Spells.Q.IsReady() && Spells.Q.IsInRange(minion) && lasthit.UseQ)
                        {
                            Spells.Q.Cast(minion);
                        }
                    }
                    if (Prediction.Health.GetPrediction(minion, Spells.W.CastDelay + Game.Ping / 3) <= Player.Instance.GetSpellDamage(minion as Obj_AI_Base, SpellSlot.W))
                    {
                        if (minion.IsValid && minion != null && Spells.W.IsReady() && Spells.W.IsInRange(minion) && lasthit.UseW)
                        {
                            Spells.W.Cast(minion);
                        }
                    }
                    if (Prediction.Health.GetPrediction(minion, Spells.E.CastDelay + Game.Ping / 3) <= Player.Instance.GetSpellDamage(minion as Obj_AI_Base, SpellSlot.Q))
                    {
                        if (minion.IsValid && minion != null && Spells.E.IsReady() && Spells.E.IsInRange(minion) && lasthit.UseE)
                        {
                            Spells.E.Cast(minion);
                        }
                    }
                }
            }
        }
    } 
}

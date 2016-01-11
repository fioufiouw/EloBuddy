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
    class LaneClear : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        void IModule.MenuCreate()
        {
            mMenu = Menu.AddSubMenu("Lane Clear", "laneclear");
            mMenu.AddCheckBox("useq", "Use Q");
            mMenu.AddCheckBox("usew", "Use W");
            mMenu.AddCheckBox("usee", "Use E", false);
            mMenu.AddCheckBox("dangerlow", "Don't hit dangerously low minions");
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
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LaneClear )
            {
                return true;
            }
            return false;
        }
        void IModule.Do()
        {
            MenuIndex.LaneClear laneclear = new MenuIndex.LaneClear();
            foreach (var minion in EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => Player.Instance.IsInRange(m, 900)))
            {
                if (laneclear.DangerLow)
                {
                    if (Prediction.Health.GetPrediction(minion, 5 + Spells.Q.CastDelay * 2 + Game.Ping / 3) >= 10)
                    {
                        if (minion.IsValid && minion != null && Spells.Q.IsReady() && Spells.Q.IsInRange(minion) && laneclear.UseQ)
                        {
                            Spells.Q.Cast(minion);
                        }
                    }
                    if (Prediction.Health.GetPrediction(minion, 5 + Spells.W.CastDelay * 2 + Game.Ping / 3) >= 10)
                    {
                        if (minion.IsValid && minion != null && Spells.W.IsReady() && Spells.W.IsInRange(minion) && laneclear.UseW)
                        {
                            Spells.W.Cast(minion);
                        }
                    }
                    if (Prediction.Health.GetPrediction(minion, 5 + Spells.E.CastDelay * 2 + Game.Ping / 3) >= 10)
                    {
                        if (minion.IsValid && minion != null && Spells.E.IsReady() && Spells.E.IsInRange(minion) && laneclear.UseE)
                        {
                            Spells.E.Cast(minion);
                        }
                    }
                }
                else
                {
                    if (minion.IsValid && minion != null && Spells.Q.IsReady() && Spells.Q.IsInRange(minion) && laneclear.UseQ)
                    {
                        Spells.Q.Cast(minion);
                    }

                    if (minion.IsValid && minion != null && Spells.W.IsReady() && Spells.W.IsInRange(minion) && laneclear.UseW)
                    {
                        Spells.W.Cast(minion);
                    }

                    if (minion.IsValid && minion != null && Spells.E.IsReady() && Spells.E.IsInRange(minion) && laneclear.UseE)
                    {
                        Spells.E.Cast(minion);
                    }
                }
            }
        }
    }
}

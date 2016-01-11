using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class KillSteal : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        private bool HasIgnite = false;
        void IModule.MenuCreate()
        {
            mMenu = Menu.AddSubMenu("KillSteal", "killsteal");
            mMenu.AddCheckBox("Use Q", "useq");
            mMenu.AddCheckBox("Use W", "usew");
            mMenu.AddCheckBox("Use E", "usee");
            mMenu.AddCheckBox("WardJump", "wardjump");
            mMenu.AddCheckBox("SmartKS", "smartks");
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
            return true;
        }

        void IModule.Do()
        {
            foreach (var hero in EntityManager.Heroes.Enemies.Where(h => Player.Instance.IsInRange(h, 1200)).OrderBy(h => h.Health))
            {
                var igniteslot = GetIgniteSpellSlot();

                if (igniteslot != SpellSlot.Unknown && Player.CanUseSpell(igniteslot) == SpellState.Ready)
                    HasIgnite = true;

                //very inefficient way, but meh
                var qdamage = Player.Instance.GetSpellDamage(hero, SpellSlot.Q);
                var wdamage = Player.Instance.GetSpellDamage(hero, SpellSlot.W);
                var edamage = Player.Instance.GetSpellDamage(hero, SpellSlot.E);
                var ignitedamage = Player.Instance.GetSummonerSpellDamage(hero, DamageLibrary.SummonerSpells.Ignite);

                if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) <= qdamage)
                {
                    if (Spells.Q.IsInRange(hero) && Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(hero);
                    }
                }
                else
                {
                    if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) - qdamage <= ignitedamage && HasIgnite && igniteslot != SpellSlot.Unknown && Player.CanUseSpell(igniteslot) == SpellState.Ready)
                    {
                        if (Spells.Q.IsInRange(hero) && Spells.Q.IsReady())
                        {
                            Spells.Q.Cast(hero);
                            Player.Instance.Spellbook.CastSpell(igniteslot);
                        }
                    }
                }
                if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) <= wdamage)
                {
                    if (Spells.W.IsInRange(hero) && Spells.W.IsReady())
                    {
                        Spells.W.Cast();
                    }
                }
                if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) <= edamage)
                {
                    if (Spells.E.IsInRange(hero) && Spells.E.IsReady())
                    {
                        Spells.E.Cast(hero);
                    }
                }
                if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) <= qdamage + edamage)
                {
                    if (Spells.E.IsInRange(hero) && Spells.E.IsReady() && Spells.Q.IsInRange(hero) && Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(hero);
                        Spells.E.Cast(hero);
                    }
                }
                if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) <= edamage + wdamage)
                {
                    if (Spells.E.IsInRange(hero) && Spells.E.IsReady() && Spells.W.IsReady())
                    {
                        Spells.E.Cast(hero);
                        Spells.W.Cast();
                    }
                }
                if (Prediction.Health.GetPrediction(hero, 5 + (Game.Ping / 4) / 2) <= qdamage + wdamage + edamage)
                {
                    if (Spells.E.IsInRange(hero) && Spells.E.IsReady() && Spells.Q.IsInRange(hero) && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        Spells.Q.Cast(hero);
                        Spells.E.Cast(hero);
                        Spells.W.Cast();
                    }
                }
            }
        }
        private static SpellSlot GetIgniteSpellSlot()
        {
            if (Player.GetSpell(SpellSlot.Summoner1).Name == "summonerignite")
                return SpellSlot.Summoner1;
            if (Player.GetSpell(SpellSlot.Summoner2).Name == "summonerignite")
                return SpellSlot.Summoner2;
            return SpellSlot.Unknown;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace BuddyAIO.Champions
{
    class Selector
    {
        public static void Select()
        {
            ChampionIndex();

            /* // We don't need this because the ChampionIndex below already checks for champions.
            #region ChampionPick

            switch (Player.Instance.Hero)
            {
                case EloBuddy.Champion.Katarina:
                    Katarina.Katarina.Init();
                    break;
                case EloBuddy.Champion.Zed:
                    break;
                case EloBuddy.Champion.Fizz:
                    break;
                case EloBuddy.Champion.Caitlyn:
                    break;
                case EloBuddy.Champion.Ziggs:
                    break;
                case EloBuddy.Champion.Ezreal:
                    break;
                case EloBuddy.Champion.Jinx:
                    break;
            }
       
            #endregion */
        }

        private static void ChampionIndex()
        {
            #region Load
            //We don't need to use the OnLoad event because we are already in the loading stage
            foreach (var champion in Champions.ChampionIndex.ChampList)
            {
                if (champion.Hero() == Player.Instance.Hero)
                {
                    champion.OnLoad();
                }
            }
            #endregion

            #region Update
            Game.OnUpdate += delegate
            {
                foreach (var champion in Champions.ChampionIndex.ChampList)
                {
                    if (champion.Hero() == Player.Instance.Hero)
                    {
                        champion.OnUpdate();
                    }
                }
            };
            #endregion

            #region AfterAA
            Orbwalker.OnPostAttack += delegate
            {
                foreach (var champion in Champions.ChampionIndex.ChampList)
                {
                    if (champion.Hero() == Player.Instance.Hero)
                    {
                        champion.AfterAA();
                    }
                }
            };
            #endregion
        }
    }
}

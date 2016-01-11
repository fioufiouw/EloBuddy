using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;

namespace BuddyAIO.Bootstrap
{
    internal class BAIO
    {
        public static List<Champion> SupportedChampions = new List<Champion>();

        public static void Init()
        {
            ChampionSort();
            Chat.Print("BuddyAIO Loaded - version " + Program.ActualVersion);

            VersionChecker.SearchVersion();

            #region SupportedCheck

            if (!SupportedChampions.Contains(Player.Instance.Hero))
            {
                BAIOBootstrap.Unsupported();
            }

            #endregion

            else
            {
                BAIOBootstrap.Boot();
            }
        }

        private static void ChampionSort()
        {
            SupportedChampions.Add(Champion.Katarina);
            SupportedChampions.Add(Champion.Zed);
            SupportedChampions.Add(Champion.Fizz);
            /*SupportedChampions.Add(Champion.Tristana);
            SupportedChampions.Add(Champion.Caitlyn);
            SupportedChampions.Add(Champion.Ezreal);
            SupportedChampions.Add(Champion.Jinx);*/
        }

        public static bool ChampionIsSupported()
        {
            if (SupportedChampions.Contains(Player.Instance.Hero))
            {
                return true;
            }
            return false;
        }
    }
}

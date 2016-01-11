using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Menu;

namespace BuddyAIO.Utility.Config
{
    class MenuGenerator
    {
        public static Menu Menu, OrbMenu, DrawMenu, MiscMenu;

        public static void MenuDraw()
        {
            Menu = MainMenu.AddMenu(Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower());

            Menu.AddGroupLabel(Player.Instance.ChampionName + "Loaded");

        }
    }
}

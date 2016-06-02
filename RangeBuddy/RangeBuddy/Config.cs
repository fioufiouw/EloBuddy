using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace RangeBuddy
{
    class Config
    {
        public static Menu Menu, RangesMenu;
        public static void Generate()
        {
            Menu = MainMenu.AddMenu("RangeBuddy", "rangebuddy");

            Menu.Add("enabled", new CheckBox("Enabled"));


            RangesMenu = Menu.AddSubMenu("Ranges");


        }

        private static void AddColor(Menu menu, int stemID, int[] defaultValues)
        {
            menu.Add(stemID + "R", new Slider("R", defaultValues[0], 0, 255));
            menu.Add(stemID + "G", new Slider("G", defaultValues[1], 0, 255));
            menu.Add(stemID + "B", new Slider("B", defaultValues[2], 0, 255));
            menu.Add(stemID + "A", new Slider("A", defaultValues[3], 0, 255));
        }
        private static void RemoveColor(Menu menu, int stemID)
        {
            menu.Remove(stemID + "R");
            menu.Remove(stemID + "G");
            menu.Remove(stemID + "B");
            menu.Remove(stemID + "A");
        }
        public static void AddRange(Menu menu)
        {
            menu.Add("index" + Program.Index, new GroupLabel(Program.Index.ToString()));
            menu.Add("enabled" + Program.Index, new CheckBox("Enabled"));
            menu.Add("range" + Program.Index, new Slider("Range", 50, 1, 5000));
            AddColor(menu, Program.Index, new[] { 255, 255, 0, 255 });
            menu.Add("seperator" + Program.Index, new Separator(15));
        }

        public static void RemoveRange(Menu menu, int index)
        {
            menu.Remove("index" + index);
            menu.Remove("enabled" + index);
            menu.Remove("range" + index);
            menu.Remove("seperator" + index);
            RemoveColor(menu, index);
        }
    }
}

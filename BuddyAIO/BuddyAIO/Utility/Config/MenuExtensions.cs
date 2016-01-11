using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace BuddyAIO.Utility.Config
{
    internal static class MenuExtensions
    {
        public static void AddCheckBox(this Menu menu, string uniqueID, string displayName, bool defaultValue = true)
        {
            menu.Add(uniqueID, new CheckBox(displayName, defaultValue));
        }

        public static void AddSlider(this Menu menu, string uniqueID, string displayName, int defaultValue,
            int minValue, int maxValue)
        {
            menu.Add(uniqueID, new Slider(displayName, defaultValue, minValue, maxValue));
        }

        public static void AddKeyBind(this Menu menu, string uniqueID, string displayName, bool defaultValue,
            KeyBind.BindTypes type, char defKey)
        {
            menu.Add(uniqueID, new KeyBind(displayName, defaultValue, type, defKey));
        }

        public static void AddStringList(this Menu menu, string uniqueID, string displayText, string[] cases, int defaultValue)
        {
            menu.AddSlider(uniqueID, displayText + cases[GetSliderValue(menu, uniqueID)], defaultValue, 1, cases.Count());
        }

        public static Menu AddSubMenu(string name, string uniqueID = null, string longTitle = null)
        {
           return MenuGenerator.Menu.AddSubMenu(name, uniqueID, longTitle);
        }

        public static bool GetCheckBoxValue(this Menu menu, string uniqueID)
        {
            return menu[uniqueID].Cast<CheckBox>().CurrentValue;
        }

        public static int GetSliderValue(this Menu menu, string uniqueID)
        {
            return menu[uniqueID].Cast<Slider>().CurrentValue;
        }
        public static bool GetKeyBindValue(this Menu menu, string uniqueID)
        {
            return menu[uniqueID].Cast<KeyBind>().CurrentValue;
        }
    }
}

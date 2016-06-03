using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace CondemnBuddy
{
    class Config
    {
        public static Menu Menu;
        public static void GenerateMenu()
        {
            Menu = MainMenu.AddMenu("CondemnBuddy", "condemnbuddy");

            Menu.Add("enabled", new CheckBox("Enabled"));
            Menu.Add("keybind", new KeyBind("Flash Condemn", false, KeyBind.BindTypes.HoldActive, 't'));
            Menu.Add("cdelay", new Slider("Custom Delay Modifier", 0, -15, 15));
        }
    }
}

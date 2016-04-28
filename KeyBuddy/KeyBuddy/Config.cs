using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KeyBuddy
{
    internal class Config
    {
        internal static Menu Menu;
        internal static void Generate()
        {
            Menu = MainMenu.AddMenu("KeyBuddy", "keybuddy", "KeyBuddy - By Buddy");
            Menu.Add("enabled", new CheckBox("Enabled"));

            Menu.AddSeparator(15);

            Menu.Add("up", new KeyBind("Up", false, KeyBind.BindTypes.HoldActive, "w".ToCharArray()[0]));
            Menu.Add("down", new KeyBind("Down", false, KeyBind.BindTypes.HoldActive, "s".ToCharArray()[0]));
            Menu.Add("left", new KeyBind("Left", false, KeyBind.BindTypes.HoldActive, "a".ToCharArray()[0]));
            Menu.Add("right", new KeyBind("Right", false, KeyBind.BindTypes.HoldActive, "d".ToCharArray()[0]));

            Menu.AddSeparator(15);

            Menu.AddLabel("By Buddy - EloBuddy.net");
            Menu.AddLabel("Last Update: 18/02/2016");
        }

        internal class Get
        {
            internal static bool MoveUp
            {
                get { return Config.Menu["up"].Cast<KeyBind>().CurrentValue; }
            }
            internal static bool MoveDown
            {
                get { return Config.Menu["down"].Cast<KeyBind>().CurrentValue; }
            }
            internal static bool MoveLeft
            {
                get { return Config.Menu["left"].Cast<KeyBind>().CurrentValue; }
            }
            internal static bool MoveRight
            {
                get { return Config.Menu["right"].Cast<KeyBind>().CurrentValue; }
            }
            internal static bool Enabled
            {
                get { return Config.Menu["enabled"].Cast<CheckBox>().CurrentValue; }
            }
        }
    }
}

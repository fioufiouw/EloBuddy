using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace LanternBuddy
{
    class Config
    {
        public static Menu Menu, DrawMenu, HumanizerMenu;

        public static void Generate()
        {
            Menu = MainMenu.AddMenu("LanternBuddy", "lanternbuddy", "LanternBuddy by Buddy");

            Menu.Add("enable", new KeyBind("Enable", true, KeyBind.BindTypes.PressToggle, "a".ToCharArray()[0]));
            Menu.Add("use", new KeyBind("Use", false, KeyBind.BindTypes.HoldActive, "s".ToCharArray()[0]));

            Menu.AddSeparator(15);

            Menu.Add("priority", new ComboBox("Priority", new[] {"Lowest Health", "Closest", "Farthest"}));
            Menu.Add("measureToCursor", new CheckBox("Measure Priority to Cursor"));
            Menu.Add("checkDist", new Slider("Maximum Distance to Check", 1200, 500, 2000));

            Menu.Add("minmana", new Slider("Minimum Mana % to Throw", 20));
            Menu.Add("walk", new CheckBox("Walk to Cursor"));

            Menu.Add("castEnemyCount", new Slider("Enemy Count to Cast", 1, 0, 5));

            DrawMenu = Menu.AddSubMenu("Draw");

            DrawMenu.Add("drawLine", new CheckBox("Draw Line from Origin to Target"));
            DrawMenu.Add("drawTargetCircle", new CheckBox("Draw Cirle at Target"));
            DrawMenu.Add("drawCheckDist", new CheckBox("Draw Check Distance"));

            DrawMenu.AddSeparator(15);

            DrawMenu.Add("lineR", new Slider("Line R", 0, 0, 255));
            DrawMenu.Add("lineG", new Slider("Line G", 255, 0, 255));
            DrawMenu.Add("lineB", new Slider("Line B", 255, 0, 255));
            DrawMenu.Add("lineA", new Slider("Line A", 255, 0, 255));

            DrawMenu.AddSeparator(15);

            DrawMenu.Add("circleR", new Slider("Circle R", 0, 0, 255));
            DrawMenu.Add("circleG", new Slider("Circle G", 255, 0, 255));
            DrawMenu.Add("circleB", new Slider("Circle B", 255, 0, 255));
            DrawMenu.Add("circleA", new Slider("Circle A", 255, 0, 255));


            HumanizerMenu = Menu.AddSubMenu("Humanizer");

            HumanizerMenu.Add("castOnScreen", new CheckBox("Only Cast Lantern if Target is on Screen"));

            HumanizerMenu.Add("minDelay", new Slider("Minimum Cast Delay"));
            HumanizerMenu.Add("maxDelay", new Slider("Maximum Cast Delay"));

            HumanizerMenu["minDelay"].Cast<Slider>().OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                {
                    var nextValue = args.NewValue;
                    var maxValue = HumanizerMenu["maxDelay"].Cast<Slider>().CurrentValue;

                    if (nextValue > maxValue)
                        sender.CurrentValue = maxValue;
                };

            HumanizerMenu["maxDelay"].Cast<Slider>().OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                {
                    var nextValue = args.NewValue;
                    var minValue = HumanizerMenu["minDelay"].Cast<Slider>().CurrentValue;

                    if (nextValue < minValue)
                        sender.CurrentValue = minValue;
                };

        }

        public static Color GetColor(int a, int r, int g, int b)
        {
            return Color.FromArgb(a, r, g, b);
        }
    }
}

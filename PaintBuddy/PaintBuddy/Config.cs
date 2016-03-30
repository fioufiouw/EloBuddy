using System.Drawing;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace PaintBuddy
{
    internal class Config
    {
        public static Menu Menu;

        public class Get
        {
            public static Color Color
            {
                get
                {
                    return Color.FromArgb(Menu.Get<Slider>("a").CurrentValue,
                        Menu.Get<Slider>("r").CurrentValue, Menu.Get<Slider>("g").CurrentValue,
                        Menu.Get<Slider>("b").CurrentValue);
                }
            }

            public static bool IsGlow
            {
                get { return Menu["type"].Cast<ComboBox>().CurrentValue == 1; }
            }

            public static bool Draw
            {
                get { return Menu["draw"].Cast<KeyBind>().CurrentValue; }
            }

            public static bool Remove
            {
                get { return Menu["remove"].Cast<KeyBind>().CurrentValue; }
            }

            public static int Radius
            {
                get { return Menu["radius"].Cast<Slider>().CurrentValue; }
            }
        }

        public class Generate
        {
            public static void GenerateMenu()
            {
                Menu = MainMenu.AddMenu("PaintBuddy", "paintbuddy");

                Menu.Add("draw", new KeyBind("Draw", false, KeyBind.BindTypes.HoldActive, 1));
                Menu.Add("remove", new KeyBind("Remove", false, KeyBind.BindTypes.HoldActive, 'g'));
                Menu.Add("type", new ComboBox("Circle Type", 0, "Plain", "Sexy Glow"));

                Menu.AddSeparator(15);

                Menu.AddGroupLabel("Color");
                Menu.Add("r", new Slider("R", 255, 1, 255));
                Menu.Add("g", new Slider("G", 255, 1, 255));
                Menu.Add("b", new Slider("B", 255, 1, 255));
                Menu.Add("a", new Slider("A", 255, 1, 255));

                Menu.AddSeparator(15);

                Menu.Add("radius", new Slider("Radius", 25, 5, 60));

                Menu.AddSeparator(15);

                Menu.AddLabel("Developed by Buddy");
                Menu.AddLabel("Last update 30/03/2016");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace RangeBuddy
{
    class Program
    {
        public static int Index = 0;
        public static List<Range> ActiveRanges = new List<Range>(); 
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Config.Generate();

            Drawing.OnDraw += Drawing_OnDraw;
            Chat.OnInput += Chat_OnInput;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            try
            {
                if (ActiveRanges == null || !ActiveRanges.Any() || !Config.Menu["enabled"].Cast<CheckBox>().CurrentValue)
                    return;

                foreach (var range in ActiveRanges)
                {
                    var i = range.Index;
                    
                    range.Color = Color.FromArgb(Config.RangesMenu[i + "A"].Cast<Slider>().CurrentValue,
                        Config.RangesMenu[i + "R"].Cast<Slider>().CurrentValue,
                        Config.RangesMenu[i + "G"].Cast<Slider>().CurrentValue,
                        Config.RangesMenu[i + "B"].Cast<Slider>().CurrentValue);
                    range.Radius = Config.RangesMenu["range" + i].Cast<Slider>().CurrentValue;
                    range.Enabled = Config.RangesMenu["enabled" + i].Cast<CheckBox>().CurrentValue;

                    if (range.Enabled)
                        new Circle() {BorderWidth = 1, Color = range.Color, Radius = range.Radius}.Draw(
                            Player.Instance.ServerPosition);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Chat.Print("drawerror");
            }
        }

        private static void Chat_OnInput(ChatInputEventArgs args)
        {
            try
            {
                if (args.Input.ToLower() == "/range new")
                {
                    Config.AddRange(Config.RangesMenu);
                    ActiveRanges.Add(new Range(Index, Color.Yellow));
                    Index++;
                }
                if (args.Input.ToLower() == "/range clear")
                {
                    for (int i = 0; i < Index; i++)
                    {
                        Config.RemoveRange(Config.RangesMenu, i);
                    }
                    ActiveRanges.Clear();
                    Index = 0;
                }
                if (args.Input.ToLower().StartsWith("/range delete "))
                {
                    string outStr;
                    uint uOut;
                    outStr = args.Input.ToLower().Replace("/range delete ", "");

                    if (uint.TryParse(outStr, out uOut))
                    {
                        if (!ActiveRanges.Any(r => r.Index == uOut))
                            Chat.Print("Couldn't find range with that index.");
                        else
                        {
                            if (ActiveRanges != null && ActiveRanges.Any())
                            {
                                ActiveRanges.Remove(ActiveRanges.FirstOrDefault(r => r.Index == uOut));
                                /*
                                foreach (var rad in ActiveRanges)
                                {
                                    if (rad.Index == uOut)
                                        ActiveRanges.Remove(rad);
                                }
                                */
                                Config.RemoveRange(Config.RangesMenu, (int)uOut);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

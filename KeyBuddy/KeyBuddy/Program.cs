using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace KeyBuddy
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static Program()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Extensions.WriteToConsole("KeyBuddy LOADED", ConsoleColor.Green);

            // Generate the config
            Config.Generate();

            Game.OnUpdate += Game_OnTick;
        }
        
        private static void Game_OnTick(EventArgs args)
        {
            // Listen to the streams
            Mover.Listen();
        }
    }
}

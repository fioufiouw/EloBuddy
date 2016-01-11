using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;
using System.Reflection;

namespace BuddyAIO
{
    class Program
    {
        public static string VERS = "0.1 Build 1 WIP";
        public static System.Version ActualVersion;
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            ActualVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Bootstrap.BAIO.Init();
        }
    }
}

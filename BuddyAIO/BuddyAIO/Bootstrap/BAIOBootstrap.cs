using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using SharpDX;

namespace BuddyAIO.Bootstrap
{
    class BAIOBootstrap
    {
        public static void Boot()
        {
            Utility.Config.MenuGenerator.MenuDraw();
            Champions.Selector.Select();
        }
        public static void Unsupported()
        {
            Chat.Print("<font color = 'red'>Your Champion is unsupported</font>");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace KeyBuddy
{
    internal class Mover
    {
        internal static void Listen()
        {
            if (!Config.Get.Enabled)
                return;

            if (Config.Get.MoveUp && Config.Get.MoveLeft)
                new Vector3(-200, 200, 0).MoveTo();
            else if (Config.Get.MoveUp && Config.Get.MoveRight)
                new Vector3(175, 175, 0).MoveTo();
            else if (Config.Get.MoveDown && Config.Get.MoveLeft)
                new Vector3(-175, -175, 0).MoveTo();
            else if (Config.Get.MoveDown && Config.Get.MoveRight)
                new Vector3(-175, -175, 0).MoveTo();
            else if (Config.Get.MoveUp)
                new Vector3(0, 175, 0).MoveTo();
            else if (Config.Get.MoveDown)
                new Vector3(0, -175, 0).MoveTo();
            else if (Config.Get.MoveLeft)
                new Vector3(-175, 0, 0).MoveTo();
            else if (Config.Get.MoveRight)
                new Vector3(175, 0, 0).MoveTo();
        }
    }
}

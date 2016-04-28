using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace KeyBuddy
{
    internal static class Extensions
    {
        /// <summary>
        /// Writes a message to the console
        /// </summary>
        /// <param name="text">the message</param>
        /// <param name="color">the message color</param>
        internal static void WriteToConsole(string text, ConsoleColor? color)
        {
            ConsoleColor cc = color ?? default(ConsoleColor);

            Console.ForegroundColor = cc;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        internal static void MoveTo(this Vector3 offsets)
        {
            Vector3 pos = new Vector3(Player.Instance.ServerPosition.X + offsets.X,
                Player.Instance.ServerPosition.Y + offsets.Y, Player.Instance.ServerPosition.Z + offsets.Z);

            Orbwalker.OrbwalkTo(pos);
            //Player.IssueOrder(GameObjectOrder.MoveTo, pos);
        }
    }
}

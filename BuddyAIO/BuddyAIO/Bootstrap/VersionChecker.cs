using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using EloBuddy;

namespace BuddyAIO.Bootstrap
{
    class VersionChecker
    {
        // Thanks to WujuSan
        public static void SearchVersion()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string Text = new WebClient().DownloadString("https://raw.githubusercontent.com/Unlsh/EloBuddy/BuddyAIO/Properties/AssemblyInfo.cs");

                    var Match = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(Text);

                    if (Match.Success)
                    {
                        var CorrectVersion = new System.Version(string.Format("{0}.{1}.{2}.{3}", Match.Groups[1], Match.Groups[2], Match.Groups[3], Match.Groups[4]));

                        if (CorrectVersion > Program.ActualVersion)
                        {
                            Chat.Print("<font color='#FFFF00'>BuddyAIO - </font><font color='#FF0000'>OUTDATED</font><font color='#FFFF00'>, The correct version is: " + CorrectVersion + "</font>");
                            //Chat.Print("<font color='#FFFF00'>BuddyAIO - </font><font color='#FF0000'>OUTDATED</font><font color='#FFFF00'>, The correct version is: " + CorrectVersion + "</font>"); //Lol why?
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + "\n [ [RIP] Search ]");
                }
            });
        }
    }
}

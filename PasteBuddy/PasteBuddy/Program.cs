using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using System.Windows.Forms;

namespace PasteBuddy
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Chat.OnInput += Chat_OnInput;
        }

        private static void Chat_OnInput(ChatInputEventArgs args)
        {
            if (args.Input.Trim() == "/paste")
            {
                if (string.IsNullOrEmpty(Clipboard.GetText()))
                    Chat.Print("Nice try Kappa - Clipboard is empty FeelsBadMan");
                else
                    args.Input = Clipboard.GetText();
            }

            if (args.Input.Contains("_paste_"))
            {
                if (string.IsNullOrEmpty(Clipboard.GetText()))
                    Chat.Print("Nice try Kappa - Clipboard is empty FeelsBadMan");
                else
                    args.Input = args.Input.Replace("_paste_", Clipboard.GetText());
            }
        }
    }
}

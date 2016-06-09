using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace RageBuddy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Core.DelayAction(() => Chat.Say("/mute all"), new Random(15789472).Next(1000, 10000));
            

            Chat.OnInput += Chat_OnInput;
            Player.OnEmote += Player_OnEmote;
        }

        private static void Player_OnEmote(AIHeroClient sender, PlayerDoEmoteEventArgs args)
        {
            args.Process = false;
        }

        private static void Chat_OnInput(ChatInputEventArgs args)
        {
            args.Process = false;
        }
    }
}
using System;
using EloBuddy;
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
            Chat.Say("/mute all");

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
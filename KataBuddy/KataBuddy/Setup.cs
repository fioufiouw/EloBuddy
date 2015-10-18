using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

using Color = System.Drawing.Color;

namespace KataBuddy
{
    class Setup
    {
        private static Obj_AI_Base ComboTarget = TargetSelector.GetTarget(1250, DamageType.Magical);
        public static Drawings.DamageIndicator.DamageIndicator Indicator;
        private static AIHeroClient playerClient = ObjectManager.Player;
        static void Main(string[] args)
        {
            Console.WriteLine("Katarina Injected Succesfully", Color.Lime);
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (playerClient.Hero != Champion.Katarina)
            {
                Chat.Print("Error During Load (-1)", Color.Red);
                return;
            }

            Spells.GetSpells();
            Drawings._Menu.Initialize();

            Indicator = new Drawings.DamageIndicator.DamageIndicator();
            Indicator.Add("Combo", new Drawings.DamageIndicator.SpellData((int)DamageLib.GetComboDamage(ComboTarget), DamageType.Magical, Color.Lime));

            Chat.Print("Initialized; Setup Loaded Succesfully (0)", Color.LimeGreen);

            Drawing.OnDraw += Utils.DrawingManager.Drawing_OnDraw;
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            Utils.StateManager.GetState();
            Utils.KillSteal.Init();
            Utils.UltiManager.Init();

            Indicator.Update("Combo", new Drawings.DamageIndicator.SpellData((int)DamageLib.GetComboDamage(ComboTarget), DamageType.Magical, Color.Lime));
        }
    }
}

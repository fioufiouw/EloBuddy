using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using System.Drawing;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class DrawSpells : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        private bool HasFlash = false;
        private bool HasIgnite = false;
        public override void MenuCreate()
        {
            mMenu = Menu.AddSubMenu("Drawings", "drawings");
            mMenu.AddCheckBox("drawq", "Draw Q");
            mMenu.AddCheckBox("draww", "Draw W");
            mMenu.AddCheckBox("drawe", "Draw E");
            mMenu.AddCheckBox("drawr", "Draw R");
            if (HasFlash == true)
                mMenu.AddCheckBox("drawflash", "Draw Flash");
            if (HasIgnite == true)
                mMenu.AddCheckBox("drawignite", "Draw Ignite");

        }
        public override void OnLoad()
        {
            var flashslot = GetFlashSpellSlot();
            var igniteslot = GetIgniteSpellSlot();

            if (flashslot != SpellSlot.Unknown && Player.CanUseSpell(flashslot) == SpellState.Ready)
                HasFlash = true;
            if (igniteslot != SpellSlot.Unknown && Player.CanUseSpell(igniteslot) == SpellState.Ready)
                HasIgnite = true;

            MenuIndex.Drawings drawings = new MenuIndex.Drawings();

            var flashrange = 425;
            var igniterange = 600;

            Drawing.OnDraw += delegate
            {
                #region Q
                if (drawings.DrawQ)
                {
                    if (Spells.Q.IsReady())
                        new Circle() { BorderWidth = 2, Color = Color.Green, Radius = Spells.Q.Range }.Draw(Player.Instance.Position);
                    if (Spells.Q.IsOnCooldown)
                        new Circle() { BorderWidth = 2, Color = Color.Orange, Radius = Spells.Q.Range }.Draw(Player.Instance.Position);
                    if (!Spells.Q.IsLearned)
                        new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.Q.Range }.Draw(Player.Instance.Position);
                }
                #endregion

                #region W
                if (drawings.DrawW)
                {
                    if (Spells.W.IsReady())
                        new Circle() { BorderWidth = 2, Color = Color.Green, Radius = Spells.W.Range }.Draw(Player.Instance.Position);
                    if (Spells.W.IsOnCooldown)
                        new Circle() { BorderWidth = 2, Color = Color.Orange, Radius = Spells.W.Range }.Draw(Player.Instance.Position);
                    if (!Spells.W.IsLearned)
                        new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.W.Range }.Draw(Player.Instance.Position);
                }
                #endregion

                #region E
                if (drawings.DrawE)
                {
                    if (Spells.E.IsReady())
                        new Circle() { BorderWidth = 2, Color = Color.Green, Radius = Spells.E.Range }.Draw(Player.Instance.Position);
                    if (Spells.E.IsOnCooldown)
                        new Circle() { BorderWidth = 2, Color = Color.Orange, Radius = Spells.E.Range }.Draw(Player.Instance.Position);
                    if (!Spells.E.IsLearned)
                        new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.E.Range }.Draw(Player.Instance.Position);
                }
                #endregion

                #region R
                if (drawings.DrawR)
                {
                    if (!Spells.R.IsOnCooldown)
                        new Circle() { BorderWidth = 2, Color = Color.Green, Radius = Spells.R.Range }.Draw(Player.Instance.Position);
                    if (Spells.R.IsOnCooldown)
                        new Circle() { BorderWidth = 2, Color = Color.Orange, Radius = Spells.R.Range }.Draw(Player.Instance.Position);
                    if (!Spells.R.IsLearned)
                        new Circle() { BorderWidth = 2, Color = Color.Red, Radius = Spells.R.Range }.Draw(Player.Instance.Position);
                }
                #endregion

                #region Summoners
                #region Flash
                if (drawings.DrawFlash)
                {
                    if (Player.CanUseSpell(flashslot) == SpellState.Ready)
                        new Circle() { BorderWidth = 1, Color = Color.GreenYellow, Radius = flashrange }.Draw(Player.Instance.Position);
                    if (Player.CanUseSpell(flashslot) == SpellState.Cooldown)
                        new Circle() { BorderWidth = 1, Color = Color.DarkRed, Radius = flashrange }.Draw(Player.Instance.Position);
                }
                #endregion

                #region Ignite
                if (drawings.DrawIgnite)
                {
                    if (Player.CanUseSpell(igniteslot) == SpellState.Ready)
                        new Circle() { BorderWidth = 1, Color = Color.GreenYellow, Radius = igniterange }.Draw(Player.Instance.Position);
                    if (Player.CanUseSpell(igniteslot) == SpellState.Cooldown)
                        new Circle() { BorderWidth = 1, Color = Color.DarkRed, Radius = igniterange }.Draw(Player.Instance.Position);
                }
                #endregion

                #endregion
            };
        }
        public override ModuleType GetModuleType()
        {
            return ModuleType.Other;
        }

        private static SpellSlot GetFlashSpellSlot()
        {
            if (Player.GetSpell(SpellSlot.Summoner1).Name == "summonerflash")
                return SpellSlot.Summoner1;
            if (Player.GetSpell(SpellSlot.Summoner2).Name == "summonerflash")
                return SpellSlot.Summoner2;
            return SpellSlot.Unknown;
        }

        private static SpellSlot GetIgniteSpellSlot()
        {
            if (Player.GetSpell(SpellSlot.Summoner1).Name == "summonerignite")
                return SpellSlot.Summoner1;                           
            if (Player.GetSpell(SpellSlot.Summoner2).Name == "summonerignite")
                return SpellSlot.Summoner2;
            return SpellSlot.Unknown;
        }
    }
}

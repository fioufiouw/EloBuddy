using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using TheShadow;
using SharpDX;

namespace TheShadow
{
    internal class MainShadow
    {
        public const bool wUsed = false;
        public const bool rUsed = false;
        public static Menu Menu, ComboMenu, FarmMenu, HarassMenu, DrawMenu;
        private static string VERS = "0.1ALPH";
        public static AIHeroClient myHero;
        public static Spell.Skillshot _Q;
        public static Spell.Skillshot _W;
        public static Spell.Skillshot _E;
        public static Spell.Targeted _R;
        public static Spell.Active _Flash;
        public static Obj_AI_Base sender;
        public static Obj_AI_BaseBuffGainEventArgs buff;
        public static float myMana;
        public static Vector3 mousePos { get { return Game.CursorPos; } }
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Hacks.AntiAFK = true;
            Bootstrap.Init(null);
            myMana = ObjectManager.Player.Mana;
            _Flash = new Spell.Active(SpellSlot.Summoner1, 425);
            _Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear);
            _W = new Spell.Skillshot(SpellSlot.W, 550, SkillShotType.Linear);
            _E = new Spell.Skillshot(SpellSlot.E, 290, SkillShotType.Circular);
            _R = new Spell.Targeted(SpellSlot.R, 675);

            Menu = MainMenu.AddMenu("The Shadow", "theshadow");
            Menu.AddGroupLabel("The Shadow");
            Menu.AddLabel("By Buddy");
            Menu.AddSeparator();
            Menu.AddLabel("VERS=0.2BETA RELEASE 1");

            ComboMenu = Menu.AddSubMenu("Combo Menu", "combomenu");

            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("useQ", new CheckBox("Use Q"));
            ComboMenu.Add("useW", new CheckBox("Use W"));
            ComboMenu.Add("useE", new CheckBox("Use E"));
            ComboMenu.Add("useR", new CheckBox("Use R"));

            DrawMenu = Menu.AddSubMenu("Drawings Menu", "drawingsmenu");

            DrawMenu.AddGroupLabel("Drawings Menu");
            DrawMenu.Add("drawQ", new CheckBox("Draw Q"));
            DrawMenu.Add("drawW", new CheckBox("Draw W"));
            DrawMenu.Add("drawE", new CheckBox("Draw E"));
            DrawMenu.Add("drawR", new CheckBox("Draw R"));
            DrawMenu.Add("drawText", new CheckBox("Draw Text"));
            DrawMenu.Add("drawFlash", new CheckBox("Draw Flash"));

            HarassMenu = Menu.AddSubMenu("Harass Menu", "harassmenu");

            HarassMenu.AddGroupLabel("Harass Menu");
            HarassMenu.Add("hUseQ", new CheckBox("Use Q"));
            HarassMenu.Add("hUseW", new CheckBox("Use W"));
            HarassMenu.Add("hUseE", new CheckBox("Use E"));
            
            FarmMenu = Menu.AddSubMenu("Farm Menu", "farmmenu");

            FarmMenu.AddLabel("Wave Clear");
            FarmMenu.Add("fwUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("fwUseE", new CheckBox("Use E"));

            FarmMenu.AddSeparator();

            FarmMenu.AddLabel("Last Hit");
            FarmMenu.Add("flUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("flUseE", new CheckBox("Use E"));
            
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Draw.OnDraw;

            Chat.Print("<font color='#00FF00'>The Shadow by Buddy Loaded</font>");
            Chat.Print("<font color='#00FF00'>VERS=</font><font color='#ffffff'>0.2BETA RELEASE 1</font>");
        }

        private static void Game_OnTick(EventArgs args)
        {

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                OrbManager.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                OrbManager.Harass();
            }
            /*if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                OrbManager.WaveClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                OrbManager.LastHit();
            }*/
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                OrbManager.Flee();
            }
        }
    }
}
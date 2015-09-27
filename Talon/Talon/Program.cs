using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Constants;
using EntityManager = EloBuddy.SDK.EntityManager;
using Geometry = EloBuddy.SDK.Geometry;
using EloBuddy.SDK.Properties;
using SharpDX;

namespace Talon
{
    internal class Program
    {
        public static Vector3 _MousePos = (Game.CursorPos);
        public static Spell.Active _Q;
        public static Spell.Skillshot _W, _R;
        public static Spell.Targeted _E;
        public static SpellSlot IgniteSlot;
        public static readonly AIHeroClient _Player = (ObjectManager.Player);
        public static Menu Menu, ComboMenu, KSMenu, HarassMenu, FarmMenu, FleeMenu, DrawMenu;
        static void Main(string[] args)
        {
            Console.WriteLine("Talon Injected Succesfully");
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (_Player.Hero != Champion.Talon)
            {
                Chat.Print("<font color='#ff0000'>ERROR 0: FAILED TO LAUNCH");
                return;
            }

            _Q = new Spell.Active(SpellSlot.Q);
            _W = new Spell.Skillshot(SpellSlot.W, 600, SkillShotType.Cone, 250, null, 25);
            _E = new Spell.Targeted(SpellSlot.E, 700);
            _R = new Spell.Skillshot(SpellSlot.R, 500, SkillShotType.Circular);

            //IgniteSlot = Player.Spells.FirstOrDefault
            //             (o => o.SData.Name.ToLower().Contains("summonerdot")).Slot;

            Menu = MainMenu.AddMenu("TalonBuddy", "talonbuddy");

            Menu.AddGroupLabel("TalonBuddy");
            Menu.AddLabel("By Buddy - Feel free to donate me a monitor :^)");

            ComboMenu = Menu.AddSubMenu("::Combo Menu", "combomenu");

            ComboMenu.AddGroupLabel("Combo Menu");
            ComboMenu.Add("cUseQ", new CheckBox("Use Q"));
            ComboMenu.Add("cUseW", new CheckBox("Use W"));
            ComboMenu.Add("cUseE", new CheckBox("Use E"));
            ComboMenu.Add("cUseR", new CheckBox("Use R"));
            ComboMenu.Add("cUseAA", new CheckBox("Use AA"));

            HarassMenu = Menu.AddSubMenu("::Harass Menu", "harassmenu");

            HarassMenu.AddGroupLabel("Harass Menu");
            HarassMenu.Add("hUseW", new CheckBox("Use W"));
            HarassMenu.Add("hUseE", new CheckBox("Use E"));

            FleeMenu = Menu.AddSubMenu("::Flee Menu", "fleemenu");

            FleeMenu.AddGroupLabel("Flee Menu");
            FleeMenu.Add("fUseE", new CheckBox("Use E"));
            FleeMenu.Add("fUseR", new CheckBox("Use R", false));
            
            FarmMenu = Menu.AddSubMenu("::Farm Menu", "farmmenu");

            FarmMenu.AddGroupLabel("Farm Menu");

            FarmMenu.AddLabel("Last Hit");
            FarmMenu.Add("fLHUseQ", new CheckBox("Use Q", true));
            FarmMenu.Add("fLHUseW", new CheckBox("Use W", true));
            FarmMenu.Add("fLHUseE", new CheckBox("Use E", true));

            FarmMenu.AddSeparator();

            FarmMenu.AddLabel("Lane Clear");
            FarmMenu.Add("fLCUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("fLCUseW", new CheckBox("Use W"));
            FarmMenu.Add("fLCUseE", new CheckBox("Use E"));
            
            KSMenu = Menu.AddSubMenu("::KS Menu", "ksmenu");

            KSMenu.AddGroupLabel("Ks Menu");
            KSMenu.Add("ksUseQ", new CheckBox("Use Q"));
            KSMenu.Add("ksUseW", new CheckBox("Use W"));
            KSMenu.Add("ksUseE", new CheckBox("Use E"));
            KSMenu.Add("ksUseI", new CheckBox("Use Ignite"));

            DrawMenu = Menu.AddSubMenu("::Draw Menu", "drawmenu");

            DrawMenu.AddGroupLabel("Draw Menu");
            DrawMenu.Add("DrawQ", new CheckBox("Draw Q"));
            DrawMenu.Add("DrawW", new CheckBox("Draw W"));
            DrawMenu.Add("DrawE", new CheckBox("Draw E"));
            DrawMenu.Add("DrawR", new CheckBox("Draw R"));

            //Calls...
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Draw.Drawing_OnDraw;

            Chat.Print("Talon Initialized Succesfully - Version 0.2");
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
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                //OrbManager.WaveClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                OrbManager.LastHit();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                OrbManager.Flee();
            }
        }
    }
}

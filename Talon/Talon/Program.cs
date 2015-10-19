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
using SpellData = Talon.DamageIndicator.SpellData;
using EntityManager = EloBuddy.SDK.EntityManager;
using Geometry = EloBuddy.SDK.Geometry;
using Color = System.Drawing.Color;
using EloBuddy.SDK.Properties;
using SharpDX;

namespace Talon
{
    internal class Program
    {
        public static GameObject BladeObject;
        public static Vector3 _MousePos = (Game.CursorPos);
        public static Spell.Active _Q;
        public static Spell.Skillshot _W, _R;
        public static Spell.Targeted _E, _Ignite;
        public static SpellSlot IgniteSlot;
        public static readonly AIHeroClient _Player = (ObjectManager.Player);
        public static Menu Menu, ComboMenu, KSMenu, HarassMenu, FarmMenu, FleeMenu, DrawMenu, ItemMenu, LogicMenu, ManaMenu;
        public static Item _tiamat, _hydra, _blade, _bilge, _rand, _lotis, _youmuu, _botrk, _manapot;
        public static DamageIndicator.DamageIndicator Indicator;

        static void Main(string[] args)
        {
            Console.WriteLine("Talon Injected Succesfully");
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (_Player.Hero != Champion.Talon)
            {
                Chat.Print("<font color='#ff0000'>ERROR 0: FAILED TO LAUNCH</font>");
                return;
            }

            _Q = new Spell.Active(SpellSlot.Q);
            _W = new Spell.Skillshot(SpellSlot.W, 600, SkillShotType.Cone, 250, null, 25);
            _E = new Spell.Targeted(SpellSlot.E, 700);
            _R = new Spell.Skillshot(SpellSlot.R, 500, SkillShotType.Circular);

            IgniteSlot = _Player.GetSpellSlotFromName("SummonerDot");

            _bilge = new Item(3144, 475f);
            _blade = new Item(3153, 425f);
            _hydra = new Item(3074, 250f);
            _tiamat = new Item(3077, 250f);
            _rand = new Item(3143, 490f);
            _lotis = new Item(3190, 590f);
            _youmuu = new Item(3142, 10);
            _botrk = new Item(3153, 550f);
            _manapot = new Item(2004, 10);

            Menu = MainMenu.AddMenu("Talon", "talon");

            Menu.AddGroupLabel("Talon");
            Menu.AddLabel("By Buddy - Feel free to donate me a monitor :^)");

            Menu.AddSeparator();

            Menu.Add("gobalDelay", new Slider("Gobal Delay (ms)", 5, 0, 100));

            ComboMenu = Menu.AddSubMenu("::Combo Menu", "combomenu");

            ComboMenu.AddGroupLabel("Combo Menu");
            ComboMenu.Add("cUseQ", new CheckBox("Use Q"));
            ComboMenu.Add("cUseW", new CheckBox("Use W"));
            ComboMenu.Add("cUseE", new CheckBox("Use E"));
            ComboMenu.Add("cUseR", new CheckBox("Use R"));

            ComboMenu.AddSeparator();

            ComboMenu.Add("cUseYoumuu", new CheckBox("Use Youmuu's"));
            ComboMenu.Add("cUseTiamat", new CheckBox("Use Tiamat"));
            ComboMenu.Add("cUseHydra" , new CheckBox("Use Hydra"));
            ComboMenu.Add("cUseBilge" , new CheckBox("Use Bilge"));
            ComboMenu.Add("cUseBotrk" , new CheckBox("Use Botrk"));

            HarassMenu = Menu.AddSubMenu("::Harass Menu", "harassmenu");

            HarassMenu.AddGroupLabel("Harass Menu");
            HarassMenu.Add("hUseW", new CheckBox("Use W"));
            HarassMenu.Add("hUseE", new CheckBox("Use E"));

            FleeMenu = Menu.AddSubMenu("::Flee Menu", "fleemenu");

            FleeMenu.AddGroupLabel("Flee Menu");
            FleeMenu.Add("fUseE"     , new CheckBox("Use E"));
            FleeMenu.Add("fUseR"     , new CheckBox("Use R", false));
            FleeMenu.Add("fUseYoumuu", new CheckBox("Use Youmuu's", false));

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
            DrawMenu.Add("DrawW"          , new CheckBox("Draw W"));
            DrawMenu.Add("DrawE"          , new CheckBox("Draw E"));
            DrawMenu.Add("DrawR"          , new CheckBox("Draw R"));
            DrawMenu.Add("DrawComboCircle", new CheckBox("Draw Combo Circle"));
            DrawMenu.Add("DrawComboLine"  , new CheckBox("Draw Combo Line"));
            DrawMenu.Add("DrawHPBarCombo" , new CheckBox("Draw HP Bar Combo Damage"));
            DrawMenu.Add("DrawText"       , new CheckBox("Draw Text Combo Ready"));
            DrawMenu.Add("DrawMana"       , new CheckBox("Draw Calculated Mana"));
            ItemMenu = Menu.AddSubMenu("::Item Menu", "itemmenu");

            ItemMenu.AddGroupLabel("Item Menu");
            ItemMenu.Add("useHydra" , new CheckBox("Use Hydra"));
            ItemMenu.Add("useYoumuu", new CheckBox("Use Youmuu's"));
            ItemMenu.Add("useTiamat", new CheckBox("Use Tiamat"));
            ItemMenu.Add("useBilge" , new CheckBox("Use Bilge"));
            ItemMenu.Add("useBotrk" , new CheckBox("Use Botrk"));

            LogicMenu = Menu.AddSubMenu("::Logic Menu", "logicmenu");
            
            LogicMenu.AddGroupLabel("Logic Menu");
            LogicMenu.Add("eUseHealthCheck" , new CheckBox("Only Use E if Health is Above Amount"));
            LogicMenu.Add("eUseHealthSlider", new Slider  ("Health:", 250, 50, 1250));

            LogicMenu.AddSeparator();

            LogicMenu.Add("DrawRCastCircle"     , new CheckBox("Draw R Recommended Cast Range Circle"));
            LogicMenu.Add("DrawRCastBufferRange", new Slider("Buffer Range:", 0, -100, 100));

            ManaMenu = Menu.AddSubMenu("::Mana Menu", "manamenu");

            ManaMenu.AddGroupLabel("Mana Menu");
            ManaMenu.Add("useManaPotion", new Slider("Use Mana Pot if Mana: --not working yet", 150, 0, (int) _Player.Mana));
            ManaMenu.Add("waveClearMana", new Slider("Dont Cast Waveclear Spells if Mana:", 75, 0, 150));
            
            //LogicMenu.AddSeparator();

            //LogicMenu.Add("DrawRecommendedRRange", new CheckBox("Draw Recommended R Cast Range"));

            Indicator = new DamageIndicator.DamageIndicator();
            Indicator.Add("Combo", new SpellData(0, DamageType.True, Color.Lime));

            //Calls...
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Draw.Drawing_OnDraw;

            //BladeObject = ObjectManager.Get<GameObject>().FirstOrDefault(obj => obj.Name != null && obj.IsValid && obj.Name.ToLower().Contains("doomball"));

            Chat.Print("Talon Initialized Succesfully - Version 0.2");
        }

        private static void Game_OnTick(EventArgs args)
        {
            Indicator.Update("Combo", new SpellData((int)Damage.AvailableComboDamage(), DamageType.Physical, Color.Lime));

            OrbManager.KillSteal();

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
                OrbManager.LaneClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                OrbManager.LastHit();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                OrbManager.Flee();
            }

            if (ManaMenu["useManaPotion"].Cast<Slider>().CurrentValue >= _Player.Mana)
            {
                if (_manapot.IsOwned() && _Player.HasBuff("mana_potion") && _manapot.IsReady())
                {
                    _manapot.Cast();
                }
            }
        }
    }
}

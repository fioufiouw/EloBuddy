using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SpellData = Azir.DamageIndicator.SpellData;
using Color = System.Drawing.Color;
using SharpDX;

namespace Azir
{
    class Program
    {
        public static AIHeroClient _Player = ObjectManager.Player;
        public static Spell.Skillshot _Q, _W, _E, _R, _SoldierAARange;
        public static Spell.Targeted _Ignite;
        public static Menu Menu, DrawMenu, ComboMenu, FarmMenu, KSMenu, FleeMenu, HarassMenu, ItemMenu, ManaMenu;
        public static Vector3 _CursorPos = Game.CursorPos;
        public static Item _Zhonyas, _ManaPot, _HealthPot;
        public static DamageIndicator.DamageIndicator Indicator;
        static float LastGapclose = 0f;
        static void Main(string[] args)
        {
            Console.WriteLine("Azir Injected Succesfully");
            Bootstrap.Init(null);
            Interrupter.Initialize();
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (_Player.Hero != Champion.Azir)
                return;

            var slot = _Player.GetSpellSlotFromName("summonerdot");

            _Q = new Spell.Skillshot(SpellSlot.Q, 875, SkillShotType.Linear, 0, 1600, 70);
            _W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 0);
            _E = new Spell.Skillshot(SpellSlot.E, 1250, SkillShotType.Linear, 0, 1700, 100);
            _R = new Spell.Skillshot(SpellSlot.R, 450, SkillShotType.Linear, 1, 1400, 0);

            if (slot != SpellSlot.Unknown)
            {
                _Ignite = new Spell.Targeted(slot, 600);
            }

            //Just a placeholder...
            _SoldierAARange = new Spell.Skillshot(SpellSlot.Unknown, 320, SkillShotType.Linear);

            _Zhonyas = new Item(3157);
            _ManaPot = new Item(2004);
            _HealthPot = new Item(2003);

            Menu = MainMenu.AddMenu("Azir", "azir");

            Menu.AddGroupLabel("Azir");
            Menu.AddLabel("By Buddy");

            Menu.AddSeparator();

            //Menu.Add("mGobalDelay", new Slider("Gobal Delay (ms)", 5, 0, 100));

            //Menu.AddSeparator();

            //Menu.Add("mCheckHitChance", new CheckBox("Check Hitchance"));
            //Menu.Add("mMinimumHitChance", new Slider("HitChance: Low|Medium|High", 1, 1, 3));

            //Menu.AddSeparator();

            Menu.Add("mUseItems", new CheckBox("Use Items"));
            Menu.Add("mUseHealthPot", new Slider("Use Health Pot At %:", 20, 0, 100));
            
            Menu.AddSeparator();

            Menu.Add("mDamageBuffer", new Slider("Damage Calculations Buffer %:", 90, 10, 100));
            Menu.AddLabel("Only Change this if you know what you're doing");

            Menu.AddSeparator();

            //Menu.Add("mSkinID", new Slider("Skin ID", 1, 1, 2));
            Menu.Add("mUseROnInterrupt", new CheckBox("Use R To Interrupt"));
            Menu.Add("mAutoIgnite", new CheckBox("Auto Ignite Killable"));

            DrawMenu = Menu.AddSubMenu("Draw", "draw");

            DrawMenu.AddGroupLabel("Draw Menu");
            DrawMenu.Add("dDrawQ", new CheckBox("Draw Q"));
            DrawMenu.Add("dDrawLines", new CheckBox("Draw Lines"));
            DrawMenu.Add("dDrawW", new CheckBox("Draw W"));
            DrawMenu.Add("dDrawE", new CheckBox("Draw E"));
            DrawMenu.Add("dDrawR", new CheckBox("Draw R"));
            DrawMenu.Add("dDrawWRange", new CheckBox("Draw W Range"));
            DrawMenu.Add("dDrawWCommandRange", new CheckBox("Draw W Command Range"));
            DrawMenu.Add("dDrawComboIndicator", new CheckBox("Draw Combo Indicator"));
            DrawMenu.Add("dDrawText", new CheckBox("Draw Text"));
            DrawMenu.Add("dDrawManaUsage", new CheckBox("Draw Q-W-E-R Mana Usage"));

            ComboMenu = Menu.AddSubMenu("Combo", "combo");

            ComboMenu.AddGroupLabel("Combo Menu");
            ComboMenu.Add("cUseQ", new CheckBox("Use Q"));
            ComboMenu.Add("cUseW", new CheckBox("Use W"));
            ComboMenu.Add("cUseE", new CheckBox("Use E"));
            ComboMenu.Add("cUseEGC", new CheckBox("Use E To Gapclose"));
            ComboMenu.Add("cUseR", new CheckBox("Use R"));
            ComboMenu.Add("cUseAA", new CheckBox("Use AA"));
            ComboMenu.Add("cUseIgnite", new CheckBox("Use Ignite"));

            HarassMenu = Menu.AddSubMenu("Harass", "harass");

            HarassMenu.AddGroupLabel("Harass Menu");
            HarassMenu.Add("hUseQ", new CheckBox("Use Q"));
            HarassMenu.Add("hUseW", new CheckBox("Use W"));
            HarassMenu.Add("hUseAA", new CheckBox("Use AA"));
            
            FarmMenu = Menu.AddSubMenu("Farm", "farm");

            FarmMenu.AddGroupLabel("Farm Menu");

            FarmMenu.AddSeparator();

            FarmMenu.AddLabel("Last Hit");
            FarmMenu.Add("fLHUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("fLHUseW", new CheckBox("Use W"));
            FarmMenu.Add("fLHUseWHealth", new Slider("Use W When Minion Healh %:", 50, 0, 100));

            FarmMenu.AddSeparator();

            FarmMenu.AddLabel("Lane Clear");
            FarmMenu.Add("fLCUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("fLCUseW", new CheckBox("Use W"));
            FarmMenu.Add("fLCWaitHealth", new Slider("Wait for minion at %:", 10, 0, 100));
            FarmMenu.Add("fLCUseWHealth", new Slider("Use W When Minion Healh %:", 80, 0, 100));

            KSMenu = Menu.AddSubMenu("KillSteal", "killsteal");

            KSMenu.AddGroupLabel("KS Menu");
            KSMenu.Add("ksUseWQ", new CheckBox("Use W-Q"));
            KSMenu.Add("ksUseE", new CheckBox("Use E To Gapclose"));
            KSMenu.Add("ksUseR", new CheckBox("Use R"));
            KSMenu.Add("ksUseIgnite", new CheckBox("Use Ignite"));

            FleeMenu = Menu.AddSubMenu("Flee", "flee");

            FleeMenu.AddGroupLabel("Flee Menu");
            FleeMenu.Add("fUseWE", new CheckBox("Use W-E"));
            FleeMenu.Add("fUseQ", new CheckBox("Use W-Q-E"));
            FleeMenu.Add("fUseR", new CheckBox("Use R"));
            FleeMenu.Add("fUseRHealth", new Slider("Use R if Health <", 100, 0, 500));

            ItemMenu = Menu.AddSubMenu("Items", "items");

            ItemMenu.AddGroupLabel("Items Menu");
            ItemMenu.Add("iUseZhonyas", new CheckBox("Use Zhonya's"));
            ItemMenu.Add("iUseZhonyasHealth", new Slider("Use Zhonya's at Health:", 250, 50, 750));

            Indicator = new DamageIndicator.DamageIndicator();
            Indicator.Add("Combo", new SpellData(0, DamageType.True, Color.Lime));

            ManaMenu = Menu.AddSubMenu("Mana", "mana");
            
            ManaMenu.AddGroupLabel("Mana Menu");
            ManaMenu.Add("mUsePot", new Slider("Use Mana Pot At %:", 35, 0, 100));
            ManaMenu.Add("mManaCalculations", new CheckBox("Do Mana Calculations"));

            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Drawing.OnDraw += Draw.Drawing_OnDraw;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;

            Chat.Print("<font color='#32cd32'>Azir Built Successfully - {0} {1}</font>", DateTime.Now.ToString("h:mm:ss tt"), DateTime.Now.ToString("d/M/yyyy"));
        }
        private static void Game_OnTick(EventArgs args)
        {
            //Core.DelayAction(() => Game_OnTick(args), Menu["mGobalDelay"].Cast<Slider>().CurrentValue);

            var _Target = TargetSelector.GetTarget(1500, DamageType.Magical);

            if (DrawMenu["dDrawComboIndicator"].Cast<CheckBox>().CurrentValue)
            {
                Indicator.Update("Combo",
                    new SpellData((int) Damage.GetComboDamage(_Target), DamageType.Magical, Color.Lime));
            }
            OrbManager.ItemManager();
            OrbManager.KillSteal();
            OrbManager.AutoIgnite();

            Mana.ManaManager();

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
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base target, Interrupter.InterruptableSpellEventArgs args)
        {
            if (Menu["mUseROnInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                if (target != null && target.IsValid)
                {
                    if (_R.IsInRange(target) && _R.IsReady())
                    {
                        _R.Cast(target);
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (args.Sender.Team == _Player.Team)
            {
                var target = TargetSelector.GetTarget(_W.Range, DamageType.Magical, _Player.Position);
                if (true)
                {
                    _W.Cast(args.Sender);
                    if (Orbwalker.ValidAzirSoldiers.Count > 0)
                    {
                        foreach (var soldier in Orbwalker.ValidAzirSoldiers)
                        {
                            _E.Cast(soldier);
                        }
                    }
                }
            }
        }
    }
}

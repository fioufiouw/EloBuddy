using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KataBuddy.Drawings
{
    class _Menu
    {
        public static Menu Menu, ComboMenu, FleeMenu, HarassMenu, FarmMenu, BrainMenu, DrawMenu, KSMenu;
        public static void Initialize()
        {
            Menu = MainMenu.AddMenu("Buddy Series", "buddyseries");
            DrawMainMenu();

            ComboMenu = Menu.AddSubMenu("Combo");
            DrawComboMenu();

            FleeMenu = Menu.AddSubMenu("Flee");
            DrawFleeMenu();

            HarassMenu = Menu.AddSubMenu("Harass");
            DrawHarassMenu();

            FarmMenu = Menu.AddSubMenu("Farm");
            DrawFarmMenu();

            BrainMenu = Menu.AddSubMenu("Brain");
            DrawBrainMenu();

            DrawMenu = Menu.AddSubMenu("Draw");
            DrawDrawMenu();

            KSMenu = Menu.AddSubMenu("KillSteal");
            DrawKSMenu();
        }
        private static void DrawMainMenu()
        {
            Menu.AddGroupLabel("Buddy Series");
            Menu.AddLabel("Katarina");

            Menu.AddSeparator();

            Menu.Add("mFullUlt", new CheckBox("Wait Until Full Ult Casted", false));
            Menu.Add("mDbg", new CheckBox("Enable Debug Options", false));
        }
        private static void DrawComboMenu()
        {
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.Add("cUseQ", new CheckBox("Use Q"));
            ComboMenu.Add("cUseW", new CheckBox("Use W"));
            ComboMenu.Add("cUseE", new CheckBox("Use E"));
            ComboMenu.Add("cUseR", new CheckBox("Use R"));
            ComboMenu.Add("cUseIgnite", new CheckBox("Use Ignite"));
        }
        private static void DrawFleeMenu()
        {
            FleeMenu.AddGroupLabel("Flee");
            FleeMenu.Add("flMinionJump", new CheckBox("Jump To Minion"));
            FleeMenu.Add("flWardJump", new CheckBox("Jump To Ward", false));
            FleeMenu.Add("flTowerDive", new CheckBox("Dont E Into Tower"));
        }
        private static void DrawHarassMenu()
        {
            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.Add("hUseQ", new CheckBox("Use Q"));
            HarassMenu.Add("hUseW", new CheckBox("Use W"));
        }
        private static void DrawFarmMenu()
        {
            FarmMenu.AddGroupLabel("Farm");
            
            FarmMenu.AddSeparator();

            FarmMenu.AddLabel("Last Hit");
            FarmMenu.Add("fLhUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("fLhUseW", new CheckBox("Use W"));
            FarmMenu.Add("fLhUseE", new CheckBox("Use E"));
            FarmMenu.Add("fLhTowerDive", new CheckBox("Dont E Into Tower"));

            FarmMenu.AddSeparator();

            FarmMenu.AddLabel("Lane Clear");
            FarmMenu.Add("fLcUseQ", new CheckBox("Use Q"));
            FarmMenu.Add("fLcUseW", new CheckBox("Use W"));
            FarmMenu.Add("fLcUseE", new CheckBox("Use E"));
            FarmMenu.Add("fLcTowerDive", new CheckBox("Dont E Into Tower"));

        }
        private static void DrawBrainMenu()
        {
            BrainMenu.AddGroupLabel("The Brain");
            BrainMenu.Add("bAutomaticLogic", new CheckBox("Use Automatic Logic"));
            BrainMenu.AddLabel("^^ Uncheck to use custom logic");
            BrainMenu.AddSeparator();

            BrainMenu.Add("bStopRComboHealth", new Slider("Stop Ult in Combo Health", 40, 0, 100));
            BrainMenu.Add("bStopRComboCount", new Slider("Only Stop Combo if ... Are Below Health", 2, 1, 5));
        }

        private static void DrawKSMenu()
        {
            KSMenu.AddGroupLabel("KillSteal");
            KSMenu.Add("kUseQ", new CheckBox("Use Q"));
            KSMenu.Add("kUseW", new CheckBox("Use W"));
            KSMenu.Add("kUseE", new CheckBox("Use E"));
            KSMenu.Add("kUseIgnite", new CheckBox("Use Ignite"));
            KSMenu.Add("kUseCancelR", new CheckBox("Calcel R to KS", false));
            KSMenu.Add("kUseCancelRHealth", new Slider("Enemy Health To Ult:", 100, 5, 500));
            KSMenu.Add("kWardJump", new CheckBox("Ward Jump"));
            KSMenu.Add("kSmartKS", new CheckBox("Smart KS"));
        }

        private static void DrawDrawMenu()
        {
            DrawMenu.AddGroupLabel("Draw");
            DrawMenu.Add("dDrawQ", new CheckBox("Draw Q"));
            DrawMenu.Add("dDrawW", new CheckBox("Draw W"));
            DrawMenu.Add("dDrawE", new CheckBox("Draw E"));
            DrawMenu.Add("dDrawR", new CheckBox("Draw R"));
            DrawMenu.Add("dDrawTarget", new CheckBox("Draw Target"));
            DrawMenu.Add("dDrawIndicator", new CheckBox("Draw Indicator"));
            DrawMenu.Add("dDrawLines", new CheckBox("Draw Lines"));
            DrawMenu.Add("dDrawText", new CheckBox("Draw Text"));
            DrawMenu.Add("dDrawRAffected", new CheckBox("Draw Affected By R"));
        }
    }
}

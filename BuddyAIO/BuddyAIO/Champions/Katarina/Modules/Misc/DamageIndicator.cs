using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Drawing;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class DamageIndicator : IModule
    {
        private const int BarWidth = 104;
        private const int LineThickness = 9;

        public delegate float DamageToUnitDelegate(AIHeroClient hero);

        private static DamageToUnitDelegate DamageToUnit { get; set; }


        private static readonly Vector2 BarOffset = new Vector2(1, 0); // -9, 11

        private static System.Drawing.Color _drawingColor;
        public static System.Drawing.Color DrawingColor
        {
            get { return _drawingColor; }
            set { _drawingColor = System.Drawing.Color.FromArgb(170, value); }
        }

        public override void MenuCreate()
        {
            try
            {
                DrawSpells drawspells = new DrawSpells();
                var mMenu = drawspells.mMenu;

                mMenu.AddCheckBox("drawdamageindicator", "Draw Damage Indicator");
                mMenu.AddCheckBox("drawdamageindicator", "Draw Damage Indicator Percentage");
            }
            catch
            {
                Chat.Print("BuddyAIO:: An error has occured!", System.Drawing.Color.Red);
                Console.WriteLine("Exception caught - Code[KATARINA.DAMAGEINDICATOR.MENUCREATE]");
                
            }
        }
        public override void OnLoad()
        {
            try
            {
                MenuIndex.Drawings drawings = new MenuIndex.Drawings();

                var HealthbarEnabled = drawings.DrawDamageIndicator;
                var PercentEnabled = drawings.DrawDamageIndicatorPercentage;

                // Apply needed field delegate for damage calculation
                DamageToUnit = Utility.Damage.GetComboDamage;
                DrawingColor = System.Drawing.Color.Green;
                HealthbarEnabled = true; //TODO:: Check

                // Register event handlers
                Drawing.OnEndScene += delegate
                {
                    if (HealthbarEnabled || PercentEnabled)
                    {
                        foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered))
                        {
                        // Get damage to unit
                        var damage = DamageToUnit(unit);

                        // Continue on 0 damage
                        if (damage <= 0)
                            {
                                continue;
                            }

                            if (HealthbarEnabled)
                            {
                            // Get remaining HP after damage applied in percent and the current percent of health
                            var damagePercentage = ((unit.TotalShieldHealth() - damage) > 0 ? (unit.TotalShieldHealth() - damage) : 0) /
                                                       (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                                var currentHealthPercentage = unit.TotalShieldHealth() / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);

                            // Calculate start and end point of the bar indicator
                            var startPoint = new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + damagePercentage * BarWidth), (int)(unit.HPBarPosition.Y + BarOffset.Y) - 5);
                                var endPoint = new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + currentHealthPercentage * BarWidth) + 1, (int)(unit.HPBarPosition.Y + BarOffset.Y) - 5);

                            // Draw the line
                            Drawing.DrawLine(startPoint, endPoint, LineThickness, DrawingColor);
                            }

                            if (PercentEnabled)
                            {
                            // Get damage in percent and draw next to the health bar
                            Drawing.DrawText(unit.HPBarPosition, System.Drawing.Color.MediumVioletRed, string.Concat(Math.Ceiling((damage / unit.TotalShieldHealth()) * 100), "%"), 10);
                            }
                        }
                    }
                };
            }
            catch
            {
                Chat.Print("BuddyAIO:: An error has occured!", System.Drawing.Color.Red);
                Console.WriteLine("Exception caught - Code[KATARINA.DAMAGEINDICATOR.ONLOAD]");
                
            }
        }
        public override ModuleType GetModuleType()
        {
            return ModuleType.Other;
        }
    }
}

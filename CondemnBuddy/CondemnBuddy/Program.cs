using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace CondemnBuddy
{
    class Program
    {
        private const float CondemnRange = 550;
        private const float CondemnKnockbackRange = 470;
        private static Spell.Skillshot Flash;
        private static Spell.Targeted Condemn;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingOnOnLoadingComplete;
        }

        private static void LoadingOnOnLoadingComplete(EventArgs args)
        {
            if (Extensions.GetFlashSpellSlot() == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[CondemnBuddy] No Flash detected, aborting injection");
                Console.ResetColor();
                return;
            }

            Flash = new Spell.Skillshot(Extensions.GetFlashSpellSlot() ?? default(SpellSlot), 425u, SkillShotType.Circular,
                0);

            Condemn = new Spell.Targeted(SpellSlot.E, (uint)CondemnRange);

            Config.GenerateMenu();

            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += DrawingOnOnDraw;
        }

        private static void DrawingOnOnDraw(EventArgs args)
        {
            var curPos = Game.CursorPos;
            var target = TargetSelector.GetTarget(1000, DamageType.Physical);

            if (target == null)
                return;
            
            if (Config.Menu["keybind"].Cast<KeyBind>().CurrentValue)
            {
                new Circle() {BorderWidth = 1, Color = Color.Yellow, Radius = 50}.Draw(curPos);

                if (Condemn.IsInRange(target))
                {
                    bool isWall =
                        Game.CursorPos.Extend(target,
                            Game.CursorPos.Distance(target) + CondemnKnockbackRange).IsWallOrStructure();

                    Color color = isWall ? Color.Lime : Color.Red;

                    Drawing.DrawLine(Drawing.WorldToScreen(Player.Instance.ServerPosition),
                        Drawing.WorldToScreen(Game.CursorPos), 2, color);
                    Drawing.DrawLine(Drawing.WorldToScreen(Game.CursorPos),
                        Drawing.WorldToScreen(target.ServerPosition), 2, color);

                    new Geometry.Polygon.Rectangle(target.ServerPosition, Game.CursorPos.Extend(target,
                        Game.CursorPos.Distance(target) + CondemnKnockbackRange).To3D(), 50).Draw(color, 2);
                    /*
                    new Circle() {BorderWidth = 2, Color = color, Radius = 50}.Draw(
                        Game.CursorPos.Extend(target,
                            Game.CursorPos.Distance(target) + CondemnKnockbackRange).To3D());*/
                }
            }

        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!Config.Menu["keybind"].Cast<KeyBind>().CurrentValue)
                return;

            Orbwalker.OrbwalkTo(Game.CursorPos);

            var target = TargetSelector.GetTarget(1000, DamageType.Physical);

            if (target == null)
                return;

            bool isWall =
                Game.CursorPos.Extend(target,
                    Game.CursorPos.Distance(target) + CondemnKnockbackRange).ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall | CollisionFlags.Building);

            if (Condemn.IsInRange(target) && isWall && Flash.IsInRange(Game.CursorPos) && Flash.IsReady())
            {
                Condemn.Cast(target);

                Core.DelayAction(() => Flash.Cast(Game.CursorPos), 60 + Config.Menu["cdelay"].Cast<Slider>().CurrentValue);
            }
        }
    }
}

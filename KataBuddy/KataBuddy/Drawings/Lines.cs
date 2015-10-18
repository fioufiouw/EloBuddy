using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;

namespace KataBuddy.Drawings
{
    internal class Lines
    {
        private static AIHeroClient playerClient = ObjectManager.Player;

        public static void DrawTargetLine(bool value)
        {
            var ComboTarget = TargetSelector.GetTarget(1250, DamageType.Magical);
            if (ComboTarget != null && ComboTarget.IsValid)
            {
                if (value == true)
                {
                    Drawing.DrawLine(ObjectManager.Player.Position.WorldToScreen(),
                        ComboTarget.Position.WorldToScreen(), 1,
                        Color.White);
                }
            }
        }

        /*public static void DrawPredictedBounce(bool value)
        {
            if (value == true)
            {
                var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                    .Where(m => m.Distance(Game.CursorPos) < 100 && m.IsValid && !m.IsDead);

                foreach (var senderminion in minions)
                {
                    var mS = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                        .Where(m => m != senderminion && m.IsValid && !m.IsDead && m.Distance(senderminion) < 650);

                    foreach (var min in mS)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            Drawing.DrawLine(senderminion.Position.WorldToScreen(),
                                min.Position.WorldToScreen(), 1,
                                Color.White);
                        }
                    }
                }
            }
        }*/
    }
}

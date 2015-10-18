using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace KataBuddy.States
{
    class Flee
    {
        private static Vector3 CursorPos = Game.CursorPos;
        public static void MinionJumpInit()
        {
            var minions = EntityManager.MinionsAndMonsters.Get(EntityManager.MinionsAndMonsters.EntityType.Both,
                EntityManager.UnitTeam.Both)
                .OrderBy(m => m.Distance(CursorPos));

            foreach (var minion in minions)
            {
                if (Spells.E.IsReady() && Spells.E.IsInRange(minion))
                {
                    foreach (var turret in EntityManager.Turrets.Enemies)
                    {
                        if (Drawings._Menu.FleeMenu["flTowerDive"].Cast<CheckBox>().CurrentValue)
                        {
                            if (turret == null || minion == null || !minion.IsValid || !turret.IsValid)
                                return;

                            if (!minion.IsInRange(turret, 775))
                            {
                                Spells.E.Cast(minion);
                            }
                        }
                        else
                        {
                            Spells.E.Cast(minion);
                        }
                    }
                }
            }
        }
    }
}

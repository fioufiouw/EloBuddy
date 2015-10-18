using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace KataBuddy.States
{
    class LastHit
    {
        private static AIHeroClient playerClient = ObjectManager.Player;

        public static void QInit()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                .OrderBy(m => m.Health);

            foreach (var minion in minions)
            {
                if (minion != null && minion.IsValid && !minion.IsDead)
                {
                    if (Spells.Q.IsReady() && Spells.Q.IsInRange(minion) &&
                        playerClient.GetSpellDamage(minion, SpellSlot.Q) >= minion.Health)
                    {
                        Spells.Q.Cast(minion);
                    }
                }
            }
        }

        public static void WInit()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                .OrderBy(m => m.Health);

            foreach (var minion in minions)
            {
                if (Spells.W.IsReady() && Spells.W.IsInRange(minion) &&
                    playerClient.GetSpellDamage(minion, SpellSlot.W) >= minion.Health)
                {
                    Spells.W.Cast(minion);
                }
            }
        }

        public static void EInit()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                .OrderBy(m => m.Health);

            foreach (var minion in minions)
            {
                if (playerClient.GetSpellDamage(minion, SpellSlot.E) >= minion.Health)
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(minion))
                    {
                        foreach (var turret in EntityManager.Turrets.Enemies)
                        {
                            if (Drawings._Menu.FarmMenu["fLhTowerDive"].Cast<CheckBox>().CurrentValue)
                            {
                                if (turret == null || minion == null || !minion.IsValid || !turret.IsValid)
                                    return;

                                if (!turret.IsInAutoAttackRange(minion))
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
}

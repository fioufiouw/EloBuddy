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
    internal class LaneClear
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
                    if (minion.Health < playerClient.GetSpellDamage(minion, SpellSlot.Q) + ((5f/100f)*minion.Health) ||
                        playerClient.GetSpellDamage(minion, SpellSlot.Q) >= minion.Health)
                    {
                        if (Spells.Q.IsReady() && Spells.Q.IsInRange(minion))
                        {
                            Spells.Q.Cast(minion);
                        }
                    }
                    else
                    {
                        if (Spells.Q.IsReady() && Spells.Q.IsInRange(minion))
                        {
                            Spells.Q.Cast(minion);
                        }
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
                if (minion.Health < playerClient.GetSpellDamage(minion, SpellSlot.W) + ((5f/100f)*minion.Health) ||
                    playerClient.GetSpellDamage(minion, SpellSlot.W) >= minion.Health)
                {
                    if (Spells.W.IsReady() && Spells.W.IsInRange(minion))
                    {
                        Spells.W.Cast(minion);
                    }
                }
                else
                {
                    if (Spells.W.IsReady() && Spells.W.IsInRange(minion))
                    {
                        Spells.W.Cast(minion);
                    }
                }
            }
        }

        public static void EInit()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy)
                .OrderBy(m => m.Health);

            foreach (var minion in minions)
            {
                if (minion.Health < playerClient.GetSpellDamage(minion, SpellSlot.E) + ((5f/100f)*minion.Health) ||
                    playerClient.GetSpellDamage(minion, SpellSlot.E) >= minion.Health)
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(minion))
                    {
                        foreach (var turret in EntityManager.Turrets.Enemies
                            .OrderBy(t => t.Distance(minion)))
                        {
                            if (turret == null || minion == null || !minion.IsValid || !turret.IsValid)
                                return;

                            if (Drawings._Menu.FarmMenu["fLcTowerDive"].Cast<CheckBox>().CurrentValue)
                            {
                                if (!turret.IsInRange(minion, 775))
                                {
                                    Chat.Print("ohshit");
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
                else
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(minion))
                    {
                        Spells.E.Cast(minion);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace KataBuddy.Utils
{
    internal class KillSteal
    {
        private static AIHeroClient playerClient = ObjectManager.Player;

        public static void Init()
        {
            if (Drawings._Menu.KSMenu["kUseE"].Cast<CheckBox>().CurrentValue)
            {
                EInit();
            }

            if (Drawings._Menu.KSMenu["kUseW"].Cast<CheckBox>().CurrentValue)
            {
                WInit();
            }

            if (Drawings._Menu.KSMenu["kUseQ"].Cast<CheckBox>().CurrentValue)
            {
                QInit();
            }

            if (Drawings._Menu.KSMenu["kUseW"].Cast<CheckBox>().CurrentValue &&
                Drawings._Menu.KSMenu["kUseE"].Cast<CheckBox>().CurrentValue)
            {
                EWInit();
            }

            if (Drawings._Menu.KSMenu["kUseQ"].Cast<CheckBox>().CurrentValue &&
                Drawings._Menu.KSMenu["kUseE"].Cast<CheckBox>().CurrentValue)
            {
                EQInit();
            }

            if (Drawings._Menu.KSMenu["kUseQ"].Cast<CheckBox>().CurrentValue &&
                Drawings._Menu.KSMenu["kUseE"].Cast<CheckBox>().CurrentValue &&
                Drawings._Menu.KSMenu["kUseW"].Cast<CheckBox>().CurrentValue)
            {
                QWEInit();
            }

            if (Drawings._Menu.KSMenu["kUseCancelR"].Cast<CheckBox>().CurrentValue &&
                Drawings._Menu.KSMenu["kUseE"].Cast<CheckBox>().CurrentValue)
            {
                ERInit();
            }

            if (Drawings._Menu.KSMenu["kUseIgnite"].Cast<CheckBox>().CurrentValue)
            {
                IgniteInit();
            }
        }

        private static void EWInit()
        {
            var targetsW = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.W) && !h.IsDead)
                .OrderBy(h => h.Health);

            var targetsEW = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.W) + playerClient.GetSpellDamage(h, SpellSlot.E) && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var targetw in targetsEW)
            {
                if (Spells.E.IsInRange(targetw))
                {
                    if (Spells.E.IsReady())
                    {
                        Spells.E.Cast(targetw);
                    }
                    if (Spells.W.IsInRange(targetw) && Spells.W.IsReady())
                    {
                        Spells.W.Cast(targetw);
                    }
                }
                else
                {
                    foreach (var targetwe in targetsW)
                    {
                        var validminions = EntityManager.MinionsAndMonsters.Get(
                            EntityManager.MinionsAndMonsters.EntityType.Both, EntityManager.UnitTeam.Both)
                            .Where(m => m.IsTargetable && m.IsValid)
                            .OrderBy(m => m.Distance(targetwe));

                        foreach (var minion in validminions)
                        {
                            if (playerClient.IsInRange(targetwe, MiscRanges.GetEWRange()))
                            {
                                if (Spells.E.IsInRange(minion) && Spells.E.IsReady())
                                {
                                    Spells.E.Cast(minion);
                                }

                                if (Spells.W.IsInRange(targetwe) && Spells.W.IsReady())
                                {
                                    Spells.W.Cast(targetwe);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void EQInit()
        {
            var targetsQ = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.Q) && !h.IsDead)
                .OrderBy(h => h.Health);

            var targetsEQ = EntityManager.Heroes.Enemies
                .Where( h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.Q) + playerClient.GetSpellDamage(h, SpellSlot.E) && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var targetq in targetsEQ)
            {
                if (Spells.E.IsInRange(targetq))
                {
                    if (Spells.E.IsReady())
                    {
                        Spells.E.Cast(targetq);
                    }
                    if (Spells.Q.IsInRange(targetq) && Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(targetq);
                    }
                }
                else
                {
                    foreach (var targetqe in targetsQ)
                    {
                        var validminions = EntityManager.MinionsAndMonsters.Get(
                            EntityManager.MinionsAndMonsters.EntityType.Both, EntityManager.UnitTeam.Both)
                            .Where(m => m.IsTargetable && m.IsValid)
                            .OrderBy(m => m.Distance(targetqe));

                        foreach (var minion in validminions)
                        {
                            if (playerClient.IsInRange(targetqe, MiscRanges.GetEWRange()))
                            {
                                if (Spells.E.IsInRange(minion) && Spells.E.IsReady())
                                {
                                    Spells.E.Cast(minion);
                                }

                                if (Spells.Q.IsInRange(targetqe) && Spells.Q.IsReady())
                                {
                                    Spells.Q.Cast(targetqe);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void QInit()
        {
            var targetsQ = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.Q) && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var targetq in targetsQ)
            {
                if (Spells.Q.IsInRange(targetq))
                {
                    if (Spells.Q.IsInRange(targetq) && Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(targetq);
                    }
                }
            }
        }

        private static void EInit()
        {
            var targetsE = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.E) && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var targete in targetsE)
            {
                if (Spells.E.IsInRange(targete))
                {
                    if (Spells.E.IsInRange(targete) && Spells.E.IsReady())
                    {
                        Spells.E.Cast(targete);
                    }
                }
            }
        }

        private static void WInit()
        {
            var targetsW = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.W) && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var targetw in targetsW)
            {
                if (Spells.W.IsInRange(targetw))
                {
                    if (Spells.W.IsInRange(targetw) && Spells.W.IsReady())
                    {
                        Spells.W.Cast(targetw);
                    }
                }
            }
        }

        private static void ERInit()
        {
            var targetsR = EntityManager.Heroes.Enemies
                .Where(h => h.Health < Drawings._Menu.KSMenu["kUseCancelUltHealth"].Cast<Slider>().CurrentValue)
                .OrderBy(h => h.Health);

            var targetsER = EntityManager.Heroes.Enemies
                .Where(h => h.Health < Drawings._Menu.KSMenu["kUseCancelUltHealth"].Cast<Slider>().CurrentValue + playerClient.GetSpellDamage(h, SpellSlot.E))
                .OrderBy(h => h.Health);

            foreach (var targetr in targetsER)
            {
                if (targetr.IsDead && Utils.UltiManager.IsCastingUlt)
                {
                    Player.IssueOrder(GameObjectOrder.MoveTo, playerClient);
                }

                if (Spells.E.IsInRange(targetr))
                {
                    if (Spells.E.IsReady())
                    {
                        Spells.E.Cast(targetr);
                    }
                    if (Spells.R.IsInRange(targetr) && Spells.R.IsReady())
                    {
                        Spells.R.Cast(targetr);
                    }
                }
                else
                {
                    foreach (var targetre in targetsR)
                    {
                        var validminions = EntityManager.MinionsAndMonsters.Get(
                            EntityManager.MinionsAndMonsters.EntityType.Both, EntityManager.UnitTeam.Both)
                            .Where(m => m.IsTargetable && m.IsValid)
                            .OrderBy(m => m.Distance(targetre));

                        foreach (var minion in validminions)
                        {
                            if (playerClient.IsInRange(targetre, MiscRanges.GetEWRange()))
                            {
                                if (Spells.E.IsInRange(minion) && Spells.E.IsReady())
                                {
                                    Spells.E.Cast(minion);
                                }

                                if (Spells.R.IsInRange(targetre) && Spells.R.IsReady())
                                {
                                    Spells.R.Cast(targetre);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void RInit()
        {
            var targetsR = EntityManager.Heroes.Enemies
                .Where(h => h.Health < Drawings._Menu.KSMenu["kUseCancelUltHealth"].Cast<Slider>().CurrentValue && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var targetr in targetsR)
            {
                if (targetr.IsDead && Utils.UltiManager.IsCastingUlt)
                {
                    Player.IssueOrder(GameObjectOrder.MoveTo, playerClient);
                }

                if (Spells.R.IsInRange(targetr))
                {
                    if (Spells.R.IsInRange(targetr) && Spells.R.IsReady())
                    {
                        Spells.R.Cast(targetr);
                    }
                }
            }
        }

        private static void QWEInit()
        {
            var targetsQWE = EntityManager.Heroes.Enemies
                .Where(h => h.Health < playerClient.GetSpellDamage(h, SpellSlot.Q) + playerClient.GetSpellDamage(h, SpellSlot.E) + playerClient.GetSpellDamage(h, SpellSlot.W) && !h.IsDead)
                .OrderBy(h => h.Health);

            foreach (var target in targetsQWE)
            {
                if (Spells.Q.IsInRange(target) && Spells.Q.IsReady())
                {
                    Spells.Q.Cast(target);
                }
                if (Spells.W.IsInRange(target) && Spells.W.IsReady())
                {
                    Spells.W.Cast(target);
                }
                if (Spells.E.IsInRange(target) && Spells.E.IsReady())
                {
                    Spells.E.Cast(target);
                }
            }
        }

        private static void IgniteInit()
        {
            var targetsQWE = EntityManager.Heroes.Enemies
                .Where(h => h.Health < DamageLib.GetIgniteDamage(h) && !h.IsDead)
                .OrderBy(h => h.Health);
        }
    }
}

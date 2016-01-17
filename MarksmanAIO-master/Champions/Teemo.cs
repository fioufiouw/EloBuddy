using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using MarksmanAIO.Menu_Settings;
using SharpDX;
using Color = System.Drawing.Color;

namespace MarksmanAIO.Champions
{
    class Teemo : AIOChampion
    {
        #region Initialize and Declare

        private static Spell.Targeted _q;
        private static Spell.Active _w;
        private static Spell.Skillshot _r;
        private static readonly int[] Rranges = { 300, 600, 900 };
        private bool _rDelay;


        public override void Init()
        {

            try
            {
                //spells
                _q = new Spell.Targeted(SpellSlot.Q, 680);
                _w = new Spell.Active(SpellSlot.W);
                _r = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Circular, 250, 1000, 120)
                {
                    AllowedCollisionCount = int.MaxValue
                };

                //menu

                //combo
                MainMenu.ComboKeys(true, true, false, true);
                MainMenu._combo.AddSeparator();
                MainMenu._combo.AddSlider("combo.w.distance", "Use W when enemy is in {0} range", 600, 1, 1200, true);
                MainMenu._combo.AddSlider("combo.r.stacks", "Keep shrooms at {0} stacks", 1 , 0, 3, true);
                MainMenu._combo.AddGroupLabel("Prediction", "combo.grouplabel.addonmenu", true);
                MainMenu._combo.AddSlider("combo.r.prediction", "Hitchance Percentage for R", 80, 0, 100, true);

                //flee
                MainMenu.FleeKeys(true, true, false, true);
                MainMenu._flee.AddSeparator();
                MainMenu._flee.AddSlider("flee.r.stacks", "Keep shrooms at {0} stacks", 1, 0, 3, true);
                MainMenu._flee.AddGroupLabel("Mana Manager:", "flee.grouplabel.addonmenu", true);
                MainMenu.FleeManaManager(true, true, false, true, 20, 20, 0, 20);

                //lasthit
                MainMenu.LastHitKeys(true, false, false, false);
                MainMenu._lasthit.AddSeparator();
                MainMenu._lasthit.AddGroupLabel("Mana Manager:", "lasthit.grouplabel.addonmenu", true);
                MainMenu.LasthitManaManager(true, false, false, false, 20, 0, 0, 0);


                //laneclear
                MainMenu.LaneKeys(true, false, false, true);
                MainMenu._lane.AddSeparator();
                MainMenu._lane.AddSlider("lane.r.min", "Min. {0} minions for R", 3, 1, 7, true);
                MainMenu._lane.AddSlider("lane.r.stacks", "Keep shrooms at {0} stacks", 1, 0, 3, true);
                MainMenu._lane.AddGroupLabel("Mana Manager:", "lane.grouplabel.addonmenu", true);
                MainMenu.LaneManaManager(true, false, false, true, 60, 0, 0, 60);

                //jungleclear
                MainMenu.JungleKeys(true, false, false, true);
                MainMenu._jungle.AddSeparator();
                MainMenu._jungle.AddSlider("jungle.r.min", "Min. {0} minions for R", 3, 1, 7, true);
                MainMenu._jungle.AddSlider("jungle.r.stacks", "Keep shrooms at {0} stacks", 1, 0, 3, true);
                MainMenu._jungle.AddGroupLabel("Mana Manager:", "jungle.grouplabel.addonmenu", true);
                MainMenu.JungleManaManager(true, false, false, true, 60, 0, 0, 60);

                //harass
                MainMenu.HarassKeys(true, false, false, false);
                MainMenu._harass.AddSeparator();
                MainMenu._harass.AddGroupLabel("Mana Manager:", "harass.grouplabel.addonmenu", true);
                MainMenu.HarassManaManager(true, false, false, false, 60, 0, 0, 0);

                //Ks
                MainMenu.KsKeys(true, false, false, false);
                MainMenu._ks.AddSeparator();
                MainMenu._ks.AddGroupLabel("Mana Manager:", "killsteal.grouplabel.addonmenu", true);
                MainMenu.KsManaManager(true, false, false, false, 20, 0, 0, 0);

                //misc
                MainMenu.MiscMenu();
                MainMenu._misc.AddCheckBox("misc.q.gapcloser", "Use Q on gapcloser", true, true);
                MainMenu._misc.AddSeparator();
                MainMenu._misc.AddGroupLabel("Auto R Settings", "misc.grouplabel1.addonmenu", true);
                MainMenu._misc.AddCheckBox("misc.r.charm", "Use R on Charmed Enemy", true, true);
                MainMenu._misc.AddCheckBox("misc.r.stun", "Use R on Stunned Enemy", true, true);
                MainMenu._misc.AddCheckBox("misc.r.knockup", "Use R on Knocked Enemy", true, true);
                MainMenu._misc.AddCheckBox("misc.r.snare", "Use R on Snared Enemy", true, true);
                MainMenu._misc.AddCheckBox("misc.r.suppression", "Use R on Suppressed Enemy", true, true);
                MainMenu._misc.AddCheckBox("misc.r.taunt", "Use R on Taunted Enemy", true, true);

                //draw
                MainMenu.DrawKeys(true, false, false, true);


            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print(
                    "<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code MENU)</font>");
            }

            try
            {
                Value.Init();
                Game.OnUpdate += GameOnOnUpdate;
                Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
                Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
                Drawing.OnDraw += GameOnDraw;
            }

            catch (Exception e)
            {

                Console.WriteLine(e);
                Chat.Print(
                    "<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code INIT)</font>");
            }

        }
        #endregion

        #region Gamerelated Logic
        public override void Combo()
        {
            if (Value.Use("combo.w") && _w.IsReady())
            {
                if (Player.Instance.CountEnemiesInRange(Value.Get("combo.w.distance")) > 0)
                {
                    _w.Cast();
                }
            }

            if (Value.Use("combo.r") && _r.IsReady() && Rstacks > Value.Get("combo.r.stacks"))
            {
                var targetr = TargetSelector.GetTarget(Rranges[_r.Level - 1], DamageType.Magical);

                if (targetr != null)
                {
                    var rpred = _r.GetPrediction(targetr);
                    if (rpred.HitChancePercent >= Value.Get("combo.r.prediction") && !Orbwalker.IsAutoAttacking && !RPredShroomed(rpred.CastPosition) && !targetr.IsMoving)
                    {
                        _r.Cast(rpred.CastPosition);
                    }
                }

            }
        }

        public override void Harass()
        {
            var target = TargetSelector.GetTarget(_q.Range, DamageType.Magical);

            if (Value.Use("harass.q") && _q.IsReady() && Player.Instance.ManaPercent >= Value.Get("harass.q.mana"))
            {
                if (target != null && !Orbwalker.IsAutoAttacking)
                {
                    _q.Cast(target);
                }
            }

        }

        public override void Laneclear()
        {
            var minionq =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .OrderByDescending(a => a.Health)
                    .FirstOrDefault(a => a.IsValidTarget(_q.Range) && a.Health <= Player.Instance.GetSpellDamage(a, SpellSlot.Q));
            

            if (Value.Use("lane.q") && _q.IsReady() && Player.Instance.ManaPercent >= Value.Get("lane.q.mana"))
            {
                if (minionq != null && !Orbwalker.IsAutoAttacking)
                {
                    _q.Cast(minionq);
                }
            }

            if (_r.IsLearned)
            {

                if (Value.Use("lane.r") && _r.IsReady() && Player.Instance.ManaPercent >= Value.Get("lane.r.mana") && !_rDelay)
                {
                    var minionr = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                        Player.Instance.Position, Rranges[_r.Level - 1]).Where(a => !a.HasBuff("bantamtraptarget"));
                    var farmr = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minionr, _r.Width + 80,
                        Rranges[_r.Level - 1]);

                    if (farmr.HitNumber >= Value.Get("lane.r.min") && !RPredShroomed(farmr.CastPosition) && Rstacks > Value.Get("lane.r.stacks"))
                    {
                        _r.Cast(farmr.CastPosition);
                        _rDelay = true; Core.DelayAction(() => _rDelay = false, 1000);
                    }
                }
            }
        }

        public override void Jungleclear()
        {
            var monsterq =
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(a => a.MaxHealth)
                    .FirstOrDefault(
                        a =>
                            a.IsValidTarget(_q.Range) && Variables.SummonerRiftJungleList.Contains(a.BaseSkinName) &&
                            a.Health >= Player.Instance.GetSpellDamage(a, SpellSlot.Q));

            if (Value.Use("jungle.q") && _q.IsReady() && Player.Instance.ManaPercent >= Value.Get("jungle.q.mana"))
            {
                if (monsterq != null)
                {
                    _q.Cast(monsterq);
                }
            }

            if (_r.IsLearned)
            {

                if (Value.Use("jungle.r") && _r.IsReady() && Player.Instance.ManaPercent >= Value.Get("jungle.r.mana") && !_rDelay)
                {
                    var monsterr =
                        EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position,
                            Rranges[_r.Level - 1]).Where(a => !a.HasBuff("bantamtraptarget"));
                    var farmr = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(monsterr, _r.Width + 80,
                        Rranges[_r.Level - 1]);

                    if (farmr.HitNumber >= Value.Get("jungle.r.min") && !RPredShroomed(farmr.CastPosition) && Rstacks > Value.Get("jungle.r.stacks"))
                    {
                        _r.Cast(farmr.CastPosition);
                        _rDelay = true; Core.DelayAction(() => _rDelay = false, 1000);
                    }

                }
            }
        }

        public override void Flee()
        {
            var target = TargetSelector.GetTarget(_q.Range, DamageType.Magical);

            if (Value.Use("flee.w") && _w.IsReady() && Player.Instance.ManaPercent >= Value.Get("flee.w.mana"))
            {
                _w.Cast();
            }

            if (target == null)
            {
                return;
            }
            var tpos = Player.Instance.Position.Extend(target.Position, 200).To3D();

            if (Value.Use("flee.q") && _q.IsReady() && Player.Instance.ManaPercent >= Value.Get("flee.q.mana"))
            {
                _q.Cast(target);
            }

            if (Value.Use("flee.r") && _r.IsReady() && Player.Instance.ManaPercent >= Value.Get("flee.r.mana") && !_rDelay)
            {
                if (!RPredShroomed(tpos) && Rstacks > Value.Get("flee.r.stacks"))
                {
                    _r.Cast(tpos);
                    _rDelay = true; Core.DelayAction(() => _rDelay = false, 1000);
                }
            }
        }

        public override void LastHit()
        {
            var minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .OrderByDescending(a => a.MaxHealth)
                    .FirstOrDefault(
                        a => a.IsValidTarget(_q.Range) && a.Health <= Player.Instance.GetSpellDamage(a, SpellSlot.Q));

            if (Value.Use("lasthit.q") && _q.IsReady() && Player.Instance.ManaPercent >= Value.Get("lasthit.q.mana"))
            {
                if (minion != null)
                {
                    _q.Cast(minion);
                }
            }
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            var enemy = target as AIHeroClient;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {

                if (Value.Use("combo.q") && _q.IsReady())
                {
                    _q.Cast(enemy);
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                var targetq = TargetSelector.GetTarget(_q.Range, DamageType.Magical);

                if (Value.Use("harass.q") && _q.IsReady() && targetq != null && Player.Instance.ManaPercent >= Value.Get("harass.q.mana"))
                {
                    _q.Cast(targetq);
                }
            }
        }

        private static void GameOnOnUpdate(EventArgs args)
        {
            Ks();

            AutoR();
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsAlly || !Value.Use("misc.q.gapcloser"))
            {
                return;
            }

            if (_q.IsReady() && _q.IsInRange(sender))
            {
                _q.Cast(sender);
            }          
        }

        #endregion

        #region Utils

        private static int Rstacks
        {
            get { return _r.Handle.Ammo; }
        }

        private static bool RPredShroomed(Vector3 castposition)
        {
            return
                ObjectManager.Get<Obj_AI_Minion>()
                    .Where(a => a.Name == "Noxious Trap").Any(a => castposition.Distance(a.Position) <= _r.Width + 80);
        }

        private static void Ks()
        {
            var target = TargetSelector.GetTarget(_q.Range, DamageType.Magical);

            if (Value.Use("killsteal.q") && _q.IsReady() && Player.Instance.ManaPercent >= Value.Get("killsteal.q.mana"))
            {
                if (target != null && target.TotalShieldHealth() <= Player.Instance.GetSpellDamage(target, SpellSlot.Q) && !Orbwalker.IsAutoAttacking)
                {
                    _q.Cast(target);
                }
            }
        }

        private static void AutoR()
        {
            if (_r.IsLearned)
            {
                var target =
                    EntityManager.Heroes.Enemies.FirstOrDefault(
                        a => a.IsValidTarget(Rranges[_r.Level - 1]) &&
                             (a.HasBuffOfType(BuffType.Charm) || a.HasBuffOfType(BuffType.Knockup) ||
                              a.HasBuffOfType(BuffType.Snare) || a.HasBuffOfType(BuffType.Stun) ||
                              a.HasBuffOfType(BuffType.Suppression) || a.HasBuffOfType(BuffType.Taunt)));

                if (target != null)
                {
                    if (Value.Use("misc.r.charm") && target.IsCharmed)
                    {
                        _r.Cast(target);
                    }
                    if (Value.Use("misc.r.knockup"))
                    {
                        _r.Cast(target);
                    }
                    if (Value.Use("misc.r.stun") && target.IsStunned)
                    {
                        _r.Cast(target);
                    }
                    if (Value.Use("misc.r.snare") && target.IsRooted)
                    {
                        _r.Cast(target);
                    }
                    if (Value.Use("misc.r.suppression") && target.IsSuppressCallForHelp)
                    {
                        _r.Cast(target);
                    }
                    if (Value.Use("misc.r.taunt") && target.IsTaunted)
                    {
                        _r.Cast(target);
                    }
                }
            }
        }

        #endregion

        #region Drawings
        private static void GameOnDraw(EventArgs args)
        {
            Color colorQ = MainMenu._draw.GetColor("color.q");
            var widthQ = MainMenu._draw.GetWidth("width.q");
            Color colorR = MainMenu._draw.GetColor("color.r");
            var widthR = MainMenu._draw.GetWidth("width.r");


            if (!Value.Use("draw.disable"))
            {
                if (Value.Use("draw.q") && ((Value.Use("draw.ready") && _q.IsReady()) || !Value.Use("draw.ready")))
                {
                    new Circle
                    {
                        Color = colorQ,
                        Radius = _q.Range,
                        BorderWidth = widthQ
                    }.Draw(Player.Instance.Position);
                }

                if (_r.IsLearned)
                {

                    if (Value.Use("draw.r") && ((Value.Use("draw.ready") && _r.IsReady()) || !Value.Use("draw.ready")))
                    {
                        new Circle
                        {
                            Color = colorR,
                            Radius = Rranges[_r.Level - 1],
                            BorderWidth = widthR
                        }.Draw(Player.Instance.Position);
                    }
                }
            }
        }
        #endregion 
    }
}

using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using MarksmanAIO.Menu_Settings;
using MarksmanAIO.Spell_Library;
using MarksmanAIO.Utility;
using SpellData = MarksmanAIO.Spell_Library.SpellData;

namespace MarksmanAIO.Champions
{
    class Jinx : AIOChampion
    {
        private static Spell.Active _q;
        private static Spell.Skillshot _w;
        private static Spell.Skillshot _e;
        private static Spell.Skillshot _r;

        private static bool MissleActive
        {
            get
            {
                return Player.HasBuff("JinxQ");
            }
        }

        private static float MissleRange
        {
            get
            {
                return 670f + Player.Instance.BoundingRadius + (25f * _q.Level);
            }
        }

        private static float NormalRange(GameObject target = null)
        {
            return 650f + Player.Instance.BoundingRadius + (target == null ? 0f : target.BoundingRadius);

        }

        private static float GetBoundingDistance(Obj_AI_Base target)
        {
            return Player.Instance.ServerPosition.Distance(target.ServerPosition) + Player.Instance.BoundingRadius + target.BoundingRadius;
        }

        private static bool EnoughManaToQ(bool countE = false, float extra = 0f)
        {
            var qMana = 20f;
            var wMana = _w.IsLearned ? new float[] {50, 60, 70, 80, 90}[_w.Level - 1] : 0f;
            var eMana = countE ? 50f : 0f;
            var rMana = _r.IsReady() ? 100f : 0f;

            return Player.Instance.Mana > (qMana + wMana + eMana + rMana + extra);
        }

        public override void Init()
        {
            try
            {
                //Create spells
                _q = new Spell.Active(SpellSlot.Q);
                _w = new Spell.Skillshot(SpellSlot.W, 1500, SkillShotType.Linear, 600, 3300, 60)
                {
                    MinimumHitChance = HitChance.Medium,
                    AllowedCollisionCount = 0
                };
                _e = new Spell.Skillshot(SpellSlot.E, 900, SkillShotType.Circular, 1200, 1750, 100);
                _r = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Linear, 700, 1500, 140);

                try
                {
                    #region Create menu

                    //Combo Menu Settings
                    MainMenu.ComboKeys(true, true, true, true);

                    //Lane Clear Menu Settings
                    MainMenu.LaneKeys(true, false, false, false);
                    MainMenu._lane.Add("lane.mana", new Slider("Minimum {0}% to laneclear with Q", 80));

                    //Jungle Clear Menu Settings
                    MainMenu.JungleKeys(true, true, false, false, false);

                    //Harras Menu Settings
                    MainMenu.HarassKeys(true, true, false, false);

                    //Flee Menu
                    MainMenu.FleeKeys(false, false, true, false);

                    //Killsteal Menu
                    MainMenu.KsKeys(false, true, false, true);

                    //Misc Menu
                    MainMenu.MiscMenu();
                    MainMenu._misc.Add("misc.farmQAARange", new CheckBox("Use Q when minion out of AA range"));

                    //Draw Menu
                    MainMenu.DrawKeys(true, true, true, false);

                    Value.Init();

                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code MENU)</font>");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code 503)</font>");
            }

            try
            {
                Game.OnTick += OnTick;
                Drawing.OnDraw += OnDraw;
                Orbwalker.OnPreAttack += OnPreAttack;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code INIT)</font>");
            }
        }

        #region Drawings
        private static void OnDraw(EventArgs args)
        {
            if (Value.Use("draw.disable") || Player.Instance.IsDead) return;

            if (Value.Use("draw.q"))
            {
                if (!(Value.Use("draw.ready") && !_q.IsReady()))
                {
                    new Circle
                    {
                        Radius = _q.Range,
                        Color = MainMenu._draw.GetColor("color.q"),
                        BorderWidth = MainMenu._draw.GetWidth("width.q")
                    }.Draw(Player.Instance.Position);
                }
            }

            if (Value.Use("draw.w"))
            {
                if (!(Value.Use("draw.ready") && !_w.IsReady()))
                {
                    new Circle
                    {
                        Radius = _w.Range,
                        Color = MainMenu._draw.GetColor("color.w"),
                        BorderWidth = MainMenu._draw.GetWidth("width.w")
                    }.Draw(Player.Instance.Position);
                }
            }

            if (Value.Use("draw.e"))
            {
                if (!(Value.Use("draw.ready") && !_e.IsReady()))
                {
                    new Circle
                    {
                        Radius = _e.Range,
                        Color = MainMenu._draw.GetColor("color.e"),
                        BorderWidth = MainMenu._draw.GetWidth("width.e")
                    }.Draw(Player.Instance.Position);
                }
            }
        }
        #endregion

        private static void OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead) return;

            PermaActive();
        }

        public override void Combo()
        {
            if (Value.Use("combo.q"))
            {
                var qTarget = GetValidMissleTarget();
                if (qTarget != null && !MissleActive)
                {
                    if (EnoughManaToQ() 
                        || (Player.Instance.GetAutoAttackDamage(qTarget) * 4 > qTarget.TotalShieldHealth() && Player.Instance.Mana > 60))
                    {
                        _q.Cast();
                    }
                }
                else if (MissleActive && !EnoughManaToQ())
                {
                    _q.Cast();
                }
                else if (MissleActive && Player.Instance.CountEnemiesInRange(2000) == 0)
                {
                    _q.Cast();
                }
            }

            if (Value.Use("combo.w"))
            {
                WLogic();
            }
        }

        public override void Harass()
        {
            if (Value.Use("harass.q"))
            {
                QFarmMode();
            }
            if (Value.Use("harass.w"))
            {
                WLogic();
            }
        }

        public override void Laneclear()
        {
            if (Value.Use("lane.q"))
            {
                QFarmMode();
            }
        }

        public override void LastHit()
        {
            if (MissleActive)
            {
                _q.Cast();
            }
        }

        public override void Jungleclear()
        {
            
        }

        private static void PermaActive()
        {
            
        }

        private static void QFarmMode()
        {
            if (!MissleActive
                && EnoughManaToQ() 
                && !Orbwalker.IsAutoAttacking 
                && Orbwalker.CanAutoAttack 
                && Value.Use("misc.farmQAARange"))
            {
                var qMissleKillableMinion = EntityManager.MinionsAndMonsters.GetLaneMinions(
                    EntityManager.UnitTeam.Enemy, Player.Instance.Position, MissleRange)
                    .FirstOrDefault(
                        minion =>
                            !Player.Instance.IsInAutoAttackRange(minion)
                            && minion.Health < Player.Instance.GetAutoAttackDamage(minion)*1.2
                            && MissleRange > GetBoundingDistance(minion));

                if (qMissleKillableMinion != null)
                {
                    Orbwalker.ForcedTarget = qMissleKillableMinion;
                    _q.Cast();
                    return;
                }
            }

            var qTarget = GetValidMissleTarget();
            if (qTarget != null && !MissleActive)
            {
                var distance = GetBoundingDistance(qTarget);
                if (Value.Use("harass.q")
                    && !Orbwalker.IsAutoAttacking
                    && Orbwalker.CanAutoAttack
                    && !Player.Instance.IsUnderEnemyturret()
                    && EnoughManaToQ()
                    && MissleRange + qTarget.BoundingRadius + Player.Instance.BoundingRadius > distance)
                {
                    _q.Cast();
                }
            }
            
            else if (MissleActive)
            {
                _q.Cast();
            }
        }

        private static AIHeroClient GetValidMissleTarget()
        {
            var enemy = TargetSelector.GetTarget(MissleRange + 60, DamageType.Physical);
            if (enemy == null || !enemy.IsValidTarget()) return null;

            if (!MissleActive && (!Player.Instance.IsInAutoAttackRange(enemy) || enemy.CountEnemiesInRange(250) > 2) && Orbwalker.GetTarget() == null)
            {
                return enemy;
            }

            return null;
        }

        private static void WLogic()
        {
            if (!Orbwalker.IsAutoAttacking) return;

            var target = TargetSelector.SelectedTarget != null && _w.IsInRange(TargetSelector.SelectedTarget) ?
                    TargetSelector.SelectedTarget : TargetSelector.GetTarget(_w.Range, DamageType.Physical);

            if (target != null && target.IsValidTarget())
            {
                _w.Cast(target);
            }
        }

        private static void OnPreAttack(AttackableUnit unit, Orbwalker.PreAttackArgs args)
        {
            if (!_q.IsReady()) return;

            var target = args.Target as AIHeroClient;

            if (MissleActive && target != null && target.IsValidTarget())
            {
                Console.WriteLine("inside func");
                var distance = GetBoundingDistance(target);

                if (Value.Use("combo.q")
                    && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)
                    && NormalRange(target) > distance 
                    && target.IsValidTarget())
                {
                    Console.WriteLine("combo switched to powpow inrange");
                    _q.Cast();
                }

                else if (Value.Use("lane.q") 
                    && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)
                    && (distance > MissleRange || distance < NormalRange(target) || !EnoughManaToQ(true)))
                {
                    _q.Cast();
                }

                else if (Value.Use("harass.q")  
                    && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)
                    && (distance > MissleRange || distance < NormalRange(target) || !EnoughManaToQ(true)))
                {
                    _q.Cast();
                }
            }

            else if (Value.Use("lane.q") 
                && !MissleActive 
                && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)
                && Player.Instance.ManaPercent > Value.Get("lane.mana") 
                && EnoughManaToQ())
            {
                var qMinions = EntityManager.MinionsAndMonsters.GetLaneMinions(
                    EntityManager.UnitTeam.Enemy, Player.Instance.Position, MissleRange)
                    .FirstOrDefault(
                                x => 
                                    args.Target.NetworkId != x.NetworkId 
                                    && x.Distance(args.Target.Position) < 200 
                                    && (5 - _q.Level) * Player.Instance.GetAutoAttackDamage(x) < args.Target.Health
                                    && (5 - _q.Level) * Player.Instance.GetAutoAttackDamage(x) < x.Health);

                if (qMinions != null)
                {
                    _q.Cast();
                }
            }
        }

        public static float GetTotalDamage(Obj_AI_Base target)
        {
            var damage = 0f;

            if (_q.IsReady())
            {
                damage = damage + QDamage(target);
            }

            if (_w.IsReady())
            {
                damage = damage + WDamage(target);
            }

            if (_e.IsReady())
            {
                damage = damage + EDamage(target);
            }

            if (_r.IsReady())
            {
                damage = damage + RDamage(target);
            }

            if (Player.Instance.HasBuff("summonerexhaust"))
            {
                damage = damage * 0.6f;
            }

            if (target.HasBuff("FerociousHowl"))
            {
                damage = damage * 0.7f;
            }

            return damage;
        }

        private static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.GetAutoAttackDamage(target);
        }

        private static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(
                target,
                DamageType.Physical,
                new[] { 0, 10, 60, 110, 160, 210 }[_w.Level]
                + (Player.Instance.TotalAttackDamage * 1.4f));
        }

        private static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(
                target,
                DamageType.Magical,
                new[] { 0, 80, 135, 190, 245, 300 }[_e.Level] + (Player.Instance.TotalMagicalDamage));
        }

        private static float RDamage(Obj_AI_Base target)
        {
            if (!_r.IsLearned) return 0;
            var level = _r.Level - 1;

            #region Less than Range

            if (target.Distance(Player.Instance) < 1350 && !target.IsMinion && !target.IsMonster)
            {
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                    (float)
                        (new double[] { 25, 35, 45 }[level] +
                            new double[] { 25, 30, 35 }[level] / 100 * (target.MaxHealth - target.Health) +
                            0.1 * Player.Instance.FlatPhysicalDamageMod));
            }

            if ((target.IsMonster || target.IsMinion) && target.Distance(Player.Instance) < 1350)
            {
                var damage = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                    (float)
                        (new double[] { 25, 35, 45 }[level] +
                            new double[] { 25, 30, 35 }[level] / 100 * (target.MaxHealth - target.Health) +
                            0.1 * Player.Instance.FlatPhysicalDamageMod));

                return (damage * 0.8) > 300f ? 300f : damage;
            }

            #endregion

            #region More Than Range

            var damage2 = Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)
                    (new double[] { 250, 350, 450 }[level] +
                        new double[] { 25, 30, 35 }[level] / 100 * (target.MaxHealth - target.Health) +
                        Player.Instance.FlatPhysicalDamageMod));

            if ((target.IsMonster || target.IsMinion) && (damage2 * 0.8) > 300f)
            {
                damage2 = 300f;
            }

            return damage2;

            #endregion
        }
    }
}

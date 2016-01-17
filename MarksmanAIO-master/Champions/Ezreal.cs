using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using MarksmanAIO.Menu_Settings;
using SharpDX;

namespace MarksmanAIO.Champions
{
    class Ezreal : AIOChampion
    {
        //Spells
        private static Spell.Skillshot _q, _w, _r;
        private static Spell.Targeted _e;
        private int EMode;
        private bool AN = true;
        private int MinionID;

        //Some Usefull Things
        private static readonly AIHeroClient Player = ObjectManager.Player;

        public override void Init()
        {
            //Spells

            _q = new Spell.Skillshot(SpellSlot.Q, 1150, SkillShotType.Linear, 250, 2000, (int) 60f)
            {
                AllowedCollisionCount = 1
            };
            _w = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Linear, 250, 1600, (int) 80f)
            {
            };
            _e = new Spell.Targeted(SpellSlot.E, 475);
            _r = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Linear, 1000, 2000, (int) 160f)
            {

            };


            try
            {
                //Menu Init
                //Combo
                MainMenu.ComboKeys(true, true, true, true);
                MainMenu._combo.AddGroupLabel("Mana Manager: Settings", "combo.advanced.manamanagerlabel", true);
                MainMenu.ComboManaManager(true, true, true, true, 10, 10, 10, 10);

                MainMenu._combo.AddSeparator();
                MainMenu._combo.AddGroupLabel("Prediction Settings", "combo.advanced.predctionlabel", true);
                MainMenu._combo.AddSlider("combo.q.pred", "Use Q if HitChance is above than {0}%", 45, 0, 100, true);
                MainMenu._combo.AddSlider("combo.w.pred", "Use W if HitChance is above than {0}%", 30, 0, 100, true);
                MainMenu._combo.AddSlider("combo.r.pred", "Use R if HitChance is above than {0}%", 70, 0, 100, true);
                MainMenu._combo.AddSeparator();
                MainMenu._combo.AddGroupLabel("E Settings", "combo.advanced.ezlabel", true);
                MainMenu._combo.AddSlider("combo.e.type", "Dash Method: Mouse", 1, 1, 3, true);
                MainMenu._combo.Get<Slider>("combo.e.type").OnValueChange +=
                    delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                    {
                        if (sender.Cast<Slider>().CurrentValue == 1)
                        {
                            sender.DisplayName = "Dash Method: Mouse";
                            EMode = 1;
                        }
                        else if (sender.Cast<Slider>().CurrentValue == 2)
                        {
                            sender.DisplayName = "Dash Method: Safe";
                            EMode = 2;
                        }
                        else if (sender.Cast<Slider>().CurrentValue == 3)
                        {
                            sender.DisplayName = "Dash Method: Burst";
                            EMode = 3;
                        }
                    };

                //Harass
                MainMenu.HarassKeys(true, true, false, false);
                MainMenu.HarassManaManager(true, true, false, false, 60, 60, 0, 0);
                MainMenu._harass.AddSeparator();
                MainMenu._harass.AddGroupLabel("Prediction Settings", "harass.advanced.predctionlabel", true);
                MainMenu._harass.AddSlider("harass.q.pred", "Use Q if HitChance is above than {0}%", 45, 0, 100, true);
                MainMenu._harass.AddSlider("harass.w.pred", "Use W if HitChance is above than {0}%", 30, 0, 100, true);

                //Farm
                MainMenu.LaneKeys(true, false, false, false);
                MainMenu.LaneManaManager(true, false, false, false, 65, 0, 0, 0);
                MainMenu._lane.AddSeparator();
                MainMenu._harass.AddGroupLabel("Q Settings", "lane.advanced.qsettingslabel", true);
                MainMenu._lane.AddCheckBox("lane.q.aa", "Use Q only when can't AA", true, true);


                //Misc
                MainMenu.MiscMenu();

                //GapCloser
                MainMenu._misc.AddGroupLabel("GapCloser Settigs","gapcloser.settings.label",true);
                MainMenu._misc.AddCheckBox("gap.e", "Use E on GapCloser", true, true);
                MainMenu._misc.AddCheckBox("gap.e.wall", "Safe GapCloser E (Wall)",true,true);




                //Draw
                MainMenu.DrawKeys(true, true, true, true);
                MainMenu.DamageIndicator(true);


                //Value
                Value.Init();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print(
                    "<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code MENU)</font>");
            }
            try
            {
                Drawing.OnDraw += Draw;
                Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
                Orbwalker.OnPreAttack += OrbwalkerOnOnPreAttack;
                Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print(
                    "<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code 503)</font>");
            }
        }


      

        private void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            AN = false;
            if (MinionID != target.NetworkId) MinionID = target.NetworkId;
        }

        private void OrbwalkerOnOnPreAttack(AttackableUnit target, EventArgs args)
        {
            if (!target.IsMe) return;

            AN = true;
            if (MinionID != target.NetworkId) MinionID = target.NetworkId;
            if (_w.IsReady() && Value.Use("spell.w.push") && target.Type == GameObjectType.obj_AI_Turret &&
                target.IsValid && Player.ManaPercent >= Value.Get("spell.w.push.mana"))
            {
                foreach (var allies in EntityManager.Heroes.Allies)
                {
                    if (!allies.IsMe && allies.IsAlly && allies.Distance(Player.Position) < 600)
                    {
                        _w.Cast(allies);
                    }
                }
            }
        }

        private void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if(!sender.IsEnemy) return;

            if (Value.Use("spell.e.wall"))
            {
                _e.Cast(DetectWall());
            }

            _e.Cast(Player.Position.Extend(Game.CursorPos, _e.Range).To3D());
        }

        #region Drawings

        private void Draw(EventArgs args)
        {
            if (Value.Use("draw.disable")) return;
            var colorQ = MainMenu._draw.GetColor("color.q");
            var widthQ = MainMenu._draw.GetWidth("width.q");
            var colorW = MainMenu._draw.GetColor("color.w");
            var widthW = MainMenu._draw.GetWidth("width.w");
            var colorE = MainMenu._draw.GetColor("color.e");
            var widthE = MainMenu._draw.GetWidth("width.e");
            var colorR = MainMenu._draw.GetColor("color.r");
            var widthR = MainMenu._draw.GetWidth("width.r");
            if (Value.Use("draw.ready"))
            {
                if (Value.Use("draw.q") && _q.IsReady())
                {
                    new Circle() {BorderWidth = widthQ, Color = colorQ, Radius = _q.Range}.Draw(Player.Position);
                }
                if (Value.Use("draw.w") && _w.IsReady())
                {
                    new Circle() {BorderWidth = widthW, Color = colorW, Radius = _w.Range}.Draw(Player.Position);
                }
                if (Value.Use("draw.e") && _e.IsReady())
                {
                    new Circle() {BorderWidth = widthE, Color = colorE, Radius = _e.Range}.Draw(Player.Position);
                }
                if (Value.Use("draw.r") && _r.IsReady())
                {
                    new Circle() {BorderWidth = widthR, Color = colorR, Radius = _r.Range}.Draw(Player.Position);
                        //Todo: Change Range in SLider
                }
            }
            else
            {
                if (Value.Use("draw.q"))
                {
                    new Circle() {BorderWidth = widthQ, Color = colorQ, Radius = _q.Range}.Draw(Player.Position);
                }
                if (Value.Use("draw.w"))
                {
                    new Circle() {BorderWidth = widthW, Color = colorW, Radius = _w.Range}.Draw(Player.Position);
                }
                if (Value.Use("draw.e"))
                {
                    new Circle() {BorderWidth = widthE, Color = colorE, Radius = _e.Range}.Draw(Player.Position);
                }
                if (Value.Use("draw.r"))
                {
                    new Circle() {BorderWidth = widthR, Color = colorR, Radius = _r.Range}.Draw(Player.Position);
                        //Todo: Change Range in SLider
                }
            }
        }

        #endregion

        #region Combo

        public override void Combo()
        {
            if (EloBuddy.Player.Instance.IsDead || EloBuddy.Player.Instance.HasBuff("Recall")
                || EloBuddy.Player.Instance.IsStunned || EloBuddy.Player.Instance.IsRooted || EloBuddy.Player.Instance.IsCharmed) return;

            var target = TargetSelector.GetTarget(_q.Range, DamageType.Mixed);

            if (target == null) return;

            if (_r.IsReady() && Value.Use("combo.r"))
            {
                var pred = _r.GetPrediction(target);
                    _r.Cast(pred.CastPosition);
            }
            if (_e.IsReady() && Value.Use("combo.e"))
            {
                if (EMode == 1)
                {
                    _e.Cast(Game.CursorPos);
                }
                else if (EMode == 2)
                {
                    _e.Cast(target.Position.Extend(target, target.GetAutoAttackRange()).To3D());
                }
                else if (EMode == 3)
                {
                    _e.Cast(target);
                }
            }
            if (_w.IsReady() && Value.Use("combo.w") && Player.ManaPercent >= Value.Get("combo.w.mana"))
            {
                var pred = _w.GetPrediction(target);
                
                _w.Cast(pred.CastPosition);
            }
            if (_q.IsReady() && Value.Use("combo.q") && Player.ManaPercent >= Value.Get("combo.q.mana"))
            {
                var pred = _q.GetPrediction(target);
                
                _q.Cast(pred.CastPosition);
            }
        }

        #endregion

        #region Harass

        public override void Harass()
        {
            if (EloBuddy.Player.Instance.IsDead || EloBuddy.Player.Instance.HasBuff("Recall")
                || EloBuddy.Player.Instance.IsStunned || EloBuddy.Player.Instance.IsRooted || EloBuddy.Player.Instance.IsCharmed) return;

            var target = TargetSelector.GetTarget(_q.Range, DamageType.Mixed);

            if (Value.Use("harass.q") && Value.Get("harass.q.mana") >= Player.ManaPercent)
            {
                var pred = _q.GetPrediction(target);
                if (pred.HitChancePercent < Value.Get("harass.q.pred")) return;
                _q.Cast(pred.CastPosition);
            }
            if (Value.Use("harass.w") && Value.Get("harass.w.mana") >= Player.ManaPercent)
            {
                var pred = _w.GetPrediction(target);
                if (pred.HitChancePercent < Value.Get("harass.w.pred")) return;
                _w.Cast(pred.CastPosition);
            }
        }

        #endregion

        #region LaneClear

        //TODO

        public override void Laneclear()
        {
            if (EloBuddy.Player.Instance.IsDead || EloBuddy.Player.Instance.HasBuff("Recall")
                || EloBuddy.Player.Instance.IsStunned || EloBuddy.Player.Instance.IsRooted || EloBuddy.Player.Instance.IsCharmed) return;

            var minions = Orbwalker.AlmostLasthittableMinions;

            foreach (
                var minion in
                    minions.Where(
                        m =>
                            m.IsValidTarget() && _q.GetPrediction(m).Collision &&
                            Player.CalculateDamageOnUnit(m, DamageType.Mixed,
                                new float[] {35, 55, 75, 95, 115}[_q.Level] + Player.TotalAttackDamage*1.1f +
                                Player.TotalMagicalDamage*0.4f) > m.TotalShieldHealth())
                        .Where(minion => Value.Use("lane.q") && Player.ManaPercent >= Value.Get("lane.q.mana")))
            {
                _q.Cast(minion);
            }
        }

        #endregion

        private Vector3 DetectWall()
        {

            const int circleLineSegmentN = 20;

            var outRadius = 700 / (float)Math.Cos(2 * Math.PI / circleLineSegmentN);
            var inRadius = 300 / (float)Math.Cos(2 * Math.PI / circleLineSegmentN);
            var bestPoint = ObjectManager.Player.Position;
            for (var i = 1; i <= circleLineSegmentN; i++)
            {
                var angle = i * 2 * Math.PI / circleLineSegmentN;
                var point = new Vector2(ObjectManager.Player.Position.X + outRadius * (float)Math.Cos(angle), ObjectManager.Player.Position.Y + outRadius * (float)Math.Sin(angle)).To3D();
                var point2 = new Vector2(ObjectManager.Player.Position.X + inRadius * (float)Math.Cos(angle), ObjectManager.Player.Position.Y + inRadius * (float)Math.Sin(angle)).To3D();
                if ((point.ToNavMeshCell().CollFlags & CollisionFlags.Wall) != 0 &&
                    (point2.ToNavMeshCell().CollFlags & CollisionFlags.Wall) != 0 &&
                    Game.CursorPos.Distance(point) < Game.CursorPos.Distance(bestPoint))
                {
                    bestPoint = point;
                    return bestPoint;
                }
            }

            return new Vector3();
        }
    }
}

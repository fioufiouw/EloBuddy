using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using MarksmanAIO.Utility;
using EloBuddy.SDK.Rendering;
using Color = System.Drawing.Color;
using EloBuddy.SDK.Menu;

namespace MarksmanAIO.Champions
{
    class Katarina : AIOChampion
    {
        #region Initialization
        #region SpellsDefine
        private static Spell.Targeted Q;
        private static Spell.Active W;
        private static Spell.Targeted E;
        private static Spell.Active R;
        #endregion

        private bool IsUlting;
        private static Menu HumanizerMenu;
        public override void Init()
        {
            try
            {
                try
                {
                    #region Spells
                    // Defining Spells
                    Q = new Spell.Targeted(EloBuddy.SpellSlot.Q, 675);
                    W = new Spell.Active(EloBuddy.SpellSlot.W, 375);
                    E = new Spell.Targeted(EloBuddy.SpellSlot.E, 700);
                    R = new Spell.Active(EloBuddy.SpellSlot.R, 550);
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code SPELL)</font>");
                }

                try
                {
                    #region Menu
                    var combo = MainMenu._combo;
                    var s = new string[2] { "QEWR", "EQWR" };

                    combo.AddStringList("combo.mode", "Mode: ", s, 1);
                    MainMenu.ComboKeys(true, true, true, true);
                    MainMenu.HarassKeys(true, true, true, true);
                    MainMenu._harass.Add("harass.autow", new CheckBox("Use Auto W"));

                    MainMenu.FleeKeys(false, false, true, false);
                    MainMenu._flee.Add("flee.ward", new CheckBox("Use Wardjump"));

                    MainMenu.LaneKeys(true, true, true, false);
                    MainMenu.LastHitKeys(true, true, true, false);
                    MainMenu.KsKeys(true, true, true, true);
                    MainMenu._ks.Add("killsteal.ignite", new CheckBox("Use Ignite"));

                    MainMenu.DamageIndicator();
                    MainMenu.DrawKeys(true, true, true, true);
                    MainMenu._draw.AddSeparator();

                    MainMenu._draw.AddGroupLabel("Flash Settings");
                    MainMenu._draw.Add("draw.flash", new CheckBox("Draw flash"));
                    MainMenu._draw.AddColorItem("color.flash");
                    MainMenu._draw.AddWidthItem("width.flash");
                    MainMenu._draw.AddSeparator();

                    MainMenu._draw.AddGroupLabel("Ignite Settings");
                    MainMenu._draw.Add("draw.ignite", new CheckBox("Draw ignite"));
                    MainMenu._draw.AddColorItem("color.ignite");
                    MainMenu._draw.AddWidthItem("width.ignite");

                    HumanizerMenu = MainMenu._menu.AddSubMenu("Humanizer Menu");
                    HumanizerMenu.AddGroupLabel("Q Settings");
                    HumanizerMenu.Add("min.q", new Slider("Min Q Delay", 0, 0, 50));
                    HumanizerMenu.Add("max.q", new Slider("Max Q Delay", 0, 0, 50));
                    HumanizerMenu.AddSeparator(10);

                    HumanizerMenu.AddGroupLabel("W Settings");
                    HumanizerMenu.Add("min.w", new Slider("Min W Delay", 0, 0, 50));
                    HumanizerMenu.Add("max.w", new Slider("Max W Delay", 0, 0, 50));
                    HumanizerMenu.AddSeparator(10);

                    HumanizerMenu.AddGroupLabel("E Settings");
                    HumanizerMenu.Add("min.e", new Slider("Min E Delay", 0, 0, 50));
                    HumanizerMenu.Add("max.e", new Slider("Max E Delay", 0, 0, 50));
                    HumanizerMenu.AddSeparator(10);

                    HumanizerMenu.AddGroupLabel("R Settings");
                    HumanizerMenu.Add("min.r", new Slider("Min R Delay", 4, 0, 50));
                    HumanizerMenu.Add("max.r", new Slider("Max R Delay", 4, 0, 50));
                    HumanizerMenu.AddSeparator(10);

                    #endregion
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code MENU)</font>");
                }

                #region UtilityInit
                DamageIndicator.DamageToUnit = GetActualRawComboDamage;
                Menu_Settings.Value.Init();
                Menu_Settings.Value.MenuList.Add(HumanizerMenu);
                Drawing.OnDraw += DrawRanges;
                #region MenuValueChange
                HumanizerMenu["min.q"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["min.q"].Cast<Slider>().CurrentValue > HumanizerMenu["max.q"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["min.q"].Cast<Slider>().CurrentValue = HumanizerMenu["max.q"].Cast<Slider>().CurrentValue;
                };
                HumanizerMenu["max.q"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["max.q"].Cast<Slider>().CurrentValue < HumanizerMenu["min.q"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["max.q"].Cast<Slider>().CurrentValue = HumanizerMenu["min.q"].Cast<Slider>().CurrentValue;
                };
                HumanizerMenu["min.w"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["min.w"].Cast<Slider>().CurrentValue > HumanizerMenu["max.w"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["min.w"].Cast<Slider>().CurrentValue = HumanizerMenu["max.w"].Cast<Slider>().CurrentValue;
                };
                HumanizerMenu["max.w"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["max.w"].Cast<Slider>().CurrentValue < HumanizerMenu["min.w"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["max.w"].Cast<Slider>().CurrentValue = HumanizerMenu["min.w"].Cast<Slider>().CurrentValue;
                };
                    HumanizerMenu["min.e"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["min.e"].Cast<Slider>().CurrentValue > HumanizerMenu["max.e"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["min.e"].Cast<Slider>().CurrentValue = HumanizerMenu["max.e"].Cast<Slider>().CurrentValue;
                };
                HumanizerMenu["min.e"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["max.e"].Cast<Slider>().CurrentValue < HumanizerMenu["min.e"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["max.e"].Cast<Slider>().CurrentValue = HumanizerMenu["min.e"].Cast<Slider>().CurrentValue;
                };
                    HumanizerMenu["min.r"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["min.r"].Cast<Slider>().CurrentValue > HumanizerMenu["max.r"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["min.r"].Cast<Slider>().CurrentValue = HumanizerMenu["max.r"].Cast<Slider>().CurrentValue;
                };
                HumanizerMenu["min.r"].Cast<Slider>().OnValueChange += delegate
                {
                    if (HumanizerMenu["max.r"].Cast<Slider>().CurrentValue < HumanizerMenu["min.r"].Cast<Slider>().CurrentValue)
                        HumanizerMenu["max.r"].Cast<Slider>().CurrentValue = HumanizerMenu["min.r"].Cast<Slider>().CurrentValue;
                };
                    #endregion
                    #endregion
                }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code 503)</font>");
            }

            Game.OnUpdate += delegate
            {
                try
                {
                    #region IsUlting
                    IsUlting = Player.Instance.HasBuff("katarinarsound");

                    Orbwalker.DisableAttacking = IsUlting;
                    Orbwalker.DisableMovement = IsUlting;
                    #endregion
                    #region AutoW
                    if (MainMenu._harass["harass.autow"].Cast<CheckBox>().CurrentValue)
                    {
                        var e = EntityManager.Heroes.Enemies.Where(ee => !ee.IsDead && ee.IsValid);
                        foreach (var enemy in e)
                        {
                            if (W.IsInRange(enemy) && W.IsReady() && !IsUlting)
                            {
                                W.Cast();
                            }
                        }
                    }
                    #endregion

                    KillSteal();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> san error ocurred. (Code 5)</font>");
                }
                    //KillSteal();
            };
        }
        #endregion

        #region HumanizerMenuIndex
        private static int MinQDelay
        {
            get
            {
                return HumanizerMenu["min.q"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MaxQDelay
        {
            get
            {
                return HumanizerMenu["max.q"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MinWDelay
        {
            get
            {
                return HumanizerMenu["min.w"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MaxWDelay
        {
            get
            {
                return HumanizerMenu["max.w"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MinEDelay
        {
            get
            {
                return HumanizerMenu["min.e"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MaxEDelay
        {
            get
            {
                return HumanizerMenu["max.e"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MinRDelay
        {
            get
            {
                return HumanizerMenu["min.e"].Cast<Slider>().CurrentValue;
            }
        }
        private static int MaxRDelay
        {
            get
            {
                return HumanizerMenu["max.r"].Cast<Slider>().CurrentValue;
            }
        }
        #endregion
        #region Combo
        #region ComboMenuIndex
        private static bool ComboUseQ
        {
            get
            {
                return Menu_Settings.Value.Use("combo.q");
            }
        }
        private static bool ComboUseW
        {
            get
            {
                return Menu_Settings.Value.Use("combo.w");
            }

        }
        private static bool ComboUseE
        {
            get
            {
                return Menu_Settings.Value.Use("combo.e");
            }

        }
        private static bool ComboUseR
        {
            get
            {
                return Menu_Settings.Value.Use("combo.r");
            }

        }

        private static int ComboMode
        {
            get
            {
                return Menu_Settings.Value.Get("combo.mode");
            }

        }
        #endregion
        #region ComboMain
        public override void Combo()
        {
            try
            {
                var Target = TargetSelector.GetTarget(1000, DamageType.Magical);
                if (Target == null || !Target.IsValid)
                    return;

                switch (ComboMode)
                {
                    #region Mode QEWR
                    case 0:
                        if (Q.IsReady() && Q.IsInRange(Target) && ComboUseQ)
                        {
                            Core.DelayAction(() => Q.Cast(Target), new Random().Next(MinQDelay, MaxQDelay));
                        }
                        if (E.IsReady() && E.IsInRange(Target) && ComboUseE)
                        {
                            Core.DelayAction(() => E.Cast(Target), new Random().Next(MinWDelay, MaxWDelay));
                        }
                        if (W.IsReady() && W.IsInRange(Target) && ComboUseW)
                        {
                            Core.DelayAction(() => W.Cast(), new Random().Next(MinEDelay, MaxEDelay));
                        }
                        if (R.IsReady() && R.IsInRange(Target) && ComboUseR)
                        {
                            Core.DelayAction(() => R.Cast(), new Random().Next(MinRDelay, MaxRDelay));
                        }
                        break;
                    #endregion
                    #region Mode EQWR
                    case 1:
                        if (E.IsReady() && E.IsInRange(Target) && ComboUseE)
                        {
                            Core.DelayAction(() => E.Cast(Target), new Random().Next(MinWDelay, MaxWDelay));
                        }
                        if (Q.IsReady() && Q.IsInRange(Target) && ComboUseQ)
                        {
                            Core.DelayAction(() => Q.Cast(Target), new Random().Next(MinQDelay, MaxQDelay));
                        }
                        if (W.IsReady() && W.IsInRange(Target) && ComboUseW)
                        {
                            Core.DelayAction(() => W.Cast(), new Random().Next(MinEDelay, MaxEDelay));
                        }
                        if (R.IsReady() && R.IsInRange(Target) && ComboUseR)
                        {
                            Core.DelayAction(() => R.Cast(), new Random().Next(MinRDelay, MaxRDelay));
                        }
                        break;
                }
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code COMBO)</font>");
            }
        }
        #endregion
        #endregion
        #region Harass
        #region HarassMenuIndex
        private static bool HarassUseQ
        {
            get
            {
                return Menu_Settings.Value.Use("harass.q");
            }
        }
        private static bool HarassUseW
        {
            get
            {
                return Menu_Settings.Value.Use("harass.q");
            }

        }
        private static bool HarassUseE
        {
            get
            {
                return Menu_Settings.Value.Use("harass.e");
            }

        }
        #endregion
        #region HarassMain
        public override void Harass()
        {
            try
            {
                var Target = TargetSelector.GetTarget(1000, DamageType.Magical);
                if (Target == null || !Target.IsValid)
                    return;



                if (Q.IsReady() && Q.IsInRange(Target) && HarassUseQ)
                {
                    Core.DelayAction(() => Q.Cast(Target), new Random().Next(MinQDelay, MaxQDelay));
                }
                if (W.IsReady() && W.IsInRange(Target) && HarassUseW)
                {
                    Core.DelayAction(() => W.Cast(), new Random().Next(MinEDelay, MaxEDelay));
                }
                if (E.IsReady() && E.IsInRange(Target) && HarassUseE)
                {
                    Core.DelayAction(() => E.Cast(Target), new Random().Next(MinWDelay, MaxWDelay));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code HARASS)</font>");
            }
        }
        #endregion
        #endregion
        #region LastHit
        #region LastHitMenuIndex
        private static bool LastHitUseQ
        {
            get { return Menu_Settings.Value.Use("lasthit.q"); }
        }
        private static bool LastHitUseW
        {
            get { return Menu_Settings.Value.Use("lasthit.w"); }
        }
        private static bool LastHitUseE
        {
            get { return Menu_Settings.Value.Use("lasthit.e"); }
        }
        #endregion
        #region LastHitMain
        public override void LastHit()
        {
            try
            {
                foreach (var minion in EntityManager.MinionsAndMonsters.EnemyMinions)
                {
                    if (minion == null || !minion.IsValid)
                        return;

                    #region Q
                    try
                    {
                        if (Prediction.Health.GetPrediction(minion, Q.CastDelay + (Game.Ping / 4)) <= Player.Instance.GetSpellDamage(minion, SpellSlot.Q))
                        {
                            if (Q.IsInRange(minion) && Q.IsReady() && LastHitUseQ)
                            {
                                Core.DelayAction(() => Q.Cast(minion), new Random().Next(MinQDelay, MaxQDelay));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code LASTHIT.Q)</font>");
                    }
                    #endregion
                    #region W
                    try
                    {
                        if (Prediction.Health.GetPrediction(minion, W.CastDelay + (Game.Ping / 4)) <= Player.Instance.GetSpellDamage(minion, SpellSlot.W))
                        {
                            if (W.IsInRange(minion) && W.IsReady() && LastHitUseW)
                            {
                                Core.DelayAction(() => W.Cast(), new Random().Next(MinEDelay, MaxEDelay));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code LASTHIT.W)</font>");
                    }
                    #endregion
                    #region E
                    try
                    {
                        if (Prediction.Health.GetPrediction(minion, E.CastDelay + (Game.Ping / 4)) <= Player.Instance.GetSpellDamage(minion, SpellSlot.E))
                        {
                            if (E.IsInRange(minion) && E.IsReady() && LastHitUseE)
                            {
                                Core.DelayAction(() => E.Cast(minion), new Random().Next(MinWDelay, MaxWDelay));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code LASTHIT.W)</font>");
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code LASTHIT)</font>");
            }
        }
        #endregion
        #endregion
        #region Flee
        #region FleeMenuIndex
        private static bool FleeUseE
        {
            get
            {
                return Menu_Settings.Value.Use("flee.e");
            }
        }
        private static bool FleeWardJump
        {
            get
            {
                return Menu_Settings.Value.Use("flee.ward");
            }
        }
        #endregion
        #region FleeMain
        public override void Flee()
        {
            try
            {

                foreach (var minion in ObjectManager.Get<Obj_AI_Base>().Where(o => o.IsTargetable && o.IsValid && !o.IsDead && o.IsHPBarRendered && (o.IsMinion || o.IsMonster || (o is AIHeroClient && !o.IsMe) || o.IsWard())).OrderBy(o => o.Distance(Game.CursorPos)))
                {
                    if (!minion.IsValid || minion == null)
                    {
                        return;
                    }
                    if (FleeUseE && E.IsReady() && E.IsInRange(minion))
                    {
                        if (minion.IsInRange(Game.CursorPos, 200))
                        {
                            Core.DelayAction(() => E.Cast(minion), new Random().Next(MinWDelay, MaxWDelay));
                        }
                    }
                    else
                    {
                        try
                        {
                            if (Utility.Extensions.GetWardSlot() == null || !Utility.Extensions.GetWardSlot().IsWard)
                                return;

                            if (FleeWardJump && Utility.Extensions.GetWardSlot().CanUseItem() && E.IsReady() && FleeUseE)
                            {
                                var pos = Player.Instance.Position.Extend(Game.CursorPos, 600);
                                Utility.Extensions.GetWardSlot().Cast(pos.To3D());
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code FLEE.WARDJUMP)</font>");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code FLEE)</font>");
            }
        }
        #endregion
        #endregion
        #region LaneClear
        #region LaneClearMenuIndex
        private static bool LaneClearUseQ
        {
            get
            {
                return Menu_Settings.Value.Use("lane.q");
            }
        }
        private static bool LaneClearUseW
        {
            get
            {
                return Menu_Settings.Value.Use("lane.w");
            }
        }
        private static bool LaneClearUseE
        {
            get
            {
                return Menu_Settings.Value.Use("lane.e");
            }
        }
        #endregion
        #region LaneClearMain
        public override void Laneclear()
        {
            try
            {
                foreach (var minion in EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsInRange(Player.Instance, 1200)))
                {
                    if (minion == null || !minion.IsValid)
                        return;

                    if (Q.IsInRange(minion) && Q.IsReady() && LaneClearUseQ)
                    {
                        Core.DelayAction(() => Q.Cast(minion), new Random().Next(MinQDelay, MaxQDelay));
                    }

                    if (W.IsInRange(minion) && W.IsReady() && LaneClearUseW)
                    {
                        Core.DelayAction(() => W.Cast(), new Random().Next(MinEDelay, MaxEDelay));
                    }

                    if (E.IsInRange(minion) && E.IsReady() && LaneClearUseE)
                    {
                        Core.DelayAction(() => E.Cast(minion), new Random().Next(MinWDelay, MaxWDelay));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code LANECLEAR)</font>");
            }
        }
        #endregion
        #endregion
        #region KillSteal
        #region KillStealMenuIndex
        private static bool KillStealUseQ
        {
            get
            {
                return Menu_Settings.Value.Use("killsteal.q");
            }
        }
        private static bool KillStealUseW
        {
            get
            {
                return Menu_Settings.Value.Use("killsteal.w");
            }
        }
        private static bool KillStealUseE
        {
            get
            {
                return Menu_Settings.Value.Use("killsteal.e");
            }
        }
        private static bool KillStealUseR
        {
            get
            {
                return Menu_Settings.Value.Use("killsteal.r");
            }
        }
        #endregion
        #region KillStealMain
        private static void KillSteal()
        {
            try
            {
                var e = EntityManager.Heroes.Enemies.Where(ee => !ee.IsDead && ee.IsValid);

                foreach (var enemy in e)
                {
                    var damage = Player.Instance.CalculateDamageOnUnit(enemy, DamageType.Magical, GetActualRawComboDamage(enemy), true, true);
                    if (enemy.Health <= damage)
                    {
                        if (Q.IsReady() && Q.IsInRange(enemy) && KillStealUseQ)
                        {
                            Core.DelayAction(() => Q.Cast(enemy), new Random().Next(MinQDelay, MaxQDelay));
                        }
                        if (W.IsReady() && W.IsInRange(enemy) && KillStealUseW)
                        {
                            Core.DelayAction(() => W.Cast(), new Random().Next(MinEDelay, MaxEDelay));
                        }
                        if (E.IsReady() && E.IsInRange(enemy) && KillStealUseE)
                        {
                            Core.DelayAction(() => E.Cast(enemy), new Random().Next(MinWDelay, MaxWDelay));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code KILLSTEAL)</font>");
            }
        }
        #endregion
        #endregion
        #region Damages
        #region BaseDamages
        private static float[] QDamage = new float[6] { 0, 60, 85, 110, 135, 160};
        private static float[] BonusQDamage = new float[6] { 0, 15, 30, 45, 60, 75 };
        private static float[] WDamage = new float[6] { 0, 40, 75, 110, 145, 180 };
        private static float[] EDamage = new float[6] { 0, 40, 70, 100, 130, 160 };
        private static float[] RDamage = new float[4] { 0, 350, 550, 750 };
        #endregion
        #region GetSpellDamage
        private static float GetSpellDamage(SpellSlot slot)
        {
            try
            {
                var qbasedamage = QDamage[Q.Level];
                var wbasedamage = WDamage[W.Level];
                var ebasedamage = EDamage[E.Level];
                var rbasedamage = RDamage[R.Level];

                var qbonusdamage = (45f / 100f * Player.Instance.FlatMagicDamageMod);
                var wbonusdamage = (25f / 100f * Player.Instance.FlatMagicDamageMod);
                var ebonusdamage = (25f / 100f * Player.Instance.FlatMagicDamageMod);
                var rbonusdamage = (25f / 100f * Player.Instance.FlatMagicDamageMod);

                if (slot == SpellSlot.Q)
                    return qbasedamage + qbonusdamage + (BonusQDamage[Q.Level] + (15f / 100f * Player.Instance.FlatMagicDamageMod));
                if (slot == SpellSlot.W)
                    return wbasedamage + wbonusdamage + (60f / 100f * Player.Instance.FlatPhysicalDamageMod);
                if (slot == SpellSlot.E)
                    return ebasedamage + ebonusdamage;
                if (slot == SpellSlot.R)
                    return rbasedamage + rbonusdamage + (375f / 1000f * Player.Instance.FlatPhysicalDamageMod);

                //if (raw)
                //return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, damage, true, true);
                return 0f;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code GETSPELLDAMAGE)</font>");
                return 0f;
            }
        }
        #endregion
        #region RawComboDamage
        private static float GetActualRawComboDamage(Obj_AI_Base enemy)
        {
            try
            {
                var damage = 0f;

                var spells = new List<SpellSlot>();
                spells.Add(SpellSlot.Q);
                spells.Add(SpellSlot.W);
                spells.Add(SpellSlot.E);
                spells.Add(SpellSlot.R);
                foreach (var spell in spells.Where(s => Player.Instance.Spellbook.CanUseSpell(s) == SpellState.Ready && s != SpellSlot.R))
                {
                    if (Player.Instance.Spellbook.CanUseSpell(spell) == SpellState.Ready)
                    damage += GetSpellDamage(spell);
                }
                if (Player.Instance.Spellbook.CanUseSpell(GetIgniteSpellSlot()) != SpellState.Cooldown && Player.Instance.Spellbook.CanUseSpell(GetIgniteSpellSlot()) != SpellState.NotLearned && Player.Instance.Spellbook.CanUseSpell(GetIgniteSpellSlot()) == SpellState.Ready)
                    damage += Player.Instance.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite);
                    if (Player.Instance.Spellbook.CanUseSpell(SpellSlot.R) != SpellState.Cooldown && Player.Instance.Spellbook.CanUseSpell(SpellSlot.R) != SpellState.NotLearned)
                    damage += GetSpellDamage(SpellSlot.R);
                return damage;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code GETACTUALRAWCOMBODAMAGE)</font>");
                return 0f;
            }
        }
        #endregion
        #endregion
        #region Drawings
        #region ShouldDrawIndex
        private static bool DrawReady
        {
            get
            {
                return Menu_Settings.Value.Use("draw.ready");
            }
        }
        private static bool DrawDisable
        {
            get
            {
                return Menu_Settings.Value.Use("draw.disable");
            }
        }

        private static bool DrawQ
        {
            get
            {
                return Menu_Settings.Value.Use("draw.q");
            }
        }
        private static bool DrawW
        {
            get
            {
                return Menu_Settings.Value.Use("draw.w");
            }
        }
        private static bool DrawE
        {
            get
            {
                return Menu_Settings.Value.Use("draw.e");
            }
        }
        private static bool DrawR
        {
            
            get
            {
                return Menu_Settings.Value.Use("draw.r");
            }
        }
        #endregion
        #region DrawColorIndex
        private static Color ColorQ
        {
            get {return MainMenu._draw.GetColor("color.q"); }
        }
        private static Color ColorW
        {
            get {return MainMenu._draw.GetColor("color.w"); }
        }
        private static Color ColorE
        {
            get {return MainMenu._draw.GetColor("color.e"); }
        }
        private static Color ColorR
        {
            get { return MainMenu._draw.GetColor("color.r"); }
        }
        private static Color ColorIgnite
        {
            get { return MainMenu._draw.GetColor("color.ignite"); }
        }
        private static Color ColorFlash
        {
            get { return MainMenu._draw.GetColor("color.flash"); }
        }
        #endregion
        #region DrawWidthIndex
        private static float WidthQ
        {
            get {return MainMenu._draw.GetWidth("width.q"); }
        }
        private static float WidthW
        {
            get {return MainMenu._draw.GetWidth("width.w"); }
        }
        private static float WidthE
        {
            get {return MainMenu._draw.GetWidth("width.e"); }
        }
        private static float WidthR
        {
            get { return MainMenu._draw.GetWidth("width.r"); }
        }
        private static float WidthIgnite
        {
            get { return MainMenu._draw.GetWidth("width.ignite"); }
        }
        private static float WidthFlash
        {
            get { return MainMenu._draw.GetWidth("width.flash"); }
        }
        #endregion
        #region DrawSummonersIndex
        private static bool DrawFlash
        {
            get
            {
                return Menu_Settings.Value.Use("draw.flash");
            }
        }
        private static bool DrawIgnite
        {
            get
            {
                return Menu_Settings.Value.Use("draw.ignite");
            }
        }
        #endregion
        #region SummonersRanges
        private static float flashrange = 425;
        private static float igniterange = 600;
        #endregion
        #region DrawRangesMain
        private static void DrawRanges(EventArgs args)
        {
            try
            {
                if (DrawDisable)
                    return;

                try
                {
                    #region Q
                    if (DrawReady)
                    {
                        if (Q.IsReady())
                            new Circle() { BorderWidth = WidthQ, Color = ColorQ, Radius = Q.Range }.Draw(Player.Instance.Position);
                    }
                    else
                    {
                        new Circle() { BorderWidth = WidthQ, Color = ColorQ, Radius = Q.Range }.Draw(Player.Instance.Position);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES.Q)</font>");
                }

                try
                {
                    #region W
                    if (DrawReady)
                    {
                        if (W.IsReady())
                            new Circle() { BorderWidth = WidthW, Color = ColorW, Radius = W.Range }.Draw(Player.Instance.Position);
                    }
                    else
                    {
                        new Circle() { BorderWidth = WidthW, Color = ColorW, Radius = W.Range }.Draw(Player.Instance.Position);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES.W)</font>");
                }

                try
                {
                    #region E
                    if (DrawReady)
                    {
                        if (E.IsReady())
                            new Circle() { BorderWidth = WidthE, Color = ColorE, Radius = E.Range }.Draw(Player.Instance.Position);
                    }
                    else
                    {
                        new Circle() { BorderWidth = WidthE, Color = ColorE, Radius = E.Range }.Draw(Player.Instance.Position);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES.E)</font>");
                }

                try
                {
                    #region R
                    if (DrawR)
                    {
                        if (!R.IsOnCooldown)
                            new Circle() { BorderWidth = 2, Color = Color.Green, Radius = R.Range }.Draw(Player.Instance.Position);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES.R)</font>");
                }

                #region Summoners
                try
                {
                    #region Flash
                    if (DrawFlash)
                    {
                        if (Player.CanUseSpell(GetFlashSpellSlot()) == SpellState.Ready)
                            new Circle() { BorderWidth = WidthFlash, Color = ColorFlash, Radius = flashrange }.Draw(Player.Instance.Position);
                        if (Player.CanUseSpell(GetFlashSpellSlot()) == SpellState.Cooldown)
                            new Circle() { BorderWidth = WidthFlash, Color = ColorFlash, Radius = flashrange }.Draw(Player.Instance.Position);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES.FLASH)</font>");
                }

                try
                {
                    #region Ignite
                    if (DrawIgnite)
                    {
                        if (Player.CanUseSpell(GetIgniteSpellSlot()) == SpellState.Ready)
                            new Circle() { BorderWidth = WidthIgnite, Color = ColorIgnite, Radius = igniterange }.Draw(Player.Instance.Position);
                        if (Player.CanUseSpell(GetIgniteSpellSlot()) == SpellState.Cooldown)
                            new Circle() { BorderWidth = WidthIgnite, Color = ColorIgnite, Radius = igniterange }.Draw(Player.Instance.Position);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES.Ignite)</font>");
                }

                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code DRAWRANGES)</font>");
            }
        }
        #endregion
        #endregion
        #region GetSpellSlots
        private static SpellSlot GetFlashSpellSlot()
        {
            try
            {
                if (Player.GetSpell(SpellSlot.Summoner1).Name == "summonerflash")
                    return SpellSlot.Summoner1;
                if (Player.GetSpell(SpellSlot.Summoner2).Name == "summonerflash")
                    return SpellSlot.Summoner2;
                return SpellSlot.Unknown;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code GETFLASHSPELLSLOT)</font>");
                return SpellSlot.Unknown;
            }
        }

        private static SpellSlot GetIgniteSpellSlot()
        {
            try
            {
                if (Player.GetSpell(SpellSlot.Summoner1).Name.ToLower() == "summonerignite")
                    return SpellSlot.Summoner1;
                if (Player.GetSpell(SpellSlot.Summoner2).Name.ToLower() == "summonerignite")
                    return SpellSlot.Summoner2;
                return SpellSlot.Unknown;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code GETIGNITESPELLSLOT)</font>");
                return SpellSlot.Unknown;
            }
        }
        #endregion
    }

    #region Misc
    static class KataExtensions
    {
        public static void AddStringList(this EloBuddy.SDK.Menu.Menu m, string uniqueId, string displayName, string[] values, int defaultValue)
        {
            try
            {
                var mode = m.Add(uniqueId, new Slider(displayName, defaultValue, 0, values.Length - 1));
            mode.DisplayName = displayName + ": " + values[mode.CurrentValue];
            mode.OnValueChange += delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                sender.DisplayName = displayName + ": " + values[args.NewValue];
            };
        }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print("<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code ADDSTRINGLIST)</font>");
            }
        }

    }
    #endregion
}

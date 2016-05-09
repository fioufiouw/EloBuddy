using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace LanternBuddy
{
    class Program
    {
        public static Obj_AI_Base PrioritizedAlly;
        public static Spell.Skillshot W;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingOnOnLoadingComplete;
        }

        private static void LoadingOnOnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Thresh)
                return;

            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 250, 400);

            Config.Generate();

            DrawManager.Init();

            Game.OnTick += GameOnOnTick;
        }

        private static void GameOnOnTick(EventArgs args)
        {

            if (!Config.Menu["enable"].Cast<KeyBind>().CurrentValue)
                return;
            if (!Config.Menu["use"].Cast<KeyBind>().CurrentValue)
                return;
            if (Config.Menu["minmana"].Cast<Slider>().CurrentValue > Player.Instance.ManaPercent)
                return;

            var toCursor = Config.Menu["measureToCursor"].Cast<CheckBox>().CurrentValue;
            var priority = Config.Menu["priority"].Cast<ComboBox>().CurrentValue;


            switch (priority)
            {
                case 0:
                    PrioritizedAlly = EntityManager.Heroes.Allies.Where(h => h.IsValid && !h.IsDead && !h.IsMe)
                        .OrderBy(h => h.Health)
                        .FirstOrDefault();
                    break;
                case 1:
                    PrioritizedAlly = EntityManager.Heroes.Allies.Where(h => h.IsValid && !h.IsDead && !h.IsMe)
                        .OrderBy(h => h.Distance(toCursor ? Game.CursorPos : Player.Instance.ServerPosition))
                        .FirstOrDefault();
                    break;
                case 2:
                    PrioritizedAlly = EntityManager.Heroes.Allies.Where(h => h.IsValid && !h.IsDead && !h.IsMe)
                        .OrderBy(h => h.Distance(toCursor ? Game.CursorPos : Player.Instance.ServerPosition))
                        .FirstOrDefault();
                    break;
            }


            if (PrioritizedAlly == null || !W.IsReady())
                return;

            var enemyCount = EntityManager.Heroes.Enemies.Count(h => !h.IsDead && h.IsInRange(PrioritizedAlly, 700));
            if (enemyCount < Config.Menu["castEnemyCount"].Cast<Slider>().CurrentValue)
                return;

            if (PrioritizedAlly.IsInRange(Player.Instance, Config.Menu["checkDist"].Cast<Slider>().CurrentValue) && Config.Menu["walk"].Cast<CheckBox>().CurrentValue)
                Orbwalker.MoveTo(toCursor ? Game.CursorPos : PrioritizedAlly.ServerPosition);

            if (Config.HumanizerMenu["castOnScreen"].Cast<CheckBox>().CurrentValue && !PrioritizedAlly.IsHPBarRendered)
                return;


            var minDelay = Config.HumanizerMenu["minDelay"].Cast<Slider>().CurrentValue;
            var maxDelay = Config.HumanizerMenu["maxDelay"].Cast<Slider>().CurrentValue;
            var rand = new Random((Environment.TickCount/Game.TicksPerSecond)).Next(minDelay, maxDelay);
            var predPos = Prediction.Position.PredictUnitPosition(PrioritizedAlly, W.CastDelay + W.Speed).To3D();

            Console.WriteLine(rand);
            Core.DelayAction(() =>
                W.Cast(predPos),
                rand);
        }
    }
}
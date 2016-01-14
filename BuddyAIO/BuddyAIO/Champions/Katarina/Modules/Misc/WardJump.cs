using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyAIO.Utility.Config;
using Menu = BuddyAIO.Utility.Config.MenuExtensions;
using EloBuddy.SDK;
using EloBuddy;
using SharpDX;

namespace BuddyAIO.Champions.Katarina.Modules.Misc
{
    class WardJump : IModule
    {
        public EloBuddy.SDK.Menu.Menu mMenu;
        private bool Wardjump;
        public override void MenuCreate()
        {
            mMenu = Menu.AddSubMenu("Flee", "flee");
            mMenu.AddCheckBox("usee", "Use E");
            mMenu.AddCheckBox("wardjump", "Wardjump");
        }
        public override ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }
        public new bool ShouldDo()
        {
            MenuIndex.Flee flee = new MenuIndex.Flee();
            if (flee.UseE && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Flee)
            {
                if (flee.WardJump)
                {
                    Wardjump = true;
                }
                return true;
            }
            return false;
        }

        public override void Do()
        {
            foreach (var minion in EntityManager.MinionsAndMonsters.Get(EntityManager.MinionsAndMonsters.EntityType.Both, EntityManager.UnitTeam.Both
                    , Player.Instance.Position, Spells.E.Range))
            {
                foreach (var hero in EntityManager.Heroes.AllHeroes.Where(h => Player.Instance.IsInRange(h, Spells.E.Range)))
                {
                    var cursorpos = Game.CursorPos;
                    if (minion.IsValid && minion != null && minion.IsInRange(cursorpos, 250) && Spells.E.IsReady())
                    {
                        Spells.E.Cast(minion);
                    }

                    else
                    {
                        if (hero.IsValid && hero != null && hero.IsInRange(cursorpos, 250) && Spells.E.IsReady())
                        {
                            Spells.E.Cast(hero);
                        }
                        else
                        {
                            var wardslot = Utility.Extensions.GetWardSlot();
                            if (wardslot != null && wardslot.CanUseItem() && Wardjump)
                            {
                                wardslot.Cast(cursorpos);
                                foreach (var ward in ObjectManager.Get<Obj_Ward>().OrderBy(w => w.Distance(Player.Instance)))
                                {
                                    if (Spells.E.IsReady() && Spells.E.IsInRange(ward) && ward.IsValid && ward != null)
                                    {
                                        Spells.E.Cast(ward);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void WardJumpTo(Vector3 vect)
        {
            var wardslot = Utility.Extensions.GetWardSlot();
            if (wardslot != null && wardslot.CanUseItem())
            {
                wardslot.Cast(vect);
                foreach (var ward in ObjectManager.Get<Obj_Ward>().OrderBy(w => w.Distance(Player.Instance)))
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(ward) && ward.IsValid && ward != null)
                    {
                        Spells.E.Cast(ward);
                    }
                }
            }
        }
    }
}

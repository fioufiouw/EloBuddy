using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace KataBuddy.Utils
{
    class WardJumper
    {
        private static AIHeroClient _playerClient = ObjectManager.Player;

        private static Item GreenWard = new Item(2044, 600);
        private static Item PinkWard = new Item(2043, 600);
        private static Item YellowWard = new Item(3340, 600);
        private static Item YellowWardPlus = new Item(3361, 600);

        public static void YellowJump()
        {
            var wards = ObjectManager.Get<Obj_Ward>()
                .Where(w => w.IsMe);

            if (HasYellowWard())
            {
                if (YellowWard.IsReady())
                {
                    YellowWard.Cast(Game.CursorPos);
                }
                foreach (var ward in wards)
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(ward))
                    {
                        Spells.E.Cast(ward);
                    }
                }
            }
        }

        public static void YellowJumpPlus()
        {
            var wards = ObjectManager.Get<Obj_Ward>()
                .Where(w => w.IsMe);

            if (HasYellowWardPlus())
            {
                if (YellowWardPlus.IsReady())
                {
                    YellowWardPlus.Cast(Game.CursorPos);
                }
                foreach (var ward in wards)
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(ward))
                    {
                        Spells.E.Cast(ward);
                    }
                }
            }
        }

        public static void GreenJump()
        {
            var wards = ObjectManager.Get<Obj_Ward>()
                .Where(w => w.IsMe);

            if (HasGreenWard())
            {
                if (GreenWard.IsReady())
                {
                   GreenWard.Cast(Game.CursorPos);
                }
                foreach (var ward in wards)
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(ward))
                    {
                        Spells.E.Cast(ward);
                    }
                }
            }
        }

        public static void PinkJump()
        {
            var wards = ObjectManager.Get<Obj_Ward>()
                .Where(w => w.IsMe);

            if (HasPinkWard())
            {
                if (PinkWard.IsReady())
                {
                    PinkWard.Cast(Game.CursorPos);
                }
                foreach (var ward in wards)
                {
                    if (Spells.E.IsReady() && Spells.E.IsInRange(ward))
                    {
                        Spells.E.Cast(ward);
                    }
                }
            }
        }

        private static bool HasGreenWard()
        {
            GreenWard = new Item(2044, 600);

            if (GreenWard.IsOwned(_playerClient))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool HasPinkWard()
        {
            PinkWard = new Item(2043, 600);

            if (PinkWard.IsOwned(_playerClient))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool HasYellowWard()
        {
            YellowWard = new Item(3340, 600);

            if (YellowWard.IsOwned(_playerClient))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool HasYellowWardPlus()
        {
            YellowWardPlus = new Item(3361, 600);

            if (YellowWardPlus.IsOwned(_playerClient))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

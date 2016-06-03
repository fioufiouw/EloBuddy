using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CondemnBuddy
{
    static class Extensions
    {
        public static SpellSlot? GetFlashSpellSlot()
        {
            return Player.Instance.GetSpellSlotFromName("summonerflash");
        }

        public static bool IsWallOrStructure(this Vector3 pos)
        {
            return pos.ToNavMeshCell().CollFlags == CollisionFlags.Building ||
                   pos.ToNavMeshCell().CollFlags == CollisionFlags.Wall;
        }

        public static bool IsWallOrStructure(this Vector2 pos)
        {
            return pos.ToNavMeshCell().CollFlags == CollisionFlags.Building ||
                   pos.ToNavMeshCell().CollFlags == CollisionFlags.Wall;
        }
    }
}

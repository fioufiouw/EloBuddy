using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace KataBuddy.Utils
{
    class UltiManager
    {
        public static bool IsCastingUlt;

        private static AIHeroClient playerClient
        {
            get { return ObjectManager.Player; }
        }

        public static void Init()
        {
            Orbwalker.DisableMovement = playerClient.HasBuff("katarinarsound");
            Orbwalker.DisableAttacking =  playerClient.HasBuff("katarinarsound");
            IsCastingUlt = playerClient.HasBuff("katarinarsound");
        }
    }
}

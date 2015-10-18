using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBuddy
{
    class MiscRanges
    {
        public static float GetEQRange()
        {
            return Spells.Q.Range + Spells.E.Range;
        }

        public static float GetERRange()
        {
            return Spells.R.Range + Spells.E.Range;
        }

        public static float GetEWRange()
        {
            return Spells.Q.Range + Spells.E.Range;
        }

        public static float GetEIgniteRange()
        {
            return Spells.Ignite.Range + Spells.E.Range;
        }
    }
}

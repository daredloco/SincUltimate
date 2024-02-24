using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincUltimate
{
    public static class Globals
    {
        public static bool FirstStart = false;
        private const uint MinOSFans = 100000;

        public static bool CanOS()
        {
            return GameSettings.Instance.MyCompany.Fans >= MinOSFans;
        }
    }
}

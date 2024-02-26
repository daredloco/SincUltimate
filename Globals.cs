using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincUltimate
{
    public static class Globals
    {
        public static bool DebugMode = false;
        public static bool FirstStart = false;
        private const uint MinOSFans = 100000;

        public static bool CanOS()
        {
            return GameSettings.Instance.MyCompany.Fans >= MinOSFans;
        }

        public static void WriteLog(string msg, string type = "")
        {
            if (!DebugMode)
            {
                return;
            }
            switch (type)
            {
                case "warn":
                    DevConsole.Console.LogWarning(msg);
                    break;
                case "info":
                    DevConsole.Console.LogInfo(msg);
                    break;
                case "error":
                    DevConsole.Console.LogError(msg);
                    break;
                default:
                    DevConsole.Console.Log(msg);
                    break;

            }
        }
    }
}

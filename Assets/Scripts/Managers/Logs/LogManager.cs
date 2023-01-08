using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.Managers;
using Kebab.DesignData;

namespace Kebab.BattleEngine.Logs
{
    public static class BattleEngineLogs
    {
        private const string PREFIX = "(BattleEngine) ";
        private static DebugDesignData debugDesignData = null;

        private static DebugDesignData DebugDesignData
        {
            get
            {
                if (debugDesignData == null)
                    debugDesignData = DesignDataManager.Get<DebugDesignData>();
                return (debugDesignData);
            }
        }
        public static void Log(LogVerbosity verbosity, string format, params object[] parameters)
        {
            Log(verbosity, string.Format(format, parameters));
        }

        public static void Log(LogVerbosity verbosity, string text)
        {
            if ((int)DebugDesignData.logVerbosity < (int)verbosity)
                return;
            Debug.Log(PREFIX + text);
        }
    }
}

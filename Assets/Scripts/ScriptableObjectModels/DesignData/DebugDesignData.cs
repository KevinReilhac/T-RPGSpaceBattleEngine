using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kebab.DesignData;
using Kebab.BattleEngine.Logs;

[CreateAssetMenu(fileName = "DebugDesignData", menuName = "DesignData/DebugDesignData", order = 20)] 
public class DebugDesignData : baseDesignData
{
    public LogVerbosity logVerbosity = LogVerbosity.Low;
}
using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static float GetDifference(float a, float b)
        {
            return Math.Abs(a - b);
        }
        
    }
}
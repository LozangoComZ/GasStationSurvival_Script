using System;
using System.Collections;
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
        public static object DrawRandomObject(Dictionary<object, object> dic)
        {
            int totalChances = 0;
            foreach (int chance in dic.Values)
            {
                totalChances += chance;
            }

            int rndDraw = rnd.Next(0, totalChances);

            int acumulador = 0;
            for(int i = 0;i < dic.Values.Count;i++)
            {
                acumulador += (int)dic.Values.ToArray()[i];
                if (rndDraw < acumulador){
                    return (object)dic.Keys.ToArray()[i];
                }
            }

            //Error
            
            throw new Exception("Cannot draw random object");
        }
    }
}
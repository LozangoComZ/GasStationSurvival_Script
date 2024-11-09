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
        public static object DrawRandomObject(object[] items, int[] chances)
        {
            int totalChances = 0;
            foreach (int chance in chances){
                totalChances += chance;
            }

            int rndDraw = rnd.Next(0, totalChances);

            int acumulador = 0;
            foreach (int chance in chances)
            {
                acumulador += chance;
                if (rndDraw < acumulador){
                    return items[Array.IndexOf(chances,chance)];
                }
            }

            //Error
            Msg("Items length = "+items.Length.ToString());
            Msg("Chances length = " + chances.Length.ToString());
            throw new Exception("Cannot draw random object");
        }
    }
}
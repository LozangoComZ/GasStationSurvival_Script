using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static class Wave
        {
            //

            public static WaveConfig Settings;

            public static int CurrentSessionIndex = -1;
            public static List<Enemy> EnemiesList = new List<Enemy>();           //Todos inimigos em cena & inimigos em cena vivos
            public static List<Enemy> AliveEnemiesList { get { return EnemiesList.Where(enemy => !enemy.ply.IsDead).ToList(); } }

            public static bool KillAllToEnd = true;
            //public static readonly int 
        }
    }
}
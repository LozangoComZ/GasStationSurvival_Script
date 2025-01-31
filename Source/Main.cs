using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public Main() : base(null) { }


        public static Random rnd = new Random();

        public void OnStartup()
        {
            StartEvents();
            //Start Wave
            WaveManager.ManualSetup(Game.SurvivalWave);
            EnemySpawn.RefreshSpawnPoints();
        }

        //Others
        /*public static void MsgG(object s) { Game.WriteToConsole("DEBUG: " + s.ToString()); }
        public static void MsgG(object s, string origin) { Game.WriteToConsole(origin+": "+s.ToString()); }
        */
        public static void MsgG(object s) { Game.ShowChatMessage("DEBUG: " + s.ToString()); }
        public static void MsgG(object s, string origin) { Game.ShowChatMessage(origin + ": " + s.ToString()); }
    }
}
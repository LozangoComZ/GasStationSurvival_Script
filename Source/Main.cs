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
        Events.PlayerDeathCallback m_playerDeathEvent = null;

        public void OnStartup()
        {
            //Events
            m_playerDeathEvent = Events.PlayerDeathCallback.Start(OnPlayerDeath);

            //Setup Wave
            WaveManager.ManualSetup(Game.SurvivalWave);
            EnemySpawn.RefreshSpawnPoints();
            WaveManager.TryNextSession();
        }

        public void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (!args.Killed) return;
            //Check if is Enemy
            foreach (Enemy e in Wave.EnemiesList.Where(en => en.ply == player))
            {
                OnEnemyDeath(e,args);
                return;
            }
            //When it's 'hero'
            WaveManager.GameOverCheck();
        }


        //Others
        public static void MsgG(object s) { Game.WriteToConsole("DEBUG: " + s.ToString()); }
        public static void MsgG(object s, string origin) { Game.WriteToConsole(origin+": "+s.ToString()); }
        public static void Msg(object s) { Game.ShowChatMessage("DEBUG: " + s.ToString()); }
        public static void Msg(object s, string origin) { Game.ShowChatMessage(origin + ": " + s.ToString()); }
    }
}
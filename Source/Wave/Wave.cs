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

            //public static readonly int 
        }


        #region Manager

        public static partial class WaveManager
        {
            //INICIA A ONDA
            public static void ManualSetup(int WAVEINDEX)
            {
                MsgG("Setting wave", "WAVEMANAGER");
                if (Wave.Settings != null) { MsgG("Already started.", "WAVEMANAGER"); return; }

                Wave.Settings = WaveConfigList[WAVEINDEX-1];
            }

            //Checa se todos os inimigos da onda estão mortos
            public static bool GameOverCheck()
            {
                //When (All enemies is dead and its the final session) or (All non-bot players is dead).
                if ((AllEnemiesIsDead() && IsFinalSession()) || AllPlayersIsDead())
                {

                    MsgG("Ending wave", "WAVEMANAGER");
                    Game.SetGameOver();
                    return true;
                }
                return false;
            }

            #region SESSIONS
            //Inicia a proxima sessão de inimigos.
            public static bool TryNextSession()
            {
                if (!AllEnemiesIsDead()) return false;

                Wave.CurrentSessionIndex += 1;

                //Chegou no fim das sessões?
                if (IsFinalSession()){
                    MsgG("All spawn session are already spawned", "WAVEMANAGER");
                    return false;
                }

                SpawnSessionEnemies();
                return true;
            }
            //Carrega a sessão
            private static void SpawnSessionEnemies()
            {
                //Start spawning enemies
                MsgG("Spawning enemy session", "WAVEMANAGER");

                int[] ScorePerEnemy = EnemyScore.ShareScore(CurrentSession().EnemyUnits,CurrentSession().ScorePerEnemy * CurrentSession().EnemyUnits,CurrentSession().ScoreConcentration,100);

                for (int i = 0; i < CurrentSession().EnemyUnits; i++)
                {
                    //Delay compatibility
                    int scr = ScorePerEnemy[i];
                    Events.UpdateCallback.Start((float e) => {
                        Wave.EnemiesList.Add(EnemySpawn.SpawnEnemy(scr));
                    }, (uint)(600 * (i+1)), 1);
                }
            }

            public static bool IsFinalSession(){        //Checa se é a ultima sessão
                if (Wave.CurrentSessionIndex == Wave.Settings.SessionsArray.Count) return true;
                return false;
            }
            public static bool AllEnemiesIsDead(){      //Checa se todos os inimigos estão mortos
                if (Wave.AliveEnemiesList.Count < 1) return true;
                return false;
            }
            public static bool AllPlayersIsDead(){      //Checa se todos os players (non-bots) estão mortos
                if (Game.GetPlayers().Where(x => x.GetTeam() == PlayerTeam.Team1 && !x.IsDead).ToArray().Length < 1) return true;
                return false;
            }
            public static WaveConfig.Session CurrentSession() { return Wave.Settings.SessionsArray[Wave.CurrentSessionIndex]; }
            #endregion
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static partial class WaveManager
        {
            //INICIA A ONDA
            public static void ManualSetup(int WAVEINDEX)
            {

                MsgG("Setting wave", "WAVEMANAGER");
                if (Wave.Settings != null) { MsgG("Already started.", "WAVEMANAGER"); return; }

                Wave.Settings = WaveConfigList[WAVEINDEX - 1];
                TryNextSession();
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
            public static void ForceEnd()
            {
                if (Wave.KillAllToEnd) KillAll();
                else TryNextSession();
            }
            public static void KillAll()
            {
                StopCurrentEnemySpawnOrders();
                foreach (Enemy enemy in Wave.AliveEnemiesList)
                {
                    enemy.ply.Remove();
                }
            }
            public static bool TryNextSession()
            {
                if (!AllEnemiesIsDead()) return false;
                NextSession();
                return true;
            }
            //Carrega a sessão
            private static void NextSession()
            {
                Wave.CurrentSessionIndex += 1;
                if (IsFinalSession())
                {
                    MsgG("All spawn session are already spawned", "WAVEMANAGER");
                    return;
                }
                SpawnEnemies();
            }

            private static List<Events.UpdateCallback> EnemySpawnOrders = new List<Events.UpdateCallback>();
            private static void SpawnEnemies()
            {
                //Start spawning enemies
                MsgG("Spawning enemy session", "WAVEMANAGER");

                int[] ScorePerEnemy = EnemyScore.ShareScore(CurrentSession().EnemyUnits, CurrentSession().ScorePerEnemy * CurrentSession().EnemyUnits, CurrentSession().ScoreConcentration, 100);

                for (int i = 0; i < CurrentSession().EnemyUnits; i++)
                {
                    //Spawn enemy with score
                    int scr = ScorePerEnemy[i];
                    EnemySpawnOrders.Add(Events.UpdateCallback.Start((float e) => {
                        Wave.EnemiesList.Add(EnemySpawn.SpawnEnemy(scr));
                    }, (uint)(600 * (i + 1)), 1));

                }
            }
            public static void StopCurrentEnemySpawnOrders()
            {
                foreach (Events.UpdateCallback spawnOrder in EnemySpawnOrders)
                {
                    spawnOrder.Stop();
                }
                EnemySpawnOrders = new List<Events.UpdateCallback>();
            }

            public static bool IsFinalSession()
            {      //Checa se é a ultima sessão
                if (Wave.CurrentSessionIndex == Wave.Settings.SessionsArray.Count) return true;
                return false;
            }
            public static bool AllEnemiesIsDead()
            {      //Checa se todos os inimigos estão mortos
                if (Wave.AliveEnemiesList.Count < 1) return true;
                return false;
            }
            public static bool AllPlayersIsDead()
            {      //Checa se todos os players (non-bots) estão mortos
                if (Game.GetPlayers().Where(x => x.GetTeam() == PlayerTeam.Team1 && !x.IsDead).ToArray().Length < 1) return true;
                return false;
            }
            public static WaveConfig.Session CurrentSession() { return Wave.Settings.SessionsArray[Wave.CurrentSessionIndex]; }
            #endregion
        }
    }
}

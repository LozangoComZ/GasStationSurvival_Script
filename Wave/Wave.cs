using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static class Wave
        {
            //

            public static WaveSetting Settings = null;

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
                Msg("Setting wave", "WAVEMANAGER");
                if (Wave.Settings != null) { Msg("Already started.", "WAVEMANAGER"); return; }

                Wave.Settings = WaveSettingsList[WAVEINDEX-1];
            }

            //Checa se todos os inimigos da onda estão mortos
            public static bool GameOverCheck()
            {
                //When (All enemies is dead and its the final session) or (All non-bot players is dead).
                if ((AllEnemiesIsDead() && IsFinalSession()) || AllPlayersIsDead())
                {

                    Msg("Ending wave", "WAVEMANAGER");
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
                    Msg("All spawn session are already spawned", "WAVEMANAGER");
                    return false;
                }

                LoadCurrentSession();
                return true;
            }
            private static void LoadCurrentSession()
            {
                Msg("Spawning enemy session", "WAVEMANAGER");

                //Spawn enemies
                int enemysToSpawn = CurrentSession().EnemyUnits;
                int scorePerEnemy = (int)(CurrentSession().EnemyScore / enemysToSpawn);
                for (int i = 0; i < enemysToSpawn; i++)
                {
                    Wave.EnemiesList.Add(EnemyManager.SpawnEnemy(scorePerEnemy));
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
            public static WaveSetting.Session CurrentSession() { return Wave.Settings.SessionsArray[Wave.CurrentSessionIndex]; }
            #endregion
        }

        #endregion
    }
}
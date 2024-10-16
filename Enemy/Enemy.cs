using System;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public class Enemy
        {
            public IPlayer ply;
            public EnemySettings Settings;
        }

        #region MANAGER

        //Domina diretamente os INIMIGOS
        public static class EnemyManager
        {
            //Define locais para invocar inimigos.
            private static Vector2[] SpawnPoints;
            public static void RefreshSpawnPoints()
            {
                IObject[] objs = Game.GetObjects("ENEMYSPAWNPOINT");
                SpawnPoints = objs.Select(obj => obj.GetWorldPosition()).ToArray();
            }

            //Invoca um inimigo
            public static Enemy SpawnEnemy(int score)
            {
                Vector2 SpawnPoint = SpawnPoints[rnd.Next(0, SpawnPoints.Length)];
                EnemySettings EnemySet = GetEnemyTemplateByBaseScore(score);

                Msg(String.Concat("Spawning new enemy. Score: ",score.ToString()," / Set: ", EnemySet.Name), "SPAWNENEMY");

                Enemy newEnemy = new Enemy();
                newEnemy.Settings = EnemySet;
                newEnemy.ply = newEnemy.Settings.GetSpawn().CreatePlayer();
                newEnemy.ply.SetWorldPosition(SpawnPoint);
                newEnemy.ply.SetCameraSecondaryFocusMode(CameraFocusMode.Ignore);
                return newEnemy;
            }

            public static void OnEnemyDeath(Enemy enemy, PlayerDeathArgs args)
            {
                if (enemy.Settings == null) return;

                Game.TotalScore += enemy.Settings.BaseScore;
                Msg("New score: "+Game.TotalScore.ToString(),"ONENEMYDEATH");

                WaveManager.TryNextSession();
                WaveManager.GameOverCheck();
            }
        }

        public static EnemySettings GetEnemyTemplateByBaseScore(float baseScore)
        {
            return EnemySettingsList.OrderBy(x => GetDifference(x.BaseScore, baseScore)).First();
        }

        #endregion
    }
}

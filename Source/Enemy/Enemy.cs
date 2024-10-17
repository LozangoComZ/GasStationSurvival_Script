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
            public int score;
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
                //Set things
                Vector2 SpawnPoint = SpawnPoints[rnd.Next(0, SpawnPoints.Length)];
                EnemySettings EnemySet = GetEnemyTemplateByBaseScore(score);

                Msg(String.Concat("Spawning new enemy. Score: ", score.ToString(), " / Set: ", EnemySet.Name), "SPAWNENEMY");

                //Configure enemy settings
                Enemy newEnemy = new Enemy();
                newEnemy.score = score;
                newEnemy.Settings = EnemySet;

                //Configure enemy player
                newEnemy.ply = newEnemy.Settings.GetSpawn().CreatePlayer();
                IPlayer ply = newEnemy.ply;

                //Configure player status
                PlayerModifiers newMod = ply.GetModifiers();
                newMod.MaxHealth = (int)(score / 2);
                newMod.CurrentHealth = newMod.CurrentHealth;
                newMod.SizeModifier = Math.Min((score-EnemySet.BaseScore) / 100,1.25f);

                ply.SetModifiers(newMod);

                //Configure player profile
                IProfile profile = EnemySet.GetAltProfiles()[rnd.Next(EnemySet.GetAltProfiles().Count)];
                ply.SetProfile(profile);

                //Teleport & others
                ply.SetWorldPosition(SpawnPoint);
                ply.SetCameraSecondaryFocusMode(CameraFocusMode.Ignore);
                ply.SetBotName(score.ToString());

                return newEnemy;
            }

            public static void OnEnemyDeath(Enemy enemy, PlayerDeathArgs args)
            {
                if (enemy.Settings == null) return;

                Game.TotalScore += enemy.score;
                Msg("New score: " + Game.TotalScore.ToString(), "ONENEMYDEATH");

                WaveManager.TryNextSession();
                WaveManager.GameOverCheck();
            }
        }

        #endregion
    }
}

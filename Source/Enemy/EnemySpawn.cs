using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static class EnemySpawn
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
                EnemyConfig EnemySet = EnemyScore.GetEnemyTemplateByBaseScore(score);

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
                newMod.CurrentHealth = newMod.MaxHealth;
                newMod.MeleeDamageDealtModifier = score / 150f;
                newMod.MeleeForceModifier = score / 100f;
                newMod.SizeModifier = Math.Min((score < 100 ? 100 : score) / 100, 1.25f);

                ply.SetModifiers(newMod);

                //Configure player weapons
                int wpnScore = score;
                if (wpnScore < 0) wpnScore = 0;
                EnemyWeapon.WeaponSet wpnSet = EnemyWeapon.GetWeaponSet(wpnScore, (float)(rnd.NextDouble() + 1), rnd.Next(-10, 10));

                ply.GiveWeaponItem(wpnSet.meleeWpn);
                ply.GiveWeaponItem(wpnSet.handgunWpn);
                ply.GiveWeaponItem(wpnSet.rifleWpn);

                //Configure player profile
                IProfile profile = EnemySet.GetAltProfiles()[rnd.Next(EnemySet.GetAltProfiles().Count)];
                ply.SetProfile(profile);

                //Teleport & others
                ply.SetWorldPosition(SpawnPoint);
                ply.SetCameraSecondaryFocusMode(CameraFocusMode.Ignore);
                ply.SetBotName(score.ToString());

                return newEnemy;
            }
        }
    }
}

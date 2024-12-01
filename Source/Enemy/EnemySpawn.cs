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
                EnemyConfig EnemySet = GetEnemyConfigByChance(score);

                MsgG(String.Concat("Spawning new enemy. Score: ", score.ToString(), " / Set: ", EnemySet.Name), "SPAWNENEMY");

                //Configure enemy settings
                Enemy newEnemy = new Enemy(EnemySet);
                score -= EnemySet.BaseScore;

                newEnemy.Settings = EnemySet;
                newEnemy.score = score;

                //Configure enemy player
                IPlayer ply = newEnemy.ply;

                //Configure player status
                PlayerModifiers newMod = ply.GetModifiers();
                newMod.MaxHealth = (int)((score / 2) * EnemySet.healthMultiplier);
                newMod.CurrentHealth = newMod.MaxHealth;
                newMod.MeleeDamageDealtModifier = score / 150f;
                newMod.MeleeForceModifier = (score / 180f) * EnemySet.forceMultiplier;
                newMod.SizeModifier = EnemySet.baseSize;
                newMod.MaxEnergy = (int)(100 * EnemySet.energyMultiplier);

                ply.SetModifiers(newMod);

                //Configure player weapons
                int wpnScore = score;
                if (wpnScore < 0) wpnScore = 0;
                EnemyWeapon.WeaponSet wpnSet = EnemyWeapon.GetWeaponSet(wpnScore, EnemySet, rnd.Next(-10, 10));

                ply.GiveWeaponItem(wpnSet.meleeWpn);
                ply.GiveWeaponItem(wpnSet.handgunWpn);
                ply.GiveWeaponItem(wpnSet.rifleWpn);
                ply.GiveWeaponItem(wpnSet.specialWpn);

                //Configure player profile
                IProfile profile = EnemySet.GetProfiles()[rnd.Next(EnemySet.GetProfiles().Count)];
                ply.SetProfile(profile);

                //BotIA
                ply.SetBotBehaviorSet(EnemySet.BotIASet(score));

                //Teleport & others
                ply.SetWorldPosition(SpawnPoint);
                ply.SetCameraSecondaryFocusMode(CameraFocusMode.Ignore);
                ply.SetBotName(score.ToString() + " " + EnemySet.Name);
                ply.SetInputMode(PlayerInputMode.Disabled);
                Events.UpdateCallback.Start((float e) => {
                    ply.SetInputMode(PlayerInputMode.Enabled);
                }, (uint)800, 1);
                return newEnemy;
            }
        }
    }
}

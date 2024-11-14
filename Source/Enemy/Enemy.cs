using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public class Enemy
        {
            public Enemy(EnemyConfig ec)
            {
                ply = ec.GetSpawn().CreatePlayer();
                EnemyByIPlayer.Add(ply, this);
            }
            public IPlayer ply;
            public int score;
            public EnemyConfig Settings;
        }

        public static void OnEnemyDeath(Enemy enemy, PlayerDeathArgs args)
        {
            if (enemy.Settings == null) return;

            Game.TotalScore += enemy.score;
            MsgG("New score: " + Game.TotalScore.ToString(), "ONENEMYDEATH");

            WaveManager.TryNextSession();
            WaveManager.GameOverCheck();
        }

        public static Dictionary<IPlayer, Enemy> EnemyByIPlayer = new Dictionary<IPlayer, Enemy>() { };
    }
}

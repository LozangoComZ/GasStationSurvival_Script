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
            public EnemyConfig Settings;
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
}

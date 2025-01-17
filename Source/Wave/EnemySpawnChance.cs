using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;
using static GasStationSurvival_Script.Main;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public class EnemySpawnChance
        {

            public List<EnemyConfig> enemyConfigs = new List<EnemyConfig> { };
            public List<int> configsChance = new List<int> { };
            public List<int> configsMinScore = new List<int> { };
            public EnemySpawnChance(EnemyConfig[] enemyConfigs, int[] configsChance, int[] configsMinScore)
            {
                this.enemyConfigs = enemyConfigs.ToList();
                this.configsChance = configsChance.ToList();
                this.configsMinScore = configsMinScore.ToList();
            }
        }

        public static EnemySpawnChance SpawnChanceInitial
        {
            get
            {
                return new EnemySpawnChance(
                    new EnemyConfig[] {
                        getEnemyConfigByName("ROOKIE"),
                        getEnemyConfigByName("PUNK"),
                    }
                    , new int[] { 10, 10 }, new int[] { 0, 55 });
            }
        }
        public static EnemySpawnChance SpawnChanceMiddle
        {
            get
            {
                return new EnemySpawnChance(
                    new EnemyConfig[] {
                        getEnemyConfigByName("ROOKIE"),
                        getEnemyConfigByName("PUNK"),
                        getEnemyConfigByName("MOLOTOV"),
                        getEnemyConfigByName("BIG"),
                        getEnemyConfigByName("SUICIDE"),
                    }
                    , new int[] { 10, 10, 6, 4, 1000 }, new int[] { 0, 60, 80, 80, 20 });
            }
        }
    }
}

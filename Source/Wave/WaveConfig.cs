using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;
using static GasStationSurvival_Script.Main;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        //OBJETO DE LEITURA
        //Seu uso é especifico para: SETUP de wave
        public class WaveConfig
        {
            public class Session
            {
                public int EnemyUnits = 0;              //Quantidade de inimigos
                public int ScorePerEnemy = 0;           //Dificuldade distribuida entre todos os inimigos
                public double ScoreConcentration = 0;   //Concentração dos pontos nos inimigos. (0 = Distribuido igualmente; 1 = Distribui minimamente)
                public EnemySpawnChance enemySpawnChance = SpawnChance0;
            }

            public List<Session> SessionsArray =    //*Será usada de index 0 até o final durante a leitura numa Wave.
            new List<Session>{
            };
        }

        //

        public class EnemySpawnChance
        {

            public List<EnemyConfig> enemyConfigs = new List<EnemyConfig>{};
            public List<int> configsChance = new List<int> { };
            public List<int> configsMinScore = new List<int> { };
            public EnemySpawnChance(EnemyConfig[] enemyConfigs, int[] configsChance, int[] configsMinScore)
            {
                this.enemyConfigs = enemyConfigs.ToList();
                this.configsChance = configsChance.ToList();
                this.configsMinScore = configsMinScore.ToList();
            }
        }

        public static EnemySpawnChance SpawnChance0 {
            get{ 
                return new EnemySpawnChance(
                    new EnemyConfig[] {     
                        getEnemyConfigByName("ROOKIE"),
                        getEnemyConfigByName("PUNK"),
                        getEnemyConfigByName("MOLOTOV"),
                        getEnemyConfigByName("BIG")
                    }
                    , new int[] { 10 , 10,  6,  100}, new int[] { 0 , 60, 80, 80}); 
            } 
        }        //

        #region SETTINGS_LIST
        public static readonly WaveConfig[] WaveConfigList = {
            new WaveConfig(){ //0
                SessionsArray ={
                    new WaveConfig.Session(){
                    EnemyUnits=1,
                    ScorePerEnemy=100,
                    ScoreConcentration=1,
                    enemySpawnChance = SpawnChance0,
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=4,
                    ScorePerEnemy=80,
                    ScoreConcentration=1.25,
                    enemySpawnChance = SpawnChance0,
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=4,
                    ScorePerEnemy=100,
                    ScoreConcentration=1.5,
                    enemySpawnChance = SpawnChance0,
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=4,
                    ScorePerEnemy=130,
                    ScoreConcentration=2,
                    enemySpawnChance = SpawnChance0,
                    },
                },
            },//Default

        };
        #endregion
    }
}

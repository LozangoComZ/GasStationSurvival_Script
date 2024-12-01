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
                public EnemySpawnChance enemySpawnChance = SpawnChanceInitial;
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

        public static EnemySpawnChance SpawnChanceInitial {
            get{ 
                return new EnemySpawnChance(
                    new EnemyConfig[] {     
                        getEnemyConfigByName("ROOKIE"),
                        getEnemyConfigByName("PUNK"),
                    }
                    , new int[] { 10 , 10}, new int[] { 0 , 55}); 
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
                    , new int[] { 10, 10, 6, 4, 1000}, new int[] { 0, 60, 80, 80, 20 });
            }
        }

        #region SETTINGS_LIST
        //Lista das Waves manuais.
        public static readonly WaveConfig[] WaveConfigList = {
            new WaveConfig(){ //0
                SessionsArray ={
                    new WaveConfig.Session(){
                    EnemyUnits=1,
                    ScorePerEnemy=250,
                    ScoreConcentration=1,
                    enemySpawnChance = SpawnChanceMiddle,
                    },
                },
            },//Default

        };
        #endregion
    }
}

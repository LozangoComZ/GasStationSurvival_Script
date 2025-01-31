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

                public BossConfig Boss;
            }

            public List<Session> SessionsArray = new List<Session>{};//*Será usada o index durante a leitura numa Wave.
            
        }

        #region SETTINGS_LIST
        //Lista das Waves manuais.
        public static readonly WaveConfig[] WaveConfigList = {
            new WaveConfig(){ //0
                SessionsArray ={
                    new WaveConfig.Session(){
                    EnemyUnits=3,
                    ScorePerEnemy=90,
                    ScoreConcentration=0.5,
                    enemySpawnChance = SpawnChanceInitial,
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=3,
                    ScorePerEnemy=100,
                    ScoreConcentration=0.5,
                    enemySpawnChance = SpawnChanceInitial,
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=3,
                    ScorePerEnemy=120,
                    ScoreConcentration=0.5,
                    enemySpawnChance = SpawnChanceInitial,
                    },
                },
            },//Default

        };
        #endregion
    }
}

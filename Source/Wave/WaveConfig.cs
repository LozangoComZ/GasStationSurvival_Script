using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

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
            }

            public List<Session> SessionsArray =    //*Será usada de index 0 até o final durante a leitura numa Wave.
            new List<Session>{
            };

        }

        //

        #region SETTINGS_LIST
        public static readonly WaveConfig[] WaveConfigList = {
            new WaveConfig(){ //0
                SessionsArray ={
                    new WaveConfig.Session(){
                    EnemyUnits=3,
                    ScorePerEnemy=50,
                    ScoreConcentration=0.2
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=3,
                    ScorePerEnemy=60,
                    ScoreConcentration=0.5
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=4,
                    ScorePerEnemy=60,
                    ScoreConcentration=1
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=4,
                    ScorePerEnemy=70,
                    ScoreConcentration=1.5
                    },
                    new WaveConfig.Session(){
                    EnemyUnits=4,
                    ScorePerEnemy=100,
                    ScoreConcentration=2
                    },
                }
            },//Default

        };
        #endregion
    }
}

using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        //OBJETO DE LEITURA
        //Seu uso é especifico para: SETUP de wave
        public class WaveSetting
        {
            public class Session
            {
                public int EnemyUnits = 0;              //Quantidade de inimigos
                public int EnemyScore = 0;         //Dificuldade distribuida entre todos os inimigos
                public int ScoreConcentration = 0;       //Concentração dos pontos nos inimigos. (0 = Distribuido igualmente; 1 = Distribui minimamente)
                
            }

            public List<Session> SessionsArray =    //*Será usada de index 0 até o final durante a leitura numa Wave.
            new List<Session>{
            };

        }

        //

        #region SETTINGS_LIST
        public static readonly WaveSetting[] WaveSettingsList = {
            new WaveSetting(){ //0
                SessionsArray ={
                    new WaveSetting.Session(){
                    EnemyUnits=2,
                    EnemyScore=200,
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=4,
                    EnemyScore=500,
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=4,
                    EnemyScore=500,
                    },
                }
            }, //Default
            new WaveSetting(){ //1
                SessionsArray ={
                    new WaveSetting.Session(){
                    EnemyUnits=2,
                    EnemyScore=200,
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=4,
                    EnemyScore=500,
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=4,
                    EnemyScore=500,
                    },
                }
            }, //Default
            new WaveSetting(){ //2
                SessionsArray ={
                    new WaveSetting.Session(){
                    EnemyUnits=2,
                    EnemyScore=200,
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=4,
                    EnemyScore=500,
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=4,
                    EnemyScore=500,
                    },
                }
            }, //Default

        };
        #endregion
    }
}

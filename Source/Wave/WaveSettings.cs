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
                public double ScoreConcentration = 0;       //Concentração dos pontos nos inimigos. (0 = Distribuido igualmente; 1 = Distribui minimamente)
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
                    EnemyUnits=3,
                    EnemyScore=300,
                    ScoreConcentration=0
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=5,
                    EnemyScore=500,
                    ScoreConcentration=0.5
                    },
                    new WaveSetting.Session(){
                    EnemyUnits=5,
                    EnemyScore=500,
                    ScoreConcentration=1
                    },
                }
            },//Default

        };
        #endregion
    }
}

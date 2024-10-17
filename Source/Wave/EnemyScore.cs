using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {

        public partial class WaveManager
        {
            public static int[] ShareScore(int enemyCount, int initScore, double scoreConcentration, int minScorePerIndividual)
            {
                //Passo 0: Separar o minimo para cada individuo
                int minScore = 20;
                int reservedScore = minScore * enemyCount;
                if (initScore < reservedScore) { Msg("Error: Session has insufficient score", "WAVEMANAGER"); return null; }

                int score = initScore - reservedScore;

                // Passo 1: Calcular os pesos para cada item com base no índice z
                double[] weights = new double[enemyCount];
                for (int i = 0; i < enemyCount; i++)
                {
                    weights[i] = Math.Pow(i + 1, scoreConcentration);
                }

                // Passo 2: Calcular a soma total dos pesos
                double weightsSum = weights.Sum();

                // Passo 3: Distribuir os pontos para cada item proporcionalmente aos pesos
                int[] finalScoreList = new int[enemyCount];

                for (int i = 0; i < enemyCount; i++)
                {
                    finalScoreList[i] = (int)Math.Round((weights[i] / weightsSum) * score) + minScore;
                }

                return finalScoreList;
            }
        }
    }
}

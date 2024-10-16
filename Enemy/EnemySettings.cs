using SFDGameScriptInterface;
using System.Collections.Generic;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {

        public static readonly List<EnemySettings> EnemySettingsList = new List<EnemySettings>()
        {
            new EnemySettings
            {

            }
        };
        public class EnemySettings
        {
            public string Name = "Debug";
            public IObjectPlayerSpawnTrigger GetSpawn() { return (IObjectPlayerSpawnTrigger)Game.GetObject("PS-"+Name.ToUpper()); }
            public int BaseScore = 100;

        }


    }
}

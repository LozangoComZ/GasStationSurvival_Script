using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public class EnemyConfig
        {
            // ENEMY CORE
            public string Name = "Debug";
            public int BaseScore = 0;
            public List<string> profilesID = new List<string>();
            public List<IProfile> GetProfiles()
            {
                if (profilesID.Count <= 0) return new List<IProfile> { new IProfile() };

                //Encontra objetos IObjectIPlayerProfileInfo com o CustomID do profilesID.
                List<IObjectPlayerProfileInfo> altProfilesObj = new List<IObjectPlayerProfileInfo>();
                foreach (string profId in profilesID)
                {
                    foreach (IObject obj in Game.GetObjectsByCustomID("PP-" + profId))
                    {
                        altProfilesObj.Add((IObjectPlayerProfileInfo)obj);
                    }
                }

                //Extrai IProfile de altProfilesObj e retorna.
                return altProfilesObj.Select(profObj => profObj.GetProfile()).ToList();
            }
            public IObjectPlayerSpawnTrigger GetSpawn() { return (IObjectPlayerSpawnTrigger)Game.GetObject("SPAWNER"); }


            // WEAPON SET
            public float meleePreference = 1;
            public float handgunPreference = 1;
            public float riflePreference = 1;
            public static Dictionary<WeaponItem, int> specialWpns = new Dictionary<WeaponItem, int>();

            // EVENT
            public Func<Enemy, object> OnSpawn = (Enemy ec) => { return null; };
        }


        public static readonly List<EnemyConfig> EnemyConfigList = new List<EnemyConfig>(){

            new EnemyConfig
            {
                Name = "ROOKIE",
                profilesID = { "DefaultMale", "DefaultFemale" },
                meleePreference = 1f,
                handgunPreference = 0.80f,
                riflePreference = 0.50f,
            },
            new EnemyConfig
            {
                Name = "PUNK",
                profilesID = { "DefaultMale", "DefaultFemale" },
                meleePreference = 0.25f,
                handgunPreference = 1.30f,
                riflePreference = 1.30f,
            }
        };
        public static EnemyConfig getEnemyConfigByName(string name)
        {
            foreach(EnemyConfig eC in EnemyConfigList)
            {
                if (eC.Name == name.ToUpper()) return eC;
            }
            throw new Exception("Tried to get a EnemyConfig that doesn't exist.");
        }
        public static EnemyConfig GetEnemyConfigByChance(float score)
        {
            EnemySpawnChance enemySpawnChance = Wave.Settings.enemySpawnChance;
            List<EnemyConfig> enemyConfigs = enemySpawnChance.enemyConfigs;
            List<int> configsMinScore = enemySpawnChance.configsMinScore;
            List<int> configsChance = enemySpawnChance.configsChance;

            List<EnemyConfig> filteredEnemyConfigs = new List<EnemyConfig>();
            List<int> filteredConfigChances = new List<int>();

            //Filtro de score minimo
            for (int i = 0; i < enemyConfigs.Count; i++)
            {
                if (score > enemySpawnChance.configsMinScore[i])
                {
                    filteredEnemyConfigs.Add(enemyConfigs[i]);
                    filteredConfigChances.Add(configsChance[i]);
                }
            }

            if (filteredConfigChances.Count == 0 || filteredEnemyConfigs.Count == 0)
                throw new Exception("Cannot get a valid EnemyConfig sort list");

            return (EnemyConfig)DrawRandomObject(filteredEnemyConfigs.ToArray(), filteredConfigChances.ToArray());
        }

    }
}

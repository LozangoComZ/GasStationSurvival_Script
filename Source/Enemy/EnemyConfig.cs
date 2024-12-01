using SFDGameScriptInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public partial class EnemyConfig
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
                    foreach (IObject obj in Game.GetObjectsByCustomID("PP-" + profId.ToUpper()))
                    {
                        altProfilesObj.Add((IObjectPlayerProfileInfo)obj);
                    }
                }

                //Extrai IProfile de altProfilesObj e retorna.
                return altProfilesObj.Select(profObj => profObj.GetProfile()).ToList();
            }
            public IObjectPlayerSpawnTrigger GetSpawn() { return (IObjectPlayerSpawnTrigger)Game.GetObject("SPAWNER"); }

            // STATUS

            public float healthMultiplier = 1;
            public float forceMultiplier = 1;
            public float energyMultiplier = 1;

            // -1 = Score-based
            public float baseSize = -1;

            // WEAPON SET
            public float meleePreference = 1;
            public float handgunPreference = 1;
            public float riflePreference = 1;
            public Dictionary<WeaponItem, int> specialWpns = new Dictionary<WeaponItem, int>();

            // EVENT
            public Action<Enemy> OnSpawn = (Enemy ec) => { };
            public Action<Enemy> OnDeath = (Enemy ec) => { };


            // BOT IA
            public Func<int, BotBehaviorSet> BotIASet;
        }


        public static readonly List<EnemyConfig> EnemyConfigList = new List<EnemyConfig>(){

            new EnemyConfig
            {
                Name = "ROOKIE",
                profilesID = { "DefaultMale", "DefaultFemale" },
                meleePreference = 1f,
                handgunPreference = 0.80f,
                riflePreference = 0.30f,
                BotIASet = RookieIA,
            },
            new EnemyConfig
            {
                Name = "PUNK",
                profilesID = { "DefaultMale", "DefaultFemale" },
                meleePreference = 0.25f,
                handgunPreference = 1.30f,
                riflePreference = 1.30f,
                BotIASet = PunkIA,
                forceMultiplier = 0.5f
            },
            new EnemyConfig
            {
                Name = "MOLOTOV",
                profilesID = { "MOLOTOV" },
                meleePreference = 0f,
                handgunPreference = 1.30f,
                riflePreference = 1.30f,
                specialWpns = new Dictionary<WeaponItem, int>()
                {
                    {WeaponItem.MOLOTOVS,0}
                },
                BotIASet = MolotovIA
            },
            new EnemyConfig
            {
                Name = "SUICIDE",
                profilesID = { "SUICIDE" },
                meleePreference = 0.5f,
                handgunPreference = 0.5f,
                riflePreference = 0,
                energyMultiplier = 1.50f,
                healthMultiplier = 0.25f,
                forceMultiplier = 0.25f,
                BotIASet = SuicideIA,
                OnDeath = (Enemy ec) =>
                {
                    new Timer(3, ec.ply, () =>
                    {
                        Game.TriggerExplosion( ec.ply.GetWorldPosition() );
                    });
                }
            },
            new EnemyConfig
            {
                Name = "BIG",
                profilesID = { "BIG" },
                meleePreference = 1.5f,
                handgunPreference = 0,
                riflePreference = 0,
                BotIASet = BigIA,
                baseSize = 1.10f,
                forceMultiplier = 1.25f,
                healthMultiplier = 1.5f
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
            EnemySpawnChance enemySpawnChance = WaveManager.CurrentSession().enemySpawnChance;
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

            var dic = filteredEnemyConfigs.Zip(filteredConfigChances, (key, value) => new { key, value }).ToDictionary(item => (object)item.key, item => (object)item.value);

            return (EnemyConfig)DrawRandomObject(dic);
        }
    }
}

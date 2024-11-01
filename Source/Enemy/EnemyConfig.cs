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
            public IObjectPlayerSpawnTrigger GetSpawn() { return (IObjectPlayerSpawnTrigger)Game.GetObject("PS-" + Name.ToUpper()); }


            // SIMPLE EVENT
            public Func<Enemy, object> OnSpawn = (Enemy ec) => { return null; };


            // MODS
            //public SortedDictionary
        }


        public static readonly List<EnemyConfig> EnemyConfigList = new List<EnemyConfig>(){

            new EnemyConfig
            {
                profilesID = { "DefaultMale", "DefaultFemale" },

            }
        };
    }
}

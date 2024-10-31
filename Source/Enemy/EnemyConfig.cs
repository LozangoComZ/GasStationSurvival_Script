using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static readonly List<EnemyConfig> EnemyConfigList = new List<EnemyConfig>(){
            new EnemyConfig
            {
                altProfilesID = { "DefaultMale", "DefaultFemale" }
            }
        };
        public class EnemyConfig
        {

            public string Name = "Debug";
            public int BaseScore = 0;
            public List<string> altProfilesID = new List<string>();

            public List<IProfile> GetAltProfiles(){
                if (altProfilesID.Count <= 0) return new List<IProfile>{ new IProfile()};

                //Encontra objetos IObjectIPlayerProfileInfo com o CustomID do altProfilesID.
                List<IObjectPlayerProfileInfo> altProfilesObj = new List<IObjectPlayerProfileInfo>();
                foreach (string profId in altProfilesID){
                    foreach (IObject obj in Game.GetObjectsByCustomID("PP-" + profId)){
                        altProfilesObj.Add((IObjectPlayerProfileInfo)obj);
                    }
                }

                //Extrai IProfile de altProfilesObj e retorna.
                return altProfilesObj.Select(profObj => profObj.GetProfile()).ToList();
            }
            public IObjectPlayerSpawnTrigger GetSpawn() { return (IObjectPlayerSpawnTrigger)Game.GetObject("PS-" + Name.ToUpper()); }

        }
    }
}

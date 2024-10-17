using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {

        public class EnemySettings
        {
            public static readonly List<EnemySettings> EnemySettingsList = new List<EnemySettings>(){
                new EnemySettings
                {
                    altProfilesID = { "DefaultMale", "DefaultFemale" }
                }
            };

            private string name = "Debug";
            private int baseScore = 100;
            private List<string> altProfilesID = new List<string>();

            public string Name { get { return name; } }
            public int BaseScore { get { return baseScore; } }

            public List<IProfile> GetAltProfiles(){
                if (altProfilesID.Count <= 0) return new List<IProfile>{ new IProfile()};

                //Encontra objetos IObjectIPlayerProfileInfo com o CustomID do altProfilesID.
                List<IObjectPlayerProfileInfo> altProfilesObj = new List<IObjectPlayerProfileInfo>();
                foreach (string profId in altProfilesID){
                    Msg(profId);
                    foreach (IObject obj in Game.GetObjectsByCustomID("PP-" + profId)){
                        altProfilesObj.Add((IObjectPlayerProfileInfo)obj);
                    }
                }

                //Extrai IProfile de altProfilesObj e retorna.
                return altProfilesObj.Select(profObj => profObj.GetProfile()).ToList();
            }
            public IObjectPlayerSpawnTrigger GetSpawn() { return (IObjectPlayerSpawnTrigger)Game.GetObject("PS-" + Name.ToUpper()); }
        }

        public static EnemySettings GetEnemyTemplateByBaseScore(float baseScore)
        {
            return EnemySettings.EnemySettingsList.OrderBy(x => GetDifference(x.BaseScore, baseScore)).First();
        }
    }
}

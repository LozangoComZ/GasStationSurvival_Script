using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        //Not complete yet
        public Vector2 BossSpawn = new Vector2(-304,-112);
        public void PreCutscene()
        {
            foreach(IPlayer ply in Game.GetPlayers())
            {
                ply.SetInputEnabled(false);
            }
        }
        public void PostCutscene()
        {
            foreach (IPlayer ply in Game.GetPlayers())
            {
                ply.SetInputEnabled(true);
            }
        }
        public void Boss1_1Cutscene(Action callback)
        {
            PreCutscene();
            IObjectPlayerProfileInfo profile = (IObjectPlayerProfileInfo)Game.GetObject("PP-BOSS1");
            IPlayer boss = Game.CreatePlayer(BossSpawn);
            boss.SetProfile(profile.GetProfile());

            boss.SetWorldPosition(BossSpawn);

            Events.UpdateCallback.Start((float a) => {

                float nextdelay = 5000;
                Game.CreateDialogue("Oh, you think you can waltz in here and take my hideout?", boss, "", nextdelay, false);
                Events.UpdateCallback.Start((float b) => {

                    Game.CreateDialogue("Buddy, you will have better luck finding gas in this dump than getting me outta here.", boss, "", nextdelay, false);
                    Events.UpdateCallback.Start((float c) => {

                        boss.Remove();
                        callback();
                        PostCutscene();
                    }, (uint)nextdelay, 1);
                }, (uint)nextdelay, 1);
            }, 1000, 1);
        }


    }
}

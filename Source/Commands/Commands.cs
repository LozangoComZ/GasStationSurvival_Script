using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static GasStationSurvival_Script.Main;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public void OnUserMessage(UserMessageCallbackArgs args)
        {
            MsgG(args.Command);
            if (args.IsCommand)
            {
                if (args.User.IsModerator)
                {
                    switch (args.Command.ToUpper())
                    {
                        case "SKIPSESSION":
                            WaveManager.ForceEnd();
                            break;
                    }
                }
            }
        }
    }
}

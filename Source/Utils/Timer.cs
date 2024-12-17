using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public class Timer
        {
            public Timer(int delayS, IObject followObj, Action onFinish)
            {
                delayRemain = delayS;
                text = (IObjectText)Game.CreateObject("Text");
                
                Action stop = () => { };

                Events.UpdateCallback m_followObjTick = null;
                Action<float> followObjTick = (float f) => {
                    if (followObj.IsRemoved)
                    {
                        stop();
                        return;
                    }
                    text.SetWorldPosition(followObj.GetWorldPosition());
                };
                

                Events.UpdateCallback m_timerTick = null;
                Action<float> timerTick = (float f) => {
                    if(delayRemain == 0) {
                        stop();
                        onFinish();
                        return;
                    }
                    delayRemain -= 1;
                    text.SetText(delayRemain.ToString());
                };
                /*
                Events.UpdateCallback m_colorTick = null;
                bool y = false;
                Action<float> colorTick = (float f) => {
                    if (y == false)
                    {
                        y = true;
                        text.SetTextColor(Color.Yellow);
                    }
                    else
                    {
                        y = false;
                        text.SetTextColor(Color.Red);
                    }
                };
                */
                stop = () =>
                {
                    m_followObjTick.Stop();
                    m_timerTick.Stop();
                    //m_colorTick.Stop();
                    text.Remove();
                };

                m_timerTick = Events.UpdateCallback.Start(timerTick, 1000);
                m_followObjTick = Events.UpdateCallback.Start(followObjTick, 200);
                //m_colorTick = Events.UpdateCallback.Start(colorTick, 200);
            }

            public int delayRemain = -1;
            private IObjectText text;
        }
    }
}

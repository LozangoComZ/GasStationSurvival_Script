using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public class EnemyModifier
        {
            public double forceMultiplier = 1;
            public double speedMultiplier = 1;
            public double sizeMultiplier = 1;
            public double lifeMultiplier = 1;

            public IProfile profile;

            //Armas especiais priorizadas no Weapon Set
            public static Dictionary<WeaponItem, int> meleeWpn = new Dictionary<WeaponItem, int>
            {
                {WeaponItem.NONE,50},
            };
            public static Dictionary<WeaponItem, int> handgunWpn = new Dictionary<WeaponItem, int>
            {
                {WeaponItem.NONE,50},
            };
            public static Dictionary<WeaponItem, int> rifleWpn = new Dictionary<WeaponItem, int>
            {
                {WeaponItem.NONE,50},
            };
            public bool specialWpn = false;


        }
    }
}

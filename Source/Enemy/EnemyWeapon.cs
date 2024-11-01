using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static class EnemyWeapon{
            public static Dictionary<WeaponItem, int> MeleeWpnCostDic = new Dictionary<WeaponItem, int>
                {
                    {WeaponItem.NONE,50},
                    {WeaponItem.HAMMER,70},
                    {WeaponItem.PIPE,80},
                    {WeaponItem.LEAD_PIPE,100},
                    {WeaponItem.BAT,110},
                    {WeaponItem.CHAIN,150},
                };
            public static Dictionary<WeaponItem, int> HandgunWpnCostDic = new Dictionary<WeaponItem, int>
                {
                    {WeaponItem.NONE,50},
                    {WeaponItem.PISTOL,60},
                    {WeaponItem.PISTOL45,100},
                    {WeaponItem.UZI,120},
                    {WeaponItem.MAGNUM,200}
                };
            public static Dictionary<WeaponItem, int> RifleWpnCostDic = new Dictionary<WeaponItem, int>
                {
                    {WeaponItem.NONE,100},
                    {WeaponItem.SHOTGUN,200},
                    {WeaponItem.TOMMYGUN,400},
                    {WeaponItem.ASSAULT,400}
                };

            //Conjunto de armas de um inimigo padrão
            public class WeaponSet
            {
                public WeaponItem meleeWpn = WeaponItem.NONE;
                public WeaponItem handgunWpn = WeaponItem.NONE;
                public WeaponItem rifleWpn = WeaponItem.NONE;
            }
            public static WeaponSet GetWeaponSet(int score, float meleePreference, int randomGap)
            {
                int currentScore = score;
                WeaponSet weaponSet = new WeaponSet();

                KeyValuePair<WeaponItem, int> rifle = RifleWpnCostDic.OrderBy(x => GetDifference(x.Value, (currentScore / meleePreference) + randomGap)).First();
                weaponSet.rifleWpn = rifle.Key;
                if(weaponSet.rifleWpn != WeaponItem.NONE) currentScore -= rifle.Value;
                KeyValuePair<WeaponItem, int> handgun = HandgunWpnCostDic.OrderBy(x => GetDifference(x.Value, (currentScore / meleePreference) + randomGap)).First();
                weaponSet.handgunWpn = handgun.Key;
                if (weaponSet.handgunWpn != WeaponItem.NONE) currentScore -= handgun.Value;
                KeyValuePair<WeaponItem, int> melee = MeleeWpnCostDic.OrderBy(x => GetDifference(x.Value, (currentScore * meleePreference) + randomGap)).First();
                weaponSet.meleeWpn = melee.Key;
                if (weaponSet.meleeWpn != WeaponItem.NONE) currentScore -= melee.Value;

                return weaponSet;
            }
        }
    }
}

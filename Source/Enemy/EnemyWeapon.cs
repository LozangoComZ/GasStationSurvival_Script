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
                    {WeaponItem.NONE,30},
                    {WeaponItem.BOTTLE,50},
                    {WeaponItem.HAMMER,60},
                    {WeaponItem.PIPE,80},
                    {WeaponItem.LEAD_PIPE,100},
                    {WeaponItem.BAT,110},
                    {WeaponItem.CHAIN,150},
                };
            public static Dictionary<WeaponItem, int> HandgunWpnCostDic = new Dictionary<WeaponItem, int>
                {
                    {WeaponItem.NONE,50},
                    {WeaponItem.PISTOL,60},
                    {WeaponItem.REVOLVER,100},
                    {WeaponItem.UZI,200},
                    {WeaponItem.SUB_MACHINEGUN,200}
                };
            public static Dictionary<WeaponItem, int> RifleWpnCostDic = new Dictionary<WeaponItem, int>
                {
                    {WeaponItem.NONE,100},
                    {WeaponItem.SAWED_OFF,150},
                    {WeaponItem.SHOTGUN,200},
                    {WeaponItem.TOMMYGUN,250}
                };

            //Conjunto de armas de um inimigo padrão
            public class WeaponSet
            {
                public WeaponItem meleeWpn = WeaponItem.NONE;
                public WeaponItem handgunWpn = WeaponItem.NONE;
                public WeaponItem rifleWpn = WeaponItem.NONE;
                public WeaponItem specialWpn = WeaponItem.NONE;
            }
            public static WeaponSet GetWeaponSet(int score, EnemyConfig enemyConfig, int randomGap)
            {
                int currentScore = score;
                WeaponSet weaponSet = new WeaponSet();
                float meleePreference = enemyConfig.meleePreference;
                float handgunPreference = enemyConfig.handgunPreference;
                float riflePreference = enemyConfig.riflePreference;

                KeyValuePair<WeaponItem, int> melee;
                KeyValuePair<WeaponItem, int> handgun;
                KeyValuePair<WeaponItem, int> rifle;

                Action buyRifle = () => {
                    rifle = RifleWpnCostDic.OrderBy(x => GetDifference(x.Value, (currentScore * riflePreference) + randomGap)).First();
                    weaponSet.rifleWpn = rifle.Key;
                    if (weaponSet.rifleWpn != WeaponItem.NONE) currentScore -= rifle.Value;
                };
                Action buyHandgun = () => {
                    handgun = HandgunWpnCostDic.OrderBy(x => GetDifference(x.Value, (currentScore * handgunPreference) + randomGap)).First();
                    weaponSet.handgunWpn = handgun.Key;
                    if (weaponSet.handgunWpn != WeaponItem.NONE) currentScore -= handgun.Value;
                };
                Action buyMelee = () => {
                    melee = MeleeWpnCostDic.OrderBy(x => GetDifference(x.Value, (currentScore * meleePreference) + randomGap)).First();
                    weaponSet.meleeWpn = melee.Key;
                    if (weaponSet.meleeWpn != WeaponItem.NONE) currentScore -= melee.Value;
                };

                // Atribui preferencia a compra
                var wpnPerPreference = new Dictionary<Action, float>
                {
                    { buyRifle, enemyConfig.riflePreference},
                    { buyHandgun, enemyConfig.handgunPreference},
                    { buyMelee, enemyConfig.meleePreference}
                };

                // Ordenar as compras pela preferencia (da maior para a menor)
                var sortedBuy = wpnPerPreference
                    .OrderByDescending(par => par.Value)
                    .Select(par => par.Key)
                    .ToList();

                // Ativa as compras em ordem
                foreach (Action act in sortedBuy){
                    act();
                }

                if(enemyConfig.specialWpns.Keys.Count > 0) {
                    var special = enemyConfig.specialWpns.OrderBy(x => GetDifference(x.Value, (currentScore * riflePreference) + randomGap)).First();
                    weaponSet.specialWpn = special.Key;
                }
                

                return weaponSet;
            }
        }
    }
}

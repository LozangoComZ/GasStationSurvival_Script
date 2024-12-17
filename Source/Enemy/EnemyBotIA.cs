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
        // BOT IA
        public static BotBehaviorSet GenericIA(int score)
        {
            BotBehaviorSet bbs = new BotBehaviorSet();
            bbs.OffensiveClimbingLevel = Math.Min(score / 200, 1f);
            bbs.OffensiveEnrageLevel = Math.Min(score / 200, 1f);
            bbs.OffensiveSprintLevel = Math.Min(score / 200, 0.8f);
            bbs.DefensiveRollFireLevel = Math.Min(score / 200, 1f);
            bbs.SearchForItems = false;
            bbs.SearchItems = SearchItems.None;

            return bbs;
        }
        public static Func<int, BotBehaviorSet> RookieIA = (int score) =>
        {
            BotBehaviorSet bbs = GenericIA(score);
            bbs.RangedWeaponAccuracy = score / 400;
            bbs.RangedWeaponAimShootDelayMax = (300 / score) * 1500;
            bbs.RangedWeaponAimShootDelayMin = bbs.RangedWeaponAimShootDelayMax - 500;
            bbs.RangedWeaponBurstPauseMax = bbs.RangedWeaponAimShootDelayMax / 3;
            bbs.RangedWeaponBurstPauseMin = bbs.RangedWeaponAimShootDelayMin / 3;
            bbs.DefensiveAvoidProjectilesLevel = Math.Min(score / 200, 1f);
            bbs.OffensiveSprintLevel = Math.Min(score / 150, 0.8f);
            bbs.SetMeleeActionsToEasy();
            if (score > 60)
            {
                bbs.SetMeleeActionsToNormal();
            }
            if (score > 90)
            {
                bbs.SetMeleeActionsToHard();
            }
            if (score > 130)
            {
                bbs.SetMeleeActionsToExpert();
            }
            return bbs;
        };
        public static Func<int, BotBehaviorSet> PunkIA = (int score) =>
        {
            BotBehaviorSet bbs = GenericIA(score);
            bbs.RangedWeaponAccuracy = score / 150;
            bbs.RangedWeaponAimShootDelayMax = (200 / score) * 1500;
            bbs.RangedWeaponAimShootDelayMin = bbs.RangedWeaponAimShootDelayMax - 500;
            bbs.RangedWeaponBurstPauseMax = bbs.RangedWeaponAimShootDelayMax / 2;
            bbs.RangedWeaponBurstPauseMin = bbs.RangedWeaponAimShootDelayMin / 2;


            bbs.RangedWeaponPrecisionAccuracy = bbs.RangedWeaponAccuracy;
            bbs.RangedWeaponPrecisionAimShootDelayMax = bbs.RangedWeaponAimShootDelayMax;
            bbs.RangedWeaponPrecisionAimShootDelayMin = bbs.RangedWeaponAimShootDelayMin;
            bbs.RangedWeaponPrecisionBurstPauseMax = bbs.RangedWeaponBurstPauseMax;
            bbs.RangedWeaponPrecisionBurstPauseMin = bbs.RangedWeaponBurstPauseMin;


            bbs.SeekCoverWhileShooting = 1;

            bbs.OffensiveClimbingLevel = Math.Min(score / 200, 1f);
            bbs.OffensiveEnrageLevel = Math.Min(score / 200, 1f);
            bbs.OffensiveSprintLevel = Math.Min(score / 500, 0.8f);
            bbs.SetMeleeActionsToEasy();
            if (score > 80)
            {
                bbs.SetMeleeActionsToNormal();
            }
            if (score > 150)
            {
                bbs.SetMeleeActionsToHard();
            }
            if (score > 200)
            {
                bbs.SetMeleeActionsToExpert();
            }
            return bbs;
        };

        public static Func<int, BotBehaviorSet> MolotovIA = (int score) =>
        {
            BotBehaviorSet bbs = PunkIA(score);
            return bbs;
        };

        public static Func<int, BotBehaviorSet> BigIA = (int score) =>
        {
            BotBehaviorSet bbs = RookieIA(score);
            bbs.RangedWeaponUsage = false;
            bbs.OffensiveClimbingLevel = 1;
            bbs.OffensiveEnrageLevel = 1;
            bbs.OffensiveSprintLevel = 1;
            return bbs;
        };

        public static Func<int, BotBehaviorSet> SuicideIA = (int score) =>
        {
            BotBehaviorSet bbs = RookieIA(score/2);
            bbs.RangedWeaponAccuracy = 0.5f;
            bbs.DefensiveAvoidProjectilesLevel = 0;
            bbs.OffensiveSprintLevel = 0;
            bbs.OffensiveDiveLevel = 1;
            bbs.OffensiveClimbingLevel = 1;
            bbs.OffensiveEnrageLevel = 1;
            return bbs;
        };
    }
}

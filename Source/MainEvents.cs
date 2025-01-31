using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        public static Events.PlayerDeathCallback m_playerDeathEvent = null;
        public static Events.PlayerMeleeActionCallback m_playerMeleeActionEvent = null;
        public Events.UserMessageCallback m_userMessageCallback = null;
        public void StartEvents()
        {
            //Events
            m_userMessageCallback = Events.UserMessageCallback.Start(OnUserMessage);
            m_playerDeathEvent = Events.PlayerDeathCallback.Start(OnPlayerDeath);
            m_playerMeleeActionEvent = Events.PlayerMeleeActionCallback.Start(OnPlayerMeleeAction);
        }
        public void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (!args.Killed) return;
            //Check if is Enemy
            foreach (Enemy e in Wave.EnemiesList.Where(en => en.ply == player))
            {
                OnEnemyDeath(e, args);
                return;
            }
            //When it's 'hero'
            WaveManager.GameOverCheck();
        }

        public void OnPlayerMeleeAction(IPlayer player, PlayerMeleeHitArg[] args)
        {
            if (!EnemyByIPlayer.ContainsKey(player)) return;
            Enemy enemy = EnemyByIPlayer[player];
            int score = enemy.score;

            //Calculate delay interval
            int interval = (300 / score) * 200;

            var status = "COOLDOWN";
            foreach (PlayerMeleeHitArg arg in args)
            {
                if (arg.IsPlayer) if (((IPlayer)arg.HitObject).IsBlocking)
                    {
                        interval *= 3;
                        status = "STUNNED";
                        Game.PlayEffect("Smack", player.GetWorldPosition() + new Vector2(0, 10));
                    }

            }
            if (interval < 200) return;

            //Delay application
            var name = player.Name;
            player.SetInputMode(PlayerInputMode.Disabled);
            player.SetBotName(status);
            Events.UpdateCallback.Start((float e) => {
                if (!player.IsDead)
                {
                    player.SetInputMode(PlayerInputMode.Enabled);
                    player.SetBotName(name);
                }
            }, (uint)(interval), 1);
        }
    }
}

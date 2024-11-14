using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;

namespace GasStationSurvival_Script
{
    public partial class Main : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public Main() : base(null) { }


        public static Random rnd = new Random();
        public static Events.PlayerDeathCallback m_playerDeathEvent = null;
        public static Events.PlayerMeleeActionCallback m_playerMeleeActionEvent = null;

        public void OnStartup()
        {
            //Events
            m_playerDeathEvent = Events.PlayerDeathCallback.Start(OnPlayerDeath);
            m_playerMeleeActionEvent = Events.PlayerMeleeActionCallback.Start(OnPlayerMeleeAction);

            //Setup Wave
            WaveManager.ManualSetup(Game.SurvivalWave);
            EnemySpawn.RefreshSpawnPoints();
            WaveManager.TryNextSession();
        }

        public void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (!args.Killed) return;
            //Check if is Enemy
            foreach (Enemy e in Wave.EnemiesList.Where(en => en.ply == player))
            {
                OnEnemyDeath(e,args);
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
            int interval = (300 / score) * 400;

            if (interval < 200) return;

            var status = "COOLDOWN";
            foreach(PlayerMeleeHitArg arg in args)
            {
                if (arg.IsPlayer) if (((IPlayer)arg.HitObject).IsBlocking) {
                    interval *= 3;
                    status = "STUNNED";
                    Game.PlayEffect("Smack", player.GetWorldPosition() + new Vector2(0, 10));
                }

            }

            //Delay application
            var name = player.Name;
            player.SetInputMode(PlayerInputMode.Disabled);
            player.SetBotName(status);
            Events.UpdateCallback.Start((float e) => {
                if (!player.IsDead){
                    player.SetInputMode(PlayerInputMode.Enabled);
                    player.SetBotName(name);
                }
            }, (uint)(interval), 1);
        }
        //Others
        /*public static void MsgG(object s) { Game.WriteToConsole("DEBUG: " + s.ToString()); }
        public static void MsgG(object s, string origin) { Game.WriteToConsole(origin+": "+s.ToString()); }
        */
        public static void MsgG(object s) { Game.ShowChatMessage("DEBUG: " + s.ToString()); }
        public static void MsgG(object s, string origin) { Game.ShowChatMessage(origin + ": " + s.ToString()); }
    }
}
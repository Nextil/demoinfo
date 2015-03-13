using System;
using EHVAG.DemoInfo.DemoPackets.GameEvents;
using EHVAG.DemoInfo.DemoPackets.GameEvents.Events;

namespace EHVAG.DemoInfo.States
{
    public class EventState
    {
        readonly DemoParser Parser;

        /// <summary>
        /// Occurs when a phase ends. I think this occures when warmup ends. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<AnnouncePhaseEnd>> AnnouncePhaseEnd;

        /// <summary>
        /// Occurs when the bomb is dropped.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombDropped>> BombDropped;

        /// <summary>
        /// Occurs when a bot is taken over.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BotTakeover>> BotTakeover;

        /// <summary>
        /// Occurs when the game is over, and the endmatch-panel is shown. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<CSWinPanelMatch>> CSWinPanelMatch;

        /// <summary>
        /// Event to control the camera. Look at the HLTVChase class documentation for more info
        /// </summary>
        public event EventHandler<GameEventEventArgs<HLTVChase>> HLTVChase;

        /// <summary>
        /// Occurs when player connects.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerConnect>> PlayerConnect;

        /// <summary>
        /// Occurs when player jumps.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerJump>> PlayerJump;

        /// <summary>
        /// Announces the match-start-round, right at the end of the freezetime. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundAnnounceMatchStart>> RoundAnnounceMatchStart;

        /// <summary>
        /// Occurs when the round started.
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundPoststart>> RoundPoststart;

        /// <summary>
        /// Occurs when smokegrenade expired. This is not raised at the end of a round, 
        /// so if you keep track of active smokes make sure to remove the smokes at 
        /// the end of the round. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<SmokegrenadeExpired>> SmokegrenadeExpired;

        /// <summary>
        /// Annouces the beginning of a new match. This is raised at the end of the freezetime. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<BeginNewMatch>> BeginNewMatch;

        /// <summary>
        /// Occurs when bomb exploded.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombExploded>> BombExploded;

        /// <summary>
        /// Occurs when the Round-Win-Panel in CS:GO is shown. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<CSWinPanelRound>> CSWinPanelRound;

        /// <summary>
        /// Contains information about how many people view the game (and are eligible for drops)
        /// </summary>
        public event EventHandler<GameEventEventArgs<HLTVStatus>> HLTVStatus;

        /// <summary>
        /// Occurs when a player has fully connected
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerConnectFull>> PlayerConnectFull;

        /// <summary>
        /// Occurs when player spwans.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerSpwan>> PlayerSpwan;

        /// <summary>
        /// Occurs when a round ends. Contains information about round-winners. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundEnd>> RoundEnd;

        /// <summary>
        /// Occurs when round prestart.
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundPrestart>> RoundPrestart;

        /// <summary>
        /// Only relevant for majors. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<TournamentReward>> TournamentReward;

        /// <summary>
        /// Occurs when the bomb beeps.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombBeep>> BombBeep;

        /// <summary>
        /// Occurs when bomb pickup.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombPickup>> BombPickup;

        /// <summary>
        /// Occurs when decoy detonate. This is not raised at the end of a round, 
        /// so if you keep track of active decoys make sure to remove the smokes at 
        /// the end of the round. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<DecoyDetonate>> DecoyDetonate;

        /// <summary>
        /// Occurs when inferno expire. This is not raised at the end of a round, 
        /// so if you keep track of active molotovs make sure to remove the smokes at 
        /// the end of the round. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<InfernoExpire>> InfernoExpire;

        /// <summary>
        /// Occurs when a player dies. By the way: There is no player_hurt event, 
        /// (at least it isn't stored in demos)
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerDeath>> PlayerDeath;

        /// <summary>
        /// Occurs when a player joins a team.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerTeam>> PlayerTeam;

        /// <summary>
        /// Occurs when round final beep.
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundFinalBeep>> RoundFinalBeep;

        /// <summary>
        /// Occurs when round start beep.
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundStartBeep>> RoundStartBeep;

        /// <summary>
        /// Occurs when a weapon is fired.
        /// </summary>
        public event EventHandler<GameEventEventArgs<WeaponFire>> WeaponFire;

        /// <summary>
        /// Occurs when bomb begindefuse.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombBegindefuse>> BombBegindefuse;

        /// <summary>
        /// Occurs when bomb planted.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombPlanted>> BombPlanted;

        /// <summary>
        /// Occurs when decoy started.
        /// </summary>
        public event EventHandler<GameEventEventArgs<DecoyStarted>> DecoyStarted;

        /// <summary>
        /// Might occur when a inferno is extinguished. I've never seen this occur though, 
        /// so this might not be networked. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<InfernoExtinguish>> InfernoExtinguish;

        /// <summary>
        /// Occurs when a player disconnects.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerDisconnect>> PlayerDisconnect;

        /// <summary>
        /// Annouces the final round. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundAnnounceFinal>> RoundAnnounceFinal;

        /// <summary>
        /// Occurs when the freezetime ends. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundFreezeEnd>> RoundFreezeEnd;

        /// <summary>
        /// Occurs when round starts. This is before the freezetime. The positions, HP etc. propbably hasn't 
        /// chaned yet. So when this event occurs, don't be surprised when there are less than 10 people alive. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundStart>> RoundStart;

        /// <summary>
        /// Occurs when a weapon is being reloaded. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<WeaponReload>> WeaponReload;

        /// <summary>
        /// Occurs when the bomb is begun to planted. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombBeginplant>> BombBeginplant;

        /// <summary>
        /// Occurs when buytime ended.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BuytimeEnded>> BuytimeEnded;

        /// <summary>
        /// Occurs when flashbang detonate. It contains a list with blind players. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<FlashbangDetonate>> FlashbangDetonate;

        /// <summary>
        /// Occurs when inferno startburn.
        /// </summary>
        public event EventHandler<GameEventEventArgs<InfernoStartburn>> InfernoStartburn;

        /// <summary>
        /// Occurs when a player takes falldamage.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerFalldamage>> PlayerFalldamage;

        /// <summary>
        /// Announces the last round of the half
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundAnnounceLastRoundHalf>> RoundAnnounceLastRoundHalf;

        /// <summary>
        /// Announces the MVP of the round. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundMVP>> RoundMVP;

        /// <summary>
        /// Occurs when the remaining round time is little. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundTimeWarning>> RoundTimeWarning;

        /// <summary>
        /// Occurs when a weapon is zoomed. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<WeaponZoom>> WeaponZoom;

        /// <summary>
        /// Occurs when the bomb is defused.
        /// </summary>
        public event EventHandler<GameEventEventArgs<BombDefused>> BombDefused;

        /// <summary>
        /// Occurs right before a restart. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<CSPreRestart>> CSPreRestart;

        /// <summary>
        /// Occurs when HE grenade detonates.
        /// </summary>
        public event EventHandler<GameEventEventArgs<HEGrenadeDetonate>> HEGrenadeDetonate;

        /// <summary>
        /// Occurs when player is blind. The flashbang-detonate right after contains this player. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerBlind>> PlayerBlind;

        /// <summary>
        /// Occurs when player steps.
        /// </summary>
        public event EventHandler<GameEventEventArgs<PlayerFootstep>> PlayerFootstep;

        /// <summary>
        /// Announces the match point. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundAnnounceMatchPoint>> RoundAnnounceMatchPoint;

        /// <summary>
        /// Occurs when the round officially ended.This is right before the players respwan. 
        /// </summary>
        public event EventHandler<GameEventEventArgs<RoundOfficiallyEnded>> RoundOfficiallyEnded;

        /// <summary>
        /// Occurs when a smokegrenade detonates.
        /// </summary>
        public event EventHandler<GameEventEventArgs<SmokegrenadeDetonate>> SmokegrenadeDetonate;

        public EventState(DemoParser parser)
        {
            this.Parser = parser;
        }

        internal void RaiseAnnouncePhaseEnd(AnnouncePhaseEnd evnt)
        {
            if(AnnouncePhaseEnd != null)
                AnnouncePhaseEnd(Parser, new GameEventEventArgs<AnnouncePhaseEnd>(evnt));
        }

        internal void RaiseBombDropped(BombDropped evnt)
        {
            if(BombDropped != null)
                BombDropped(Parser, new GameEventEventArgs<BombDropped>(evnt));
        }

        internal void RaiseBotTakeover(BotTakeover evnt)
        {
            if(BotTakeover != null)
                BotTakeover(Parser, new GameEventEventArgs<BotTakeover>(evnt));
        }

        internal void RaiseCSWinPanelMatch(CSWinPanelMatch evnt)
        {
            if(CSWinPanelMatch != null)
                CSWinPanelMatch(Parser, new GameEventEventArgs<CSWinPanelMatch>(evnt));
        }

        internal void RaiseHLTVChase(HLTVChase evnt)
        {
            if(HLTVChase != null)
                HLTVChase(Parser, new GameEventEventArgs<HLTVChase>(evnt));
        }

        internal void RaisePlayerConnect(PlayerConnect evnt)
        {
            if(PlayerConnect != null)
                PlayerConnect(Parser, new GameEventEventArgs<PlayerConnect>(evnt));
        }

        internal void RaisePlayerJump(PlayerJump evnt)
        {
            if(PlayerJump != null)
                PlayerJump(Parser, new GameEventEventArgs<PlayerJump>(evnt));
        }

        internal void RaiseRoundAnnounceMatchStart(RoundAnnounceMatchStart evnt)
        {
            if(RoundAnnounceMatchStart != null)
                RoundAnnounceMatchStart(Parser, new GameEventEventArgs<RoundAnnounceMatchStart>(evnt));
        }

        internal void RaiseRoundPoststart(RoundPoststart evnt)
        {
            if(RoundPoststart != null)
                RoundPoststart(Parser, new GameEventEventArgs<RoundPoststart>(evnt));
        }

        internal void RaiseSmokegrenadeExpired(SmokegrenadeExpired evnt)
        {
            if(SmokegrenadeExpired != null)
                SmokegrenadeExpired(Parser, new GameEventEventArgs<SmokegrenadeExpired>(evnt));
        }

        internal void RaiseBeginNewMatch(BeginNewMatch evnt)
        {
            if(BeginNewMatch != null)
                BeginNewMatch(Parser, new GameEventEventArgs<BeginNewMatch>(evnt));
        }

        internal void RaiseBombExploded(BombExploded evnt)
        {
            if(BombExploded != null)
                BombExploded(Parser, new GameEventEventArgs<BombExploded>(evnt));
        }

        internal void RaiseCSWinPanelRound(CSWinPanelRound evnt)
        {
            if(CSWinPanelRound != null)
                CSWinPanelRound(Parser, new GameEventEventArgs<CSWinPanelRound>(evnt));
        }

        internal void RaiseHLTVStatus(HLTVStatus evnt)
        {
            if(HLTVStatus != null)
                HLTVStatus(Parser, new GameEventEventArgs<HLTVStatus>(evnt));
        }

        internal void RaisePlayerConnectFull(PlayerConnectFull evnt)
        {
            if(PlayerConnectFull != null)
                PlayerConnectFull(Parser, new GameEventEventArgs<PlayerConnectFull>(evnt));
        }

        internal void RaisePlayerSpwan(PlayerSpwan evnt)
        {
            if(PlayerSpwan != null)
                PlayerSpwan(Parser, new GameEventEventArgs<PlayerSpwan>(evnt));
        }

        internal void RaiseRoundEnd(RoundEnd evnt)
        {
            if(RoundEnd != null)
                RoundEnd(Parser, new GameEventEventArgs<RoundEnd>(evnt));
        }

        internal void RaiseRoundPrestart(RoundPrestart evnt)
        {
            if(RoundPrestart != null)
                RoundPrestart(Parser, new GameEventEventArgs<RoundPrestart>(evnt));
        }

        internal void RaiseTournamentReward(TournamentReward evnt)
        {
            if(TournamentReward != null)
                TournamentReward(Parser, new GameEventEventArgs<TournamentReward>(evnt));
        }

        internal void RaiseBombBeep(BombBeep evnt)
        {
            if(BombBeep != null)
                BombBeep(Parser, new GameEventEventArgs<BombBeep>(evnt));
        }

        internal void RaiseBombPickup(BombPickup evnt)
        {
            if(BombPickup != null)
                BombPickup(Parser, new GameEventEventArgs<BombPickup>(evnt));
        }

        internal void RaiseDecoyDetonate(DecoyDetonate evnt)
        {
            if(DecoyDetonate != null)
                DecoyDetonate(Parser, new GameEventEventArgs<DecoyDetonate>(evnt));
        }

        internal void RaiseInfernoExpire(InfernoExpire evnt)
        {
            if(InfernoExpire != null)
                InfernoExpire(Parser, new GameEventEventArgs<InfernoExpire>(evnt));
        }

        internal void RaisePlayerDeath(PlayerDeath evnt)
        {
            if(PlayerDeath != null)
                PlayerDeath(Parser, new GameEventEventArgs<PlayerDeath>(evnt));
        }

        internal void RaisePlayerTeam(PlayerTeam evnt)
        {
            if(PlayerTeam != null)
                PlayerTeam(Parser, new GameEventEventArgs<PlayerTeam>(evnt));
        }

        internal void RaiseRoundFinalBeep(RoundFinalBeep evnt)
        {
            if(RoundFinalBeep != null)
                RoundFinalBeep(Parser, new GameEventEventArgs<RoundFinalBeep>(evnt));
        }

        internal void RaiseRoundStartBeep(RoundStartBeep evnt)
        {
            if(RoundStartBeep != null)
                RoundStartBeep(Parser, new GameEventEventArgs<RoundStartBeep>(evnt));
        }

        internal void RaiseWeaponFire(WeaponFire evnt)
        {
            if(WeaponFire != null)
                WeaponFire(Parser, new GameEventEventArgs<WeaponFire>(evnt));
        }

        internal void RaiseBombBegindefuse(BombBegindefuse evnt)
        {
            if(BombBegindefuse != null)
                BombBegindefuse(Parser, new GameEventEventArgs<BombBegindefuse>(evnt));
        }

        internal void RaiseBombPlanted(BombPlanted evnt)
        {
            if(BombPlanted != null)
                BombPlanted(Parser, new GameEventEventArgs<BombPlanted>(evnt));
        }

        internal void RaiseDecoyStarted(DecoyStarted evnt)
        {
            if(DecoyStarted != null)
                DecoyStarted(Parser, new GameEventEventArgs<DecoyStarted>(evnt));
        }

        internal void RaiseInfernoExtinguish(InfernoExtinguish evnt)
        {
            if(InfernoExtinguish != null)
                InfernoExtinguish(Parser, new GameEventEventArgs<InfernoExtinguish>(evnt));
        }

        internal void RaisePlayerDisconnect(PlayerDisconnect evnt)
        {
            if(PlayerDisconnect != null)
                PlayerDisconnect(Parser, new GameEventEventArgs<PlayerDisconnect>(evnt));
        }

        internal void RaiseRoundAnnounceFinal(RoundAnnounceFinal evnt)
        {
            if(RoundAnnounceFinal != null)
                RoundAnnounceFinal(Parser, new GameEventEventArgs<RoundAnnounceFinal>(evnt));
        }

        internal void RaiseRoundFreezeEnd(RoundFreezeEnd evnt)
        {
            if(RoundFreezeEnd != null)
                RoundFreezeEnd(Parser, new GameEventEventArgs<RoundFreezeEnd>(evnt));
        }

        internal void RaiseRoundStart(RoundStart evnt)
        {
            if(RoundStart != null)
                RoundStart(Parser, new GameEventEventArgs<RoundStart>(evnt));
        }

        internal void RaiseWeaponReload(WeaponReload evnt)
        {
            if(WeaponReload != null)
                WeaponReload(Parser, new GameEventEventArgs<WeaponReload>(evnt));
        }

        internal void RaiseBombBeginplant(BombBeginplant evnt)
        {
            if(BombBeginplant != null)
                BombBeginplant(Parser, new GameEventEventArgs<BombBeginplant>(evnt));
        }

        internal void RaiseBuytimeEnded(BuytimeEnded evnt)
        {
            if(BuytimeEnded != null)
                BuytimeEnded(Parser, new GameEventEventArgs<BuytimeEnded>(evnt));
        }

        internal void RaiseFlashbangDetonate(FlashbangDetonate evnt)
        {
            if(FlashbangDetonate != null)
                FlashbangDetonate(Parser, new GameEventEventArgs<FlashbangDetonate>(evnt));
        }

        internal void RaiseInfernoStartburn(InfernoStartburn evnt)
        {
            if(InfernoStartburn != null)
                InfernoStartburn(Parser, new GameEventEventArgs<InfernoStartburn>(evnt));
        }

        internal void RaisePlayerFalldamage(PlayerFalldamage evnt)
        {
            if(PlayerFalldamage != null)
                PlayerFalldamage(Parser, new GameEventEventArgs<PlayerFalldamage>(evnt));
        }

        internal void RaiseRoundAnnounceLastRoundHalf(RoundAnnounceLastRoundHalf evnt)
        {
            if(RoundAnnounceLastRoundHalf != null)
                RoundAnnounceLastRoundHalf(Parser, new GameEventEventArgs<RoundAnnounceLastRoundHalf>(evnt));
        }

        internal void RaiseRoundMVP(RoundMVP evnt)
        {
            if(RoundMVP != null)
                RoundMVP(Parser, new GameEventEventArgs<RoundMVP>(evnt));
        }

        internal void RaiseRoundTimeWarning(RoundTimeWarning evnt)
        {
            if(RoundTimeWarning != null)
                RoundTimeWarning(Parser, new GameEventEventArgs<RoundTimeWarning>(evnt));
        }

        internal void RaiseWeaponZoom(WeaponZoom evnt)
        {
            if(WeaponZoom != null)
                WeaponZoom(Parser, new GameEventEventArgs<WeaponZoom>(evnt));
        }

        internal void RaiseBombDefused(BombDefused evnt)
        {
            if(BombDefused != null)
                BombDefused(Parser, new GameEventEventArgs<BombDefused>(evnt));
        }

        internal void RaiseCSPreRestart(CSPreRestart evnt)
        {
            if(CSPreRestart != null)
                CSPreRestart(Parser, new GameEventEventArgs<CSPreRestart>(evnt));
        }

        internal void RaiseHEGrenadeDetonate(HEGrenadeDetonate evnt)
        {
            if(HEGrenadeDetonate != null)
                HEGrenadeDetonate(Parser, new GameEventEventArgs<HEGrenadeDetonate>(evnt));
        }

        internal void RaisePlayerBlind(PlayerBlind evnt)
        {
            if(PlayerBlind != null)
                PlayerBlind(Parser, new GameEventEventArgs<PlayerBlind>(evnt));
        }

        internal void RaisePlayerFootstep(PlayerFootstep evnt)
        {
            if(PlayerFootstep != null)
                PlayerFootstep(Parser, new GameEventEventArgs<PlayerFootstep>(evnt));
        }

        internal void RaiseRoundAnnounceMatchPoint(RoundAnnounceMatchPoint evnt)
        {
            if(RoundAnnounceMatchPoint != null)
                RoundAnnounceMatchPoint(Parser, new GameEventEventArgs<RoundAnnounceMatchPoint>(evnt));
        }

        internal void RaiseRoundOfficiallyEnded(RoundOfficiallyEnded evnt)
        {
            if(RoundOfficiallyEnded != null)
                RoundOfficiallyEnded(Parser, new GameEventEventArgs<RoundOfficiallyEnded>(evnt));
        }

        internal void RaiseSmokegrenadeDetonate(SmokegrenadeDetonate evnt)
        {
            if(SmokegrenadeDetonate != null)
                SmokegrenadeDetonate(Parser, new GameEventEventArgs<SmokegrenadeDetonate>(evnt));
        }

    }
}

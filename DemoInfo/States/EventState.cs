using System;
using EHVAG.DemoInfo.DemoPackets.GameEvents;
using EHVAG.DemoInfo.DemoPackets.GameEvents.Events;

namespace EHVAG.DemoInfo.States
{
    public class EventState
    {
        readonly DemoParser Parser;


        public event EventHandler<GameEventEventArgs<AnnouncePhaseEnd>> AnnouncePhaseEnd;
        public event EventHandler<GameEventEventArgs<BombDropped>> BombDropped;
        public event EventHandler<GameEventEventArgs<CSWinPanelMatch>> CSWinPanelMatch;
        public event EventHandler<GameEventEventArgs<HLTVChase>> HLTVChase;
        public event EventHandler<GameEventEventArgs<PlayerConnect>> PlayerConnect;
        public event EventHandler<GameEventEventArgs<PlayerJump>> PlayerJump;
        public event EventHandler<GameEventEventArgs<RoundAnnounceMatchStart>> RoundAnnounceMatchStart;
        public event EventHandler<GameEventEventArgs<RoundPoststart>> RoundPoststart;
        public event EventHandler<GameEventEventArgs<SmokegrenadeExpired>> SmokegrenadeExpired;
        public event EventHandler<GameEventEventArgs<BeginNewMatch>> BeginNewMatch;
        public event EventHandler<GameEventEventArgs<BombExploded>> BombExploded;
        public event EventHandler<GameEventEventArgs<CSWinPanelRound>> CSWinPanelRound;
        public event EventHandler<GameEventEventArgs<HLTVStatus>> HLTVStatus;
        public event EventHandler<GameEventEventArgs<PlayerConnectFull>> PlayerConnectFull;
        public event EventHandler<GameEventEventArgs<PlayerSpwan>> PlayerSpwan;
        public event EventHandler<GameEventEventArgs<RoundEnd>> RoundEnd;
        public event EventHandler<GameEventEventArgs<RoundPrestart>> RoundPrestart;
        public event EventHandler<GameEventEventArgs<TournamentReward>> TournamentReward;
        public event EventHandler<GameEventEventArgs<BombBeep>> BombBeep;
        public event EventHandler<GameEventEventArgs<BombPickup>> BombPickup;
        public event EventHandler<GameEventEventArgs<DecoyDetonate>> DecoyDetonate;
        public event EventHandler<GameEventEventArgs<InfernoExpire>> InfernoExpire;
        public event EventHandler<GameEventEventArgs<PlayerDeath>> PlayerDeath;
        public event EventHandler<GameEventEventArgs<PlayerTeam>> PlayerTeam;
        public event EventHandler<GameEventEventArgs<RoundFinalBeep>> RoundFinalBeep;
        public event EventHandler<GameEventEventArgs<RoundStartBeep>> RoundStartBeep;
        public event EventHandler<GameEventEventArgs<WeaponFire>> WeaponFire;
        public event EventHandler<GameEventEventArgs<BombBegindefuse>> BombBegindefuse;
        public event EventHandler<GameEventEventArgs<BombPlanted>> BombPlanted;
        public event EventHandler<GameEventEventArgs<DecoyStarted>> DecoyStarted;
        public event EventHandler<GameEventEventArgs<InfernoExtinguish>> InfernoExtinguish;
        public event EventHandler<GameEventEventArgs<PlayerDisconnect>> PlayerDisconnect;
        public event EventHandler<GameEventEventArgs<RoundAnnounceFinal>> RoundAnnounceFinal;
        public event EventHandler<GameEventEventArgs<RoundFreezeEnd>> RoundFreezeEnd;
        public event EventHandler<GameEventEventArgs<RoundStart>> RoundStart;
        public event EventHandler<GameEventEventArgs<WeaponReload>> WeaponReload;
        public event EventHandler<GameEventEventArgs<BombBeginplant>> BombBeginplant;
        public event EventHandler<GameEventEventArgs<BuytimeEnded>> BuytimeEnded;
        public event EventHandler<GameEventEventArgs<FlashbangDetonate>> FlashbangDetonate;
        public event EventHandler<GameEventEventArgs<InfernoStartburn>> InfernoStartburn;
        public event EventHandler<GameEventEventArgs<PlayerFalldamage>> PlayerFalldamage;
        public event EventHandler<GameEventEventArgs<RoundAnnounceLastRoundHalf>> RoundAnnounceLastRoundHalf;
        public event EventHandler<GameEventEventArgs<RoundMVP>> RoundMVP;
        public event EventHandler<GameEventEventArgs<RoundTimeWarning>> RoundTimeWarning;
        public event EventHandler<GameEventEventArgs<WeaponZoom>> WeaponZoom;
        public event EventHandler<GameEventEventArgs<BombDefused>> BombDefused;
        public event EventHandler<GameEventEventArgs<CSPreRestart>> CSPreRestart;
        public event EventHandler<GameEventEventArgs<HEGrenadeDetonate>> HEGrenadeDetonate;
        public event EventHandler<GameEventEventArgs<PlayerBlind>> PlayerBlind;
        public event EventHandler<GameEventEventArgs<PlayerFootstep>> PlayerFootstep;
        public event EventHandler<GameEventEventArgs<RoundAnnounceMatchPoint>> RoundAnnounceMatchPoint;
        public event EventHandler<GameEventEventArgs<RoundOfficiallyEnded>> RoundOfficiallyEnded;
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

using System;
using EHVAG.DemoInfo.DemoPackets.GameEvents;
using EHVAG.DemoInfo.DemoPackets.GameEvents.Events;

namespace EHVAG.DemoInfo.States
{
    public class EventState
    {
        readonly DemoParser Parser;

        public event EventHandler<GameEventEventArgs<PlayerFootstep>> PlayerFootstep;
        public event EventHandler<GameEventEventArgs<WeaponFire>> WeaponFire;
        public event EventHandler<GameEventEventArgs<PlayerBlind>> PlayerBlind;
        public event EventHandler<GameEventEventArgs<PlayerJump>> PlayerJump;
        public event EventHandler<GameEventEventArgs<WeaponReload>> WeaponReload;
        public event EventHandler<GameEventEventArgs<TournamentReward>> TournamentReward;
        public event EventHandler<GameEventEventArgs<BombBeep>> BombBeep;
        public event EventHandler<GameEventEventArgs<WeaponZoom>> WeaponZoom;
        public event EventHandler<GameEventEventArgs<PlayerSpwan>> PlayerSpwan;
        public event EventHandler<GameEventEventArgs<HLTVChase>> HLTVChase;
        public event EventHandler<GameEventEventArgs<HLTVStatus>> HLTVStatus;
        public event EventHandler<GameEventEventArgs<BombPickup>> BombPickup;
        public event EventHandler<GameEventEventArgs<BombDropped>> BombDropped;

        public EventState(DemoParser parser)
        {
            this.Parser = parser;
        }

        internal void RaisePlayerFootstep(PlayerFootstep evnt) 
        {
            if(PlayerFootstep != null)
                PlayerFootstep(Parser, new GameEventEventArgs<PlayerFootstep>(evnt));
        }

        internal void RaiseWeaponFire(WeaponFire evnt) 
        {
            if(WeaponFire != null)
                WeaponFire(Parser, new GameEventEventArgs<WeaponFire>(evnt));
        }

        internal void RaisePlayerBlind(PlayerBlind evnt) 
        {
            if(PlayerBlind != null)
                PlayerBlind(Parser, new GameEventEventArgs<PlayerBlind>(evnt));
        }

        internal void RaisePlayerJump(PlayerJump evnt) 
        {
            if(PlayerJump != null)
                PlayerJump(Parser, new GameEventEventArgs<PlayerJump>(evnt));
        }

        internal void RaiseWeaponReload(WeaponReload evnt) 
        {
            if(WeaponReload != null)
                WeaponReload(Parser, new GameEventEventArgs<WeaponReload>(evnt));
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

        internal void RaiseWeaponZoom(WeaponZoom evnt) 
        {
            if(WeaponZoom != null)
                WeaponZoom(Parser, new GameEventEventArgs<WeaponZoom>(evnt));
        }

        internal void RaisePlayerSpwan(PlayerSpwan evnt) 
        {
            if(PlayerSpwan != null)
                PlayerSpwan(Parser, new GameEventEventArgs<PlayerSpwan>(evnt));
        }

        internal void RaiseHLTVChase(HLTVChase evnt) 
        {
            if(HLTVChase != null)
                HLTVChase(Parser, new GameEventEventArgs<HLTVChase>(evnt));
        }

        internal void RaiseHLTVStatus(HLTVStatus evnt) 
        {
            if(HLTVStatus != null)
                HLTVStatus(Parser, new GameEventEventArgs<HLTVStatus>(evnt));
        }



        internal void RaiseBombPickup(BombPickup evnt) 
        {
            if(BombPickup != null)
                BombPickup(Parser, new GameEventEventArgs<BombPickup>(evnt));
        }

        internal void RaiseBombDropped(BombDropped evnt) 
        {
            if(BombDropped != null)
                BombDropped(Parser, new GameEventEventArgs<BombDropped>(evnt));
        }

    }
}


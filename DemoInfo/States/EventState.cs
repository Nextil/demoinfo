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
    }
}


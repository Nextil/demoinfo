using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_blind")]
    public class PlayerBlind : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        /// <summary>
        /// The Player who got blinded. 
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player 
        { 
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            } 
        }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaisePlayerBlind(this);
        }
    }
}


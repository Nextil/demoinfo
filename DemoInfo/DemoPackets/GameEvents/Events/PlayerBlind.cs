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
        public CSPlayer Player { get; private set; }

        internal override void HandleYourself()
        {
            Player = EventInfo.Parser.GameState.GetPlayerByUserID(UserID);

            EventInfo.Parser.Events.RaisePlayerBlind(this);
        }
    }
}


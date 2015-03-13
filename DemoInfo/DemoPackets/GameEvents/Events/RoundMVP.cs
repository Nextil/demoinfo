using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_mvp")]
    public class RoundMVP : BaseEvent
    {
        // GameEvent smokegrenade_detonate is missing variables for the properties entityid, x, y, z
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        /// <summary>
        /// The Player who is MVP
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player 
        { 
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            } 
        }

        /// <summary>
        /// Gets the falldamage inflicted to the player. 
        /// </summary>
        /// <value>The falldamage.</value>
        [NetworkedProperty("reason")]
        NetworkedVar<int> ReasonID { get; set; }

        public RoundMVPReason Reason
        {
            get
            {
                return (RoundMVPReason)ReasonID.Value;
            }
        }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseRoundMVP(this);
        }

    }
}


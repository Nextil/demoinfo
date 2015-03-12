using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_end")]
    public class RoundEnd : BaseEvent
    {
        [NetworkedProperty("winner")]
        NetworkedVar<int> WinnerID { get; set; }

        [NetworkedProperty("reason")]
        NetworkedVar<int> ReasonID { get; set; }

        /// <summary>
        /// Gets the round-end message.
        /// </summary>
        /// <value>The message.</value>
        [NetworkedProperty("message")]
        public NetworkedVar<string> Message { get; private set; }

        public CSTeam Winner { get { return EventInfo.Parser.GameState.Teams[WinnerID]; } }
       
        public RoundEndReason Reason { get { return (RoundEndReason)ReasonID.Value; } }

        public RoundEnd()
        {
        }

        internal override void HandleYourself()
        {

        }
    }
}


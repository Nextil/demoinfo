using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_jump")]
    public class PlayerJump : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        /// <summary>
        /// The Player who jumped. 
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player { get; private set; }

        internal override void HandleYourself()
        {
            Player = EventInfo.Parser.GameState.GetPlayerByUserID(UserID);

            EventInfo.Parser.Events.RaisePlayerJump(this);
        }
    }
}


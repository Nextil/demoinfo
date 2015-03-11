using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_spawn")]
    public class PlayerSpwan : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }


        [NetworkedProperty("teamnum")]
        NetworkedVar<int> TeamNum { get; set; }

        /// <summary>
        /// The Player who spwaned. 
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player { get; private set; }

        /// <summary>
        /// The team of the spwaning player
        /// </summary>
        /// <value>The team.</value>
        public CSTeam Team { get; private set; }

        internal override void HandleYourself()
        {
            Player = EventInfo.Parser.GameState.GetPlayerByUserID(UserID);

            EventInfo.Parser.Events.RaisePlayerSpwan(this);
        }
    }
}


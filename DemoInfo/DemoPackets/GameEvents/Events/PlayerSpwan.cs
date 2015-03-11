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
        public CSPlayer Player 
        { 
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            } 
        }

        /// <summary>
        /// The team of the spwaning player
        /// </summary>
        /// <value>The team.</value>
        public CSTeam Team 
        { 
            get 
            {
                return EventInfo.Parser.GameState.Teams[TeamNum];
            } 
        }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaisePlayerSpwan(this);
        }
    }
}


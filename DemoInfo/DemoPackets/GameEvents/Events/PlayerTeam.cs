using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_team")]
    public class PlayerTeam : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }



        [NetworkedProperty("team")]
        NetworkedVar<int> TeamID { get; set; }

        [NetworkedProperty("oldteam")]
        NetworkedVar<int> OldTeamID { get; set; }


        /// <summary>
        /// The Player who threw the smoke. 
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
        /// Gets the new team of the player
        /// </summary>
        /// <value>The team.</value>
        public CSTeam Team
        {
            get 
            {
                return EventInfo.Parser.GameState.Teams[TeamID];
            } 
        }

        /// <summary>
        /// Gets the old team of the player
        /// </summary>
        /// <value>The old team.</value>
        public CSTeam OldTeam
        {
            get 
            {
                return EventInfo.Parser.GameState.Teams[OldTeamID];
            } 
        }
            
        /// <summary>
        /// Team changes because the player disconnected
        /// </summary>
        /// <value>The disconnect.</value>
        [NetworkedProperty("disconnect")]
        public NetworkedVar<bool> Disconnect { get; private set; }

        /// <summary>
        /// true if the player was auto assigned to the team
        /// </summary>
        /// <value>The disconnect.</value>
        [NetworkedProperty("autoteam")]
        public NetworkedVar<bool> AutoTeam { get; private set; }


        /// <summary>
        /// if true wont print the team join messages
        /// </summary>
        /// <value>The disconnect.</value>
        [NetworkedProperty("silent")]
        public NetworkedVar<bool> Silent { get; private set; }

        /// <summary>
        /// True if the player is a bot. 
        /// </summary>
        /// <value>The disconnect.</value>
        [NetworkedProperty("isbot")]
        public NetworkedVar<bool> IsBot { get; private set; }

        internal override void HandleYourself()
        {

        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("tournament_reward")]
    public class TournamentReward : BaseEvent
    {
        /// <summary>
        /// The item-definition-index of the rewarded item
        /// </summary>
        /// <value>The user I.</value>
        [NetworkedProperty("defindex")]
        public NetworkedVar<int> UserID { get; private set; }

        /// <summary>
        /// The rewarded account
        /// </summary>
        /// <value>The user I.</value>
        [NetworkedProperty("accountid")]
        public NetworkedVar<int> AccountID { get; private set; }


        /// <summary>
        /// The total number of rewards given so far. 
        /// </summary>
        /// <value>The user I.</value>
        [NetworkedProperty("totalrewards")]
        public NetworkedVar<int> TotalRewards { get; private set; }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseTournamentReward(this);
        }
    }
}


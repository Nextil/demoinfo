using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("bot_takeover")]
    public class BotTakeover : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        [NetworkedProperty("botid")]
        NetworkedVar<int> BotID { get; set; }



        [NetworkedProperty("index")]
        NetworkedVar<int> Index { get; set; }

        /// <summary>
        /// The Player 
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
        /// The Bot 
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Bot 
        { 
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(BotID);
            } 
        }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseBotTakeover(this);
        }
    }
}


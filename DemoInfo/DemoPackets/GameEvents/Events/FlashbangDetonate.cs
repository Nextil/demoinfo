using System;
using System.Collections.Generic;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("flashbang_detonate")]
    public class FlashbangDetonate : BaseEvent
    {
        // GameEvent smokegrenade_detonate is missing variables for the properties entityid, x, y, z
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        /// <summary>
        /// The Player who jumped. 
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player 
        { 
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            } 
        }

        [NetworkedProperty("entityid")]
        NetworkedVar<int> EntityID { get; set; }

        [NetworkedProperty("x")]
        NetworkedVar<float> X { get; set; }

        [NetworkedProperty("y")]
        NetworkedVar<float> Y { get; set; }

        [NetworkedProperty("z")]
        NetworkedVar<float> Z { get; set; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector Position { get { return new Vector(X, Y, Z); }}

        /// <summary>
        /// Gets the players who where blinded by this flashbang. 
        /// </summary>
        /// <value>The blind players.</value>
        public List<CSPlayer> BlindPlayers { get; private set; } 

        public FlashbangDetonate()
        {
        }

        internal override void HandleYourself()
        {
            // It's better we create a new list for every player, since it's not that expensive, 
            // and if the user wants to keep the list he can do that easily without worrying that
            // his reference changes. 
            BlindPlayers = new List<CSPlayer>(EventInfo.Parser.RawData.BlindPlayersFromLastFlashbang);
            EventInfo.Parser.RawData.BlindPlayersFromLastFlashbang.Clear();

            EventInfo.Parser.Events.RaiseFlashbangDetonate(this);
        }

        internal override void InitializeCopy(BaseEvent oldEvent)
        {
            BlindPlayers = new List<CSPlayer>(((FlashbangDetonate)oldEvent).BlindPlayers);
        }
    }
}


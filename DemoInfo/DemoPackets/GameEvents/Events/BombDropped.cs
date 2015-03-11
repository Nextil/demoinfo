using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("bomb_dropped")]
    public class BombDropped : BaseEvent
    {

        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }


        [NetworkedProperty("entindex")]
        NetworkedVar<int> BombEntityIndex { get; set; }


        /// <summary>
        /// The Player who dropped the bomb
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
        /// The entity of a Bomb that hasn't been planted yet. 
        /// PlantedBomb is null in this case. 
        /// </summary>
        /// <value>The bomb.</value>
        public CC4 Bomb 
        { 
            get 
            {
                return EventInfo.Parser.RawData.Entities[BombEntityIndex].Instance as CC4;
            }
        }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseBombDropped(this);
        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_falldamage")]
    public class PlayerFalldamage : BaseEvent
    {
        // GameEvent smokegrenade_detonate is missing variables for the properties entityid, x, y, z
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        /// <summary>
        /// The Player who took the damage
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
        /// As far as I know the applied damage is Math.floor(Falldamage). 
        /// So if you have 5 hp, and this is 4.99, you'll survive with 1hp.
        /// </summary>
        /// <value>The falldamage.</value>
        [NetworkedProperty("damage")]
        public NetworkedVar<float> Falldamage { get; private set; }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaisePlayerFalldamage(this);
        }

    }
}


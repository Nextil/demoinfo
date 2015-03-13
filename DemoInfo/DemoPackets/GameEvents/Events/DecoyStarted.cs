using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("decoy_started")]
    public class DecoyStarted : BaseEvent
    {
        // GameEvent smokegrenade_detonate is missing variables for the properties entityid, x, y, z
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

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

        public DecoyStarted()
        {
        }

        internal override void HandleYourself()
        {

        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("weapon_fire")]
    public class WeaponFire : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        [NetworkedProperty("weapon")]
        public NetworkedVar<string> Weapon { get; private set; }

        [NetworkedProperty("silenced")]
        public NetworkedVar<bool> Silenced { get; private set; }

        /// <summary>
        /// The Player who stepped. 
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player { get; private set; }

        internal override void HandleYourself()
        {
            Player = EventInfo.Parser.GameState.GetPlayerByUserID(UserID);

            //TODO: Weapon logic! CZ will be P250, etc. 


        }
    }
}


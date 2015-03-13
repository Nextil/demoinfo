using System;
using System.Linq;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_connect_full")]
    public class PlayerConnectFull : BaseEvent
    {

        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get;  set; }

        /// <summary>
        /// Gets the connected player.
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player {
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            }
        }

        [NetworkedProperty("index")]
        NetworkedVar<int> Index { get;  set; }

        /// <summary>
        /// Gets the playerinfo referenced by the index-property. 
        /// </summary>
        /// <value>The player info.</value>
        public PlayerInfo PlayerInfo
        {
            get
            {
                return EventInfo.Parser.RawData.PlayerInfos[Index];
            }
        }

        internal override void HandleYourself()
        {

        }


    }
}


using System;
using System.Linq;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_disconnect")]
    public class PlayerDisconnect : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get;  set; }

        /// <summary>
        /// The disconnected player. Be careful, this can be null!
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player {
            get 
            {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            }
        }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        /// <value>The reason.</value>
        [NetworkedProperty("reason")]
        public NetworkedVar<string> Reason { get; private set; }

        /// <summary>
        /// Gets the 32-bit steamid (STEAM_...)
        /// </summary>
        /// <value>The reason.</value>
        [NetworkedProperty("networkid")]
        public NetworkedVar<string> SteamID32 { get; private set; }

        /// <summary>
        /// Gets the name of the disconnected player. 
        /// </summary>
        /// <value>The reason.</value>
        [NetworkedProperty("name")]
        public NetworkedVar<string> Name { get; private set; }

        internal override void HandleYourself()
        {
            foreach(var playerinfo in EventInfo.Parser.RawData.PlayerInfos.Where(a => a != null && a.UserID == UserID))
                playerinfo.Name = string.Format("unconnected (formerly {0})", Name);

            EventInfo.Parser.Events.RaisePlayerDisconnect(this);
        }


    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.Utils;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_connect")]
    public class PlayerConnect : BaseEvent
    {
        /// <summary>
        /// Gets the userid of the connected player. 
        /// Not really useful for noraml users. 
        /// </summary>
        /// <value>The user I.</value>
        [NetworkedProperty("userid")]
        public NetworkedVar<int> UserID { get;  private set; }

        /// <summary>
        /// Gets the name of the connected player. 
        /// </summary>
        /// <value>The name.</value>
        [NetworkedProperty("name")]
        public NetworkedVar<string> Name { get;  private set; }

        /// <summary>
        /// Gets the index of the userinfo stringtable entry of the new player
        /// Don't use this, except if you know 100% exactly what you're doing. 
        /// Even if you think you know what you're doing, better ask me. 
        /// (flai @quakenet, I'm usually idling in #demoinfogo). 
        /// </summary>
        /// <value>The name.</value>
        [NetworkedProperty("index")]
        public NetworkedVar<int> Index { get;  private set; }

        /// <summary>
        /// Gets the 32-Bit-SteamID (STEAM_...) of the connected player. 
        /// </summary>
        /// <value>The network I.</value>
        [NetworkedProperty("networkid")]
        public NetworkedVar<string> NetworkID { get;  private set; }

        /// <summary>
        /// Gets the IP-Address of the connected player. Probably empty
        /// since Volvo patched this to avoid ddosing of players. 
        /// </summary>
        /// <value>The network I.</value>
        [NetworkedProperty("address")]
        public NetworkedVar<string> Address { get;  private set; }

        internal override void HandleYourself()
        {
            PlayerInfo playerInfo = null;

            if (EventInfo.Parser.RawData.PlayerInfos[Index] != null)
                playerInfo = EventInfo.Parser.RawData.PlayerInfos[Index];
            else
                playerInfo = EventInfo.Parser.RawData.PlayerInfos[Index] = new PlayerInfo();

            playerInfo.Name = Name;
            playerInfo.GUID = NetworkID;
            playerInfo.UserID = UserID;
            playerInfo.XUID = playerInfo.GUID == "BOT" ? 0 : Util.GetCommunityID(playerInfo.GUID);

            EventInfo.Parser.Events.RaisePlayerConnect(this);
        }


    }
}


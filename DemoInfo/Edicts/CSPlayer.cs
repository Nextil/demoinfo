using System;
using EHVAG.DemoInfo.Edicts.Reflection;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.Edicts
{
    [ServerClass("CCSPlayer")]
    public class CSPlayer : BaseEntity
    {
        /// <summary>
        /// Gets the player info associated with this player
        /// Note: This is raw data, and can therefore behave in weird
        /// ways. Use with care. 
        /// </summary>
        /// <value>The player info.</value>
        public PlayerInfo PlayerInfo
        {
            get
            {
                return EntityInfo.Parser.RawData.PlayerInfos[this.EntityID - 1];
            }
        }

        /// <summary>
        /// Gets the name of the player. 
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return PlayerInfo.Name;
            }
        }

        /// <summary>
        /// Gets the 64 Bit steamid of the player (also referred to as Cummunity-ID)
        /// It's the one starting with 78...
        /// </summary>
        /// <value>The steam I.</value>
        public long SteamID
        {
            get
            {
                return PlayerInfo.XUID;
            }
        }

        /// <summary>
        /// Gets the 32-bit steam ID. It's the one starting with STEAM_1:1:...
        /// </summary>
        /// <value>The steam I d32.</value>
        public string SteamID32
        {
            get
            {
                return PlayerInfo.GUID;
            }
        }

        /// <summary>
        /// Gets the user ID of the player. This is an internal ID that can change anytime
        /// (for example at user disconnect / reconnect), so don't use this to identify players
        /// over time. The only valid use of this property is to  map gameevents to users. 
        /// If you're using it for anything else, you're doing it wrong. Use the SteamID instead. 
        /// </summary>
        /// <value>The user I.</value>
        public int UserID
        {
            get
            {
                return PlayerInfo.UserID;
            }
        }

        [NetworkedProperty("m_iTeamNum")]
        NetworkedVar<int> TeamNum { get; set; }

        [NetworkedProperty("cslocaldata.m_vecOrigin")]
        [NetworkedProperty("csnonlocaldata.m_vecOrigin")]
        NetworkedVar<Vector> PositionXY { get; set; }

        [NetworkedProperty("cslocaldata.m_vecOrigin[2]")]
        [NetworkedProperty("csnonlocaldata.m_vecOrigin[2]")]
        NetworkedVar<float> PositionZ { get; set; }

        /// <summary>
        /// Gets the health of the player.
        /// </summary>
        /// <value>The health.</value>
        [NetworkedProperty("m_iHealth")]
        public NetworkedVar<int> Health { get; private set; }

        /// <summary>
        /// Gets the armor of the player. 
        /// </summary>
        /// <value>The armor.</value>
        [NetworkedProperty("m_ArmorValue")]
        public NetworkedVar<int> Armor { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has a defuser.
        /// </summary>
        /// <value><c>true</c> if this instance has defuser; otherwise, <c>false</c>.</value>
        [NetworkedProperty("m_bHasDefuser")]
        public NetworkedVar<bool> HasDefuser { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has a helmet.
        /// </summary>
        /// <value><c>true</c> if this instance has helmet; otherwise, <c>false</c>.</value>
        [NetworkedProperty("m_bHasHelmet")]
        public NetworkedVar<bool> HasHelmet { get; private set; }

        /// <summary>
        /// Gets a bool indicating whether this player is defusing
        /// </summary>
        [NetworkedProperty("m_bIsDefusing")]
        public NetworkedVar<bool> IsDefusing { get; private set; }

        /// <summary>
        /// Gets a bool indicating whether this player is scoped in.
        /// </summary>
        [NetworkedProperty("m_bIsScoped")]
        public NetworkedVar<bool> IsScoped { get; private set; }

        /// <summary>
        /// Gets a bool indicating whether this player in a buyzone
        /// </summary>
        [NetworkedProperty("m_bInBuyZone")]
        public NetworkedVar<bool> InBuyZone { get; private set; }

        /// <summary>
        /// Gets a bool indicating whether this player is in a bombsite
        /// </summary>
        [NetworkedProperty("m_bInBombZone")]
        public NetworkedVar<bool> InBombZone { get; private set; }

        /// <summary>
        /// Gets a bool indicating whether this player is in a "no-defuse-area"
        /// </summary>
        [NetworkedProperty("m_bInNoDefuseArea")]
        public NetworkedVar<bool> InNoDefuseArea { get; private set; }


        /// <summary>
        /// Gets a bool indicating whether this player is walking
        /// </summary>
        [NetworkedProperty("m_bIsWalking")]
        public NetworkedVar<bool> IsWalking { get; private set; }

        /// <summary>
        /// Gets the money.
        /// </summary>
        /// <value>The money.</value>
        [NetworkedProperty("m_iAccount")]
        public NetworkedVar<int> Money { get; private set; }

        /// <summary>
        /// Gets the current equipment value.
        /// </summary>
        /// <value>The current equipment value.</value>
        [NetworkedProperty("m_unCurrentEquipmentValue")]
        public NetworkedVar<int> CurrentEquipmentValue { get; private set; }

        /// <summary>
        /// Gets the round start equipment value.
        /// </summary>
        /// <value>The round start equipment value.</value>
        [NetworkedProperty("m_unRoundStartEquipmentValue")]
        public NetworkedVar<int> RoundStartEquipmentValue { get; private set; }

        /// <summary>
        /// Gets the freezetime end equipment value.
        /// </summary>
        /// <value>The freezetime end equipment value.</value>
        [NetworkedProperty("m_unFreezetimeEndEquipmentValue")]
        public NetworkedVar<int> FreezetimeEndEquipmentValue { get; private set; }

        /// <summary>
        /// Gets the viewangles of the user. This is in the left/right direction. 
        /// </summary>
        /// <value>The eye angles1.</value>
        [NetworkedProperty("m_angEyeAngles[1]")]
        public NetworkedVar<float> EyeAngles1 { get; private set; }

        /// <summary>
        /// Gets the viewangles of the user. This is in the up/down direction. 
        /// </summary>
        /// <value>The eye angles0.</value>
        [NetworkedProperty("m_angEyeAngles[0]")]
        public NetworkedVar<float> EyeAngles0 { get; private set; }

        /// <summary>
        /// The flash-duration of the player 
        /// </summary>
        [NetworkedProperty("m_flFlashDuration")]
        public NetworkedVar<float> FlashDuration { get; private set; }

        /// <summary>
        /// The maximum alpha of the last  flash
        /// </summary>
        [NetworkedProperty("m_flFlashMaxAlpha")]
        public NetworkedVar<float> FlashMaxAlpha { get; private set; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector Position
        {
            get
            {
                return new Vector(PositionXY.Value.X, PositionXY.Value.Y, PositionZ.Value);
            }
        }

        public CSTeam Team 
        {
            get
            {
                return EntityInfo.Parser.GameState.Teams[TeamNum];
            }
        }

        public CSPlayer()
        {
        }
    }
}


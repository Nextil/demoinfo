using System;
using EHVAG.DemoInfo.Edicts.Reflection;

namespace EHVAG.DemoInfo.Edicts
{
    [ServerClass("CCSTeam")]
    public class CSTeam : BaseEntity
    {
        /// <summary>
        /// Gets the team ID.
        /// </summary>
        /// <value>The team I.</value>
        [NetworkedProperty("m_iTeamNum")]
        public NetworkedVar<int> TeamID { get; private set; }


        [NetworkedProperty("m_bSurrendered")]
        NetworkedVar<int> _hasSurrendered { get; set; }

        /// <summary>
        /// Gets a value indicating whether the team has surrendered.
        /// </summary>
        /// <value><c>true</c> if this instance has surrendered; otherwise, <c>false</c>.</value>
        public bool HasSurrendered { get { return _hasSurrendered != 0; } }

        /// <summary>
        /// Gets the score of the team
        /// </summary>
        /// <value>The score total.</value>
        [NetworkedProperty("m_scoreTotal")]
        public NetworkedVar<int> ScoreTotal { get; private set; }


        /// <summary>
        /// Gets the first half score.
        /// </summary>
        /// <value>The score first half.</value>
        [NetworkedProperty("m_scoreFirstHalf")]
        public NetworkedVar<int> ScoreFirstHalf { get; private set; }

        /// <summary>
        /// Gets the second half score.
        /// </summary>
        /// <value>The score second half.</value>
        [NetworkedProperty("m_scoreSecondHalf")]
        public NetworkedVar<int> ScoreSecondHalf { get; private set; }

        /// <summary>
        /// Gets the overtime score.
        /// </summary>
        /// <value>The score overtime.</value>
        [NetworkedProperty("m_scoreOvertime")]
        public NetworkedVar<int> ScoreOvertime { get; private set; }

        /// <summary>
        /// Gets the clan ID. 
        /// (If you know what exactly this is, please describe it here and create a pull request)
        /// </summary>
        /// <value>The clan I.</value>
        [NetworkedProperty("m_iClanID")]
        public NetworkedVar<int> ClanID { get; private set; }

        /// <summary>
        /// Gets the name of the team set by the server-admin
        /// </summary>
        /// <value>The name of the team.</value>
        [NetworkedProperty("m_szTeamname")]
        public NetworkedVar<string> TeamName { get; private set; }

        /// <summary>
        /// Gets the name of the team, that will display if all members have the same clan tag
        /// </summary>
        /// <value>The name of the clan team.</value>
        [NetworkedProperty("m_szClanTeamname")]
        public NetworkedVar<string> ClanTeamName { get; private set; }

        /// <summary>
        /// Gets the team flag image.
        /// </summary>
        /// <value>The team flag image.</value>
        [NetworkedProperty("m_szTeamFlagImage")]
        public NetworkedVar<string> TeamFlagImage { get; private set; }

        /// <summary>
        /// Gets the team match stat. I have *no idea* what this is. If you know what this is
        /// please describe it here and create a pull-request. 
        /// It's empty for every demo I've checked it. 
        /// </summary>
        /// <value>The team match stat.</value>
        [NetworkedProperty("m_szTeamMatchStat")]
        public NetworkedVar<string> TeamMatchStat { get; private set; }

        /// <summary>
        /// Gets the GunGame-Leader entity-index of the CTs
        /// This is only set on the team "Unassigned" when I checked it. 
        /// So you probably need to look there if you *ever* want to parse
        /// GunGame-Replays. If you know more about this, please add it here and
        /// create a pull-request.
        /// </summary>
        /// <value>The GG leader ent index C.</value>
        [NetworkedProperty("m_nGGLeaderEntIndex_CT")]
        public NetworkedVar<int> GGLeaderEntIndex_CT { get; private set; }


        /// <summary>
        /// Gets the GunGame-Leader entity-index of the Ts
        /// This is only set on the team "Unassigned" when I checked it. 
        /// So you probably need to look there if you *ever* want to parse
        /// GunGame-Replays. If you know more about this, please add it here and
        /// create a pull-request.
        /// </summary>
        /// <value>The GG leader ent index C.</value>
        [NetworkedProperty("m_nGGLeaderEntIndex_T")]
        public NetworkedVar<int> GGLeaderEntIndex_T { get; private set; }


        public CSTeam()
        {
        }

        internal override void FullyInitialized()
        {
            EntityInfo.Parser.GameState.Teams[TeamID] = this;

            if (this.TeamName == "TERRORIST")
                EntityInfo.Parser.GameState.Terrorists = this;

            if (this.TeamName == "CT")
                EntityInfo.Parser.GameState.Terrorists = this;
        }

        internal override void PropertiesUpdated()
        {
            
        }
    }
}


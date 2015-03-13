using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    /// <summary>
    /// This event is mostly used internally for GOTV-Camera-Control.
    /// The only "documentation" I found is here
    /// http://hlssmod.net/he_code/game/client/hltvcamera.cpp
    /// and here:
    /// http://hpmod.googlecode.com/svn/trunk/cl_dll/hltvcamera.cpp
    /// 
    /// (grep for hltv_chase)
    /// </summary>
    [ValveEvent("hltv_status")]
    public class HLTVStatus : BaseEvent
    {
        /// <summary>
        /// Number of clients connected to the GOTV-Network of this game
        /// </summary>
        /// <value>The clients.</value>
        [NetworkedProperty("clients")]
        public NetworkedVar<int> Clients { get; private set; }

        /// <summary>
        /// Number of slots avaiable to the GOTV-Network of this game
        /// </summary>
        /// <value>The slots.</value>
        [NetworkedProperty("slots")]
        public NetworkedVar<int> Slots { get; private set; }

        /// <summary>
        /// Number of proxies part of the GOTV-Network of this game
        /// </summary>
        /// <value>The proxies.</value>
        [NetworkedProperty("proxies")]
        public NetworkedVar<int> Proxies { get; private set; }

        /// <summary>
        /// IP of the GOTV-Master-Server. Can be empty though. (Probably is).
        /// </summary>
        /// <value>The master.</value>
        [NetworkedProperty("master")]
        public NetworkedVar<string> Master { get; private set; }

        /// <summary>
        /// Gets the total amount of external viewers (e.g. twitch.tv viewers)
        /// </summary>
        /// <value>The external total.</value>
        [NetworkedProperty("externaltotal")]
        public NetworkedVar<int> ExternalTotal { get; private set; }

        /// <summary>
        /// Gets the amount of external viewers (e.g. twitch.tv viewers) that have linked their accounts
        /// (and are thus eligible for drops)
        /// </summary>
        /// <value>The external linked.</value>
        [NetworkedProperty("externallinked")]
        public NetworkedVar<int> ExternalLinked { get; private set; }


        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseHLTVStatus(this);
        }
    }
}


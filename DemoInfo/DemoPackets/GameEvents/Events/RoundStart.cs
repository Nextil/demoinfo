using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_start")]
    public class RoundStart : BaseEvent
    {
        /// <summary>
        /// The timelimit for this round
        /// </summary>
        /// <value>The time limit.</value>
        [NetworkedProperty("timelimit")]
        public NetworkedVar<int> TimeLimit { get; private set; }

        /// <summary>
        /// The fraglimit for this round
        /// </summary>
        /// <value>The time limit.</value>
        [NetworkedProperty("fraglimit")]
        public NetworkedVar<int> FragLimit { get; private set; }

        /// <summary>
        /// The objective for this round
        /// </summary>
        /// <value>The time limit.</value>
        [NetworkedProperty("objective")]
        public NetworkedVar<string> Objective { get; private set; }

        internal override void HandleYourself()
        {

        }
    }
}


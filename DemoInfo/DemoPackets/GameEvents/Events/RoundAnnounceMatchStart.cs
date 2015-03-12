using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_announce_match_start")]
    public class RoundAnnounceMatchStart : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


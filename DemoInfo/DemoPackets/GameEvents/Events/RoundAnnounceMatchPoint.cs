using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_announce_match_point")]
    public class RoundAnnounceMatchPoint : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


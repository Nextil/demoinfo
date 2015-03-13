using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_announce_last_round_half")]
    public class RoundAnnounceLastRoundHalf : BaseEvent
    {
        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseRoundAnnounceLastRoundHalf(this);
        }
    }
}


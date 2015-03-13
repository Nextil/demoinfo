using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_announce_final")]
    public class RoundAnnounceFinal : BaseEvent
    {
        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseRoundAnnounceFinal(this);
        }
    }
}


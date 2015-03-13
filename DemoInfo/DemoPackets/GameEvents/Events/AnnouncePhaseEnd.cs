using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("announce_phase_end")]
    public class AnnouncePhaseEnd : BaseEvent
    {
        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseAnnouncePhaseEnd(this);
        }
    }
}


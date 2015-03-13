using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_prestart")]
    public class RoundPrestart : BaseEvent
    {
        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseRoundPrestart(this);
        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("cs_round_start_beep")]
    public class RoundStartBeep : BaseEvent
    {
        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseRoundStartBeep(this);
        }
    }
}


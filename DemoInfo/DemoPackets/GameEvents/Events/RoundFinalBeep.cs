using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("cs_round_final_beep")]
    public class RoundFinalBeep : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


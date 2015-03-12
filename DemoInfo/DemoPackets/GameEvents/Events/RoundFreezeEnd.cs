using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_freeze_end")]
    public class RoundFreezeEnd : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


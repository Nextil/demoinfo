using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_time_warning")]
    public class RoundTimeWarning : BaseEvent
    {

        internal override void HandleYourself()
        {

        }
    }
}


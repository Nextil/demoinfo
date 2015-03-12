using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("begin_new_match")]
    public class BeginNewMatch : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


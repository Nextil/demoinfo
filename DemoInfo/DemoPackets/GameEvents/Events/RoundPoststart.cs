using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_poststart")]
    public class RoundPoststart : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


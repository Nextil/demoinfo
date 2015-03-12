using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("round_officially_ended")]
    public class RoundOfficiallyEnded : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("cs_pre_restart")]
    public class CSPreRestart : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("buytime_ended")]
    public class BuytimeEnded : BaseEvent
    {
        internal override void HandleYourself()
        {

        }
    }
}


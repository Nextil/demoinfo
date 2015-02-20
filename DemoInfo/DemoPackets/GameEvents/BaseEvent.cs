using System;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents
{
    public abstract class BaseEvent
    {
        /// <summary>
        /// Tells and GameEvent to Handle itself. Rough times. 
        /// </summary>
        public abstract void HandleYourself();

        public EventInformation EventInfo { get; internal set; }
    }
}


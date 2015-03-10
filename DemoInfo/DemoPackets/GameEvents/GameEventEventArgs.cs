using System;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents
{
    public class GameEventEventArgs<T> : EventArgs
        where T : BaseEvent
    {
        public T GameEvent { get; private set; }

        public GameEventEventArgs(T baseEvent)
        {
            this.GameEvent = baseEvent;
        }
    }
}


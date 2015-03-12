using System;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents
{
    public abstract class BaseEvent
    {
        /// <summary>
        /// Tells and GameEvent to Handle itself. Rough times. 
        /// </summary>
        internal abstract void HandleYourself();

        internal virtual void InitializeCopy(BaseEvent oldEvent)
        {

        }

        public EventInformation EventInfo { get; internal set; }

        /// <summary>
        /// Copies the Event to a new Instance. The new Instance will have the same
        /// type. NetworkedVar's will be copied. The references to other things will
        /// be copied, but not the other thing itself. If PlayerFootstep refers to a 
        /// Player, the copied event will always point to this player - but the player
        /// will still change position for example. If you want to store information 
        /// for good, do this in your own data structure. 
        /// </summary>
        /// <returns>The copy.</returns>
        public BaseEvent FlatCopy()
        {
            return EventInfo.Copy().Instance;
        }
    }
}


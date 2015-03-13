using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("inferno_extinguish")]
    public class InfernoExtinguish : BaseEvent
    {
        [NetworkedProperty("entityid")]
        NetworkedVar<int> EntityID { get; set; }

        [NetworkedProperty("x")]
        NetworkedVar<float> X { get; set; }

        [NetworkedProperty("y")]
        NetworkedVar<float> Y { get; set; }

        [NetworkedProperty("z")]
        NetworkedVar<float> Z { get; set; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector Position { get { return new Vector(X, Y, Z); }}


        internal override void HandleYourself()
        {

        }
    }
}


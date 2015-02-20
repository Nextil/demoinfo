using System;

namespace EHVAG.DemoInfo.Utils.Reflection
{
    public class ValveEventAttribute : Attribute
    {
        public string ValveEvent { get; private set; }

        public ValveEventAttribute(string valveEvent)
        {
            ValveEvent = valveEvent;
        }
    }
}


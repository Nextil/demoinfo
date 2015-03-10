using System;

namespace EHVAG.DemoInfo.Utils.Reflection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValveEventAttribute : Attribute
    {
        public string ValveEvent { get; private set; }

        public ValveEventAttribute(string valveEvent)
        {
            ValveEvent = valveEvent;
        }
    }
}


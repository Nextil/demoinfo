using System;

namespace EHVAG.DemoInfo.Edicts.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NetworkedPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public bool Optional { get; private set; }

        public NetworkedPropertyAttribute(string propertyName) : this(propertyName, false)
        {
        }

        public NetworkedPropertyAttribute(string propertyName, bool optional)
        {
            PropertyName = propertyName;
            Optional = optional;
        }
    }
}


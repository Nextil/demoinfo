using System;

namespace EHVAG.DemoInfo.Edicts.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NetworkedPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }

        public NetworkedPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}


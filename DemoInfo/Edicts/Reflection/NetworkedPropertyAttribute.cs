using System;

namespace EHVAG.DemoInfo.Edicts.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NetworkedPropertyAttribute : Attribute
    {
        public NetworkedPropertyAttribute(string propertyName)
        {
        }
    }
}


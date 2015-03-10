using System;

namespace EHVAG.DemoInfo.Utils.Reflection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServerClassAttribute : Attribute
    {
        public string ServerClass { get; private set; }

        public ServerClassAttribute(string serverClass)
        {
            ServerClass = serverClass;
        }
    }
}


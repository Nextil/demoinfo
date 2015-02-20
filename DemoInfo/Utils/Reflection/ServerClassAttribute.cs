using System;

namespace EHVAG.DemoInfo.Edicts.Reflection
{
    public class ServerClassAttribute : Attribute
    {
        public string ServerClass { get; private set; }

        public ServerClassAttribute(string serverClass)
        {
            ServerClass = serverClass;
        }
    }
}


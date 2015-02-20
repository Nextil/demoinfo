using System;

namespace EHVAG.DemoInfo.Utils.Reflection
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


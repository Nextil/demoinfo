using System;
using System.Diagnostics;

namespace EHVAG.DemoInfo.Edicts.Reflection
{
    [DebuggerDisplay("{Value}")]
    public class NetworkedVar<T>
    {
        public EntityInformation entity;
        public T Value { get; internal set; }


        public static implicit operator T(NetworkedVar<T> d)
        {
            return d.Value;
        }
    }
}


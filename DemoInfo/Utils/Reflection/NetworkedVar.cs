using System;
using System.Diagnostics;

namespace EHVAG.DemoInfo.Utils.Reflection
{
    [DebuggerDisplay("{Value}")]
    public class NetworkedVar<T>
    {
        public T Value { get; internal set; }


        public static implicit operator T(NetworkedVar<T> d)
        {
            return d.Value;
        }
    }
}


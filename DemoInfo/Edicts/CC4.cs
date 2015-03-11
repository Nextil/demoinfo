using System;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.Edicts
{
    [ServerClass("CC4")]
    public class CC4 : BaseEntity
    {
        [NetworkedProperty("m_vecOrigin")]
        public NetworkedVar<Vector> Position { get; private set; } 

        public CC4()
        {
        }
    }
}


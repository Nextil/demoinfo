using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.Edicts
{
    [ServerClass("CPlantedC4")]
    public class CPlantedC4 : BaseEntity
    {
        [NetworkedProperty("m_vecOrigin")]
        public NetworkedVar<Vector> Position { get; private set; } 

    }
}


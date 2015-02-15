using System;

namespace EHVAG.DemoInfo.Edicts
{
    public class BaseEntity
    {
        public int EntityID { get; internal set;  }

        public EntityInformation EntityInfo { get; internal set; }

        public BaseEntity()
        {
        }
    }
}


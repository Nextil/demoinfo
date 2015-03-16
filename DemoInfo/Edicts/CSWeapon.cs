using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.Edicts
{
    [ServerClass("CWeaponCSBaseGun")]
    public class CSWeapon : BaseEntity
    {
        [NetworkedProperty("m_hOwner")]
        NetworkedVar<int> ownerRef { get; set; }

        public EntityInformation Owner
        {
            get {
                return EntityInfo.Parser.RawData.Entities[ownerRef & (2048 - 1)];
            }
        }

        internal override void FullyInitialized()
        {
            base.FullyInitialized();
        }
    }
}


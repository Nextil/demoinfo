using System;
using EHVAG.DemoInfo.Edicts.Reflection;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.Edicts
{
    [ServerClass("CCSPlayer")]
    public class CSPlayer : BaseEntity
    {


        [NetworkedProperty("m_iTeamNum")]
        public NetworkedVar<int> TeamNum { get; private set; }

        [NetworkedProperty("cslocaldata.m_vecOrigin")]
        NetworkedVar<Vector> PositionXY { get; set; }

        [NetworkedProperty("cslocaldata.m_vecOrigin[2]")]
        NetworkedVar<float> PositionZ { get; set; }

        [NetworkedProperty("m_iHealth")]
        public NetworkedVar<int> Health { get; private set; }

        [NetworkedProperty("m_ArmorValue")]
        public NetworkedVar<int> Armor { get; private set; }

        [NetworkedProperty("m_bHasDefuser")]
        NetworkedVar<int> _defuser { get; set; }

        [NetworkedProperty("m_bHasHelmet")]
        NetworkedVar<int> _helmet { get; set; }

        [NetworkedProperty("m_iAccount")]
        public NetworkedVar<int> Money { get; private set; }

        [NetworkedProperty("m_unCurrentEquipmentValue")]
        public NetworkedVar<int> CurrentEquipmentValue { get; private set; }

        [NetworkedProperty("m_unRoundStartEquipmentValue")]
        public NetworkedVar<int> RoundStartEquipmentValue { get; private set; }

        [NetworkedProperty("m_unFreezetimeEndEquipmentValue")]
        public NetworkedVar<int> FreezetimeEndEquipmentValue { get; private set; }

        [NetworkedProperty("m_angEyeAngles[1]")]
        public NetworkedVar<float> EyeAngles1 { get; private set; }

        [NetworkedProperty("m_angEyeAngles[0]")]
        public NetworkedVar<float> EyeAngles0 { get; private set; }

        public string Name { get; set; }

        public bool HasDefuser
        {
            get {
                return _defuser != 0;
            }
        }


        public bool HasHelmet
        {
            get {
                return _helmet != 0;
            }
        }

        public Vector Position {
            get {
                return new Vector(PositionXY.Value.X, PositionXY.Value.Y, PositionZ.Value);
            }
        }

        public CSPlayer()
        {
        }
    }
}


using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    /// <summary>
    /// This event is mostly used internally for GOTV-Camera-Control.
    /// The only "documentation" I found is here
    /// http://hlssmod.net/he_code/game/client/hltvcamera.cpp
    /// and here:
    /// http://hpmod.googlecode.com/svn/trunk/cl_dll/hltvcamera.cpp
    /// 
    /// (grep for hltv_chase)
    /// </summary>
    [ValveEvent("hltv_chase")]
    public class HLTVChase : BaseEvent
    {
        [NetworkedProperty("target1")]
        public NetworkedVar<int> Target1 { get; private set; }

        [NetworkedProperty("target2")]
        public NetworkedVar<int> Target2 { get; private set; }

        [NetworkedProperty("distance")]
        public NetworkedVar<int> Distance { get; private set; }

        [NetworkedProperty("theta")]
        public NetworkedVar<int> Theta { get; private set; }

        [NetworkedProperty("phi")]
        public NetworkedVar<int> Phi { get; private set; }

        [NetworkedProperty("inertia")]
        public NetworkedVar<int> Inertia { get; private set; }

        [NetworkedProperty("ineye")]
        public NetworkedVar<int> InEye { get; private set; }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseHLTVChase(this);
        }
    }
}


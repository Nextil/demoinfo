using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("bomb_beep")]
    public class BombBeep : BaseEvent
    {
        [NetworkedProperty("entindex")]
        NetworkedVar<int> BombEntityIndex { get; set; }

        /// <summary>
        /// The entity of a Bomb that hasn't been planted yet. 
        /// PlantedBomb is null in this case. 
        /// </summary>
        /// <value>The bomb.</value>
        public CC4 Bomb 
        { 
            get 
            {
                return EventInfo.Parser.RawData.Entities[BombEntityIndex].Instance as CC4;
            }
        }

        /// <summary>
        /// The entity of an already planted bomb. 
        /// Bomb is null in this case. 
        /// </summary>
        /// <value>The planted bomb.</value>
        public CPlantedC4 PlantedBomb 
        { 
            get 
            {
                return EventInfo.Parser.RawData.Entities[BombEntityIndex].Instance as CPlantedC4;
            }
        }

        internal override void HandleYourself()
        {
            EventInfo.Parser.Events.RaiseBombBeep(this);
        }
    }
}


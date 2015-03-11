using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("bomb_beep")]
    public class BombBeep : BaseEvent
    {
        /// <summary>
        /// Gets the index of the bomb-entity. 
        /// </summary>
        /// <value>The index of the bomb entity.</value>
        [NetworkedProperty("entindex")]
        NetworkedVar<int> BombEntityIndex { get; set; }

        /// <summary>
        /// The entity of a Bomb that hasn't been planted yet. 
        /// PlantedBomb is null in this case. 
        /// </summary>
        /// <value>The bomb.</value>
        public CC4 Bomb { get; private set; }

        /// <summary>
        /// The entity of an already planted bomb. 
        /// Bomb is null in this case. 
        /// </summary>
        /// <value>The planted bomb.</value>
        public CPlantedC4 PlantedBomb { get; private set; }

        internal override void HandleYourself()
        {
            if(EventInfo.Parser.RawData.Entities[BombEntityIndex].Class.Name == "CC4")
                Bomb = (CC4)EventInfo.Parser.RawData.Entities[BombEntityIndex].Instance;
            else if(EventInfo.Parser.RawData.Entities[BombEntityIndex].Class.Name == "CPlantedC4")
                PlantedBomb = (CPlantedC4)EventInfo.Parser.RawData.Entities[BombEntityIndex].Instance;

            EventInfo.Parser.Events.RaiseBombBeep(this);
        }
    }
}


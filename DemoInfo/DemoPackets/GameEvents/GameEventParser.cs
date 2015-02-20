using System;
using System.Linq;
using EHVAG.DemoInfo.ProtobufMessages;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents
{
    public class GameEventParser
    {
        readonly DemoParser Parser;

        EventInformation[] Events;

        bool Initialized = false;

        public GameEventParser(DemoParser parser)
        {
            this.Parser = parser;
        }

        internal void Initialize()
        {
            Events = new EventInformation[Parser.RawData.GameEventDescriptors.Values.Max(a => a.EventId) + 1];

            foreach (var gameEvent in Parser.RawData.GameEventDescriptors.Values)
            {
                Events[gameEvent.EventId] = new EventInformation(gameEvent, Parser);
            }

            Initialized = true;
        }

        public void Parse(GameEvent gameEvent)
        {
            if (!Initialized)
                return;

            var info = Events[gameEvent.EventId];
            if (info.Instance == null)
            {
                #if DEBUG_SLOW_MONO
                //Console.WriteLine("Warning: Event " + Parser.RawData.GameEventDescriptors[gameEvent.EventId].Name + " is unused!");
                #endif
                return;
            }

            info.Fill(gameEvent);

            info.Instance.HandleYourself();
        }
    }
}


using System;
using EHVAG.DemoInfo.ProtobufMessages;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents
{
    public class EventInformation
    {
        /// <summary>
        /// The instance of the GameEvent.
        /// </summary>
        public BaseEvent Instance;

        /// <summary>
        /// Gets the current DemoParser
        /// </summary>
        /// <value>The instance.</value>
        public DemoParser Parser { get; private set; }

        /// <summary>
        /// The ID of this event
        /// </summary>
        /// <value>The I.</value>
        public int ID { get; private set; }

        /// <summary>
        /// Gets or sets the integers.
        /// </summary>
        /// <value>The integers.</value>
        internal NetworkedVar<int>[] Integers { get; private set; }

        /// <summary>
        /// Gets or sets the longs.
        /// </summary>
        /// <value>The longs.</value>
        internal NetworkedVar<long>[] Longs { get; private set; }

        /// <summary>
        /// Gets or sets the floats.
        /// </summary>
        /// <value>The floats.</value>
        internal NetworkedVar<float>[] Floats { get; private set; }

        /// <summary>
        /// Gets or sets the strings.
        /// </summary>
        /// <value>The strings.</value>
        internal NetworkedVar<string>[] Strings { get; private set; }


        /// <summary>
        /// Gets or sets the bools.
        /// </summary>
        /// <value>The vectors.</value>
        internal NetworkedVar<bool>[] Bools { get; private set; }

        private readonly GameEventList.Descriptor EventDescriptor;

        private GameEvent LastEvent;

        public EventInformation(GameEventList.Descriptor eventDescriptor, DemoParser parser)
        {
            Parser = parser;
            ID = eventDescriptor.EventId;
            EventDescriptor = eventDescriptor;
            Integers = new NetworkedVar<int>[eventDescriptor.Keys.Length];
            Longs = new NetworkedVar<long>[eventDescriptor.Keys.Length];
            Floats = new NetworkedVar<float>[eventDescriptor.Keys.Length];
            Strings = new NetworkedVar<string>[eventDescriptor.Keys.Length];
            Bools = new NetworkedVar<bool>[eventDescriptor.Keys.Length];

            CreateInstance();
        }

        private void CreateInstance()
        {
            if (EventDescriptor.EventType == null)
                return;

            Instance = (BaseEvent)Activator.CreateInstance(EventDescriptor.EventType);
            Instance.EventInfo = this;

            int i = 0;
            foreach (var key in EventDescriptor.Keys)
            {
                switch (key.Type)
                {
                    case 1: // string
                        {
                            NetworkedVar<string> valref = new NetworkedVar<string>();
                            Strings[i] = valref;
                            if(key.Setter != null)
                                key.Setter.Invoke(Instance, new object[] { valref });
                            else
                                Console.WriteLine("Unhandled field: " + key.Name);
                        }
                        break;
                    case 2: // float
                        {
                            NetworkedVar<float> valref = new NetworkedVar<float>();
                            Floats[i] = valref;
                            if(key.Setter != null)
                                key.Setter.Invoke(Instance, new object[] { valref });
                            else
                                Console.WriteLine("Unhandled field: " + key.Name);
                        }
                        break;
                    case 3: // long
                    case 4: // short
                    case 5: // byte
                        {
                            NetworkedVar<int> valref = new NetworkedVar<int>();
                            Integers[i] = valref;
                            if(key.Setter != null)
                                key.Setter.Invoke(Instance, new object[] { valref });
                            else
                                Console.WriteLine("Unhandled field: " + key.Name);
                        }
                        break;
                    case 6: // bool
                        {
                            NetworkedVar<bool> valref = new NetworkedVar<bool>();
                            Bools[i] = valref;
                            if(key.Setter != null)
                                key.Setter.Invoke(Instance, new object[] { valref });
                            else
                                Console.WriteLine("Unhandled field: " + key.Name);
                        }
                        break;
                }

                i++;
            }
        }

        /// <summary>
        /// Fill the instance with the gameEvent.
        /// </summary>
        /// <param name="gameEvent">Game event.</param>
        public void Fill(GameEvent gameEvent)
        {
            LastEvent = gameEvent;
            int i = 0;
            foreach (var key in EventDescriptor.Keys)
            {
                switch (key.Type)
                {
                    case 1: // string
                        Strings[i].Value = gameEvent.Keys[i].String;
                        break;
                    case 2: // float
                        Floats[i].Value = gameEvent.Keys[i].Float;
                        break;
                    case 3: // long
                        Integers[i].Value = gameEvent.Keys[i].Long;
                        break;
                    case 4: // short
                        Integers[i].Value = gameEvent.Keys[i].Short;
                        break;
                    case 5: // byte
                        Integers[i].Value = gameEvent.Keys[i].Byte;
                        break;
                    case 6: // bool
                        Bools[i].Value = gameEvent.Keys[i].Bool;
                        break;
                }

                i++;
            }
        }
    
        public EventInformation Copy() 
        {
            EventInformation info = new EventInformation(EventDescriptor, Parser);
            info.Fill(LastEvent);
            info.Instance.InitializeCopy(Instance);
        
            return info;
        }
    }
}


using System;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.ProtobufMessages;

namespace EHVAG.DemoInfo.DemoPackets
{
    public class DemoPacketsParser
    {
        /// <summary>
        /// The parser for packet_entities-messages. 
        /// </summary>
        /// <value>The entities parser.</value>
        public PacketEntitiesParser EntitiesParser { get; private set; }

        private DemoParser Parser { get; set; }

        public DemoPacketsParser(DemoParser parser)
        {
            this.Parser = parser;

            EntitiesParser = new PacketEntitiesParser(Parser);
        }

        internal void ParsePacket(IBitStream reader)
        {
            // While there is another message
            while (!reader.ChunkFinished)
            {
                int cmd = reader.ReadProtobufVarInt(); // What type of packet is this?
                int length = reader.ReadProtobufVarInt(); // and how long is it?
                reader.BeginChunk(length * 8); // read length bytes

                switch((SVCMessages)cmd)
                {
                    case SVCMessages.PacketEntities:
                        //Parse packet entities
                        // The PacketEntities-Object calls EntitiesParser.ParseEntitiesMessage()
                        // itself
                        new PacketEntities().Parse(reader, Parser);
                        break;
                    case SVCMessages.CreateStringTable:
                        new CreateStringTable().Parse(reader, Parser);
                        break;
                    case SVCMessages.UpdateStringTable:
                        new UpdateStringTable().Parse(reader, Parser);
                        break;
                    case SVCMessages.GameEventList:
                        new GameEventList().Parse(reader, Parser);
                        break;
                    case SVCMessages.GameEvent:
                        Parser.RawData.GameEventParser.Parse(new GameEvent().Parse(reader, Parser));
                        break;
                }
                /* else if (cmd == (int)SVCMessages.GameEventList)
                { //and all this other stuff
                    new GameEventList().Parse(reader, demo);
                }
                else if (cmd == (int)SVCMessages.GameEvent)
                {
                    new GameEvent().Parse(reader, demo);
                }
                else if (cmd == (int)SVCMessages.CreateStringTable)
                {
                    new CreateStringTable().Parse(reader, demo);
                }
                else if (cmd == (int)SVCMessages.UpdateStringTable)
                {
                    new UpdateStringTable().Parse(reader, demo);
                }
                else if (cmd == (int)NET_Messages.net_Tick)
                { //and all this other stuff
                    new NETTick().Parse(reader, demo);
                } */

                //TODO: Slow protobuf parsing

                reader.EndChunk();
            }
        }
    }
}


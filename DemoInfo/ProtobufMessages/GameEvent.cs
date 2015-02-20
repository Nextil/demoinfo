using System;
using System.IO;
using System.Collections.Generic;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo;

namespace EHVAG.DemoInfo.ProtobufMessages
{
    public struct GameEvent
    {
        public struct QuickAndDirtyKey
        {
            public Int32 Type;
            public String String;
            public float Float;
            public Int32 Long;
            public Int32 Short;
            public Int32 Byte;
            public bool Bool;
            public UInt64 UInt64;
        }

        public string EventName;
        public Int32 EventId;
        public IList<QuickAndDirtyKey> Keys;

        internal void Parse(IBitStream bitstream, DemoParser parser)
        {
            Keys = new List<QuickAndDirtyKey>();
            while (!bitstream.ChunkFinished)
            {
                var desc = bitstream.ReadProtobufVarInt();
                var wireType = desc & 7;
                var fieldnum = desc >> 3;
                if ((wireType == 2) && (fieldnum == 1))
                {
                    EventName = bitstream.ReadProtobufString();
                }
                else if ((wireType == 0) && (fieldnum == 2))
                {
                    EventId = bitstream.ReadProtobufVarInt();
                }
                else if ((wireType == 2) && (fieldnum == 3))
                {
                    var qad = new QuickAndDirtyKey();
                    bitstream.BeginChunk(bitstream.ReadProtobufVarInt() * 8);
                    while (!bitstream.ChunkFinished) {
                        desc = bitstream.ReadProtobufVarInt();
                        wireType = desc & 7;
                        fieldnum = desc >> 3;
                        switch (fieldnum)
                        {
                            case 1:
                                if (wireType != 0)
                                    throw new InvalidDataException();
                                qad.Type = bitstream.ReadProtobufVarInt();
                                break;
                            case 2:
                                if (wireType != 2)
                                    throw new InvalidDataException();
                                qad.String = bitstream.ReadProtobufString();
                                break;
                            case 3:
                                if (wireType != 5)
                                    throw new InvalidDataException();
                                qad.Float = bitstream.ReadFloat();
                                break;
                            case 4:
                                if (wireType != 0)
                                    throw new InvalidDataException();
                                qad.Long = bitstream.ReadProtobufVarInt();
                                break;
                            case 5:
                                if (wireType != 0)
                                    throw new InvalidDataException();
                                qad.Short = bitstream.ReadProtobufVarInt();
                                break;
                            case 6:
                                if (wireType != 0)
                                    throw new InvalidDataException();
                                qad.Byte = bitstream.ReadProtobufVarInt();
                                break;
                            case 7:
                                if (wireType != 0)
                                    throw new InvalidDataException();
                                qad.Bool = bitstream.ReadProtobufVarInt() != 0;
                                break;
                            case 8:
                                if (wireType != 0)
                                    throw new InvalidDataException();
                                qad.UInt64 = (ulong)bitstream.ReadProtobufVarInt();
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    bitstream.EndChunk();
                    Keys.Add(qad);
                }
                else
                    throw new InvalidDataException();
            }
        }
    }
}


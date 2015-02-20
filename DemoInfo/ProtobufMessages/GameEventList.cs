using System;
using System.IO;
using System.Collections.Generic;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo.Utils.Reflection;
using System.Reflection;

namespace EHVAG.DemoInfo.ProtobufMessages
{
    public class GameEventList
    {
        public class Key
        {
            public Int32 Type;
            public String Name;
            public MethodInfo Setter;

            internal void Parse(IBitStream bitstream)
            {
                while (!bitstream.ChunkFinished)
                {
                    var desc = bitstream.ReadProtobufVarInt();
                    var wireType = desc & 7;
                    var fieldnum = desc >> 3;
                    if ((wireType == 0) && (fieldnum == 1))
                    {
                        Type = bitstream.ReadProtobufVarInt();
                    }
                    else if ((wireType == 2) && (fieldnum == 2))
                    {
                        Name = bitstream.ReadProtobufString();
                    }
                    else
                        throw new InvalidDataException();
                }
            }
        }

        public class Descriptor
        {
            public Int32 EventId;
            public String Name;
            public Key[] Keys;

            public Type EventType;


            internal void Parse(IBitStream bitstream)
            {
                var keys = new List<Key>();
                while (!bitstream.ChunkFinished)
                {
                    var desc = bitstream.ReadProtobufVarInt();
                    var wireType = desc & 7;
                    var fieldnum = desc >> 3;
                    if ((wireType == 0) && (fieldnum == 1))
                    {
                        EventId = bitstream.ReadProtobufVarInt();
                    }
                    else if ((wireType == 2) && (fieldnum == 2))
                    {
                        Name = bitstream.ReadProtobufString();
                    }
                    else if ((wireType == 2) && (fieldnum == 3))
                    {
                        var length = bitstream.ReadProtobufVarInt();
                        bitstream.BeginChunk(length * 8);
                        var key = new Key();
                        key.Parse(bitstream);
                        keys.Add(key);
                        bitstream.EndChunk();
                    }
                    else
                        throw new InvalidDataException();
                }
                Keys = keys.ToArray();
            }
        }

        internal void Parse(IBitStream bitstream, DemoParser parser)
        {
            foreach (var descriptor in ReadDescriptors(bitstream))
                parser.RawData.GameEventDescriptors[descriptor.EventId] = descriptor;

            var h = new ReflectionHelper(parser);
            h.DoGameEventReflection();
            parser.RawData.GameEventParser.Initialize();
        }

        private IEnumerable<Descriptor> ReadDescriptors(IBitStream bitstream)
        {
            while (!bitstream.ChunkFinished)
            {
                var desc = bitstream.ReadProtobufVarInt();
                var wireType = desc & 7;
                var fieldnum = desc >> 3;
                if ((wireType != 2) || (fieldnum != 1))
                    throw new InvalidDataException();

                var length = bitstream.ReadProtobufVarInt();
                bitstream.BeginChunk(length * 8);
                var descriptor = new Descriptor();
                descriptor.Parse(bitstream);
                yield return descriptor;
                bitstream.EndChunk();
            }
        }
    }
}


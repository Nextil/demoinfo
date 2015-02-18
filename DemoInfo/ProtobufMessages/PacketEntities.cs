using System;
using System.IO;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo;

namespace EHVAG.DemoInfo.ProtobufMessages
{
    struct PacketEntities
    {
        public Int32 MaxEntries;
        public Int32 UpdatedEntries;
        private Int32 _IsDelta;
        public bool IsDelta { get { return _IsDelta != 0; } }
        private Int32 _UpdateBaseline;
        public bool UpdateBaseline { get { return _UpdateBaseline != 0; } }
        public Int32 Baseline;
        public Int32 DeltaFrom;

        public void Parse(IBitStream bitstream, DemoParser parser)
        {
            while (!bitstream.ChunkFinished)
            {
                var desc = bitstream.ReadProtobufVarInt();
                var wireType = desc & 7;
                var fieldnum = desc >> 3;

                if ((fieldnum == 7) && (wireType == 2))
                {
                    // Entity data is special.
                    // We'll simply hope that gaben is nice and sends
                    // entity_data last, just like he should.

                    var len = bitstream.ReadProtobufVarInt();
                    bitstream.BeginChunk(len * 8);
                    // the execution path is so obvious :/

                    parser.RawData.PacketParser.EntitiesParser.ParseEntitesMessage(this, bitstream);
                    bitstream.EndChunk();
                    if (!bitstream.ChunkFinished)
                        throw new NotImplementedException("PacketEntities packet was in an order we can't handle (although it's valid!). Please open an issue on GitHub");

                    break;
                }

                if (wireType != 0)
                    throw new InvalidDataException();

                var val = bitstream.ReadProtobufVarInt();

                switch (fieldnum)
                {
                    case 1:
                        MaxEntries = val;
                        break;
                    case 2:
                        UpdatedEntries = val;
                        break;
                    case 3:
                        _IsDelta = val;
                        break;
                    case 4:
                        _UpdateBaseline = val;
                        break;
                    case 5:
                        Baseline = val;
                        break;
                    case 6:
                        DeltaFrom = val;
                        break;
                    default:
                        // silently drop
                        break;
                }
            }
        }
    }
}


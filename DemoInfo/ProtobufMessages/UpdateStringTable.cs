using System;
using System.IO;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo;

namespace EHVAG.DemoInfo.ProtobufMessages
{
    struct UpdateStringTable
    {
        public Int32 TableId;
        public Int32 NumChangedEntries;

        internal void Parse(IBitStream bitstream, DemoParser parser)
        {
            while (!bitstream.ChunkFinished)
            {
                var desc = bitstream.ReadProtobufVarInt();
                var wireType = desc & 7;
                var fieldnum = desc >> 3;

                if ((wireType == 2) && (fieldnum == 3))
                {
                    // String data is special.
                    // We'll simply hope that gaben is nice and sends
                    // string_data last, just like he should.
                    var len = bitstream.ReadProtobufVarInt();
                    bitstream.BeginChunk(len * 8);

                    StringTables.StringTable.ParseUpdateStringTable(parser, bitstream, this);

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
                        TableId = val;
                        break;
                    case 2:
                        NumChangedEntries = val;
                        break;
                    default:
                        // silently drop
                        break;
                }
            }
        }
    }
}


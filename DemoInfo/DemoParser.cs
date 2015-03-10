using System;
using System.IO;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.StringTables;
using EHVAG.DemoInfo.States;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo
{
    /// <summary>
    /// Demo parser.
    /// </summary>
    public class DemoParser
    {
        /// <summary>
        /// The Demo-Stream, the main input-stream. 
        /// </summary>
        readonly IBitStream DemoStream;

        /// <summary>
        /// Gets the raw data of the parser. This shouldn't be used in most cases. 
        /// </summary>
        /// <value>The raw data.</value>
        public RawDataState RawData { get; private set; }

        /// <summary>
        /// Gets access to the Events of the parser. 
        /// </summary>
        /// <value>The raw data.</value>
        public EventState Events { get; private set; }

        /// <summary>
        /// Gets most informations about the current state of the Game
        /// </summary>
        public DemoState GameState { get; private set; }


        /// <summary>
        /// Initializes the DemoParser and reads the DemoHeader.
        /// </summary>
        /// <param name="demoStream">Demo file.</param>
        public DemoParser(Stream demoStream)
        {
            DemoStream = BitStreamUtil.Create(demoStream);
            RawData = new RawDataState(this);
            GameState = new DemoState(this);
            Events = new EventState(this);

            ParseHeader();
        }

        /// <summary>
        /// Parses the next tick of the demo
        /// </summary>
        /// <returns><c>true</c>, if there is another tick, <c>false</c> otherwise.</returns>
        public bool ParseOneTick()
        {
            DemoCommand command = (DemoCommand)DemoStream.ReadByte();

            DemoStream.ReadInt(32); // tick number
            DemoStream.ReadByte(); // player slot

            switch (command)
            {
                case DemoCommand.Synctick:
                    break;
                case DemoCommand.Stop:
                    return false;
                case DemoCommand.ConsoleCommand:
                    DemoStream.BeginChunk(DemoStream.ReadSignedInt(32) * 8);
                    DemoStream.EndChunk();
                    break;
                case DemoCommand.DataTables:
                    DemoStream.BeginChunk(DemoStream.ReadSignedInt(32) * 8);
                    DataTableParser dtParser = new DataTableParser();
                    dtParser.ParsePacket(DemoStream);
                    RawData.ServerClasses.AddRange(dtParser.ServerClasses);
                    var h = new ReflectionHelper(this);
                    h.DoServerClassesReflection();
                    DemoStream.EndChunk();
                    break;
                case DemoCommand.StringTables:
                    DemoStream.BeginChunk(DemoStream.ReadSignedInt(32) * 8);
                    StringTable.ParseStringTables(this, DemoStream);
                    RawData.RegeneratePlayerInfos();
                    DemoStream.EndChunk();
                    break;
                case DemoCommand.UserCommand:
                    DemoStream.ReadInt(32);
                    DemoStream.EndChunk();
                    break;
                case DemoCommand.Signon:
                case DemoCommand.Packet:
                    ParseDemoPacket();
                    break;
                default:
                    throw new Exception("Can't handle Demo-Command " + command);
            }

            return true;
        }

        /// <summary>
        /// Parses this demo file until it ends. 
        /// </summary>
        public void ParseToEnd()
        {
            while (ParseOneTick())
            {
            }
        }


        /// <summary>
        /// Parses the file-header.
        /// </summary>
        private void ParseHeader()
        {
            RawData.FileHeader = DemoHeader.ParseFrom(DemoStream);

            if (RawData.FileHeader.Filestamp != DemoHeader.FILESTAMP)
                throw new InvalidDataException("Invalid File-Type - expecting HL2DEMO");

            if (RawData.FileHeader.Protocol != DemoHeader.DEMO_PROTOCOL)
                throw new InvalidDataException("Invalid Demo-Protocol");
        }

        /// <summary>
        /// Parses a DEM_PACKET. 
        /// </summary>
        private void ParseDemoPacket()
        {
            //Read a command-info. Contains no really useful information afaik. 
            CommandInfo.Parse(DemoStream);
            DemoStream.ReadInt(32); // SeqNrIn
            DemoStream.ReadInt(32); // SeqNrOut

            DemoStream.BeginChunk(DemoStream.ReadSignedInt(32) * 8);
            RawData.PacketParser.ParsePacket(DemoStream);
            DemoStream.EndChunk();
        }

    }
}


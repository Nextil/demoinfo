﻿using System;
using System.IO;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.StringTables;
using EHVAG.DemoInfo.States;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.Edicts.Reflection;

namespace EHVAG.DemoInfo 
{
    /// <summary>
    /// Demo parser.
    /// </summary>
    public class DemoParser
    {
        /// <summary>
        /// The demo-header of the file. 
        /// </summary>
        /// <value>The file header.</value>
        public DemoHeader FileHeader { get; private set; }


        /// <summary>
        /// The Demo-Stream, the main input-stream. 
        /// </summary>
        readonly IBitStream DemoStream;

        public RawDataState RawData { get; private set; }

        /// <summary>
        /// Initializes the DemoParser and reads the DemoHeader.
        /// </summary>
        /// <param name="demoStream">Demo file.</param>
        public DemoParser(Stream demoStream)
        {
            DemoStream = new UnsafeBitStream();
            DemoStream.Initialize(demoStream);

            RawData = new RawDataState();

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

            switch (command) {
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
                    ReflectionHelper h = new ReflectionHelper(this);
                    h.DoReflection();
                    DemoStream.EndChunk ();
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
            FileHeader = DemoHeader.ParseFrom(DemoStream);

            if (FileHeader.Filestamp != DemoHeader.FILESTAMP)
                throw new InvalidDataException("Invalid File-Type - expecting HL2DEMO");

            if (FileHeader.Protocol != DemoHeader.DEMO_PROTOCOL)
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
            //DemoPacketParser.ParsePacket(BitStream, this);
            DemoStream.EndChunk();
        }

    }
}


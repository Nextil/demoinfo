using System;
using System.Collections.Generic;
using EHVAG.DemoInfo.Utils;
using System.IO;
using EHVAG.DemoInfo.States;
using EHVAG.DemoInfo.ProtobufMessages;

namespace EHVAG.DemoInfo.StringTables
{
    public class StringTable
    {
        /// <summary>
        /// The name of the StringTable
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// The entries of the StringTable
        /// </summary>
        /// <value>The entries.</value>
        public StringTableEntry[] Entries { get; private set; }

        /// <summary>
        /// Clientside data. I'm not exactly sure what this is. 
        /// </summary>
        /// <value>The Clientside data.</value>
        public byte[] ClientData { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EHVAG.DemoInfo.StringTables.StringTable"/> class.
        /// </summary>
        private StringTable(int maxEntries)
        {
            Entries = new StringTableEntry[maxEntries];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EHVAG.DemoInfo.StringTables.StringTable"/> class.
        /// </summary>
        internal StringTable(string name, int maxEntries)
            : this(maxEntries)
        {
            this.Name = name;
        }

        /// <summary>
        /// Parses a DEM_STRINGTABLES packet, and inserts it into the state
        /// </summary>
        /// <param name="state">State.</param>
        /// <param name="reader">Reader.</param>
        internal static void ParseStringTables(DemoParser parser, IBitStream reader)
        {
            int numTables = reader.ReadByte();

            for (int i = 0; i < numTables; i++)
            {
                Parse(parser, reader);
            }
        }


        /// <summary>
        /// Parses a StringTable
        /// </summary>
        /// <param name="parser">Parser.</param>
        internal static void Parse(DemoParser parser, IBitStream reader)
        {
            string tableName = reader.ReadString();
            int numStrings = (int)reader.ReadInt(16);

            StringTable table = null;

            if (!parser.RawData.StringTables.ContainsKey(tableName))
                table = new StringTable(tableName, numStrings);
            else
                table = parser.RawData.StringTables[tableName];

            parser.RawData.StringTables[tableName] = table;

            for (int i = 0; i < numStrings; i++)
            {
                StringTableEntry entry = new StringTableEntry();

                entry.Name = reader.ReadString();
                entry.Index = i;

                if (entry.Name.Length >= 100)
                    throw new InvalidDataException(
                        "The name of the string is longer than 100 chars, which is forbidden."
                    );

                if (reader.ReadBit())
                {
                    int userDataSize = (int)reader.ReadInt(16);

                    entry.UserData = reader.ReadBytes(userDataSize);
                }

                table.Entries[entry.Index] = entry;
            }

            // "Client side stuff" is the official comment by valve
            // I'm not quite sure what it is, but I'd rather save it :)
            if (reader.ReadBit())
            {
                int numstrings = (int)reader.ReadInt(16);
                for (int i = 0; i < numstrings; i++)
                {
                    reader.ReadString(); // stringname

                    if (reader.ReadBit())
                    {
                        int userDataSize = (int)reader.ReadInt(16);

                        table.ClientData = reader.ReadBytes(userDataSize);
                    }
                }
            }
        }


        /// <summary>
        /// Parses a CreateStringTable-protobuf-message. Not that the data-field has not been read yet,
        /// so we can read the raw bytes from the stream. This is cool, mainly for performance reasons, 
        /// and the main reason we implemented protobuf ourselves, because it's so much faster. 
        /// </summary>
        /// <param name="parser">The demoparser</param>
        /// <param name="reader">The underlying stream</param>
        /// <param name="message">The createstringtables-message</param>
        internal static void ParseCreateStringTable(DemoParser parser, IBitStream reader, CreateStringTable message)
        {
            StringTable resultingTable = new StringTable(message.Name, message.MaxEntries);

            UpdateStringTable(parser, reader, message, resultingTable, message.NumEntries);

            parser.RawData.CreateStringTableMessages.Add(message);
            parser.RawData.StringTables[message.Name] = resultingTable;
        }

        internal static void ParseUpdateStringTable(DemoParser parser, IBitStream reader, UpdateStringTable message)
        {
            var createMessage = parser.RawData.CreateStringTableMessages[message.TableId];
            StringTable resultingTable = parser.RawData.StringTables[createMessage.Name];

            UpdateStringTable(parser, reader, createMessage, resultingTable, message.NumChangedEntries);
        }

        private static void UpdateStringTable(DemoParser parser, IBitStream reader, CreateStringTable createMessage, StringTable tableToUpdate, int numEntries)
        {
            if (reader.ReadBit())
                throw new NotImplementedException("The stringtable is encoded with dictionaries, unable to decode");

            int nTemp = createMessage.MaxEntries;
            int nEntryBits = 0;
            while ((nTemp >>= 1) != 0)
                ++nEntryBits;

            List<string> history = new List<string>(32);

            int lastEntry = -1;

            for (int i = 0; i < numEntries; i++)
            {
                int entryIndex = lastEntry + 1;
                // d in the entity-index
                if (!reader.ReadBit())
                {
                    entryIndex = (int)reader.ReadInt(nEntryBits);
                }

                lastEntry = entryIndex;

                // Read the name of the string into entry.
                string entry = "";
                if (entryIndex < 0 || entryIndex >= createMessage.MaxEntries)
                {
                    throw new InvalidDataException("Invalid entry-Index");
                }

                //Does the string have a name?
                if (reader.ReadBit())
                {
                    bool substringcheck = reader.ReadBit();

                    // Is there a history-entry with the same beginning?
                    // If yes, copy it over. 
                    // We have to save *every* byte!!!!!111111oneeleven. 
                    // At least that is what Valve thinks. 
                    if (substringcheck)
                    {
                        int index = (int)reader.ReadInt(5);
                        int bytestocopy = (int)reader.ReadInt(5);

                        entry = history[index].Substring(0, bytestocopy);

                        entry += reader.ReadString(1024);
                    }
                    else
                    {
                        entry = reader.ReadString(1024);
                    }
                }

                if (entry == null)
                    entry = "";

                if (history.Count > 31)
                    history.RemoveAt(0);

                history.Add(entry);

                // Read in the user data.
                byte[] userdata = null;
                if (reader.ReadBit())
                {
                    if (createMessage.UserDataFixedSize)
                    {
                        userdata = reader.ReadBits(createMessage.UserDataSizeBits);
                    }
                    else
                    {
                        int bytesToRead = (int)reader.ReadInt(14);

                        userdata = reader.ReadBytes(bytesToRead);
                    }
                }

                var tableEntry = new StringTableEntry()
                {
                    Index = entryIndex,
                    Name = entry,
                    UserData = userdata
                };


                tableToUpdate.Entries[entryIndex] = tableEntry;


                if (userdata == null)
                    break;
            }
        }
    }
}


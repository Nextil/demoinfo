using System;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.ProtobufMessages;
using System.Collections.Generic;
using System.Linq;

namespace EHVAG.DemoInfo.DataTables
{
    /// <summary>
    /// A parser for a DEM_DATATABLES packet
    /// </summary>
    public class DataTableParser
    {
        readonly List<SendTable> SendTables = new List<SendTable>();
        public List<ServerClass> ServerClasses = new List<ServerClass>();
        List<ExcludeEntry> CurrentExcludes = new List<ExcludeEntry>();
        List<ServerClass> CurrentBaseclasses = new List<ServerClass>();


        internal void ParsePacket(IBitStream bitstream)
        {
            // First, let's read the SendTables. 
            // They are encoded as protobuf-messages
            while (true)
            {
                var type = (SVCMessages)bitstream.ReadProtobufVarInt();
                if (type != SVCMessages.SendTable)
                    throw new Exception("Expected SendTable, got " + type);

                var size = bitstream.ReadProtobufVarInt();
                bitstream.BeginChunk(size * 8);
                var sendTable = new SendTable();
                sendTable.Parse(bitstream);
                bitstream.EndChunk();

                if (sendTable.IsEnd)
                    break;

                SendTables.Add(sendTable);
            }

            // Now we get the server-classes
            // These are the actual mapping between a field
            // And the property in a class
            int serverClassCount = checked((int)bitstream.ReadInt(16));

            for (int i = 0; i < serverClassCount; i++)
            {
                ServerClass entry = new ServerClass();
                entry.ClassID = checked((int)bitstream.ReadInt(16));

                if (entry.ClassID > serverClassCount)
                    throw new Exception("Invalid class index");

                entry.Name = bitstream.ReadDataTableString();
                entry.DTName = bitstream.ReadDataTableString();

                entry.DataTableID = SendTables.FindIndex(a => a.NetTableName == entry.DTName);

                ServerClasses.Add(entry);
            }

            // And now, we just need to flatten it. 
            // Let the magic begin.

            for (int i = 0; i < serverClassCount; i++)
                FlattenDataTable(i);
        }

        void FlattenDataTable(int serverClassIndex)
        {
            var currentSendTable = SendTables[ServerClasses[serverClassIndex].DataTableID];

            CurrentExcludes.Clear();
            CurrentBaseclasses = new List<ServerClass> (); //NOT .clear because we use *this* reference
            //@main--, this is warning for you. 

            GatherExcludesAndBaseclasses(currentSendTable, true);

            ServerClasses [serverClassIndex].BaseClasses = CurrentBaseclasses;

            GatherProps(currentSendTable, serverClassIndex, "");

            var flattenedProps = ServerClasses[serverClassIndex].FlattenedProps;

            List<int> priorities = new List<int>();
            priorities.Add(64);
            priorities.AddRange(flattenedProps.Select(a => a.Prop.Priority).Distinct());
            priorities.Sort();

            int start = 0;
            for (int priorityIndex = 0; priorityIndex < priorities.Count; priorityIndex++)
            {
                int priority = priorities[priorityIndex];

                while (true)
                {
                    int currentProp = start;

                    while (currentProp < flattenedProps.Count)
                    {
                        SendTable.SendProp prop = flattenedProps[currentProp].Prop;

                        if (
                            prop.Priority == priority || 
                            ((priority == 64) && (prop.Flags & (int)SendPropertyFlags.ChangesOften) == (int)SendPropertyFlags.ChangesOften))
                        {
                            if (start != currentProp)
                            {
                                FlattenedPropEntry temp = flattenedProps[start];
                                flattenedProps[start] = flattenedProps[currentProp];
                                flattenedProps[currentProp] = temp;
                            }

                            start++;
                            break;
                        }
                        currentProp++;
                    }

                    if (currentProp == flattenedProps.Count)
                        break;
                }
            }

            for (int i = 0; i < flattenedProps.Count; i++)
                flattenedProps[i].Index = i;
        }

        void GatherExcludesAndBaseclasses(SendTable sendTable, bool collectBaseClasses)
        {
            CurrentExcludes.AddRange(
                sendTable.Properties
                .Where(a => (a.Flags & (int)SendPropertyFlags.Exclude) == (int)SendPropertyFlags.Exclude)
                .Select(a => new ExcludeEntry(a.VarName, a.DtName, sendTable.NetTableName))
            );

            foreach (var prop in sendTable.Properties.Where(a => a.Type == (int)SendPropertyType.DataTable))
            {
                if (collectBaseClasses && prop.VarName == "baseclass") {
                    GatherExcludesAndBaseclasses (GetTableByName (prop.DtName), true);
                    CurrentBaseclasses.Add (FindByDTName (prop.DtName));
                } else {
                    GatherExcludesAndBaseclasses (GetTableByName (prop.DtName), false);
                }
            }
        }

        void GatherProps(SendTable table, int serverClassIndex, string prefix)
        {
            List<FlattenedPropEntry> tmpFlattenedProps = new List<FlattenedPropEntry>();
            GatherProps_IterateProps(table, serverClassIndex, tmpFlattenedProps, prefix);

            List<FlattenedPropEntry> flattenedProps = ServerClasses[serverClassIndex].FlattenedProps;

            flattenedProps.AddRange(tmpFlattenedProps);
        }

        void GatherProps_IterateProps(SendTable table, int ServerClassIndex, List<FlattenedPropEntry> flattenedProps, string prefix)
        {
            for (int i = 0; i < table.Properties.Count; i++)
            {
                SendTable.SendProp property = table.Properties[i];

                if ((property.Flags & (int)SendPropertyFlags.InsideArray) == (int)SendPropertyFlags.InsideArray || 
                    (property.Flags & (int)SendPropertyFlags.Exclude) == (int)SendPropertyFlags.Exclude ||
                    IsPropExcluded(table, property))
                    continue;

                if (property.Type == (int)SendPropertyType.DataTable)
                {
                    SendTable subTable = GetTableByName(property.DtName);

                    if ((property.Flags & (int)SendPropertyFlags.Collapsible) == (int)SendPropertyFlags.Collapsible)
                    {
                        //we don't prefix Collapsible stuff, since it is just derived mostly
                        GatherProps_IterateProps(subTable, ServerClassIndex, flattenedProps, prefix);
                    }
                    else
                    {
                        //We do however prefix everything else

                        string nfix = prefix + ((property.VarName.Length > 0) ? property.VarName + "." : "");

                        GatherProps(subTable, ServerClassIndex, nfix);
                    }
                }
                else
                {
                    if (property.Type == (int)SendPropertyType.Array)
                    {
                        flattenedProps.Add(new FlattenedPropEntry(prefix + property.VarName, property, table.Properties[i - 1]));
                    }
                    else
                    {
                        flattenedProps.Add(new FlattenedPropEntry(prefix + property.VarName, property, null));
                    }
                }


            }
        }

        bool IsPropExcluded(SendTable table, SendTable.SendProp prop)
        {
            return CurrentExcludes.Exists(a => table.NetTableName == a.DTName && prop.VarName == a.VarName);
        }

        SendTable GetTableByName(string pName)
        {
            return SendTables.FirstOrDefault(a => a.NetTableName == pName);
        }

        private ServerClass FindByDTName(string dtName)
        {
            return ServerClasses.Single(a => a.DTName == dtName);
        }

        class ExcludeEntry
        {
            public ExcludeEntry( string varName, string dtName, string excludingDT )
            {
                VarName = varName;
                DTName = dtName;
                ExcludingDT = excludingDT;
            }

            public string VarName { get; private set; }
            public string DTName { get; private set; }
            public string ExcludingDT { get; private set; }
        }
    }
}


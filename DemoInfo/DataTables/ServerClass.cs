using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EHVAG.DemoInfo.DataTables
{
    [DebuggerDisplay("ServerClass: {Name}")]
    public class ServerClass
    {
        public int ClassID { get; set; }

        public int DataTableID { get; set; }

        public string Name { get; set; }

        public string DTName { get; set; }

        public List<FlattenedPropEntry> FlattenedProps  { get; set; }

        public List<ServerClass> BaseClasses { get; set; }

        public Type EntityType { get; set; }

        public ServerClass()
        {
            FlattenedProps = new List<FlattenedPropEntry>();
            BaseClasses = new List<ServerClass>();
        }


    }
}


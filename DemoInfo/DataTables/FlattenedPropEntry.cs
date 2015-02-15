using System;
using EHVAG.DemoInfo.ProtobufMessages;
using System.Reflection;
using System.Collections.Generic;

namespace EHVAG.DemoInfo.DataTables
{
    public class FlattenedPropEntry
    {
        public SendTable.SendProp Prop { get; private set; }
        public SendTable.SendProp ArrayElementProp { get; private set; }
        public string PropertyName { get; private set; }

        internal MethodInfo Setter { get; set; }
        internal List<FlattenedPropEntry> References { get; set; }

        public int Index { get; internal set; }

        public FlattenedPropEntry(string propertyName, SendTable.SendProp prop, SendTable.SendProp arrayElementProp)
        {
            this.Prop = prop;
            this.ArrayElementProp = arrayElementProp;
            this.PropertyName = propertyName;
            References = new List<FlattenedPropEntry>();
        }

        public override string ToString()
        {
            return string.Format("[FlattenedPropEntry: PropertyName={2}, Prop={0}, ArrayElementProp={1}]", Prop, ArrayElementProp, PropertyName);
        }

    }
}


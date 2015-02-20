using System;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.Edicts
{
    public class EntityInformation
    {
        /// <summary>
        /// Gets the instance of the entity
        /// </summary>
        /// <value>The instance.</value>
        public BaseEntity Instance { get; private set; }

        /// <summary>
        /// Gets the current DemoParser
        /// </summary>
        /// <value>The instance.</value>
        public DemoParser Parser { get; private set; }

        /// <summary>
        /// Gets the serial of the entity
        /// </summary>
        /// <value>The serial.</value>
        public int Serial { get; private set; }

        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        /// <value>The entity I.</value>
        public int EntityID { get; private set; }

        /// <summary>
        /// Gets the ServerClass of this object.
        /// </summary>
        /// <value>The class.</value>
        public ServerClass Class { get; private set; }

        /// <summary>
        /// Gets or sets the integers.
        /// </summary>
        /// <value>The integers.</value>
        internal NetworkedVar<int>[] Integers { get; private set; }

        /// <summary>
        /// Gets or sets the longs.
        /// </summary>
        /// <value>The longs.</value>
        internal NetworkedVar<long>[] Longs { get; private set; }

        /// <summary>
        /// Gets or sets the floats.
        /// </summary>
        /// <value>The floats.</value>
        internal NetworkedVar<float>[] Floats { get; private set; }

        /// <summary>
        /// Gets or sets the strings.
        /// </summary>
        /// <value>The strings.</value>
        internal NetworkedVar<string>[] Strings { get; private set; }

        /// <summary>
        /// Gets or sets the vectors.
        /// </summary>
        /// <value>The vectors.</value>
        internal NetworkedVar<Vector>[] Vectors { get; private set; }

        /// <summary>
        /// Gets or sets the bools.
        /// </summary>
        /// <value>The vectors.</value>
        internal NetworkedVar<bool>[] Bools { get; private set; }


        internal EntityInformation(int id, int serial, ServerClass serverClass, DemoParser parser)
        {
            Parser = parser;
            EntityID = id;
            Serial = serial;
            Class = serverClass;
            Integers = new NetworkedVar<int>[serverClass.FlattenedProps.Count];
            Longs = new NetworkedVar<long>[serverClass.FlattenedProps.Count];
            Floats = new NetworkedVar<float>[serverClass.FlattenedProps.Count];
            Strings = new NetworkedVar<string>[serverClass.FlattenedProps.Count];
            Vectors = new NetworkedVar<Vector>[serverClass.FlattenedProps.Count];
            Bools = new NetworkedVar<bool>[serverClass.FlattenedProps.Count];

            CreateInstance();
        }

        static readonly Type[] NOPARAMETERS = new Type[0];
        static readonly object[] NOPARAMETERSVALUES = new object[0];

        /// <summary>
        /// Creates the Instace (of a BaseEntity)
        /// </summary>
        /// <param name="serverClass">Server class.</param>
        private void CreateInstance()
        {
            if (Class.EntityType == null)
                return;

            Instance = (BaseEntity)Class.EntityType.GetConstructor(NOPARAMETERS).Invoke(NOPARAMETERSVALUES);
            Instance.EntityID = EntityID;
            Instance.EntityInfo = this;

            for(int i = 0; i < Class.FlattenedProps.Count; i++)
            {
                var property = Class.FlattenedProps[i];
                if (property.Setter != null)
                {
                    switch ((SendPropertyType)property.Prop.Type)
                    {
                    //We need this ugly case, since else we'd be too slow casting every time

                        case SendPropertyType.Array: 
                            throw new NotImplementedException();
                        case SendPropertyType.Int:
                            if (property.Prop.NumBits == 1)
                            {
                                var networkedBool = new NetworkedVar<bool>();
                                property.Setter.Invoke(Instance, new object[] { networkedBool });
                                Bools[i] = networkedBool;
                                for (int j = 0; j < property.References.Count; j++)
                                    Bools[property.References[j].Index] = networkedBool;
                            }
                            else
                            {
                                var networkedInt = new NetworkedVar<int>();
                                property.Setter.Invoke(Instance, new object[] { networkedInt });
                                Integers[i] = networkedInt;
                                for (int j = 0; j < property.References.Count; j++)
                                    Integers[property.References[j].Index] = networkedInt;
                            }
                            break;
                        case SendPropertyType.Float: 
                            var networkedFloat = new NetworkedVar<float>();
                            property.Setter.Invoke(Instance, new object[] { networkedFloat });
                            Floats[i] = networkedFloat;

                            for (int j = 0; j < property.References.Count; j++)
                                Floats[property.References[j].Index] = networkedFloat;
                            break;
                        case SendPropertyType.Int64: 
                            var networkedLong = new NetworkedVar<long>();
                            property.Setter.Invoke(Instance, new object[] { networkedLong });
                            Longs[i] = networkedLong;

                            for (int j = 0; j < property.References.Count; j++)
                                Longs[property.References[j].Index] = networkedLong;
                            break;
                        case SendPropertyType.String: 
                            var networkedString = new NetworkedVar<string>();
                            property.Setter.Invoke(Instance, new object[] { networkedString });
                            Strings[i] = networkedString;

                            for (int j = 0; j < property.References.Count; j++)
                                Strings[property.References[j].Index] = networkedString;
                            break;
                        case SendPropertyType.Vector: 
                        case SendPropertyType.VectorXY: 
                            var networkedVector = new NetworkedVar<Vector>();
                            property.Setter.Invoke(Instance, new object[] { networkedVector });
                            Vectors[i] = networkedVector;

                            for (int j = 0; j < property.References.Count; j++)
                                Vectors[property.References[j].Index] = networkedVector;
                            break;
                        default:
                            throw new NotImplementedException(
                                "This should never happen. Please open an issue containing the demo that threw this error on Github"
                            );
                    }
                }
            }
        }
    }
}


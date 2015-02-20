using System;
using System.Reflection;
using System.Linq;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.Utils.Reflection
{
    public class ReflectionHelper
    {
        DemoParser parser;

        public ReflectionHelper(DemoParser parser)
        {
            this.parser = parser;
        }

        public void DoReflection()
        {
            var ass = Assembly.GetExecutingAssembly();

            foreach (var type in ass.GetTypes())
            {
                var classAttribute = type.GetCustomAttribute<ServerClassAttribute>();

                //This is marked as server-class
                if (classAttribute != null)
                {
                    if (!typeof(BaseEntity).IsAssignableFrom(type))
                        throw new InvalidCastException(string.Format("The type {0} needs to derive from BaseEntity in order to be a ServerClass", type.Name));

                    string serverClassName = classAttribute.ServerClass;

                    //First, check that it's a valid class. 
                    var serverClass = parser.RawData.ServerClasses.Single(a => a.Name == serverClassName);

                    if (serverClass.EntityType != null)
                        throw new InvalidOperationException(string.Format("Only one class can be mapped to the class \"{0}\" - got at least two ({1} and {2})!", serverClassName, serverClass.EntityType.FullName, type.FullName));

                    serverClass.EntityType = type;

                    foreach (var property in type.GetProperties(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance))
                    {
                        var propAttributes = property.GetCustomAttributes<NetworkedPropertyAttribute>();

                        FlattenedPropEntry firstProp = null;
                        foreach (var propAttribute in propAttributes)
                        {
                            string propName = propAttribute.PropertyName;

                            var field = serverClass.FlattenedProps.SingleOrDefault(a => a.PropertyName == propName);

                            if (field == null)
                            {
                                if(!propAttribute.Optional)
                                    throw new InvalidOperationException("Couldn't find property " + propAttribute.PropertyName + " of Server-Class " + serverClass.Name +". Consider setting it as optional.");

                                continue;
                            }

                            if (field.Setter != null)
                                throw new InvalidOperationException("Only one field can be mapped to the property \"" + field.PropertyName + "\" of the class " + type.FullName);

                            switch ((SendPropertyType)field.Prop.Type)
                            {
                                case SendPropertyType.Array: 
                                    throw new NotImplementedException();
                                    break;
                                case SendPropertyType.Int:
                                    if (field.Prop.NumBits != 1)
                                    {
                                        if (property.PropertyType != typeof(NetworkedVar<int>))
                                            throw new InvalidOperationException("Bound to the wrong type - you need to bind to ints for this!");
                                    }
                                    else
                                    {
                                        if (property.PropertyType != typeof(NetworkedVar<bool>))
                                            throw new InvalidOperationException("Bound to the wrong type - you need to bind to bools for this!");
                                    }

                                    break;
                                case SendPropertyType.Float:
                                    if (property.PropertyType != typeof(NetworkedVar<float>))
                                        throw new InvalidOperationException("Bound to the wrong type!");
                                    break;
                                case SendPropertyType.Int64:
                                    if (property.PropertyType != typeof(NetworkedVar<long>))
                                        throw new InvalidOperationException("Bound to the wrong type!");
                                    break;
                                case SendPropertyType.String:
                                    if (property.PropertyType != typeof(NetworkedVar<string>))
                                        throw new InvalidOperationException("Bound to the wrong type!");
                                    break;
                                case SendPropertyType.Vector:
                                case SendPropertyType.VectorXY:
                                    if (property.PropertyType != typeof(NetworkedVar<Vector>))
                                        throw new InvalidOperationException("Bound to the wrong type!");
                                    break;
                                default:
                                    throw new NotImplementedException("This should never happen...");
                            }

                            if (firstProp == null)
                            {
                                field.Setter = property.SetMethod;

                                firstProp = field;
                            }
                            else
                            {
                                firstProp.References.Add(field);
                            }
                        }
                    }

                }
            }
        }
    }
}


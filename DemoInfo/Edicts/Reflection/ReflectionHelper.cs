using System;
using System.Reflection;
using System.Linq;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.ValveStructs;

namespace EHVAG.DemoInfo.Edicts.Reflection
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
                var classAttribute = type.CustomAttributes.SingleOrDefault(a => a.AttributeType == typeof(ServerClassAttribute));

                //This is marked as server-class
                if (classAttribute != null)
                {
                    if (!typeof(BaseEntity).IsAssignableFrom(type))
                        throw new InvalidCastException(string.Format("The type {0} needs to derive from BaseEntity in order to be a ServerClass", type.Name));

                    string serverClassName = (string)classAttribute.ConstructorArguments[0].Value;

                    //First, check that it's a valid class. 
                    var serverClass = parser.RawData.ServerClasses.Single(a => a.Name == serverClassName);

                    if (serverClass.EntityType != null)
                        throw new InvalidOperationException("Only one type is allowed!");

                    serverClass.EntityType = type;

                    foreach (var property in type.GetProperties(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance))
                    {
                        var propAttributes = property.CustomAttributes.Where(a => a.AttributeType == typeof(NetworkedPropertyAttribute));

                        FlattenedPropEntry firstProp = null;
                        foreach (var propAttribute in propAttributes)
                        {
                            string propName = (string)propAttribute.ConstructorArguments[0].Value;

                            var field = serverClass.FlattenedProps.Single(a => a.PropertyName == propName);

                            if (field.Setter != null)
                                throw new InvalidOperationException("Only one setter is allowed!");

                            switch ((SendPropertyType)field.Prop.Type)
                            {
                                case SendPropertyType.Array: 
                                    throw new NotImplementedException();
                                    break;
                                case SendPropertyType.Int: 
                                    if (property.PropertyType != typeof(NetworkedVar<int>))
                                        throw new InvalidOperationException("Bound to the wrong type!");
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


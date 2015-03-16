using System;
using System.Reflection;
using System.Linq;
using EHVAG.DemoInfo.DataTables;
using EHVAG.DemoInfo.ValveStructs;
using EHVAG.DemoInfo.DemoPackets.GameEvents;
using System.IO;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.Utils.Reflection
{
    public class ReflectionHelper
    {
        readonly DemoParser parser;

        public ReflectionHelper(DemoParser parser)
        {
            this.parser = parser;
        }

        public void DoServerClassesReflection()
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
        
            AddSubClasses();
        }

        private void AddSubClasses() {
            foreach(var serverClass in parser.RawData.ServerClasses) {
                // We only care about classes that haven't a entity-type yet. 
                if (serverClass.EntityType != null)
                    continue;

                // So we check if any super-class is implemented
                // Reverse, since BaseClasses[0] is the lowest class in the hirachy. 
                foreach (var superClass in serverClass.BaseClasses.Reverse<ServerClass>())
                {
                    if (superClass.EntityType != null)
                    {
                        // So this superclass is implemented
                        serverClass.EntityType = superClass.EntityType;

                        // No we match the properties. 
                        foreach (var superClassProp in superClass.FlattenedProps)
                        {
                            // We can't match them by index because of exclude props.

                            // If this hasn't a setter we can ignore it anyways. 
                            if (superClassProp.Setter == null)
                                continue;

                            // Find the matching prop in the current class. 
                            var serverClassProp = serverClass.FlattenedProps.SingleOrDefault(a => a.PropertyName == superClassProp.PropertyName);

                            if (serverClassProp == null)
                                continue;

                            // and set it. 
                            serverClassProp.Setter = superClassProp.Setter;

                        }

                        continue;
                    }
                }
            }
        }

        public void DoGameEventReflection()
        {
            var ass = Assembly.GetExecutingAssembly();

            foreach (var type in ass.GetTypes())
            {
                var classAttribute = type.GetCustomAttribute<ValveEventAttribute>();

                //This is marked as server-class
                if (classAttribute != null)
                {
                    if (!typeof(BaseEvent).IsAssignableFrom(type))
                        throw new InvalidCastException(string.Format("The type {0} needs to derive from BaseEvent in order to be a GameEvent", type.Name));

                    string eventName = classAttribute.ValveEvent;

                    //First, check that it's a valid class. 
                    var gameEvent = parser.RawData.GameEventDescriptors.Values.Single(a => a.Name == eventName);

                    if (gameEvent.EventType != null)
                        throw new InvalidOperationException(
                            string.Format(
                                "Only one class can be mapped to the event \"{0}\" - got at least two ({1} and {2})!", 
                                eventName, 
                                gameEvent.EventType.FullName, 
                                type.FullName
                            )
                        );

                    gameEvent.EventType = type;

                    int foundProps = 0;

                    foreach (var property in type.GetProperties(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance))
                    {
                        var propAttributes = property.GetCustomAttributes<NetworkedPropertyAttribute>();


                        bool firstProp = true;

                        foreach (var propAttribute in propAttributes)
                        {
                            string propName = propAttribute.PropertyName;

                            var key = gameEvent.Keys.First(a => a.Name == propName);


                            if (key == null)
                            {
                                if(!propAttribute.Optional)
                                    throw new InvalidOperationException("Couldn't find event-key " + propAttribute.PropertyName + " of Game-Event " + gameEvent.Name +". Consider setting it as optional.");

                                continue;
                            }

                            if (key.Setter != null)
                                throw new InvalidOperationException("Only one field can be mapped to the property \"" + key.Name + "\" of the class " + type.FullName);

                            switch (key.Type)
                            {
                                case 1: // string
                                    if (property.PropertyType != typeof(NetworkedVar<string>))
                                        throw new InvalidOperationException(
                                            string.Format("Type of {0}.{1} must be NetworkedVar<string>, got {2}", type.Name, property.Name, property.PropertyType.Name)
                                        );
                                    break;
                                case 2: // float
                                    if (property.PropertyType != typeof(NetworkedVar<float>))
                                        throw new InvalidOperationException(
                                            string.Format("Type of {0}.{1} must be NetworkedVar<float>, got {2}", type.Name, property.Name, property.PropertyType.Name)
                                        );
                                    break;
                                case 3: // long
                                case 4: // short
                                case 5: // byte
                                    if (property.PropertyType != typeof(NetworkedVar<int>))
                                        throw new InvalidOperationException(
                                            string.Format("Type of {0}.{1} must be NetworkedVar<int>, got {2}", type.Name, property.Name, property.PropertyType.Name)
                                        );
                                    break;
                                case 6: // bool
                                    if (property.PropertyType != typeof(NetworkedVar<bool>))
                                        throw new InvalidOperationException(
                                            string.Format("Type of {0}.{1} must be NetworkedVar<bool>, got {2}", type.Name, property.Name, property.PropertyType.Name)
                                        );
                                    break;
                                default:
                                    throw new InvalidDataException("Looks like Valve introduced a new GameEvent-Key-Type. Please open an issue at GitHub. ");
                            }


                            key.Setter = property.SetMethod;

                            if (!firstProp)
                            {
                                throw new Exception("Game-Events can only bind one key to one variable.");
                            }
                            firstProp = false;
                            foundProps++;
                        }
                    }

                    #if DEBUG_SLOW_MONO
                    if (foundProps < gameEvent.Keys.Length)
                    {
                        string missingProperties = string.Join(", ", gameEvent.Keys.Where(a => a.Setter == null).Select(a => a.Name));

                        throw new InvalidProgramException(
                            string.Format(
                                "GameEvent {0} is missing variables for the propert{2} {1}", 
                                gameEvent.Name,
                                missingProperties, 
                                (gameEvent.Keys.Length - foundProps == 1) ? "y" : "ies"  ));
                    }
                    #endif
                }
            }
        }
    }
}


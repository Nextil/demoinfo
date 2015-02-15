using System;
using EHVAG.DemoInfo.ProtobufMessages;
using EHVAG.DemoInfo.Utils;
using EHVAG.DemoInfo.Edicts;
using EHVAG.DemoInfo.DataTables;

namespace EHVAG.DemoInfo.DemoPackets
{
    public class PacketEntitiesParser
    {
        readonly DemoParser Parser;

        int _ClassBits = -1;

        int ClassBits
        {
            get
            {
                // Now this is language magic :)
                // (_ClassBits = CalculateClassBits()) is an assignment and
                // returns the value of CalculateClassBits(). 
                return _ClassBits == -1 ? (_ClassBits = CalculateClassBits()) : _ClassBits;
            }
        }

        public PacketEntitiesParser(DemoParser parser)
        {
            this.Parser = parser;
        }

        /// <summary>
        /// Parses a packet_entites message. This method assumes that the entity_data
        /// of the message hasn't been read yet, and is now lying on the stream. 
        /// </summary>
        /// <param name="entities">The message to parse.</param>
        /// <param name="reader">The input stream.</param>
        internal void ParseEntitesMessage(PacketEntities entities, IBitStream reader)
        {
            int currentEntity = -1;
            for (int i = 0; i < entities.UpdatedEntries; i++)
            {
                //First read which entity is updated
                currentEntity += 1 + (int)reader.ReadUBitInt();

                //Find out whether we should create, destroy or update it. 
                // Leave flag
                if (!reader.ReadBit())
                {
                    // enter flag
                    if (reader.ReadBit())
                    {
                        //create it
                        var e = ReadEnterPVS(currentEntity, reader);

                        Parser.RawData.Entities[currentEntity] = e;

                        ApplyUpdate(e, reader);
                    }
                    else
                    {
                        // preserve / update
                        var e = Parser.RawData.Entities[currentEntity];
                        ApplyUpdate(e, reader);
                    }
                }
                else
                {
                    // leave / destroy
                    //parser.Entities [currentEntity].Leave ();

                    //parser.Entities[currentEntity] = null;

                    //dunno, but you gotta read this. 
                    if (reader.ReadBit())
                    {
                        Parser.RawData.Entities[currentEntity] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Reads an EnterPVS Update - when an entity enters the potentiall visible System
        /// 
        /// </summary>
        /// <returns>The created entity</returns>
        /// <param name="id">The ID of the entity</param>
        /// <param name="reader">The reader to read the EnterVPS from</param>
        private EntityInformation ReadEnterPVS(int id, IBitStream reader)
        {
            // What kind of entity?
            int serverClassID = (int)reader.ReadInt(ClassBits);

            // So find the correct server class
            ServerClass entityClass = Parser.RawData.ServerClasses[serverClassID];

            int serial = (int)reader.ReadInt(10); // Entity serial. 
            // Never used anywhere I guess. Every parser just skips this

            // Create the new entity
            EntityInformation newEntity = new EntityInformation(id, serial, entityClass, Parser);

            //TODO: Instancebasline-parsing

            return newEntity;
        }

        private void ApplyUpdate(EntityInformation info, IBitStream reader)
        {
            //Okay, how does an entity-update look like?
            //First a list of the updated props is sent
            //And then the props itself are sent.


            //Read the field-indicies in a "new" way?
            bool newWay = reader.ReadBit();
            long index = -1;

            FieldIndexStore store = new FieldIndexStore();

            //No read them. 
            while ((index = ReadFieldIndex(reader, index, newWay)) != -1)
                store.Add(index);

            //Now read the updated props
            for(int i = 0; i < store.Count; i++)
            {
                ParseProp((int)store[i], info, reader);
            }
        }

        long ReadFieldIndex(IBitStream reader, long lastIndex, bool bNewWay)
        {
            if (bNewWay) {
                if (reader.ReadBit()) {
                    return lastIndex + 1;
                }
            }

            int ret = 0;
            if (bNewWay && reader.ReadBit()) {
                ret = (int)reader.ReadInt(3);  // read 3 bits
            } else {
                ret = (int)reader.ReadInt(7); // read 7 bits
                switch (ret & ( 32 | 64 )) {
                    case 32:
                        ret = ( ret & ~96 ) | ( (int)reader.ReadInt(2) << 5 );
                        break;
                    case 64:
                        ret = ( ret & ~96 ) | ( (int)reader.ReadInt(4) << 5 );
                        break;
                    case 96:
                        ret = ( ret & ~96 ) | ( (int)reader.ReadInt(7) << 5 );
                        break;
                }
            }

            if (ret == 0xFFF) { // end marker is 4095 for cs:go
                return -1;
            }

            return lastIndex + 1 + ret;
        }

        private void ParseProp(int index, EntityInformation entity, IBitStream reader)
        {
            //When we parse a prop, we first switch over the type. 
            var sendProp = entity.Class.FlattenedProps[index].Prop;

            switch ((SendPropertyType)sendProp.Type) {
                case SendPropertyType.Int:
                    //I'll only do the int as example: 

                    //I read the value here. That's it, the property is read. 
                    var intval = PropDecoder.DecodeInt(sendProp, reader);

                    //Then I find out if there is a C#-Class that want's to have the value
                    var intref = entity.Integers[index];

                    //If yes, I set the value. 
                    if (intref != null)
                        intref.Value = intval;

                    break;
                case SendPropertyType.Float:
                    var floatval = PropDecoder.DecodeFloat(sendProp, reader);
                    var floatref= entity.Floats[index];

                    if (floatref != null)
                        floatref.Value = floatval;

                    break;
                case SendPropertyType.Vector:
                    var vecval = PropDecoder.DecodeVector(sendProp, reader);
                    var vecref = entity.Vectors[index];

                    if (vecref != null)
                        vecref.Value = vecval;

                    break;
                case SendPropertyType.Array:
                    PropDecoder.DecodeArray(entity.Class.FlattenedProps[index], reader);

                    break;
                case SendPropertyType.String:
                    var stringval = PropDecoder.DecodeString(sendProp, reader);
                    var stringref = entity.Strings[index];

                    if (stringref != null)
                        stringref.Value = stringval;

                    break;
                case SendPropertyType.VectorXY:
                    var vecval2 = PropDecoder.DecodeVectorXY(sendProp, reader);
                    var vecref2 = entity.Vectors[index];

                    if (vecref2 != null)
                        vecref2.Value = vecval2;

                    break;
                default:
                    throw new NotImplementedException("Could not read property. Please open an issue at Github and provide the demofile. ");
            }
        }

        private int CalculateClassBits()
        {
            return (int)Math.Ceiling(Math.Log(Parser.RawData.ServerClasses.Count, 2));
        }
    }
}


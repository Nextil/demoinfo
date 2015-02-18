using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EHVAG.DemoInfo.Utils
{
    static class BitStreamUtil
    {
        /// <summary>
        /// The ascii-code of a newline-char
        /// </summary>
        const byte NEWLINE = 10;

        /// <summary>
        /// Creates an instance of the preferred <see cref="IBitStream"/> implementation for streams.
        /// </summary>
        public static IBitStream Create(Stream stream)
        {
            IBitStream bitstream =  new UnsafeBitStream();
            bitstream.Initialize(stream);
            return bitstream;
        }

        /// <summary>
        /// Creates an instance of the preferred <see cref="IBitStream"/> implementation for byte arrays.
        /// </summary>
        public static IBitStream Create(byte[] data)
        {
            return new UnsafeBitStream();
        }

        /// <summary>
        /// Reads an UBitInt. An UBitInt is a special encded integer used by Valve. It reads 6 bytes, and the two
        /// highes bits decide whether more bits should be read. 
        /// </summary>
        /// <returns>The U bit int.</returns>
        /// <param name="bs">Bs.</param>
        public static uint ReadUBitInt(this IBitStream bs)
        {
            uint ret = bs.ReadInt(6);
            switch (ret & (16 | 32))
            {
                case 16:
                    ret = (ret & 15) | (bs.ReadInt(4) << 4);
                    break;
                case 32:
                    ret = (ret & 15) | (bs.ReadInt(8) << 4);
                    break;
                case 48:
                    ret = (ret & 15) | (bs.ReadInt(32 - 4) << 4);
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Reads a string until the next 0-byte.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="bs">Bs.</param>
        public static string ReadString(this IBitStream bs)
        {
            return bs.ReadString(Int32.MaxValue);
        }

        /// <summary>
        /// Reads a string until the next 0 or 10(newline) - byte, or until the limit is reached. 
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="bs">Bs.</param>
        /// <param name="limit">Limit.</param>
        public static string ReadString(this IBitStream bs, int limit)
        {
            // The value 512 has proven to be the fastest. 
            var result = new List<byte>(512);

            for (int pos = 0; pos < limit; pos++)
            {
                var b = bs.ReadByte();
                if ((b == 0) || (b == NEWLINE))
                    break;

                result.Add(b);
            }
            return Encoding.ASCII.GetString(result.ToArray());
        }

        /// <summary>
        /// Reads a null-terminated string. 
        /// </summary>
        /// <returns>The data table string.</returns>
        /// <param name="bs">Bs.</param>
        public static string ReadDataTableString(this IBitStream bs)
        {
            using (var memstream = new MemoryStream())
            {
                // not particulary efficient, but probably fine
                for (byte b = bs.ReadByte(); b != 0; b = bs.ReadByte())
                    memstream.WriteByte(b);

                return Encoding.UTF8.GetString(memstream.GetBuffer(), 0, checked((int)memstream.Length));
            }
        }

        /// <summary>
        /// Reads the C string.
        /// </summary>
        /// <returns>The C string.</returns>
        /// <param name="reader">Reader.</param>
        /// <param name="length">Length.</param>
        public static string ReadCString(this IBitStream reader, int length)
        {
            var bytes = reader.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes, 0, Array.IndexOf(bytes, (byte)0));
        }

        /// <summary>
        /// Reads a protobuf-varint
        /// </summary>
        /// <returns>The variable int.</returns>
        public static uint ReadVarInt(this IBitStream bs)
        {
            uint tmpByte = 0x80;
            uint result = 0;
            for (int count = 0; (tmpByte & 0x80) != 0; count++)
            {
                if (count > 5)
                    throw new InvalidDataException("VarInt32 out of range");
                tmpByte = bs.ReadByte();
                result |= (tmpByte & 0x7F) << (7 * count);
            }
            return result;
        }

        /// <summary>
        /// Reads the protobuf string.
        /// </summary>
        /// <returns>The protobuf string.</returns>
        /// <param name="reader">Reader.</param>
        public static string ReadProtobufString(this IBitStream reader)
        {
            return Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadProtobufVarInt()));
        }

        /// <summary>
        /// Reads a protobuf varint, used by the IBitStream-Implementations. 
        /// </summary>
        /// <returns>The protobuf variable int stub.</returns>
        /// <param name="reader">Reader.</param>
        public static int ReadProtobufVarIntStub(IBitStream reader)
        {
            byte b = 0x80;
            int result = 0;
            for (int count = 0; (b & 0x80) != 0; count++) {
                b = reader.ReadByte();

                if ((count < 4) || ((count == 4) && (((b & 0xF8) == 0) || ((b & 0xF8) == 0xF8))))
                    result |= (b & ~0x80) << (7 * count);
                else {
                    if (count >= 10)
                        throw new OverflowException("Nope nope nope nope! 10 bytes max!");
                    if ((count == 9) ? (b != 1) : ((b & 0x7F) != 0x7F))
                        throw new NotSupportedException("more than 32 bits are not supported");
                }
            }

            return result;
        }

        /// <summary>
        /// Asserts that actual <= max. Used for debugging IBitStreams.
        /// </summary>
        /// <param name="max">Max.</param>
        /// <param name="actual">Actual.</param>
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertMaxBits(int max, int actual)
        {
            Debug.Assert(actual <= max, "trying to read too many bits", "Attempted to read {0} bits (max={1})", actual, max);
        }
    }
}


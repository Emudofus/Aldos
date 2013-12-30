using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Utils.Objects
{
    public class ByteList
    {
        protected List<byte> _bytes = new List<byte>();
        protected int _indexRead = -1;

        public int Length { get { return _bytes.Count; } }

        public ByteList()
        {
        }

        #region Ecriture
        public void WriteByte(byte value)
        {
            _bytes.Add(value);
        }
        public void WriteBytes(byte[] value)
        {
            _bytes.AddRange(value);
        }
        public void WriteBytes(byte[] value, int index, int count)
        {
            for (int i = index; i < count; ++i) _bytes.Add(value[i]);
        }
        public void WriteBytes(List<byte> value)
        {
            _bytes.AddRange(value);
        }
        public void WriteBool(bool value)
        {
            _bytes.Add( (byte)(value ? 1 : 0) );
        }
        public void WriteUTF(string value)
        {
            WriteUShort((ushort)value.Length);
            _bytes.AddRange( Encoding.Default.GetBytes(value) );
        }
        public void WriteUShort(ushort value)
        {
            _bytes.AddRange( BitConverter.GetBytes(value).Reverse() );
        }
        public void WriteShort(short value)
        {
            _bytes.AddRange(BitConverter.GetBytes(value).Reverse());
        }
        public void WriteInt(int value)
        {
            _bytes.AddRange(BitConverter.GetBytes(value).Reverse());
        }
        public void WriteLong(long value)
        {
            _bytes.AddRange(BitConverter.GetBytes(value).Reverse());
        }
        public void WriteDouble(double value)
        {
            _bytes.AddRange(BitConverter.GetBytes(value).Reverse());
        }
        public void WriteCell(int value)
        {
            _bytes.AddRange(BitConverter.GetBytes(value | 4095).Reverse());
        }
        #endregion

        #region Lecture
        public byte ReadByte()
        {
            ++_indexRead;
            return _bytes[_indexRead];
        }
        public bool ReadBool()
        {
            return ReadByte() == 0x01 ? true : false;
        }
        public string ReadUTF()
        {
            int length = ReadUShort();
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < length; ++i) ret.Append( (char)ReadByte() );
            return ret.ToString();
        }
        public ushort ReadUShort()
        {
            byte[] array = new byte[sizeof(ushort)];
            for (int i = 0; i < sizeof(ushort); ++i) array[i] = ReadByte();
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }
        public short ReadShort()
        {
            byte[] array = new byte[sizeof(short)];
            for (int i = 0; i < sizeof(short); ++i) array[i] = ReadByte();
            Array.Reverse(array);
            return BitConverter.ToInt16(array, 0);
        }
        public int ReadInt()
        {
            byte[] array = new byte[sizeof(int)];
            for (int i = 0; i < sizeof(int); ++i) array[i] = ReadByte();
            Array.Reverse(array);
            return BitConverter.ToInt32(array, 0);
        }
        public long ReadLong()
        {
            byte[] array = new byte[sizeof(long)];
            for (int i = 0; i < sizeof(long); ++i) array[i] = ReadByte();
            Array.Reverse(array);
            return BitConverter.ToInt64(array, 0);
        }
        public double ReadDouble()
        {
            byte[] array = new byte[sizeof(double)];
            for (int i = 0; i < sizeof(double); ++i) array[i] = ReadByte();
            Array.Reverse(array);
            return BitConverter.ToDouble(array, 0);
        }
        public int ReadCell()
        {
            return (int)ReadShort() & 4095;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            foreach (byte B in _bytes) ret.Append(B.ToString("x2"));
            return ret.ToString();
        }
        public byte[] ToArray()
        {
            return _bytes.ToArray();
        }
    }
}

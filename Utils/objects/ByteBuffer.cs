using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Utils.Objects
{
    public class ByteBuffer : ByteList
    {
        public ByteBuffer()
        {
        }
        public ByteBuffer(byte[] value)
        {
            base._bytes.AddRange(value);
        }
        public ByteBuffer(byte[] value, int index, int count)
        {
            for (int i = index; i < count; ++i) base._bytes.Add(value[i]);
        }
        public ByteBuffer(List<byte> value)
        {
            base._bytes.AddRange(value);
        }

        public Packet Unpack()
        {
            ushort compute = ReadUShort();
            Packet p = new Packet( (PacketID)(compute >> 2) );

            int length = 0;
            switch (compute & 3)
            {
                case 1: length = ReadByte(); break;
                case 2: length = ReadUShort(); break;
                case 3: length = ((ReadByte() & 255) >> 16) + ((ReadByte() & 255) >> 8) + (ReadByte() & 255); break;
            }

            for (int i = 0; i < length; ++i) p.WriteByte(ReadByte());

            return p;
        }
    }
}

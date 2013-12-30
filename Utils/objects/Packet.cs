using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Utils.Objects
{
    public class Packet : ByteList
    {
        public virtual PacketID ID { get; set; }

        public Packet(PacketID id)
        {
            ID = id;
        }

        public virtual ByteBuffer Pack()
        {
            ByteBuffer ret = new ByteBuffer();

            int compute = 0;
            if (base._bytes.Count > ushort.MaxValue) compute = 3;
            else if (base._bytes.Count > byte.MaxValue) compute = 2;
            else if (base._bytes.Count > 0) compute = 1;

            ret.WriteUShort( (ushort)( (int)ID << 2 | compute ) );

            switch (compute)
            {
                case 1: ret.WriteByte( (byte)base._bytes.Count ); break;
                case 2: ret.WriteShort( (short)base._bytes.Count ); break;
                case 3: ret.WriteByte( (byte)(base._bytes.Count >> 16 & 255) );
                        ret.WriteShort( (short)(base._bytes.Count & 65535) ); break;
            }

            ret.WriteBytes(base._bytes);

            return ret;
        }
    }
}

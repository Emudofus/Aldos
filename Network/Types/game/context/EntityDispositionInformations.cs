using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.game.context
{
    public class EntityDispositionInformations
    {
        public static int ProtocolID { get { return 60; } }

        public int Cell { get; set; }
        public Directions Direction { get; set; }
        public bool Moving { get; set; }

        public EntityDispositionInformations(int cell, Directions direction)
        {
            Cell = cell;
            Direction = direction;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteShort((short)Cell);
            sender.WriteByte((byte)Direction);
        }
    }
}

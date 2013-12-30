using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.context
{
    public class EntityDispositionInformations
    {
        public static int ProtocolID { get { return 60; } }

        private int _mapId;
        public Global.Map Map
        {
            get { return Global.Map.FindById(_mapId); }
            set { _mapId = value.Id; }
        }

        public int Cell { get; set; }
        public int NextCell { get; set; }
        public Directions Direction { get; set; }
        public bool Moving { get; set; }

        public EntityDispositionInformations(int mapId, int cell, Directions direction)
        {
            _mapId = mapId;
            Cell = cell;
            NextCell = -1;
            Direction = direction;

            Moving = false;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteShort((short)Cell);
            sender.WriteByte((byte)Direction);
        }
    }
}

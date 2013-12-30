using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context
{
    class GameMapMovementRequestMessage : Utils.Objects.Packet
    {
        public List<int> Path { get; set; }
        public int TargetMap { get; set; }

        public GameMapMovementRequestMessage(List<int> path, int targetMap)
            : base(PacketType.GameMapMovementRequestMessage)
        {
            Path = path;
            TargetMap = targetMap;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Path, TargetMap);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, List<int> path, int targetMap)
        {
            sender.WriteShort( (short)path.Count );
            foreach (int cell in path)
                sender.WriteCell(cell);
            sender.WriteInt(targetMap);
        }

        public static GameMapMovementRequestMessage deserialize(Utils.Objects.Packet sender)
        {
            List<int> path = new List<int>();
            int nCells = sender.ReadShort();
            for (int i = 0; i < nCells; ++i)
                path.Add(sender.ReadCell());
            return new GameMapMovementRequestMessage(path, sender.ReadInt());
        }
    }
}

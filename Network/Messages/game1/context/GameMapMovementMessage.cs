using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context
{
    class GameMapMovementMessage : Utils.Objects.Packet
    {
        public List<int> Path { get; set; }
        public Global.Character Actor { get; set; }

        public GameMapMovementMessage(List<int> path, Global.Character actor)
            : base(PacketType.GameMapMovementMessage)
        {
            Path = path;
            Actor = actor;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Path, Actor);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, List<int> path, Global.Character actor)
        {
            sender.WriteShort( (short)path.Count );
            foreach (int cell in path)
                sender.WriteShort((short)cell);
            sender.WriteInt(actor.Guid);
        }

        /// <summary>
        /// NOT READY
        /// </summary>
        public static GameMapMovementMessage deserialize(Utils.Objects.Packet sender)
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay
{
    class ChangeMapMessage : Utils.Objects.Packet
    {
        public int TargetMap { get; set; }

        public ChangeMapMessage(int target)
            : base(PacketType.ChangeMapMessage)
        {
            TargetMap = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, TargetMap);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int target)
        {
            sender.WriteInt(target);
        }

        public static ChangeMapMessage deserialize(Utils.Objects.Packet sender)
        {
            return new ChangeMapMessage(sender.ReadInt());
        }
    }
}

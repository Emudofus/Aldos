using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendDeleteRequestMessage : Utils.Objects.Packet
    {
        public string Target { get; set; }

        public FriendDeleteRequestMessage(string target)
            : base(PacketType.FriendDeleteRequestMessage)
        {
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string target)
        {
            sender.WriteUTF(target);
        }

        public static FriendDeleteRequestMessage deserialize(Utils.Objects.Packet sender)
        {
            return new FriendDeleteRequestMessage(sender.ReadUTF());
        }
    }
}

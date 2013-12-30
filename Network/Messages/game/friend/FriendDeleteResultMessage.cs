using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendDeleteResultMessage : Utils.Objects.Packet
    {
        public string Target { get; set; }
        public bool Success { get; set; }

        public FriendDeleteResultMessage(string target, bool success)
            : base(PacketID.FriendDeleteResultMessage)
        {
            Target = target;
            Success = success;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target, Success);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string target, bool success)
        {
            sender.WriteBool(success);
            sender.WriteUTF(target);
        }
    }
}

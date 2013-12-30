using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat
{
    class ChatServerCopyMessage : Utils.Objects.Packet
    {
        public Channel Channel { get; set; }
        public string Message { get; set; }
        public Global.Character Target { get; set; }

        public ChatServerCopyMessage(Channel chan, string message, Global.Character target)
            : base(PacketType.ChatServerCopyMessage)
        {
            Channel = chan;
            Message = message;
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Channel, Message, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Channel chan, string message, Global.Character target)
        {
            sender.WriteByte((byte)chan);
            sender.WriteUTF(message);
            sender.WriteInt(Global.Environnement.getInstance().Timestamp);
            sender.WriteUTF("abcdef"); // fingerprint ???
            sender.WriteInt(target.Guid);
            sender.WriteUTF(target.Name);
        }
    }
}

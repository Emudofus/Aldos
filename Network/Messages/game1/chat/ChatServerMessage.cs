using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat
{
    class ChatServerMessage : Utils.Objects.Packet
    {
        public Channel Channel { get; set; }
        public string Message { get; set; }
        public Global.Character Actor { get; set; }

        public ChatServerMessage(Channel chan, string message, Global.Character actor)
            : base(PacketType.ChatServerMessage)
        {
            Channel = chan;
            Message = message;
            Actor = actor;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Channel, Message, Actor);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Channel chan, string message, Global.Character actor)
        {
            sender.WriteByte((byte)chan);
            sender.WriteUTF(message);
            sender.WriteInt(Global.Environnement.getInstance().Timestamp);
            sender.WriteUTF("abcdef");
            sender.WriteInt(actor.Guid);
            sender.WriteUTF(actor.Name);
            sender.WriteInt(actor.AccountID);
        }
    }
}

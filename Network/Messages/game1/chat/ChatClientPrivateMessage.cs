using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat
{
    class ChatClientPrivateMessage : Utils.Objects.Packet
    {
        public string Message { get; set; }
        public Global.Character Target { get; set; }

        public ChatClientPrivateMessage(string message, Global.Character target)
            : base(PacketType.ChatClientPrivateMessage)
        {
            Message = message;
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Message, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string message, Global.Character target)
        {
            sender.WriteUTF(message);
            sender.WriteUTF(target.Name);
        }

        public static ChatClientPrivateMessage deserialize(Utils.Objects.Packet sender)
        {
            return new ChatClientPrivateMessage
                (
                    sender.ReadUTF(),
                    Global.Environnement.getInstance().GetCharacterByName( sender.ReadUTF() )
                );
        }
    }
}

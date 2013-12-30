using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.fight
{
    class GameFightStartingMessage : Utils.Objects.Packet
    {
        public Enums.FightTypeEnum Type { get; set; }

        public GameFightStartingMessage(Enums.FightTypeEnum type)
            : base(PacketID.GameFightStartingMessage)
        {
            Type = type;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Type);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.FightTypeEnum type)
        {
            sender.WriteByte((byte)type);
        }
    }
}

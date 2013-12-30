using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character.stats
{
    public class LifePointsRegenBeginMessage : Utils.Objects.Packet
    {
        public LifePointsRegenBeginMessage()
            : base(PacketType.LifePointsRegenBeginMessage)
        {
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteByte(100); // regen rate
        }

        /// <summary>
        /// NOT READY
        /// </summary>
        public static LifePointsRegenBeginMessage deserialize(Utils.Objects.Packet sender)
        {
            return null;
        }
    }
}

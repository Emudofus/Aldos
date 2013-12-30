using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.notification
{
    class NotificationListMessage : Utils.Objects.Packet
    {
        public NotificationListMessage()
            : base(PacketType.NotificationListMessage)
        {
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender)
        {
            int nFlags = 0;
            sender.WriteShort((short)nFlags);
            for (int i = 0; i < nFlags; ++i)
                sender.WriteInt(0);
        }
    }
}

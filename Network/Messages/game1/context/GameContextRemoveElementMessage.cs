using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context
{
    class GameContextRemoveElementMessage : Utils.Objects.Packet
    {
        public Global.Character Target { get; set; }

        public GameContextRemoveElementMessage(Global.Character target)
            : base(PacketType.GameContextRemoveElementMessage)
        {
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Character target)
        {
            sender.WriteInt(target.Guid);
        }
    }
}

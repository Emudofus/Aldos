using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay
{
    class GameRolePlayShowActorMessage : Utils.Objects.Packet
    {
        public Global.IActor Target { get; set; }

        public GameRolePlayShowActorMessage(Global.IActor target)
            : base(PacketID.GameRolePlayShowActorMessage)
        {
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.IActor target)
        {
            sender.WriteShort( (short)Types.context.GameRolePlayActorInformations.ProtocolID );
            new Types.context.GameRolePlayActorInformations(target).serialize(sender);
        }
    }
}

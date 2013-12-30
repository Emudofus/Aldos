using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay
{
    class GameRolePlayShowActorMessage : Utils.Objects.Packet
    {
        public Global.Character Target { get; set; }

        public GameRolePlayShowActorMessage(Global.Character target)
            : base(PacketType.GameRolePlayShowActorMessage)
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
            sender.WriteShort( (short)Types.game.context.GameRolePlayActorInformations.ProtocolID );
            new Types.game.context.GameRolePlayActorInformations(target).serialize(sender);
        }
    }
}

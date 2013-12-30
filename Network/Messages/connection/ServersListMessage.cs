using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class ServersListMessage : Utils.Objects.Packet
    {
        public Global.Account Account { get; set; }

        public ServersListMessage(Global.Account acc)
            : base(PacketID.ServersListMessage)
        {
            Account = acc;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Account);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Account acc)
        {
            sender.WriteShort(1); // nGameServers

            sender.WriteUShort((ushort)GlobalConfig.Network.Game.Guid);
            sender.WriteByte((byte)ServerState.ONLINE);
            sender.WriteByte(0); // completion
            sender.WriteBool(true); // selectable
            sender.WriteByte( (byte)acc.Characters.Count );
        }

        /// <summary>
        /// NOT READY
        /// </summary>
        public static ServersListMessage deserialize(Utils.Objects.Packet sender)
        {
            return null;
        }
    }
}
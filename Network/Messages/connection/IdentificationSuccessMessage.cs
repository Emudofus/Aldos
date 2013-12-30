using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class IdentificationSuccessMessage : Utils.Objects.Packet
    {
        public Global.Account Account { get; set; }

        public IdentificationSuccessMessage(Global.Account acc)
            : base(PacketID.IdentificationSuccessMessage)
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
            int wrapper = 0;
            Utils.BooleanByteWrapper.setFlag(ref wrapper, 0, (int)acc.GmLvl > 0);
            Utils.BooleanByteWrapper.setFlag(ref wrapper, 1, acc.Connected);
            sender.WriteByte( (byte)wrapper );
            sender.WriteUTF("[" + acc.GmLvl + "] " + acc.Nickname);
            sender.WriteInt((int)acc.Id);
            sender.WriteByte(0); // communityID
            sender.WriteUTF(""); // question
            sender.WriteDouble(31536000000); // aboTime (un an en millisecondes)
        }
        /// <summary>
        /// NOT READY
        /// </summary>
        public static IdentificationSuccessMessage deserialize(Utils.Objects.Packet sender)
        {
            return null;
        }
    }
}
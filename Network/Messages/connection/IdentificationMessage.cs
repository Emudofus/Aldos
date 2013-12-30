using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class IdentificationMessage : Utils.Objects.Packet
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public bool AutoConnect { get; set; }

        public IdentificationMessage(string login, string hashedPasswd, bool autoConnect)
            : base(PacketID.IdentificationMessage)
        {
            Login = login;
            HashedPassword = hashedPasswd;
            AutoConnect = autoConnect;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Login, HashedPassword, AutoConnect);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string login, string hashedPasswd, bool autoConnect)
        {
            Version.serialize(sender);
            sender.WriteUTF(login);
            sender.WriteUTF(hashedPasswd);
            sender.WriteBool(autoConnect);
        }
        public static IdentificationMessage deserialize(Utils.Objects.Packet sender)
        {
            Version.deserialize(sender);
            return new IdentificationMessage(sender.ReadUTF(), sender.ReadUTF(), sender.ReadBool());
        }
    }
}
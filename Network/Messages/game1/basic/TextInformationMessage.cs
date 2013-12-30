using System;
namespace Aldos.Network.Messages.game.basic
{
	public class TextInformationMessage : Utils.Objects.Packet
	{
		public Enums.TextInformationType Type { get; set; }
		
		public string Message { get; set; }
		
		public TextInformationMessage (Enums.TextInformationType type, string message)
			: base(PacketType.TextInformationMessage)
		{
			Type = type;
			Message = message;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Type, 0, Message);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, Enums.TextInformationType type, int msgId, string message)
		{
			sender.WriteByte((byte)type);
			sender.WriteShort((byte)msgId);

            if (message != string.Empty)
            {
                sender.WriteShort(1); // nMessages
                sender.WriteUTF(message);
            }
            else
                sender.WriteShort(0); // nMessages
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static TextInformationMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}


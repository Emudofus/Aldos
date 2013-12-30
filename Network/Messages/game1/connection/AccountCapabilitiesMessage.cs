using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.connection
{
	public class AccountCapabilitiesMessage : Utils.Objects.Packet
	{
		public int AccountID { get; set; }
		
		public AccountCapabilitiesMessage(int accountId)
		{
			base.ID = PacketType.AccountCapabilitiesMessage;
			
			AccountID = accountId;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, AccountID);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, int accountId)
		{
			sender.WriteInt(accountId);
			sender.WriteBool(false); // tutorial available
			sender.WriteShort(8191); // breeds visible
			sender.WriteShort(2047); // breeds available
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static AccountCapabilitiesMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}


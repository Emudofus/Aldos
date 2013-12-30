using System;
namespace Aldos.Network.Messages.game.chat.channel
{
	public class EnabledChannelsMessage : Utils.Objects.Packet
	{
		public List<Channel> Enabled {
			get;
			set;
		}
		public List<Channel> Disabled {
			get;
			set;
		}
		
		public EnabledChannelsMessage (List<Channel> enabled, List<Channel> disabled)
			: base(PacketType.EnabledChannelsMessage)
		{
			Enabled = enabled;
			Disabled = disabled;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Enabled, Disabled);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, List<Channel> enabled, List<Channel> disabled)
		{
			sender.WriteShort( (short)enabled.Count );
			foreach (Channel chan in enabled)
				sender.WriteByte( (byte)chan );
			
			sender.WriteShort( (short)disabled.Count );
			foreach (Channel chan in disabled)
				sender.WriteByte( (byte)chan );
		}
		
		public static EnabledChannelsMessage deserialize(Utils.Objects.Packet sender)
		{
			int nEnabled = sender.ReadShort();
			List<Channel> enabled = new List<Channel>();
			for (int i = 0; i < nEnabled; ++i)
				enabled.Add( (Channel)sender.ReadByte() );
			
			int nDisabled = sender.ReadShort();
			List<Channel> disabled = new List<Channel>();
			for (int i = 0; i < nDisabled; ++i)
				disabled.Add( (Channel)sender.ReadByte() );
			
			return new EnabledChannelsMessage(enabled, disabled);
		}
	}
}


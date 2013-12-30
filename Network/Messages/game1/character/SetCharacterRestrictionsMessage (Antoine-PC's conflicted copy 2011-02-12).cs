using System;
namespace Aldos.Network.Messages.game.character
{
	public class SetCharacterRestrictionsMessage : Utils.Objects.Packet
	{
		/// <summary>
		/// TODO
		/// </summary>
		public SetCharacterRestrictionsMessage ()
			: base(PacketType.SetCharacterRestrictionsMessage)
		{
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender)
		{
            int wrapper1 = 0;
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 0, true); // cantBeAggressed
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 1, true); // cantBeChallenged
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 2, true); // cantTrade
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 3, true); // cantBeAttackedByMutant
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 4, false); // cantRun
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 5, false); // forceSlowWalk
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 6, true); // cantMinimize
            Utils.BooleanByteWrapper.setFlag(ref wrapper1, 7, false); // cantMove
            sender.WriteByte(wrapper1);
            int wrapper2 = 0;
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 0, true); // cantAggress
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 1, true); // cantChallenge
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 2, true); // cantExchange
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 3, true); // cantAttack
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 4, false); // cantChat
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 5, true); // cantBeMerchant
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 6, false); // cantUseObject
            Utils.BooleanByteWrapper.setFlag(ref wrapper2, 7, true); // cantUseTaxCollector
            sender.WriteByte(wrapper2);
            int wrapper3 = 0;
            Utils.BooleanByteWrapper.setFlag(ref wrapper3, 0, false); // cantUseInteractive
            Utils.BooleanByteWrapper.setFlag(ref wrapper3, 1, false); // cantSpeakToNPC
            Utils.BooleanByteWrapper.setFlag(ref wrapper3, 2, false); // cantChangeZone
            Utils.BooleanByteWrapper.setFlag(ref wrapper3, 3, false); // cantAttackMonster
            Utils.BooleanByteWrapper.setFlag(ref wrapper3, 4, false); // cantWalk8Directions
            sender.WriteByte(wrapper3);
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static SetCharacterRestrictionsMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}


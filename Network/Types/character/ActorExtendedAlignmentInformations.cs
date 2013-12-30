using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.character
{
    public class ActorExtendedAlignmentInformations
    {
        public static int ProtocolID { get { return 202; } }

        public int alignmentSide = 0;
        public int alignmentValue = 0;
        public int alignmentGrade = 0;
        public int dishonor = 0;
        public int characterPower = 0;
        public int honor = 0;
        public int honorGradeFloor = 0;
        public int honorNextGradeFloor = 0;
        public bool pvpEnabled = false;

        public ActorExtendedAlignmentInformations()
        { }

        public ActorExtendedAlignmentInformations
            (int aAlignementSide, int aAlignmentValue, int aAlignmentGrade, int aDishonor, int aCharacterPower,
            int aHonor, int aHonorGradeFloor, int aHonorNextGradeFloor, bool aPvpEnabled)
        {
            alignmentSide       = aAlignementSide;
            alignmentValue      = aAlignmentValue;
            alignmentGrade      = aAlignmentGrade;
            dishonor            = aDishonor;
            characterPower      = aCharacterPower;
            honor               = aHonor;
            honorGradeFloor     = aHonorGradeFloor;
            honorNextGradeFloor = aHonorNextGradeFloor;
            pvpEnabled          = aPvpEnabled;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteByte((byte)alignmentSide);
            sender.WriteByte((byte)alignmentValue);
            sender.WriteByte((byte)alignmentGrade);
            sender.WriteShort((short)dishonor);
            sender.WriteInt(characterPower);

            sender.WriteShort((short)honor);
            sender.WriteShort((short)honorGradeFloor);
            sender.WriteShort((short)honorNextGradeFloor);
            sender.WriteBool(pvpEnabled);
        }
    }
}

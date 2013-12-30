using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.game.character
{
    public class CharacterCharacteristicsInformations
    {
        public static int ProtocolID { get { return 8; } }

        public double experience = 0;
        public double experienceLevelFloor = 0;
        public double experienceNextLevelFloor = 0;
        public int kamas = 0;
        public int statsPoints = 0;
        public int spellsPoints = 0;
        public ActorExtendedAlignmentInformations alignmentInfos = new ActorExtendedAlignmentInformations();
        public int lifePoints = 0;
        public int maxLifePoints = 0;
        public int energyPoints = 0;
        public int maxEnergyPoints = 10000;
        public int actionPointsCurrent = 0;
        public int movementPointsCurrent = 3;
        public CharacterBaseCharacteristic initiative;
        public CharacterBaseCharacteristic prospecting;
        public CharacterBaseCharacteristic actionPoints = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic movementPoints = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic strength = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic vitality = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic wisdom = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic chance = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic agility = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic intelligence = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic range = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic summonableCreaturesBoost = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic reflect = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic criticalHit = new CharacterBaseCharacteristic();
        public int criticalHitWeapon = 0;
        public CharacterBaseCharacteristic criticalMiss = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic healBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic allDamagesBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic weaponDamagesBonusPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic damagesBonusPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic trapBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic trapBonusPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic permanentDamagePercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic tackleBlock = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic tackleEvade = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic PAAttack = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic PMAttack = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pushDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic criticalDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic neutralDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic earthDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic waterDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic airDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic fireDamageBonus = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic dodgePALostProbability = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic dodgePMLostProbability = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic neutralElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic earthElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic waterElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic airElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic fireElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic neutralElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic earthElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic waterElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic airElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic fireElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pushDamageReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic criticalDamageReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpNeutralElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpEarthElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpWaterElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpAirElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpFireElementResistPercent = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpNeutralElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpEarthElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpWaterElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpAirElementReduction = new CharacterBaseCharacteristic();
        public CharacterBaseCharacteristic pvpFireElementReduction = new CharacterBaseCharacteristic();
        //public int spellModifications:Vector.<CharacterSpellModification>;

        public CharacterCharacteristicsInformations(int level, Classe breed,
                                                    int aStatsPoints, int aSpellsPoints, int aKamas, int aEnergy,
                                                    int aVitality, int aWisdom, int aStrength, int aIntelligence,
                                                    int aChance, int aAgility)
        {
            lifePoints = (breed == Classe.Sacrieur ? 60 : 50) + (level * 5);
            maxLifePoints = (breed == Classe.Sacrieur ? 60 : 50) + (level * 5);

            statsPoints = aStatsPoints;
            spellsPoints = aSpellsPoints;
            kamas = aKamas;
            energyPoints = aEnergy;
            vitality = new CharacterBaseCharacteristic(aVitality, 0, 0, 0);
            wisdom = new CharacterBaseCharacteristic(aWisdom, 0, 0, 0);
            strength = new CharacterBaseCharacteristic(aStrength, 0, 0, 0);
            intelligence = new CharacterBaseCharacteristic(aIntelligence, 0, 0, 0);
            chance = new CharacterBaseCharacteristic(aChance, 0, 0, 0);
            agility = new CharacterBaseCharacteristic(aAgility, 0, 0, 0);

            actionPointsCurrent = level > 99 ? 7 : 6;
            prospecting = new CharacterBaseCharacteristic(breed == Classe.Enutrof ? 120 : 100, 0, 0, 0);
            initiative = new CharacterBaseCharacteristic((strength.Base + intelligence.Base + chance.Base + agility.Base) * (lifePoints / maxLifePoints), 0, 0, 0);
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteDouble(experience);
            sender.WriteDouble(experienceLevelFloor);
            sender.WriteDouble(experienceNextLevelFloor);
            sender.WriteInt(kamas);
            sender.WriteInt(statsPoints);
            sender.WriteInt(spellsPoints);
            alignmentInfos.serialize(sender);
            sender.WriteInt(lifePoints);
            sender.WriteInt(maxLifePoints);
            sender.WriteShort((short)energyPoints);
            sender.WriteShort((short)maxEnergyPoints);
            sender.WriteShort((short)actionPointsCurrent);
            sender.WriteShort((short)movementPointsCurrent);
            initiative.serialize(sender);
            prospecting.serialize(sender);
            actionPoints.serialize(sender);
            movementPoints.serialize(sender);
            strength.serialize(sender);
            vitality.serialize(sender);
            wisdom.serialize(sender);
            chance.serialize(sender);
            agility.serialize(sender);
            intelligence.serialize(sender);
            range.serialize(sender);
            summonableCreaturesBoost.serialize(sender);
            reflect.serialize(sender);
            criticalHit.serialize(sender);
            sender.WriteShort((short)criticalHitWeapon);
            criticalMiss.serialize(sender);
            healBonus.serialize(sender);
            allDamagesBonus.serialize(sender);
            weaponDamagesBonusPercent.serialize(sender);
            damagesBonusPercent.serialize(sender);
            trapBonus.serialize(sender);
            trapBonusPercent.serialize(sender);
            permanentDamagePercent.serialize(sender);
            tackleBlock.serialize(sender);
            tackleEvade.serialize(sender);
            PAAttack.serialize(sender);
            PMAttack.serialize(sender);
            pushDamageBonus.serialize(sender);
            criticalDamageBonus.serialize(sender);
            neutralDamageBonus.serialize(sender);
            earthDamageBonus.serialize(sender);
            waterDamageBonus.serialize(sender);
            airDamageBonus.serialize(sender);
            fireDamageBonus.serialize(sender);
            dodgePALostProbability.serialize(sender);
            dodgePMLostProbability.serialize(sender);
            neutralElementResistPercent.serialize(sender);
            earthElementResistPercent.serialize(sender);
            waterElementResistPercent.serialize(sender);
            airElementResistPercent.serialize(sender);
            fireElementResistPercent.serialize(sender);
            neutralElementReduction.serialize(sender);
            earthElementReduction.serialize(sender);
            waterElementReduction.serialize(sender);
            airElementReduction.serialize(sender);
            fireElementReduction.serialize(sender);
            pushDamageReduction.serialize(sender);
            criticalDamageReduction.serialize(sender);
            pvpNeutralElementResistPercent.serialize(sender);
            pvpEarthElementResistPercent.serialize(sender);
            pvpWaterElementResistPercent.serialize(sender);
            pvpAirElementResistPercent.serialize(sender);
            pvpFireElementResistPercent.serialize(sender);
            pvpNeutralElementReduction.serialize(sender);
            pvpEarthElementReduction.serialize(sender);
            pvpWaterElementReduction.serialize(sender);
            pvpAirElementReduction.serialize(sender);
            pvpFireElementReduction.serialize(sender);

            sender.WriteShort(0); // nSpellModifications
        }
    }
}

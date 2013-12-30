using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos
{
    public enum ConsoleType
    {
        Info,
        Error,
        Connect,
        Disconnect,
        Receive,
        Send
    }
    
    public enum ConsoleWriter
    {
        Realm,
        Game,
        Unknown
    }

    public enum ServerState
    {
        STATUS_UNKNOWN = 0,
        OFFLINE = 1,
        STARTING = 2,
        ONLINE = 3,
        NOJOIN = 4,
        SAVING = 5,
        STOPING = 6,
        FULL = 7,
    }

    public enum GmLvl
    {
        Banned = -1,
        Member = 0,
        Moderator = 1,
        GameMaster = 2,
        Manager = 3,
        God = 4
    }

    public enum Breed
    {
        Feca = 1,
        Osamodas = 2,
        Enutrof = 3,
        Sram = 4,
        Xelor = 5,
        Ecaflip = 6,
        Eniripsa = 7,
        Iop = 8,
        Cra = 9,
        Sadida = 10,
        Sacrieur = 11,
        Pandawa = 12,
        Roublard = 13,
        Zobal = 14,
    }

    public enum Gender
    {
        Female = 0,
        Male = 1,
    }
	
	public enum Channel
	{
		GLOBAL = 0,
        TEAM = 1,
        GUILD = 2,
        ALIGN = 3,
        PARTY = 4,
        SALES = 5,
        SEEK = 6,
        NOOB = 7,
        ADMIN = 8,
        ADS = 12,
        PSEUDO_PRIVATE = 9,
        PSEUDO_INFO = 10,
        PSEUDO_FIGHT_LOG = 11,
		
		_UNKNOWN_ = -1,
	}
	
	public enum Directions
	{
		EAST = 0,
        SOUTH_EAST = 1,
        SOUTH = 2,
        SOUTH_WEST = 3,
        WEST = 4,
        NORTH_WEST = 5,
        NORTH = 6,
        NORTH_EAST = 7,
	}

    public enum GameEventArgsType
    {
        PartyAvailable,
        PartyUpdated,
        PartyLeaderUpdated,

        MapActorUpdated,
        MapActorMoved,

        ActorSpeaked,
    }

    public enum FriendType
    {
        Friend = 0,
        Ennemy = 1,
    }

    public enum PacketID
    {
        ProtocolRequired = 1,
		
		#region   REALM  
        HelloConnectMessage = 3,
        IdentificationMessage = 4,
        LoginQueueStatusMessage = 10,
        Version = 11,
        IdentificationFailedMessage = 20,
        IdentificationSuccessMessage = 22,
        ServersListMessage = 30,
        ServerSelectionMessage = 40,
        SelectedServerDataMessage = 42,
        IdentificationWithServerIdMessage = 6194,
		#endregion

        ConsoleMessage = 75,
        AdminCommandMessage = 76,
		
		HelloGameMessage = 101,
		AlreadyConnectedMessage = 109,
		AuthenticationTicketMessage = 110,
        AuthenticationTicketAcceptedMessage = 111,
        AuthenticationTicketRefusedMessage = 112,
		CharactersListRequestMessage = 150,
		CharactersListMessage = 151,
		CharacterSelectionMessage = 152,
		CharacterSelectedSuccessMessage = 153,
		CharacterSelectedErrorMessage = 5836,
        CharacterCreationRequestMessage = 160,
        CharacterCreationResultMessage = 161,
        CharacterNameSuggestionRequestMessage = 162,
        CharacterNameSuggestionSuccessMessage = 5544,
        CharacterDeletionRequestMessage = 165,
        CharacterDeletionErrorMessage = 166,
		SetCharacterRestrictionsMessage = 170,
		BasicTimeMessage = 175,
		BasicNoOperationMessage = 176,
        GameContextCreateMessage = 200,
		GameContextDestroyMessage = 201,
		CurrentMapMessage = 220,
        ChangeMapMessage = 221,
		MapInformationsRequestMessage = 225,
		MapComplementaryInformationsDataMessage = 226,
		GameContextCreateRequestMessage = 250,
        GameContextRemoveElementMessage = 251,
		CharacterStatsListMessage = 500,
        GameFightStartingMessage = 700,
        GameFightJoinMessage = 702,
        GameFightPlacementPossiblePositionsMessage = 703,
		TextInformationMessage = 780,
        ChatAbstractClientMessage = 850,
        ChatClientPrivateMessage = 851,
        ChatClientMultiMessage = 861,
        ChatErrorMessage = 870,
        ChatServerMessage = 881,
        ChatServerCopyMessage = 882,
		ChannelEnablingMessage = 890,
		ChannelEnablingChangeMessage = 891,
		EnabledChannelsMessage = 892,
        GameMapMovementRequestMessage = 950,
        GameMapMovementMessage = 951,
        GameMapMovementConfirmMessage = 952,
        GameMapNoMovementMessage = 954,
		SpellListMessage = 1200,
		InventoryWeightMessage = 3009,
		InventoryContentMessage = 3016,
		FriendsGetListMessage = 4001,
		FriendsListMessage = 4002,
        FriendAddRequestMessage = 4004,
        PartyUpdateMessage = 5575,
        PartyJoinMessage = 5576,
        PartyLeaderUpdateMessage = 5578,
        PartyMemberRemoveMessage = 5579,
        PartyAcceptInvitationMessage = 5580,
        PartyRefuseInvitationMessage = 5582,
        PartyCannotJoinErrorMessage = 5583,
        PartyInvitationRequestMessage = 5585,
        PartyInvitationMessage = 5586,
        PartyKickedByMessage = 5590,
        PartyKickRequestMessage = 5592,
        PartyLeaveRequestMessage = 5593,
        PartyLeaveMessage = 5594,
        FriendAddedMessage = 5599,
        FriendAddFailureMessage = 5600,
        FriendDeleteResultMessage = 5601,
        FriendDeleteRequestMessage = 5603,
		ClientKeyMessage = 5607,
		QuestListRequestMessage = 5623,
		QuestListMessage = 5626,
		FriendWarnOnConnectionStateMessage = 5630,
        GameRolePlayShowActorMessage = 5632,
        BasicLatencyStatsMessage = 5663,
        IgnoredListMessage = 5674,
        IgnoredGetListMessage = 5676,
        LifePointsRegenBeginMessage = 5684,
        EmotePlayErrorMessage = 5688,
        BasicLatencyStatsRequestMessage = 5816,
        FriendUpdateMessage = 5924,
		AlignmentRankUpdateMessage = 6058,
		AlignmentSubAreasListMessage = 6059,
		FriendWarnOnLevelGainStateMessage = 6078,
        PartyAbdicateThroneMessage = 6080,
        NotificationListMessage = 6087,
		GuildMemberWarnOnConnectionStateMessage = 6160,
		AccountCapabilitiesMessage = 6216,
    }
}

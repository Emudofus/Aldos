using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Aldos.Network.Messages.console;
using Aldos.Network.Messages.game.connection;
using Aldos.Network.Messages.game.basic;
using Aldos.Network.Messages.game.character;
using Aldos.Network.Messages.game.character.stats;
using Aldos.Network.Messages.game.inventory;
using Aldos.Network.Messages.game.inventory.spells;
using Aldos.Network.Messages.game.chat;
using Aldos.Network.Messages.game.chat.channel;
using Aldos.Network.Messages.game.context;
using Aldos.Network.Messages.game.context.roleplay;
using Aldos.Network.Messages.game.context.roleplay.party;
using Aldos.Network.Messages.game.context.notification;
using Aldos.Network.Messages.game.context.fight;
using Aldos.Network.Messages.game.friend;

using Aldos.Global;

namespace Aldos.Game
{
    class Parser
    {
        private Client _client;
        private Account _account;
        private Character _character;
        private DateTime _lastWords;

        public Parser(Client client)
        {
            _client = client;
            _client.Disconnected += new Client.DisconnectedEventHandler(OnClientDisconnected);

            _client.Send(new Utils.Objects.Packet(PacketID.HelloGameMessage));
        }

        private void OnClientDisconnected(Client sender)
        {
            if (_account != null)
            {
                if (_character != null)
                {
                    _character.Disposition.Map.Updated -= ParseEvent;
                    Environment.Instance.ActorSpeaked -= ParseEvent;

                    _character.Disposition.Map.RemoveActor(_character);
                    _character.State = Network.Enums.PlayerStateEnum.NOT_CONNECTED;
                    _character.Client = null;
                    _character.PrivateMessageReceived = null;
                    _character.LastUse = Environment.Instance.Now;
                }

                _account.Connected = false;
                _account.CurrentCharacter = null;
                _account.LastIp = _client.IP;
            }

            _client.Disconnected -= OnClientDisconnected;
        }

        #region HOME
        private void Parse_AuthenticationTicketMessage(AuthenticationTicketMessage message)
        {
            _account = Environment.Instance.GetWaitingAccount(message.Ticket);

            if (_account == null)
            {
                _client.Send(new Utils.Objects.Packet(PacketID.AuthenticationTicketRefusedMessage));
                return;
            }

            if (_account.Connected)
            {
                _client.Send(new Utils.Objects.Packet(PacketID.AlreadyConnectedMessage));
                return;
            }

            _account.Connected = true;
            _client.Send(new Utils.Objects.Packet(PacketID.AuthenticationTicketAcceptedMessage));
            _client.Send(new BasicTimeMessage());
            _client.Send(new AccountCapabilitiesMessage( (int)_account.Id ));
            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_CharactersListRequestMessage()
        {
            try
            {
                _client.Send(new CharactersListMessage(_account.Characters));
                _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
            }
            catch (Exception ex)
            {
                Utils.MyConsole.WriteLine
                    (
                        ex.Message + "\n" + ex.StackTrace,
                        ConsoleType.Error, ConsoleWriter.Game
                    );
            }
        }

        private void Parse_CharacterNameSuggestionRequestMessage()
        {
            _client.Send(new CharacterNameSuggestionSuccessMessage());
            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_CharacterCreationRequestMessage(CharacterCreationRequestMessage message)
        {
            if (_account.Characters.Count >= 6)
            {
                _client.Send(new CharacterCreationResultMessage(Network.Enums.CharacterCreationResult.TOO_MANY_CHARACTERS));
                return;
            }

            if (Character.FindOne(chr => chr.Name == (string)message.CharacterInfos[0]) != null)
            {
                _client.Send(new CharacterCreationResultMessage(Network.Enums.CharacterCreationResult.NAME_ALREADY_EXISTS));
                return;
            }

            _character = Character.New
                (
                    _account,
                    (string)message.CharacterInfos[0],
                    (Gender)message.CharacterInfos[2],
                    (Breed)message.CharacterInfos[1],
                    (List<int>)message.CharacterInfos[3]
                );

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
            _client.Send(new CharacterCreationResultMessage(Network.Enums.CharacterCreationResult.OK));
            _client.Send(new CharactersListMessage(_account.Characters.ToList()));
        }

        private void Parse_CharacterDeletionRequestMessage(CharacterDeletionRequestMessage message)
        {
            Global.Character character = _account.Characters.Where(chr => chr.Id == message.CharacterID).FirstOrDefault();

            if (character == null)
            {
                _client.Send(new CharacterDeletionErrorMessage(Network.Enums.CharacterDeletionErrorEnum.RESTRICED_ZONE));
                return;
            }

            _account.Characters.Remove(character);
            character.Delete();

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
            _client.Send(new CharactersListMessage(_account.Characters.ToList()));
        }

        private void Parse_CharacterSelectionMessage(CharacterSelectionMessage message)
        {
            Character chosen = _account.Characters.Where(chr => chr.Id == message.Chosen).First();

            if (chosen == null)
            {
                _client.Send(new Utils.Objects.Packet(PacketID.CharacterSelectedErrorMessage));
                return;
            }

            _character = chosen;
            _character.PrivateMessageReceived = ParseEvent;
            _character.Client = _client;
            _account.CurrentCharacter = _character;
            Environment.Instance.ActorSpeaked += ParseEvent;

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
            _client.Send(new CharacterSelectedSuccessMessage(_character));
            _client.Send(new InventoryContentMessage(_character));
            _client.Send(new InventoryWeightMessage());
            _client.Send(new FriendsListMessage(_account.Friends));
            _client.Send(new IgnoredListMessage());
            _client.Send(new EnabledChannelsMessage(GlobalConfig.Network.Game.DefaultEnabledChannels,
                                                    GlobalConfig.Network.Game.DefaultDisabledChannels));
            _client.Send(new SpellListMessage());
            _client.Send(new SetCharacterRestrictionsMessage());

            _client.Send(new TextInformationMessage(Network.Enums.TextInformationType.INFORMATION_MESSAGE,
                                                    GlobalConfig.Network.Game.HomeMessage));
            if (_account.LastIp != _client.IP)
                _client.Send(new SystemChatMessage("Dernière adresse IP utilisée : " + _account.LastIp + "."));
            _client.Send(new SystemChatMessage("Votre adresse IP actuelle : " + _client.IP + "."));
            _client.Send(new SystemChatMessage("Dernière connexion : " + Utils.Other.GetDate(_character.LastUse).ToString() + "."));
        }
        #endregion

        #region CONTEXT
        private void Parse_GameContextCreateRequestMessage()
        {
            _character.State = Network.Enums.PlayerStateEnum.GAME_TYPE_ROLEPLAY;

            _client.Send(new Utils.Objects.Packet(PacketID.GameContextDestroyMessage));
            _client.Send(new GameContextCreateMessage(Network.Enums.GameContextEnum.ROLE_PLAY));

            _client.Send(new CharacterStatsListMessage(_character));
            _client.Send(new LifePointsRegenBeginMessage());

            _client.Send(new CurrentMapMessage(_character.Disposition.Map.Id));
            _character.Disposition.Map.AddActor(_character);
            _character.Disposition.Map.Updated += ParseEvent;

            _client.Send(new BasicTimeMessage());
            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        #region MAP ACTIONS
        private void Parse_MapInformationsRequestMessage()
        {
            _client.Send(new MapComplementaryInformationsDataMessage(_character.Disposition.Map));
            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_GameMapMovementRequestMessage(GameMapMovementRequestMessage message)
        {
            if (message.TargetMap == _character.Disposition.Map.Id &&
                !_character.Disposition.Moving &&
                _character.Disposition.NextCell < 1)
            {
                _character.Disposition.Map.OnActorMoved(_character, message.Path);
                _character.Disposition.NextCell = message.Path.Last();
                _character.Disposition.Moving = true;
            }
            else
                _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_GameMapMovementConfirmMessage()
        {
            _character.Disposition.Moving = false;
            _character.Disposition.Cell = _character.Disposition.NextCell;
            _character.Disposition.NextCell = -1;
            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_ChangeMapMessage(ChangeMapMessage message)
        {
            if (_character.Disposition.Map.TopNeighbour.Id == message.TargetMap)
            {
                _character.Disposition.NextCell = _character.Disposition.Cell + 532;
                ChangeMap(Map.FindById(message.TargetMap));
            }
            else if (_character.Disposition.Map.BottomNeighbour.Id == message.TargetMap)
            {
                _character.Disposition.NextCell = _character.Disposition.Cell - 532;
                ChangeMap(Map.FindById(message.TargetMap));
            }
            else if (_character.Disposition.Map.LeftNeighbour.Id == message.TargetMap)
            {
                _character.Disposition.NextCell = _character.Disposition.Cell + 13;
                ChangeMap(Map.FindById(message.TargetMap));
            }
            else if (_character.Disposition.Map.RightNeighbour.Id == message.TargetMap)
            {
                _character.Disposition.NextCell = _character.Disposition.Cell - 13;
                ChangeMap(Map.FindById(message.TargetMap));
            }
            else
                _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }
        #endregion

        #region CHAT
        private void Parse_ChatClientMultiMessage(ChatClientMultiMessage message)
        {
            if (message.Message[0].Equals('!'))
                ParseCommand(message.Message);

            else
            {
                switch (message.Channel)
                {
                    case Channel.GLOBAL:
                        _character.Disposition.Map.OnActorSpeaked(_character, message.Message); break;

                    case Channel.PARTY:
                        if (_character.Party != null)
                            _character.Party.OnMemberSpeaked(_character, message.Message);
                            
                            break;

                    default:
                        if (Environment.Instance.OnActorSpeaked(_character, message.Channel, message.Message, _lastWords))
                            _lastWords = DateTime.Now;
                        else
                            _client.Send(new ChatErrorMessage(Network.Enums.ChatErrorEnum.UNKNOWN));

                        break;
                }
            }
        }

        private void Parse_ChatClientPrivateMessage(ChatClientPrivateMessage message)
        {
            if (message.Target != null && message.Target.SendPrivateMessage(_character, message.Message))
            {
                _client.Send(new ChatServerCopyMessage(Channel.PSEUDO_PRIVATE, message.Message, message.Target));
                return;
            }

            _client.Send(new ChatErrorMessage(Network.Enums.ChatErrorEnum.RECEIVER_NOT_FOUND));
        }
        #endregion

        #region PARTY
        private void Parse_PartyInvitationRequestMessage(PartyInvitationRequestMessage message)
        {
            if (message.Target != null && message.Target.Client != null)
            {
                _character.Party = new Party(_character);
                _character.Party.Updated += ParseEvent;

                message.Target.Party = _character.Party;

                message.Target.Client.Send(new PartyInvitationMessage(_character, message.Target));
                _client.Send(new PartyInvitationMessage(_character, message.Target));
            }
            else
                _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_PartyAcceptInvitationMessage()
        {
            if (_character.Party != null)
            {
                if (!_character.Party.Available) _character.Party.Available = true;

                _character.Party.Updated += ParseEvent;
                _character.Party.AddMember(_character);
                _client.Send(new PartyJoinMessage(_character.Party));
            }
            
            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_PartyRefuseInvitationMessage()
        {
            if (_character.Party != null)
            {
                if (!_character.Party.Available) _character.Party.Available = false;

                _character.Party.Updated -= ParseEvent;
                _character.Party = null;
            }

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_PartyLeaveRequestMessage()
        {
            if (_character.Party != null)
            {
                _character.Party.Updated -= ParseEvent;
                _character.Party.RemoveMember(_character);
                _character.Party = null;

                _client.Send(new Utils.Objects.Packet(PacketID.PartyLeaveMessage));
            }

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_PartyAbdicateThroneMessage(PartyAbdicateThroneMessage message)
        {
            if (_character.Party != null)
            {
                Character newLeader = _character.Party.GetMember(chr => chr.Id == message.NewLeaderID);

                if (newLeader != null) _character.Party.Leader = newLeader;
            }

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_PartyKickRequestMessage(PartyKickRequestMessage message)
        {
            if (_character.Party != null)
            {
                Character toKick = _character.Party.GetMember(chr => chr.Id == message.KickedID);
                if (toKick != null) _character.Party.RemoveMember(toKick);
            }

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }
            #endregion

        #region FRIEND
        private void Parse_FriendsGetListMessage()
        {
            _client.Send(new FriendsListMessage(_account.Friends));
        }

        private void Parse_FriendAddRequestMessage(FriendAddRequestMessage message)
        {
            if (message.Name != _account.Nickname || message.Name != _character.Name)
            {
                Character toAddChr = Character.FindOne(chr => chr.Name == message.Name);

                if (toAddChr != null)
                    _client.Send(new FriendAddedMessage(_account.Friends.Add(toAddChr.Owner)));

                else
                {
                    Account toAddAcc = Account.FindOne(acc => acc.Nickname == message.Name);

                    if (toAddAcc != null)
                        _client.Send(new FriendAddedMessage(_account.Friends.Add(toAddAcc)));

                    else
                        _client.Send(new FriendAddFailureMessage(Network.Enums.ListAddFailureEnum.NOT_FOUND));
                }
            }
            else
                _client.Send(new FriendAddFailureMessage(Network.Enums.ListAddFailureEnum.EGOCENTRIC));

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void Parse_FriendDeleteRequestMessage(FriendDeleteRequestMessage message)
        {
            if (message.Target != _account.Nickname && message.Target != _character.Name)
            {
                var toRemove = _account.Friends.FindOne
                    (
                        acc => acc.Friend.Nickname == message.Target ||
                        acc.Friend.Characters.Where(chr => chr.Name == message.Target) != null
                    );
                _client.Send(new FriendDeleteResultMessage(message.Target, _account.Friends.Remove(toRemove)));
            }
            else
                _client.Send(new FriendDeleteResultMessage(message.Target, false));

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }
            #endregion
        #endregion

        #region CONSOLE
        private void Parse_AdminCommandMessage(AdminCommandMessage message)
        {
            if (_account.GmLvl > 0)
            {
                List<string> args = message.Message.Split((char)0x20).ToList();
                string command = args[0];
                args.RemoveAt(0);

                switch (command)
                {
                    case "kick":
                        if (args.Count != 1) return;
                        Character toKick = Character.FindOne(chr => chr.Name == args[0]);
                        if (toKick != null)
                        {
                            if (toKick.Client != null)
                            {
                                toKick.Client.Close();
                                return;
                            }

                            _client.Send(new ConsoleMessage(Network.Enums.ConsoleMessageTypeEnum.ERR_MESSAGE, "\"" + args[0] + "\" not found or not connected."));
                        }
                        else
                        {
                            Account toKickAcc = Account.FindOne(acc => acc.Nickname == args[0] || acc.Name == args[0]);
                            if (toKickAcc != null && toKickAcc.Connected)
                            {
                                toKickAcc.CurrentCharacter.Client.Close();
                                return;
                            }

                            _client.Send(new ConsoleMessage(Network.Enums.ConsoleMessageTypeEnum.ERR_MESSAGE, "\"" + args[0] + "\" not found or not connected."));
                        }
                        break;

                    case "announce":
                        Environment.Instance.OnActorSpeaked(_character, Channel.PSEUDO_INFO, string.Join(" ", args), DateTime.Now);
                        break;

                    case "pub":
                        Environment.Instance.OnActorSpeaked(_character, Channel.ADS, string.Join(" ", args), DateTime.Now);
                        break;

                    case "stop":
                        Program.Exit();
                        System.Environment.Exit(0);
                        break;

                    case "ram":
                        _client.Send(new ConsoleMessage(Network.Enums.ConsoleMessageTypeEnum.INFO_MESSAGE,
                            (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1000).ToString() + " ko"));
                        break;

                    default: _client.Send(new ConsoleMessage(Network.Enums.ConsoleMessageTypeEnum.ERR_MESSAGE, "Command not found.")); break;
                }
            }
        }
        #endregion

        #region HELPER
        private void ChangeMap(Map target)
        {
            if (target == null) return;

            _character.Disposition.Map.RemoveActor(_character);
            _character.Disposition.Map.Updated -= ParseEvent;

            _character.Disposition.Map = target;
            _character.Disposition.Cell = _character.Disposition.NextCell;
            _character.Disposition.NextCell = -1;

            _client.Send(new CurrentMapMessage(target.Id));

            _character.Disposition.Map.Updated += ParseEvent;
            _character.Disposition.Map.AddActor(_character);
        }

        private void ParseCommand(string command)
        {
            if (command.Contains("start"))
            {
                _character.Disposition.NextCell = GlobalConfig.Network.Game.StartCell;
                _character.Disposition.NextCell = -1;
                _character.Disposition.Moving = false;
                ChangeMap(Map.FindById(GlobalConfig.Network.Game.StartMap));
            }
            else if (command.Contains("version"))
            {
                _client.Send(new SystemChatMessage("Aldos rev.2 by Blackrush for Dofus 2 produced by Ankama Games."));
            }
            else if (command.Contains("test"))
            {
                _client.Send(new GameFightStartingMessage(Network.Enums.FightTypeEnum.PvM));
            }
        }
        #endregion

        #region EVENTS
        private void ParseEvent(Global.IActor actor, Utils.Objects.GameEventArgs args)
        {
            string name = "ParseEvent_" + args.GetType().ToString().Split('.').Last().Replace("Args", "");
            MethodInfo method = this.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method != null)
            {
                method.Invoke(this, new object[]{
                    actor, args
                });
            }
            else
                throw new Exception("Cannot invoke \"" + name + "\".");
        }

        private void ParseEvent_MapActorUpdated(Global.IActor actor, Global.MapActorUpdatedArgs args)
        {
            if (args.Type)
            {
                _client.Send(new GameRolePlayShowActorMessage(actor));
            }
            else
            {
                _client.Send(new GameContextRemoveElementMessage(actor));
            }
        }

        private void ParseEvent_MapActorMoved(Global.IActor actor, Global.MapActorMovedArgs args)
        {
            _client.Send(new GameMapMovementMessage(args.Path, actor));
        }

        private void ParseEvent_ActorSpeaked(Global.IActor actor, Global.ActorSpeakedArgs args)
        {
            _client.Send(new ChatServerMessage(args.Channel, args.Message, args.Actor));
        }

        private void ParseEvent_PartyAvailable(Global.IActor actor, Global.PartyAvailableEventArgs args)
        {
            if (args.Available) _client.Send(new PartyJoinMessage(_character.Party));

            else _client.Send(new PartyCannotJoinErrorMessage(Network.Enums.PartyJoinErrorEnum.PLAYER_BUSY));

            _client.Send(new Utils.Objects.Packet(PacketID.BasicNoOperationMessage));
        }

        private void ParseEvent_PartyUpdated(Global.IActor actor, Global.PartyUpdated args)
        {
            if (args.Type)
            {
                _client.Send(new PartyUpdateMessage(args.Updated));
            }
            else
            {
                if (args.Updated == _character)
                {
                    try
                    {
                        _client.Send(new PartyKickedByMessage(_character.Party.Leader));
                    }
                    catch
                    {
                        _client.Send(new Utils.Objects.Packet(PacketID.PartyLeaveMessage));
                    }

                    _character.Party.Updated -= ParseEvent;
                    _character.Party = null;
                }
                else
                    _client.Send(new PartyMemberRemoveMessage(args.Updated));
            }
        }

        private void ParseEvent_PartyLeaderUpdated(Global.IActor actor, Global.PartyLeaderUpdated args)
        {
            _client.Send(new PartyLeaderUpdateMessage(args.NewLeader));
        }
        #endregion
    }
}

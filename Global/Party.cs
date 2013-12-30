using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public class Party
    {
        private int _leaderId;
        private List<Character> _members = new List<Character>();
        private bool _available;

        public bool Available
        {
            get
            {
                return _available;
            }

            set
            {
                _available = value;

                if (Updated != null)
                    Updated(null, new PartyAvailableEventArgs(value));
            }
        }
        public Character Leader
        {
            get
            {
                return _members.Where(chr => chr.Id == _leaderId).FirstOrDefault();
            }
            set
            {
                if (_members.Contains(value))
                {
                    _leaderId = value.Id;
                    if (Updated != null)
                        Updated(value, new PartyLeaderUpdated(value));
                }
            }
        }
        public event GameEventHandler Updated;

        public Party(Character leader, IEnumerable<Character> members, bool available)
        {
            _leaderId = leader.Id;
            _members.Add(leader);
            if (members != null) _members.AddRange(members);
            _available = available;
        }

        public Party(Character leader)
            : this(leader, null, false)
        {
        }

        public bool AddMember(Character member)
        {
            if (_available)
            {
                if (!_members.Contains(member)) _members.Add(member);

                if (Updated != null)
                    Updated(member, new PartyUpdated(member, true));

                return true;
            }

            return false;
        }

        public bool RemoveMember(Character member)
        {
            if (_available)
            {
                if (_members.Remove(member) && Updated != null)
                {
                    Updated(member, new PartyUpdated(member, false));

                    if (_members.Count == 1)
                    {
                        Updated(_members.Last(), new PartyUpdated(_members.Last(), false));
                        _members.Clear();

                        return true;
                    }
                    else if (_leaderId == member.Id)
                        Leader = _members[new Random().Next(0, _members.Count)];

                    return true;
                }
            }

            return false;
        }

        public Character GetMember(Func<Character, bool> condition)
        {
            return _members.Where(condition).FirstOrDefault();
        }

        public void OnMemberSpeaked(Global.Character member, string message)
        {
            if (_available && Updated != null)
                Updated(member, new ActorSpeakedArgs(member, Channel.PARTY, message));
        }

        public void serializeAs_PartyJoinMessage(Utils.Objects.Packet sender)
        {
            sender.WriteInt(_leaderId);

            sender.WriteShort((short)_members.Count);
            foreach (Global.Character member in _members)
                new Network.Types.game.context.PartyMemberInformations(member).serialize(sender);

            sender.WriteBool(false); // restricted ??
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public delegate void GameEventHandler(IActor actor, Utils.Objects.GameEventArgs args);

    public class MapActorUpdatedArgs : Utils.Objects.GameEventArgs
    {
        public bool Type { get; private set; }

        public MapActorUpdatedArgs(bool type)
            : base(GameEventArgsType.MapActorUpdated)
        {
            Type = type;
        }
    }

    public class MapActorMovedArgs : Utils.Objects.GameEventArgs
    {
        public List<int> Path { get; private set; }

        public MapActorMovedArgs(List<int> path)
            : base(GameEventArgsType.MapActorMoved)
        {
            Path = path;
        }
    }

    public class ActorSpeakedArgs : Utils.Objects.GameEventArgs
    {
        public Global.Character Actor { get; private set; }
        public Channel Channel { get; private set; }
        public string Message { get; private set; }

        public ActorSpeakedArgs(Global.Character actor, Channel channel, string message)
            : base(GameEventArgsType.ActorSpeaked)
        {
            Channel = channel;
            Message = message;
            Actor = actor;
        }
    }

    public class PartyAvailableEventArgs : Utils.Objects.GameEventArgs
    {
        public bool Available { get; private set; }

        public PartyAvailableEventArgs(bool available)
            : base(GameEventArgsType.PartyAvailable)
        {
            Available = available;
        }
    }

    public class PartyUpdated : Utils.Objects.GameEventArgs
    {
        public Character Updated { get; private set; }
        public bool Type { get; private set; }

        public PartyUpdated(Character updated, bool type)
            : base(GameEventArgsType.PartyUpdated)
        {
            Updated = updated;
            Type = type;
        }
    }

    public class PartyLeaderUpdated : Utils.Objects.GameEventArgs
    {
        public Character NewLeader { get; private set; }

        public PartyLeaderUpdated(Character newLeader)
            : base(GameEventArgsType.PartyLeaderUpdated)
        {
            NewLeader = newLeader;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Aldos.Global;

namespace Aldos
{
    class Environment
    {
        private static Environment _self;
        public static Environment Instance
        {
            get
            {
                if (_self == null) _self = new Environment();
                return _self;
            }
        }

        private Dictionary<string, Account> _waitingAccounts = new Dictionary<string, Account>();

        public event Global.GameEventHandler ActorSpeaked;

        public int Now
        {
            get
            {
                return (int) ( DateTime.Now - new DateTime(1970, 1, 1) ).TotalSeconds;
            }
        }

        /// <summary>
        /// Add an account in the account list awaiting connection to gameserver.
        /// </summary>
        /// <param name="key">Retrieval key</param>
        /// <param name="value">Awaiting account</param>
        public void AddWaitingAccount(string key, Account value)
        {
            _waitingAccounts.Add(key, value);
        }

        /// <summary>
        /// Return an account with a key.
        /// </summary>
        /// <param name="key">Retrieval key</param>
        public Account GetWaitingAccount(string key)
        {
            if (_waitingAccounts.ContainsKey(key))
            {
                Account value = _waitingAccounts[key];
                _waitingAccounts.Remove(key);
                return value;
            }
            else
                return null;
        }

        /// <summary>
        /// Raise the event ActorSpeaked
        /// </summary>
        /// <param name="actor">Author</param>
        /// <param name="chan">Channel</param>
        /// <param name="message">Message</param>
        public bool OnActorSpeaked(Global.Character actor, Channel chan, string message, DateTime lastWords)
        {
            if (ActorSpeaked != null)
            {
                if (chan == Channel.SALES || chan == Channel.SEEK || chan == Channel.ALIGN)
                {
                    if ((DateTime.Now - lastWords).TotalSeconds > 30)
                    {
                        ActorSpeaked(actor, new Global.ActorSpeakedArgs(actor, chan, message));
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    ActorSpeaked(actor, new Global.ActorSpeakedArgs(actor, chan, message));
                    return true;
                }
            }
            else
                return false;
        }
    }
}

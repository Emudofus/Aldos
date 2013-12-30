using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Aldos.Network.Messages.connection;

namespace Aldos.Realm
{
    class Parser
    {
        private Client _client;
        private string _ticket = Utils.Other.RandomString(32);
        private Global.Account _account;

        public Parser(Client client)
        {
            _client = client;

            _client.Send(new HelloConnectMessage(_ticket));
        }

        private void Parse_IdentificationMessage(IdentificationMessage message)
        {
            _account = Global.Account.Find(acc => acc.Name == message.Login).FirstOrDefault();

            if (_account == null)
            {
                _client.Send(new IdentificationFailedMessage(Network.Enums.IdentificationFailureReason.Login));
                return;
            }

            if (Utils.Other.Encrypt(_account.Password, _ticket) != message.HashedPassword)
            {
                _client.Send(new IdentificationFailedMessage(Network.Enums.IdentificationFailureReason.Login));
                return;
            }

            if (_account.GmLvl == GmLvl.Banned)
            {
                _client.Send(new IdentificationFailedMessage(Network.Enums.IdentificationFailureReason.Banned));
                return;
            }

            _client.Send(new IdentificationSuccessMessage(_account));

            if (message.AutoConnect)
                Parse_ServerSelectionMessage();

            else 
                _client.Send(new ServersListMessage(_account));
        }

        private void Parse_ServerSelectionMessage()
        {
            Environment.Instance.AddWaitingAccount(_ticket, _account);

            _client.Send(new SelectedServerDataMessage(_ticket, _client.IP));
            _client.Close();
        }
    }
}

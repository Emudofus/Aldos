using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Aldos.Realm
{
    class Server
    {
        private static Server _self;
        public static Server Instance
        {
            get
            {
                if (_self == null) _self = new Server(GlobalConfig.Network.Realm.Port);
                return _self;
            }
        }

        private TcpListener _listener;
        private List<Client> _clients = new List<Client>();

        public bool Run { get; private set; }

        #region Ctors
        private Server(int listenPort)
        {
            _listener = new TcpListener(IPAddress.Any, listenPort);
        }
        #endregion

        public void Start()
        {
            Run = true;

            _listener.Start();
            BeginAccept();

            Utils.MyConsole.WriteLine
                (
                    "Ready.", ConsoleType.Info, ConsoleWriter.Realm
                );
        }

        public void Stop()
        {
            Run = false;

            _listener.Stop();

            foreach (Client client in _clients)
                client.Close();
            _clients.Clear();

            Utils.MyConsole.WriteLine
                (
                    "Stopped.", ConsoleType.Info, ConsoleWriter.Realm
                );
        }

        private void BeginAccept()
        {
            if (Run)
            {
                _listener.BeginAcceptSocket
                    (
                        new AsyncCallback(OnAccepted),
                        _listener
                    );
            }
        }

        private void OnAccepted(IAsyncResult iar)
        {
            if (!Run) return;

            try
            {
                _clients.Add
                    (
                        new Client
                            (
                                ( (TcpListener)iar.AsyncState ).EndAcceptSocket(iar),
                                new Client.DisconnectedEventHandler(OnClientDisconnected)
                            )
                    );

                BeginAccept();
            }
            catch (SocketException ex)
            {
                Utils.MyConsole.WriteLine
                    (
                        "Realm server has failed to accept. " + ex.Message,
                        ConsoleType.Error, ConsoleWriter.Realm
                    );
            }
            catch (Exception ex)
            {
                Utils.MyConsole.WriteLine
                    (
                        ex.Message, ConsoleType.Error, ConsoleWriter.Realm
                    );
            }
        }

        private void OnClientDisconnected(Client sender)
        {
            if (Run && _clients.Remove(sender))
                Utils.MyConsole.WriteLine
                    (
                        sender.IP, ConsoleType.Disconnect, ConsoleWriter.Realm
                    );
        }
    }
}

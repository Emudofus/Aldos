using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Reflection;

namespace Aldos.Realm
{
    class Client
    {
        private Socket _sock;
        private string _ip;
        private byte[] _readBuffer;
        private Parser _parser;

        public string IP
        {
            get { return _ip; }
        }

        public delegate void DisconnectedEventHandler(Client sender);
        public DisconnectedEventHandler Disconnected { get; set; }

        #region Ctor
        public Client(Socket sock, DisconnectedEventHandler callback)
        {
            _sock = sock;
            _ip = _sock.RemoteEndPoint.ToString().Split(':')[0];

            Disconnected = callback;

            Utils.MyConsole.WriteLine
                (
                    _ip, ConsoleType.Connect, ConsoleWriter.Realm
                );

            BeginReceive();

            _parser = new Parser(this);
        }
        #endregion

        /// <summary>
        /// Close the connexion of the client.
        /// </summary>
        /// <returns>Success or not</returns>
        public bool Close()
        {
            try
            {
                _sock.Shutdown(SocketShutdown.Both);
                _sock.Close();

                if (Disconnected != null)
                    Disconnected(this);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Send a message to the client.
        /// </summary>
        /// <param name="packet">The message</param>
        /// <returns>Number of byte sended</returns>
        public int Send(Utils.Objects.Packet packet)
        {
            if (!_sock.Connected) return -1;

            try
            {
                int sended = _sock.Send(packet.Pack().ToArray());
                Utils.MyConsole.WriteLine
                    (
                        _ip + ": " + packet.ID + " (length: " + sended + ")",
                        ConsoleType.Send, ConsoleWriter.Realm
                    );
                return sended;
            }
            catch (SocketException ex)
            {
                Utils.MyConsole.WriteLine
                    (
                        "Fail send data. " + ex.Message,
                        ConsoleType.Error, ConsoleWriter.Realm
                    );
                Close();
                return -1;
            }
            catch (Exception ex)
            {
                Utils.MyConsole.WriteLine
                    (
                        ex.Message + "\n" + ex.StackTrace,
                        ConsoleType.Error, ConsoleWriter.Realm
                    );
                Close();
                return -1;
            }
        }

        private void BeginReceive()
        {
            if (_sock != null && _sock.Connected)
            {
                _readBuffer = new byte[1024];
                _sock.BeginReceive
                    (
                        _readBuffer, 0, 1024, 0,
                        new AsyncCallback(OnReceived), _sock
                    );
            }
        }

        private void OnReceived(IAsyncResult iar)
        {
            if (!_sock.Connected) return;

            int received = -1;
            try
            {
                received = ( (Socket)iar.AsyncState ).EndReceive(iar);
            }
            catch (SocketException ex)
            {
                Utils.MyConsole.WriteLine
                    (
                        "Fail receive data. " + ex.Message,
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

            if (received > 0)
            {
                Utils.Objects.Packet packet = new Utils.Objects.ByteBuffer(_readBuffer, 0, received).Unpack();
                Utils.MyConsole.WriteLine
                    (
                        _ip + ": " + packet.ID + " (length: " + received + ")",
                        ConsoleType.Receive, ConsoleWriter.Realm
                    );
                Parse(packet);

                BeginReceive();
            }
            else
                Close();
        }

        private void Parse(Utils.Objects.Packet packet)
        {
            MethodInfo method = _parser.GetType().GetMethod
                (
                    "Parse_" + packet.ID.ToString(),
                    BindingFlags.NonPublic | BindingFlags.Instance
                );

            if (method != null)
            {
                ParameterInfo[] methodParameters = method.GetParameters();
                object[] argument = null;

                if (methodParameters.Length > 0)
                {
                    MethodInfo argumentMethod = methodParameters[0].ParameterType.GetMethod
                        (
                            "deserialize",
                            BindingFlags.Public | BindingFlags.Static
                        );

                    if (argumentMethod == null)
                        throw new Exception("Impossible to invoke \"deserialize\" of \"" + methodParameters[0].ParameterType + "\".");

                    argument = new object[]
                    {
                        argumentMethod.Invoke(null, new object[] { packet })
                    };
                }

                method.Invoke(_parser, argument);
            }
            else
                Close();
        }
    }
}

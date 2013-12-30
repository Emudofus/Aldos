using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace Aldos.Utils
{
    class DatabaseManager
    {
        private static DatabaseManager _self;
        public static DatabaseManager Instance
        {
            get
            {
                if (_self == null) _self = new DatabaseManager();
                return _self;
            }
        }

        private DatabaseManager() { }

        private MySqlConnection _connection;
        private System.Timers.Timer _timer = new System.Timers.Timer(GlobalConfig.MySql.Interval * 1000 * 60);
        private List<string> _commands = new List<string>();

        /// <summary>
        /// Initialize parameters and open connection to MySql
        /// </summary>
        public bool Init()
        {
            try
            {
                _connection = new MySqlConnection("server=" + GlobalConfig.MySql.Host +
                                                 "; user id=" + GlobalConfig.MySql.User +
                                                 "; password=" + GlobalConfig.MySql.Password +
                                                 "; database=" + GlobalConfig.MySql.Database);

                _timer.Elapsed += delegate { Console.WriteLine(ProcessQueue() + " commands executed."); };
                _timer.Start();

                _connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Close connection
        /// </summary>
        public bool Close()
        {
            try
            {
                MyConsole.WriteLine
                    (
                        ProcessQueue() + " commands executed.",
                        ConsoleType.Info, ConsoleWriter.Unknown
                    );

                _timer.Stop();

                _connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Read data from database (SELECT)
        /// </summary>
        public MySqlDataReader Read(string command)
        {
            return new MySqlCommand(command, _connection).ExecuteReader();
        }

        /// <summary>
        /// Operation on database (INSERT, UPDATE, DELETE)
        /// </summary>
        public void Execute(string command)
        {
            _commands.Add(command);
        }

        /// <summary>
        /// Execute queued commands.
        /// </summary>
        /// <returns>Commands executed</returns>
        public int ProcessQueue()
        {
            _timer.Stop();

            List<string> tmpCommands = new List<string>(_commands);
            _commands.Clear();

            int i = 0;
            foreach (string command in tmpCommands)
            {
                try
                {
                new MySqlCommand(command, _connection).ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Utils.MyConsole.WriteLine
                        (
                            "Erreur MySql : \"" + ex.Message + "\". Commande : \"" + command + "\".",
                            ConsoleType.Error, ConsoleWriter.Unknown
                        );
                }
                ++i;
            }

            _timer.Start();

            return i;
        }
    }
}

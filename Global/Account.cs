using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public class Account
    {
        #region Static

        private static Dictionary<int, Account> s_accounts = new Dictionary<int, Account>();
        private static System.Timers.Timer s_timer = new System.Timers.Timer(GlobalConfig.MySql.Interval * 1000 * 60);

        public static List<Account> Find(Func<Account, bool> condition)
        {
            return s_accounts.Values.Where(condition).ToList();
        }

        public static List<Account> All() {
            return s_accounts.Values.ToList();
        }

        public static Account FindOne(Func<Account, bool> condition)
        {
            return s_accounts.Values.Where(condition).FirstOrDefault();
        }

        public static Account FindById(int id)
        {
            if (s_accounts.ContainsKey(id)) return s_accounts[id];
            return null;
        }

        public static int Load()
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = Utils.DatabaseManager.Instance.Read
                (
                    "SELECT * FROM `account`;"
                );

            int i;
            for (i = 0; reader.Read(); ++i)
            {
                s_accounts.Add
                    (
                        reader.GetInt32("Id"),
                        new Account
                            (
                                reader.GetInt32("Id"), reader.GetString("Name"), reader.GetString("Password"), reader.GetString("Nickname"),
                                (GmLvl)reader.GetInt32("GmLvl"), reader.GetString("LastIp")
                            )
                    );
            }

            reader.Close();

            s_timer.Elapsed += delegate
            {
                Utils.MyConsole.WriteLine
                    (
                        SaveAll() + " accounts were saved.",
                        ConsoleType.Info, ConsoleWriter.Unknown
                    );
            };
            s_timer.Start();

            return i;
        }

        public static int SaveAll()
        {
            int i = 0;

            foreach (Account acc in s_accounts.Values)
                if (acc.Save()) ++i;

            return i;
        }

        #endregion

        #region Member

        private int _id;
        public int Id
        {
            get { return _id; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
        }

        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; }
        }

        private GmLvl _gmLvl;
        public GmLvl GmLvl
        {
            get { return _gmLvl; }
            set { _gmLvl = value; }
        }

        private string _lastIp;
        public string LastIp
        {
            get { return _lastIp; }
            set { _lastIp = value; }
        }

        private List<Character> _characters;
        public List<Character> Characters
        {
            get
            {
                if (_characters == null) _characters = Character.Find(chr => chr.Owner.Id == _id);
                return _characters;
            }
        }

        private int _currentCharacter = -1;
        public Character CurrentCharacter
        {
            get
            {
                if (_currentCharacter < 0) return null;
                return _characters[_currentCharacter];
            }
            set { _currentCharacter = _characters.IndexOf(value); }
        }

        private FriendList _friends;
        public FriendList Friends
        {
            get
            {
                if (_friends == null) _friends = FriendList.Find(_id, FriendType.Friend);
                return _friends;
            }
        }

        public Account(int id, string name, string password, string nickname, GmLvl gmLvl, string lastIp)
        {
            _id = id;
            _name = name;
            _password = password;
            _nickname = nickname;
            _connected = false;
            _gmLvl = gmLvl;
            _lastIp = lastIp;
        }

        public bool Save()
        {
            try
            {
                Utils.DatabaseManager.Instance.Execute
                    (
                        "UPDATE `account` SET " +

                        "`Connected` = '" + (_connected ? 1 : 0) + "', " +
                        "`GmLvl` = '" + (int)_gmLvl + "', " +
                        "`LastIp` = '" + _lastIp + "'" +

                        " WHERE `Id` = '" + _id + "';"
                    );

                return true;
            }
            catch (Exception ex)
            {
                Utils.MyConsole.WriteLine(ex, ConsoleWriter.Unknown);
                return false;
            }
        }

        #endregion
    }
}

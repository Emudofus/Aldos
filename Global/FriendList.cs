using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Aldos.Network.Types.context;

namespace Aldos.Global
{
    public class FriendList
    {
        #region Static
        public static FriendList Find(int owner, FriendType type)
        {
            List<int> list = new List<int>();

            MySql.Data.MySqlClient.MySqlDataReader reader = Utils.DatabaseManager.Instance.Read
                (
                    "SELECT * FROM `friend` WHERE `Owner`='" + owner + "' AND `Type`='" + type.ToString() + "';"
                );

            while (reader.Read()) list.Add(reader.GetInt32("Target"));

            reader.Close();

            return new FriendList(list, owner, type);
        }
        #endregion

        #region Member

        private List<FriendInformations> _friends;

        private int _owner;
        private FriendType _type;

        public int Count
        {
            get { return _friends.Count; }
        }

        #region Ctor
        private FriendList(List<int> friends, int owner, FriendType type)
        {
            _owner = owner;
            _type = type;
            _friends = ( from int friendId in friends select new FriendInformations(Account.FindById(friendId)) ).ToList();
        }
        #endregion  

        public FriendInformations Add(FriendInformations target)
        {
            _friends.Add(target);
            Utils.DatabaseManager.Instance.Execute
                (
                    "INSERT INTO `friend` (`Owner`, `Target`, `Type`) VALUE " +
                    "('" + _owner + "', '" + target.Friend.Id + "', '" + _type.ToString() + "');"
                );
            return target;
        }
        public FriendInformations Add(Account target)
        {
            return Add(new FriendInformations(target));
        }

        public bool Remove(FriendInformations target)
        {
            bool status = _friends.Remove(target);

            if (status)
            {
                Utils.DatabaseManager.Instance.Execute
                    (
                        "DELETE FROM `friend` " +
                        "WHERE (`Owner`='" + _owner + "') AND (`Target`='" + target.Friend.Id + "') AND (`Type`='" + _type.ToString() + "')" +
                        " LIMIT 1;"
                    );
            }

            return status;
        }
        public bool Remove(Account target)
        {
            return Remove(FindOne(friend => friend.Friend == target));
        }

        public IEnumerable<FriendInformations> Find(Func<FriendInformations, bool> condition)
        {
            return _friends.Where(condition);
        }

        public FriendInformations FindOne(Func<FriendInformations, bool> condition)
        {
            return _friends.Where(condition).FirstOrDefault();
        }

        public List<FriendInformations> ToList()
        {
            return _friends;
        }

        public bool Contains(FriendInformations friend)
        {
            return _friends.Contains(friend);
        }

        public bool Contains(Account friend)
        {
            return _friends.Where(friendz => friendz.Friend == friend).FirstOrDefault() != null;
        }

        public FriendInformations Last()
        {
            return _friends.Last();
        }

        #endregion
    }
}

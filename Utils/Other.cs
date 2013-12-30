using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace Aldos.Utils
{
    class Other
    {
        public static string MD5(string str)
        {
            byte[] data = new MD5CryptoServiceProvider().ComputeHash
                (
                    Encoding.Default.GetBytes(str)
                );
            StringBuilder ret = new StringBuilder();
            foreach (byte b in data)
                ret.Append(b.ToString("x2"));
            return ret.ToString();
        }
        public static string Encrypt(string value, string sel)
        {
            return MD5( MD5(value) + sel );
        }
        public static string RandomString(int count)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_'";
            StringBuilder ret = new StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < count; ++i)
                ret.Append(alphabet[rnd.Next(alphabet.Length)]);

            return ret.ToString();
        }
        public static string RandomPseudo(int count)
        {
            string voyelle = "aeiouy";
            string consonne = "bcdfghjklmnpqrstvwxz";
            StringBuilder ret = new StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond);

            bool flag = rnd.Next(2) == 1;

            for (int i = 0; i < count; ++i)
            {
                ret.Append(flag ? voyelle[rnd.Next(6)] : consonne[rnd.Next(20)]);
                flag = !flag;
            }

            return Capitalize(ret.ToString());
        }
        public static DateTime GetDate(long timestamp)
        {
            return new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(timestamp));
        }
        public static string Capitalize(string value)
        {
            char[] c = value.ToCharArray();
            c[0] = char.ToUpper(c[0]);
            return new string(c);
        }

        private static List<Objects.Point> _cellPos = new List<Objects.Point>();
        private static void initCellPos()
        {
            if (_cellPos.Count > 0) _cellPos.Clear();

            int _loc_1 = 0;
            int _loc_2 = 0;

            for (int _loc_5 = 0; _loc_5 < 20; ++_loc_5)
            {
                for (int _loc_4 = 0; _loc_4 < 14; ++_loc_4)
                    _cellPos.Add(new Objects.Point(_loc_1 + _loc_4, _loc_2 + _loc_4));

                _loc_1++;

                for (int _loc_4 = 0; _loc_4 < 14; ++_loc_4)
                    _cellPos.Add(new Objects.Point(_loc_1 + _loc_4, _loc_2 + _loc_4));

                ++_loc_2;
            }

            Console.WriteLine(_loc_2);

            if (_cellPos.Count != 560) throw new Exception("Cannot load coords !");
        }
        public static Objects.Point CellIdToCoord(int id)
        {
            if (_cellPos.Count != 560) initCellPos();

            try
            {
                return _cellPos[id];
            }
            catch
            {
                return new Objects.Point(-1, -1);
            }
        }
        public static int CoordToCellId(Objects.Point point)
        {
            if (_cellPos.Count != 560) initCellPos();

            return ( point.x - point.y ) * 14 + point.y + ( point.x - point.y ) / 2;
        }
    }
}

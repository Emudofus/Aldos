using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Aldos
{
    class GlobalConfig
    {
        public static bool Debug = bool.Parse(ConfigurationManager.AppSettings["debug"]);

        public static class Version
        {
            private static NameValueConfigurationCollection s_versionCollection = (NameValueConfigurationCollection)ConfigurationManager.GetSection("version");

            public static int Major = int.Parse(s_versionCollection["major"].Value);
            public static int Minor = int.Parse(s_versionCollection["minor"].Value);
            public static int Release = int.Parse(s_versionCollection["release"].Value);
            public static int Revision = int.Parse(s_versionCollection["revision"].Value);
            public static int BuildType = int.Parse(s_versionCollection["patch"].Value);
            public static int Patch = int.Parse(s_versionCollection["buildtype"].Value);
        }

        public static class MySql
        {
            private static NameValueConfigurationCollection s_mysqlCollection = (NameValueConfigurationCollection)ConfigurationManager.GetSection("mysql");

            public static int Interval = int.Parse(s_mysqlCollection["interval"].Value); // en minutes

            public static string Database = s_mysqlCollection["interval"].Value;
            public static string Host = s_mysqlCollection["interval"].Value;
            public static string User = s_mysqlCollection["interval"].Value;
            public static string Password = s_mysqlCollection["interval"].Value;
        }

        public static class Network
        {
            public static string IP = ( (NameValueConfigurationCollection)ConfigurationManager.GetSection("network") )["ip"].Value;

            public static int ReloadDatabase = 5; // minutes

            public static class Realm
            {
                public static int Port = 443;
            }

            public static class Game
            {
				public static int Guid = 12;
                public static int Port = 5556;

                public static int StartLevel = 1;
                public static int StartMap = 2323; // 21891589 : incarnam
                public static int StartCell = 355; // 257 : incarnam
				
				public static List<Channel> DefaultEnabledChannels = new List<Channel>()
				{
                    Channel.GLOBAL, Channel.PSEUDO_PRIVATE, Channel.TEAM, Channel.PARTY,
                    Channel.PSEUDO_INFO, Channel.SALES, Channel.SEEK, Channel.ADS,
                    Channel.PSEUDO_FIGHT_LOG, Channel.ALIGN, Channel.GUILD
				};
				
				public static List<Channel> DefaultDisabledChannels = new List<Channel>()
				{
                    Channel.ADMIN, Channel.NOOB
				};
				
				public static string HomeMessage = "Bienvenue sur l'émulateur Aldos (par Blackrush).";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public class Character : IActor
    {
        #region Static

        private static int s_lastId;
        private static Dictionary<int, Character> s_characters = new Dictionary<int, Character>();
        private static System.Timers.Timer s_timer = new System.Timers.Timer(GlobalConfig.MySql.Interval * 1000 * 60);

        /// <summary>
        /// Load all of characters from database in the memory.
        /// </summary>
        /// <returns>Characters loaded</returns>
        public static int Load()
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = Utils.DatabaseManager.Instance.Read
                (
                    "SELECT * FROM `character`;"
                );

            int i;
            for (i = 0; reader.Read(); ++i)
            {
                s_characters.Add
                    (
                        reader.GetInt32("Id"),
                        new Character
                            (
                                reader.GetInt32("Id"), Account.FindById(reader.GetInt32("Account_Id")), reader.GetString("Name"),
                                (Gender)reader.GetInt32("Gender"), (Breed)reader.GetInt32("Breed"), reader.GetInt32("Level"),
                                (from string color in reader.GetString("Colors").Split(';') select int.Parse(color)).ToList(),
                                reader.GetInt32("LastUse"),
                                new Network.Types.context.EntityDispositionInformations
                                    (
                                        reader.GetInt32("CurrentMap_Id"),
                                        reader.GetInt32("CurrentCell_Id"),
                                        Directions.SOUTH_WEST
                                    ),
                                new Network.Types.character.CharacterCharacteristicsInformations
                                    (
                                        reader.GetInt32("Level"), (Breed)reader.GetInt32("Breed"),
                                        reader.GetInt32("StatsPoints"), reader.GetInt32("SpellsPoints"),
                                        reader.GetInt32("Kamas"), reader.GetInt32("Energy"), reader.GetInt32("Vitality"),
                                        reader.GetInt32("Wisdom"), reader.GetInt32("Strength"), reader.GetInt32("Intelligence"),
                                        reader.GetInt32("Chance"), reader.GetInt32("Agility")
                                    )
                            )
                    );
            }

            reader.Close();

            s_timer.Elapsed += delegate
            {
                Utils.MyConsole.WriteLine
                    (
                        SaveAll() + " characters were saved.",
                        ConsoleType.Info, ConsoleWriter.Unknown
                    );
            };
            s_timer.Start();

            return i;
        }

        /// <summary>
        /// Save all of characters in the database.
        /// </summary>
        public static int SaveAll()
        {
            int i = 0;
            foreach (Character chr in s_characters.Values)
                if (chr.Save()) ++i;

            return i;
        }

        /// <summary>
        /// Initialize and insert the character in the database.
        /// </summary>
        /// <param name="owner">Owner of character</param>
        /// <param name="name">Character's name</param>
        /// <param name="gender">Character's gender</param>
        /// <param name="breed">Character's breed</param>
        /// <param name="colors">Character's colors</param>
        /// <returns>Character created</returns>
        public static Character New(Account owner, string name, Gender gender, Breed breed, List<int> colors)
        {
            Character ret = new Character
                (
                    ++s_lastId, owner, name, gender, breed, GlobalConfig.Network.Game.StartLevel,
                    colors, Environment.Instance.Now,
                    new Network.Types.context.EntityDispositionInformations
                        (
                            GlobalConfig.Network.Game.StartMap,
                            GlobalConfig.Network.Game.StartCell,
                            Directions.SOUTH_WEST
                        ),
                    new Network.Types.character.CharacterCharacteristicsInformations
                        (
                            GlobalConfig.Network.Game.StartLevel, breed,
                            (5*GlobalConfig.Network.Game.StartLevel-5), (GlobalConfig.Network.Game.StartLevel - 1),
                            0, 0, 0, 0, 0, 0, 0, 0
                        )
                );

            Insert(ret);

            return ret;
        }

        private static void Insert(Character chr)
        {
            Utils.DatabaseManager.Instance.Execute
                (
                    "INSERT INTO `character` " +

                    "(`Id`, `Account_Id`, `Name`, `Gender`, `Breed`, `Level`, `Colors`, `LastUse`, `CurrentMap_Id`, `CurrentCell_Id`, " +
                    "`StatsPoints`, `SpellsPoints`, `Kamas`, `Energy`, `Vitality`, `Wisdom`, `Strength`, `Intelligence`, `Chance`, `Agility`)" +

                    "VALUES (" +
                    chr.Id + ", '" + chr.Owner.Id + "', '" + chr.Name + "', '" + chr.Gender + "', '" + chr.Breed + "', '" + chr.Level + "', " +
                    "'" + string.Join<int>(";", chr.Look.Colors) + "', '" + chr.LastUse + "', '" + chr.Disposition.Map.Id + "', " +
                    "'" + chr.Disposition.Cell + "', '" + chr.Characteristics.statsPoints + "', '" + chr.Characteristics.spellsPoints + "', " +
                    "'" + chr.Characteristics.kamas + "', '" + chr.Characteristics.energyPoints + "', '" + chr.Characteristics.vitality + "', " +
                    "'" + chr.Characteristics.wisdom + "', '" + chr.Characteristics.strength + "', '" + chr.Characteristics.intelligence + "', " +
                    "'" + chr.Characteristics.chance + "', '" + chr.Characteristics.agility + "'" +
                           ");"
                );
        }

        /// <summary>
        /// Find characters.
        /// </summary>
        public static List<Character> Find(Func<Character, bool> condition)
        {
            return s_characters.Values.Where(condition).ToList();
        }

        /// <summary>
        /// Find a character by Id. Faster than FindOne
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Character FindById(int id)
        {
            if (s_characters.ContainsKey(id)) return s_characters[id];
            return null;
        }

        /// <summary>
        /// Find a character. Slower than FindById
        /// </summary>
        public static Character FindOne(Func<Character, bool> condition)
        {
            return s_characters.Values.Where(condition).FirstOrDefault();
        }

        #endregion

        #region Member

        private int _id;
        public int Id
        {
            get { return _id; }
            set { }
        }

        private Account _owner;
        public Account Owner
        {
            get { return _owner; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { }
        }

        private Gender _gender;
        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private Breed _breed;
        public Breed Breed
        {
            get { return _breed; }
            set { _breed = value; }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private Network.Types.EntityLook _look;
        public Network.Types.EntityLook Look
        {
            get { return _look; }
            set { }
        }

        private int _lastUse;
        public int LastUse
        {
            get { return _lastUse; }
            set { _lastUse = value; }
        }

        private Network.Types.context.EntityDispositionInformations _disposition;
        public Network.Types.context.EntityDispositionInformations Disposition
        {
            get { return _disposition; }
            set { }
        }

        private Network.Types.character.CharacterCharacteristicsInformations _characteristics;
        public Network.Types.character.CharacterCharacteristicsInformations Characteristics
        {
            get { return _characteristics; }
            set { }
        }

        private Network.Enums.PlayerStateEnum _state;
        public Network.Enums.PlayerStateEnum State
        {
            get { return _state; }
            set { _state = value; }
        }

        public GameEventHandler PrivateMessageReceived { get; set; }
        
        private Game.Client _client;
        public Game.Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private Party _party;
        public Party Party
        {
            get { return _party; }
            set { _party = value; }
        }

        public Character(int id, Account owner, string name, Gender gender, Breed breed, int level, List<int> colors,
                         int lastUse, Network.Types.context.EntityDispositionInformations disposition,
                         Network.Types.character.CharacterCharacteristicsInformations characteristics)
        {
            _id = id;
            _owner = owner;
            _name = name;
            _gender = gender;
            _breed = breed;
            _level = level;
            _look = new Network.Types.EntityLook(this, colors);
            _lastUse = lastUse;
            _disposition = disposition;
            _characteristics = characteristics;
        }

        public bool Save()
        {
            try
            {
                Utils.DatabaseManager.Instance.Execute
                    (
                        "UPDATE  `character` SET " +

                        "`Gender` =  '" + (int)_gender + "', " +
                        "`Breed` = '" + (int)_breed + "', " +
                        "`Level` = '" + _level + "', " +
                        "`LastUse` = '" + _lastUse + "', " +
                        "`CurrentMap_Id` = '" + _disposition.Map.Id + "', " +
                        "`CurrentCell_Id` = '" + _disposition.Cell + "', " +
                        "`StatsPoints` = '" + _characteristics.statsPoints + "', " +
                        "`SpellsPoints` = '" + _characteristics.spellsPoints + "', " +
                        "`Kamas` = '" + _characteristics.kamas + "', " +
                        "`Energy` = '" + _characteristics.energyPoints + "', " +
                        "`Vitality` = '" + _characteristics.vitality + "', " +
                        "`Wisdom` = '" + _characteristics.wisdom + "', " +
                        "`Strength` = '" + _characteristics.strength + "', " +
                        "`Intelligence` = '" + _characteristics.intelligence + "', " +
                        "`Chance` = '" + _characteristics.chance + "', " +
                        "`Agility` = '" + _characteristics.agility + "'" +

                        " WHERE  `Id`=" + _id + ";"
                    );

                return true;
            }
            catch (Exception ex)
            {
                Utils.MyConsole.WriteLine(ex, ConsoleWriter.Unknown);
                return false;
            }
        }

        public void Delete()
        {
            Utils.DatabaseManager.Instance.Execute
                (
                    "DELETE FROM `characters` WHERE `Id`='" + _id + "';"
                );

            s_characters.Remove(_id);
        }

        public bool SendPrivateMessage(Character sender, string message)
        {
            if (PrivateMessageReceived != null)
            {
                PrivateMessageReceived(sender, new ActorSpeakedArgs(sender, Channel.PSEUDO_PRIVATE, message));
                return true;
            }
            else
                return false;
        }

        #endregion
    }
}

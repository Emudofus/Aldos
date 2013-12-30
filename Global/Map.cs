using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public class Map
    {
        #region Static

        private static Dictionary<int, Map> s_maps = new Dictionary<int, Map>();

        /// <summary>
        /// Find maps in the memory only.
        /// </summary>
        public static List<Map> Find(Func<Map, bool> condition)
        {
            return s_maps.Values.Where(condition).ToList();
        }

        /// <summary>
        /// Find a map in the memory only.
        /// </summary>
        public static Map FindOne(Func<Map, bool> condition)
        {
            return s_maps.Values.Where(condition).FirstOrDefault();
        }

        /// <summary>
        /// Find a map in the database after the memory.
        /// </summary>
        /// <param name="id">Map's Id</param>
        public static Map FindById(int id)
        {
            if (s_maps.ContainsKey(id)) return s_maps[id];

            Map ret = Load(id);
            if (ret != null) s_maps.Add(id, ret);

            return ret;
        }

        private static Map Load(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = Utils.DatabaseManager.Instance.Read
                (
                    "SELECT * FROM `map` WHERE `Id`='" + id + "' LIMIT 1;"
                );

            Map ret = null;

            while (reader.Read() && ret == null)
            {
                ret = new Map
                    (
                        reader.GetInt32("Id"), reader.GetInt32("RelativeId"), reader.GetInt32("Version"), reader.GetInt32("Type"),
                        reader.GetInt32("SubAreaId"), reader.GetInt32("TopNeighbour_Id"), reader.GetInt32("BottomNeighbour_Id"),
                        reader.GetInt32("LeftNeighbour_Id"), reader.GetInt32("RightNeighbour_Id"),
                        reader.GetInt32("ShadowBonusOnEntities"), reader.GetInt32("UseLowpassFilter"), reader.GetInt32("UseReverb"),
                        reader.GetInt32("PresetId"), reader.GetString("Cells")
                    );
            }

            reader.Close();

            return ret;
        }

        #endregion

        #region Member

        private int _id;
        public int Id
        {
            get { return _id; }
        }

        private int _relativeId;
        public int RelativeId
        {
            get { return _relativeId; }
        }

        private int _version;
        public int Version
        {
            get { return _version; }
        }

        private int _type;
        public int Type
        {
            get { return _type; }
        }

        private int _subAreaId;
        public int SubAreaId
        {
            get { return _subAreaId; }
        }

        private int _topNeighbourId;
        private int _bottomNeighbourId;
        private int _leftNeighbourId;
        private int _rightNeighbourId;

        public Map TopNeighbour
        {
            get { return FindById(_topNeighbourId); }
        }
        public Map BottomNeighbour
        {
            get { return FindById(_bottomNeighbourId); }
        }
        public Map LeftNeighbour
        {
            get { return FindById(_leftNeighbourId); }
        }
        public Map RightNeighbour
        {
            get { return FindById(_rightNeighbourId); }
        }

        private int _shadowBonusOnEntities;
        public int ShadowBonusOnEntities
        {
            get { return _shadowBonusOnEntities; }
        }

        private int _useLowpassFilter;
        public int UseLowpassFilter
        {
            get { return _useLowpassFilter; }
        }

        private int _useReverb;
        public int UseReverb
        {
            get { return _useReverb; }
        }

        private int _presetId;
        public int PresetId
        {
            get { return _presetId; }
        }

        private List<Cell> _cells = new List<Cell>();
        private List<IActor> _actors = new List<IActor>();

        public event GameEventHandler Updated;

        public Map(int id, int relativeId, int version, int type, int subAreaId,
                   int topNeighbourId, int bottomNeighbourId, int leftNeighbourId, int rightNeighbourId,
                   int shadowBonusOnEntities, int useLowpassFilter, int useReverb, int presetId,
                   string cells)
        {
            _id = id;
            _relativeId = relativeId;
            _version = version;
            _type = type;
            _subAreaId = subAreaId;

            _topNeighbourId = topNeighbourId;
            _bottomNeighbourId = bottomNeighbourId;
            _leftNeighbourId = leftNeighbourId;
            _rightNeighbourId = rightNeighbourId;

            _shadowBonusOnEntities = shadowBonusOnEntities;
            _useLowpassFilter = useLowpassFilter;
            _useReverb = useReverb;
            _presetId = presetId;

            string[] aCells = cells.Split(';');
            for (int i = 0; i < 560; ++i)
                _cells.Add(new Cell(i, aCells[i].Split(',')));
        }

        /// <summary>
        /// Return an actor this on map.
        /// </summary>
        public IActor GetActor(Func<IActor, bool> condition)
        {
            return _actors.Where(condition).FirstOrDefault();
        }

        /// <summary>
        /// Add an actor on the map.
        /// </summary>
        public void AddActor(IActor actor)
        {
            if (!_actors.Contains(actor)) _actors.Add(actor);
            if (Updated != null)
                Updated(actor, new MapActorUpdatedArgs(true));
        }

        /// <summary>
        /// Remove an actor from the map.
        /// </summary>
        public void RemoveActor(IActor actor)
        {
            if (_actors.Remove(actor) && Updated != null)
                Updated(actor, new MapActorUpdatedArgs(false));
        }

        public void OnActorMoved(Character sender, List<int> path)
        {
            if (_actors.Contains(sender) && Updated != null)
                Updated(sender, new MapActorMovedArgs(path));
        }

        public void OnActorSpeaked(Character sender, string message)
        {
            if (_actors.Contains(sender) && Updated != null)
                Updated(sender, new ActorSpeakedArgs(sender, Channel.GLOBAL, message));
        }

        /// <summary>
        /// Serialize a packet.
        /// </summary>
        public void serializeAs_MapComplementaryInformationsDataMessage(Utils.Objects.Packet sender)
        {
            sender.WriteShort((short)_subAreaId);
            sender.WriteInt((int)_id);
            sender.WriteByte(1); // subareaAlignmentSide

            sender.WriteShort(0); // nHouses

            sender.WriteShort((short)_actors.Count);
            foreach (Character actor in _actors)
            {
                sender.WriteShort((short)Network.Types.context.GameRolePlayActorInformations.ProtocolID);
                new Network.Types.context.GameRolePlayActorInformations(actor).serialize(sender);
            }

            sender.WriteShort(0); // nInteractiveElements

            sender.WriteShort(0); // nStatedElements

            sender.WriteShort(0); // nObstacles

            sender.WriteShort(0); // nFights
        }

        #endregion
    }
}

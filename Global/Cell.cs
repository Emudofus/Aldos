using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public class Cell
    {
        public int Id { get; set; }

        public int Speed { get; set; }

        public int MapChangeData { get; set; }

        public int LosMov { get; set; }

        public int Floor { get; set; }

        public bool Los
        {
            get { return (LosMov & 2) >> 1 == 1; }
        }

        public bool Mov
        {
            get
            {
                return (LosMov & 1) == 1 && !NonWalkableDuringFight && !FarmCell;
            }
        }

        public bool NonWalkableDuringFight
        {
            get { return (LosMov & 3) >> 2 == 1; }
        }

        public bool Red
        {
            get { return (LosMov & 4) >> 3 == 1; }
        }

        public bool Blue
        {
            get { return (LosMov & 5) >> 4 == 1; }
        }

        public bool FarmCell
        {
            get { return (LosMov & 6) >> 5 == 1; }
        }

        public bool Visible
        {
            get { return (LosMov & 7) >> 6 == 1; }
        }

        public Cell(int id, string[] args)
        {
            Id = id;
            Speed = int.Parse(args[2]);
            MapChangeData = int.Parse(args[3]);
            LosMov = int.Parse(args[0]);
            Floor = int.Parse(args[1]);
        }

        private List<IActor> _actors = new List<IActor>();

        public IActor GetActor(int id)
        {
            return _actors.Where(act => act.Id == id).FirstOrDefault();
        }

        public IActor GetActor(string name)
        {
            return _actors.Where(act => act.Name == name).FirstOrDefault();
        }

        public void AddActor(IActor actor)
        {
            _actors.Add(actor);
        }

        public bool RemoveActor(IActor actor)
        {
            return _actors.Remove(actor);
        }
    }
}

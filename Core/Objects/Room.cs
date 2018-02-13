using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Objects
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int PlayersCount { get; set; }

        public Room(int id, string name, int size, int playersCount = 5)
        {
            Id = id;
            Name = name;
            Size = size;
            PlayersCount = playersCount;
        }

        public Room()
        {
        }

        public override string ToString()
        {
            return $"{Id}:{Name}:{Size}:{PlayersCount}";
        }
    }
}

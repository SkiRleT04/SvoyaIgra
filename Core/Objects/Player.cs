using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Objects
{
    public class Player
    {
        public Player() { }

        public Player(string login)
        {
            Login = login;
            Points = 0;
        }

        public int Points { get; set; }
        public string Login { get; set; }
    }
}

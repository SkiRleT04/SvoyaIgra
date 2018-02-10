using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvoyaIgraClient.Models
{
    class Player
    {
        public Player(string nickName, string login, string password)
        {
            NickName = nickName;
            Login = login;
            Password = password;
        }

        public string NickName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Objects
{
    public class User
    {
        public User() { }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

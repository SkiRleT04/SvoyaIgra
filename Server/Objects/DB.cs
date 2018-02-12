using Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects
{
    public static class DB
    {
        public static bool IsUserExist(User user)
        {
            using (var db = new ApplicationContext())
            {
                return db.Users.FirstOrDefault(u => u.Login == user.Login) != null;
            }
        }

        public static void RegisterUser(User user)
        {
            /*using (var db = new ApplicationContext())
            {
                if (db.Users.FirstOrDefault(u => u.Login == user.Login) != null)
                    response.Status = ResponseStatus.Bad;
                else
                {
                    db.Users.Add(comand.User);
                    db.SaveChanges();
                    response.Status = ResponseStatus.Ok;
                }
            }*/
        }

        public static bool Auth(User user)
        {
            using (var db = new ApplicationContext())
            {

            }
            return false;
        }
        
    }
}

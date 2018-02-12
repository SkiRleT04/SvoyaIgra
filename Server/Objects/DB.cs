using Core.Enums;
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
        public static ResponseStatus RegisterUser(User user)
        {
            using (var db = new ApplicationContext())
            {
                //if user exists
                if (db.Users.FirstOrDefault(u => u.Login == user.Login) != null)
                    return ResponseStatus.LoginIsTaken;
                db.Users.Add(user);
                if (db.SaveChanges() > 0)
                    return ResponseStatus.Ok;
                else
                    return ResponseStatus.Bad;
            }
        }

        public static ResponseStatus AuthUser(User user)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password) != null)
                    return ResponseStatus.Ok;
                if (db.Users.FirstOrDefault(u => u.Login == user.Login) == null)
                    return ResponseStatus.UserDoesntExist;
                if (db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password != user.Password) != null)
                    return ResponseStatus.WrongPassword;
            }
            return ResponseStatus.Bad;
        }
        
    }
}

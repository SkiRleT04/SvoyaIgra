using Core.Objects;
using Server.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static ServerObject server;
        static void Main(string[] args)
        {
            using (var  db = new ApplicationContext())
            {
                // создаем два объекта User
                User user1 = new User("ss","we");


                // добавляем их в бд
                db.Users.Add(user1);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Users;
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    Console.WriteLine(u.Login);
                }
            }
            Console.Read();
        }

        /*try
        {
            server = new ServerObject();
            Task.Factory.StartNew(server.Listen);
            do
            { }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            server.Disconnect();
        }*/
    }
    }
//}

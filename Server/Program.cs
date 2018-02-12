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
            try
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
            }
        }
    }
}

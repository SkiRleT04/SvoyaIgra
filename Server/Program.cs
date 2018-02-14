using Core.Objects;
using Server.Objects;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.IO;
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
            var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\Database\";
            AppDomain.CurrentDomain.SetData("DataDirectory", projectPath);

            try
            {
                server = new ServerObject();
                Task.Factory.StartNew(server.Listen);
                do { }
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

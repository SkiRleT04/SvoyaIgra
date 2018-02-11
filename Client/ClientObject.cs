using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    static class ClientObject
    {
        
        private const string host = "192.168.0.103";
        private const int port = 8587;
        static TcpClient client;
        static StreamReader reader;
        static StreamWriter writer;
        

        static ClientObject()
        {
            client = new TcpClient();
            client.Connect(host, port);
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
        }

        public static void SendMessage(string s)
        {
            writer.WriteLine(s);
            writer.Flush();
        }

        public static void RecieveMessage()
        {
            Task.Run(() =>{
                while (true)
                {
                   string s = reader.ReadLine();
                   if(!String.IsNullOrEmpty(s))
                   {
                        
                   }

                }
            });
            
        }

        static void Disconnect()
        {
           
            reader?.Close();
            writer?.Close();
            client?.Close();
        }
    }
}

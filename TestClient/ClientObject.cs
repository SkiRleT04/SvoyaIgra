using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestClient
{
    class ClientObject
    {
        const string host = "127.0.0.1";
        const int port = 8888;
        static TcpClient client;
        static StreamReader reader;
        static StreamWriter writer;

        public ClientObject()
        {
            client = new TcpClient();
            client.Connect(host, port);
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;
            Task.Factory.StartNew(ReceiveMessage);
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                string packet = reader.ReadLine();
                if (!String.IsNullOrEmpty(packet))
                {
                    MessageBox.Show("New data");
                }
            }
        }

        public void SendMessage(string message)
        {
            writer.WriteLine(message);
        }
    }
}

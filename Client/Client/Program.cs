using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        public static UdpClient client = new UdpClient(AddressFamily.InterNetworkV6);
        public static IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("2001:569:7da9:7b00:54ad:ac3f:df20:5f7e"), 80);

        static void Main(string[] args)
        {
            Console.WriteLine("Sending messages...");

            Handshake();

            byte[] data;

            int count = 0;
            
            while(count <= 1000)
            {
                Console.WriteLine($"Sending {count}");
                data = Encoding.ASCII.GetBytes(count.ToString());
                client.Send(data, data.Length);
                count++;
            }

            Console.WriteLine("Messages Sent!");
        }

        public static void Handshake()
        {
            Console.WriteLine("Pinging server...");

            byte[] data;
            byte[] serverReply;

            data = Encoding.ASCII.GetBytes("Ping!");

            client.Send(data, data.Length, endPoint);

            serverReply = client.Receive(ref endPoint);

            Console.WriteLine(Encoding.ASCII.GetString(serverReply));
        }
    }
}

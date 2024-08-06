using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleQuotaGeneratorCLI
{
    internal class Program
    {
        static void ConnectUdp()
        {
            Console.Write("Enter your ip str: ");
            string localIpStr = Console.ReadLine();
            Console.Write("Enter your port: ");
            int localPort = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter server ip str: ");
            string remoteIpStr = Console.ReadLine();
            Console.Write("Enter server port: ");
            int remotePort = Convert.ToInt32(Console.ReadLine());
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(localIpStr), localPort);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(remoteIpStr), remotePort);
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                client.Bind(localEP);
                client.ReceiveTimeout = 15_000;
                byte[] buf = new byte[1024];
                while (true)
                {
                    // цикл обработки команд серверу
                    Console.Write("Enter command or press Ctrl + C to exit: ");
                    string command = Console.ReadLine();
                    // добавить адрес клиента в команду
                    command += $":{localIpStr}:{localPort}";
                    // отправить команду серверу
                    client.SendTo(Encoding.UTF8.GetBytes(command), remoteEP);
                    // получить ответ
                    int bytesRead = client.Receive(buf);
                    string reply = Encoding.UTF8.GetString(buf, 0, bytesRead);
                    Console.WriteLine($"server reply: {reply}");
                }
            }
        }

        static void ConnectTcp()
        {
            Console.Write("Enter your ip str: ");
            string localIpStr = Console.ReadLine();
            Console.Write("Enter your port: ");
            int localPort = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter server ip str: ");
            string remoteIpStr = Console.ReadLine();
            Console.Write("Enter server port: ");
            int remotePort = Convert.ToInt32(Console.ReadLine());
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(localIpStr), localPort);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(remoteIpStr), remotePort);
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(remoteIpStr,remotePort);
                client.ReceiveTimeout = 15_000;
                byte[] buf = new byte[1024];
                while (true)
                {
                    // цикл обработки команд серверу
                    Console.Write("Enter command or press Ctrl + C to exit: ");
                    string command = Console.ReadLine();
                    // добавить адрес клиента в команду
                    command += $":{localIpStr}:{localPort}";
                    // отправить команду серверу
                    client.Send(Encoding.UTF8.GetBytes(command));
                    // получить ответ
                    int bytesRead = client.Receive(buf);
                    string reply = Encoding.UTF8.GetString(buf, 0, bytesRead);
                    Console.WriteLine($"server reply: {reply}");
                }
            }
        }
        static void Main(string[] args)
        {
            int key = 0;
            Console.WriteLine("К какому серверу подключиться:\n1.Tcp\n2.Udp");
            key = Convert.ToInt32(Console.ReadLine());
            switch (key) 
            {
                case 1:
                    ConnectTcp();
                    break;
                case 2:
                    ConnectUdp();
                    break;

                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }
}

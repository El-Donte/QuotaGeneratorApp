using QuotaGeneratorExampleApp.Network;
using QuotaGeneratorExampleApp.QuotaGenerator;
using QuotaGeneratorExampleApp.Udp;
using QuotaGeneratorExampleApp.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorExampleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();   
            List<string> testQuotas = new List<string>()
            {
                "Тише едешь, дальше будешь",
                "C++ лучший язык программирования в мире",
                "Не тот крут кто лев, а тот кто прав"
            };
            IQuotaGenerator generator = new QuotaGeneratorStub(testQuotas, random);
            string serverIpStr = "127.0.0.1";
            int serverPort = 1024;
            int key = 0;
            Console.WriteLine("Какой сервер запустить:\n1.Tcp\n2.Udp");
            key = Convert.ToInt32(Console.ReadLine());
            switch (key) 
            {
                case 1:
                    Console.WriteLine($"Starting server on {serverIpStr}:{serverPort}, press Ctrl + C to stop");
                    using (ServerBase server = new TcpServer(serverIpStr, serverPort, generator))
                    {
                        server.Run();
                    }
                    break;
                case 2:
                    Console.WriteLine($"Starting server on {serverIpStr}:{serverPort}, press Ctrl + C to stop");
                    using (ServerBase server = new UdpServer(serverIpStr, serverPort, generator))
                    {
                        server.Run();
                    }
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }
}

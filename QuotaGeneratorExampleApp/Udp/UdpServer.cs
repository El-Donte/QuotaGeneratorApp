using QuotaGeneratorExampleApp.Network;
using QuotaGeneratorExampleApp.QuotaGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorExampleApp.Udp
{
    // UdpServer - сервер, реализованный на протоколе UDP
    internal class UdpServer : ServerBase
    {
        private readonly Socket _server;

        public UdpServer(string ipStr, int port, IQuotaGenerator generator) : base(generator)
        {
            // создать и настроить сокет сервера
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(ipStr), port);
            _server.Bind(serverEP);
        }

        public override void Run()
        {
            // цикл обработки входящего сообщения сервером
            byte[] buf = new byte[1024];
            while (true)
            {
                try
                {
                    // считать сообщение от клиента
                    int bytesRead = _server.Receive(buf);
                    string message = Encoding.UTF8.GetString(buf, 0, bytesRead);  // message = command:ipAddress:port
                                                                                  // обработать сообщение
                    string[] messageTokens = message.Split(':');
                    string command = messageTokens[0];
                    string clientIpStr = messageTokens[1];
                    int clientPort = Convert.ToInt32(messageTokens[2]); 
                    // обработать сообщение
                    string reply = ProcessCommand(command);
                    // отправить ответ
                    IPEndPoint clientEP = new IPEndPoint(IPAddress.Parse(clientIpStr), clientPort);
                    _server.SendTo(Encoding.UTF8.GetBytes(reply), clientEP);
                } catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                }
            }
        }

        public override void Dispose()
        {
            _server.Close();
        }
    }
}

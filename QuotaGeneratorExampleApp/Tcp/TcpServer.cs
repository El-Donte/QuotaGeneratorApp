using QuotaGeneratorExampleApp.Network;
using QuotaGeneratorExampleApp.QuotaGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorExampleApp.Tcp
{
    internal class TcpServer : ServerBase
    {
        private Socket _server;
        private Socket _client;

        public TcpServer(string ipStr, int port, IQuotaGenerator generator) : base(generator)
        {
            _server = _client = null;
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(ipStr), port);
            _server.Bind(serverEP);
            _server.Listen(1);
            _client = _server.Accept();
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
                    int bytesRead = _client.Receive(buf);
                    string message = Encoding.UTF8.GetString(buf, 0, bytesRead);  // message = command:ipAddress:port
                    string[] messageTokens = message.Split(':');
                    string command = messageTokens[0];
                    // обработать сообщение
                    string reply = ProcessCommand(command);
                    // отправить ответ
                    _client.Send(Encoding.UTF8.GetBytes(reply));
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                }
            }
        }

        public override void Dispose()
        {
            if (_server != null)
            {
                // отключить клиента от сервера и закрыть сокеты
                _client.Shutdown(SocketShutdown.Both);
                _server.Close();
                _client.Close();
                _server = _client = null;
            }
        }
    }
}

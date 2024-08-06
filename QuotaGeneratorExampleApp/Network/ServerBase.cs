using QuotaGeneratorExampleApp.QuotaGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorExampleApp.Network
{
    // ServerBase - базовый класс сервера
    internal abstract class ServerBase : IDisposable
    {
        private IQuotaGenerator _generator; // генератор, используемый сервером

        public ServerBase(IQuotaGenerator generator)
        {
            _generator = generator;
        }

        public abstract void Dispose();

        // Run - запуск  сервера
        public abstract void Run();

        // ProcessCommand - метод обработки команды от пользователя
        protected string ProcessCommand(string command)
        {
            switch (command)
            {
                case "ping": return "pong";
                case "quota": return _generator.GetNextRandomQuota();
                case "exit": return "goodbye";
                default: return "unknown command, allowed commands: ping, quota, exit";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorExampleApp.QuotaGenerator
{
    // IQuotaGenerator - интерфейс генератора цитат
    internal interface IQuotaGenerator
    {
        // GetNextRandomQuota - получить следующую рандомную цитату
        string GetNextRandomQuota();
    }
}

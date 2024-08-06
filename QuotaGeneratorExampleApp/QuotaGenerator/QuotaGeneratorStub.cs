using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorExampleApp.QuotaGenerator
{
    // QuotaGeneratorStub - заглушечная имплементация генератора цитат
    internal class QuotaGeneratorStub : IQuotaGenerator
    {
        private List<string> quotas;
        private Random random;

        public QuotaGeneratorStub(List<string> quotas, Random random)
        {
            this.quotas = quotas;
            this.random = random;
        }

        public string GetNextRandomQuota()
        {
            return quotas[random.Next(quotas.Count)];
        }
    }
}

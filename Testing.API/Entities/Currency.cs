using System;
using System.Collections.Generic;

namespace Testing.API.Entities
{
    public class Currency
    {
        public Currency()
        {
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public IList<CurrencyRate> Currencies { get; set; }
    }
}

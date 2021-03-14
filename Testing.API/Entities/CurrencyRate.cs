using System;
namespace Testing.API.Entities
{
    public class CurrencyRate
    {
        public CurrencyRate()
        {
        }

        public int CurrencyRateId { get; set; }
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime Date { get; set; }

        public Currency Currency { get; set; }
    }
}

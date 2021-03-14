using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Testing.API.Entities;

namespace Testing.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CurrenciesController : Controller
    {
        private IList<Currency> currencyList = new List<Currency>()
            {
                new Currency()
                {
                    Code = "USD",
                    Name = "United States Dollar",
                    Currencies = new List<CurrencyRate>()
                    {
                        new CurrencyRate()
                        {
                            CurrencyRateId = 1,
                            FromCurrencyCode = "USD",
                            ToCurrencyCode = "SGD",
                            ExchangeRate = 0.74m,
                            Date = DateTime.Now
                        },
                        new CurrencyRate()
                        {
                            CurrencyRateId = 1,
                            FromCurrencyCode = "USD",
                            ToCurrencyCode = "MYR",
                            ExchangeRate = 4.12m,
                            Date = DateTime.Now
                        }
                    }
                },

                new Currency()
                {
                    Code = "SGD",
                    Name = "Singapore Dollar",
                    Currencies = new List<CurrencyRate>()
                    {
                        new CurrencyRate()
                        {
                            CurrencyRateId = 1,
                            FromCurrencyCode = "SGD",
                            ToCurrencyCode = "USD",
                            ExchangeRate = 1.34m,
                            Date = DateTime.Now
                        },
                        new CurrencyRate()
                        {
                            CurrencyRateId = 1,
                            FromCurrencyCode = "SGD",
                            ToCurrencyCode = "MYR",
                            ExchangeRate = 3.06m,
                            Date = DateTime.Now
                        }
                    }
                },

                new Currency()
                {
                    Code = "MYR",
                    Name = "Malaysian Ranggit",
                    Currencies = new List<CurrencyRate>()
                    {
                        new CurrencyRate()
                        {
                            CurrencyRateId = 1,
                            FromCurrencyCode = "MYR",
                            ToCurrencyCode = "USD",
                            ExchangeRate = 0.24m,
                            Date = DateTime.Now
                        },
                        new CurrencyRate()
                        {
                            CurrencyRateId = 1,
                            FromCurrencyCode = "MYR",
                            ToCurrencyCode = "SGD",
                            ExchangeRate = 0.33m,
                            Date = DateTime.Now
                        }
                    }
                }
            };

        public void AddDummyData()
        {
            var filePath = "CurrencyFile.json";

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(currencyList));
        }

        [HttpGet]
        public IList<Currency> GetCurrencyList()
        {
            return currencyList.Where(c => c.Currencies.Any()).ToList();
        }

        public CurrencyRate GetCurrencyRateByCurrencyCode(string fromCurrencyCode, string toCurrencyCode)
        {
            CurrencyRate currencyRate = null;

            var currencyRateList = currencyList.FirstOrDefault(c => c.Code == fromCurrencyCode).Currencies;

            if (currencyRateList != null && currencyRateList.Any())
            {
                currencyRate = currencyRateList.FirstOrDefault(cr => cr.FromCurrencyCode == fromCurrencyCode && cr.ToCurrencyCode == toCurrencyCode);
            }

            return currencyRate;
        }
    }
}

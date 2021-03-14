using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testing.Web.Models;
using Testing.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Testing.API.Entities;

namespace Testing.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(SearchCurrencyRateViewModel model)
        {
            HttpClient httpClient = new HttpClient();
            var url = "http://localhost:51652/api/Currencies/GetCurrencyList";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var currencyList = JsonConvert.DeserializeObject<List<Currency>>(content);

                if (currencyList != null && currencyList.Any())
                {
                    model.FromCurrencyCode = "SGD";
                    model.ToCurrencyCode = "USD";
                    model.FromCurrencyCodeList = new List<SelectListItem>();
                    model.ToCurrencyCodeList = new List<SelectListItem>();

                    foreach(var item in currencyList)
                    {
                        model.FromCurrencyCodeList.Add(new SelectListItem { Value = item.Code, Text = item.Name });
                        model.ToCurrencyCodeList.Add(new SelectListItem { Value = item.Code, Text = item.Name });
                    }

                    model.CurrencyRate = currencyList.FirstOrDefault(c => c.Code == "SGD").Currencies.FirstOrDefault(cr => cr.FromCurrencyCode == "SGD" && cr.ToCurrencyCode == "USD");

                    model.FromCurrency = 1;
                    model.ToCurrency = model.CurrencyRate.ExchangeRate;
                }
            }

            return View(model);
        }

        public async Task<JsonResult> CalculateExchangeRate(SearchCurrencyRateViewModel model)
        { 
            HttpClient httpClient = new HttpClient();
            var url = "http://localhost:51652/api/Currencies/GetCurrencyList";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var currencyList = JsonConvert.DeserializeObject<List<Currency>>(content);

                if (currencyList != null && currencyList.Any())
                {
                    model.FromCurrencyCodeList = new List<SelectListItem>();
                    model.ToCurrencyCodeList = new List<SelectListItem>();

                    foreach (var item in currencyList)
                    {
                        model.FromCurrencyCodeList.Add(new SelectListItem { Value = item.Code, Text = item.Name });
                        model.ToCurrencyCodeList.Add(new SelectListItem { Value = item.Code, Text = item.Name });
                    }

                    model.CurrencyRate = currencyList.FirstOrDefault(c => c.Code == model.FromCurrencyCode).Currencies.FirstOrDefault(cr => cr.FromCurrencyCode == model.FromCurrencyCode && cr.ToCurrencyCode == model.ToCurrencyCode);

                    if (!model.IsToCurrencyChanged)
                    {
                        if (model.FromCurrencyCode == model.ToCurrencyCode)
                        {
                            model.ToCurrency = model.FromCurrency;
                        }
                        else
                        {
                            model.ToCurrency = model.CurrencyRate.ExchangeRate * model.FromCurrency;
                        }
                    }
                    else
                    {
                        if (model.FromCurrencyCode == model.ToCurrencyCode)
                        {
                            model.FromCurrency = model.ToCurrency;
                        }
                        else
                        {
                            model.FromCurrency = model.ToCurrency / model.CurrencyRate.ExchangeRate;
                        }
                    }
                }
            }

            return Json(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

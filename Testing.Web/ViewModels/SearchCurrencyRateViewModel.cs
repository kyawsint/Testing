using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Testing.API.Entities;

namespace Testing.Web.ViewModels
{
    public class SearchCurrencyRateViewModel
    {
        public SearchCurrencyRateViewModel()
        {
        }

        public decimal FromCurrency { get; set; }

        public decimal ToCurrency { get; set; }

        public bool IsToCurrencyChanged { get; set; }

        public string FromCurrencyCode { get; set; }

        public List<SelectListItem> FromCurrencyCodeList { get; set; }

        public string ToCurrencyCode { get; set; }

        public List<SelectListItem> ToCurrencyCodeList { get; set; }

        public CurrencyRate CurrencyRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IFCurrenciesApi.Models
{
    public class CurrencyBase
    {
        public double BuyRate { get; set; }

        public double SellRate { get; set; }
    }
}
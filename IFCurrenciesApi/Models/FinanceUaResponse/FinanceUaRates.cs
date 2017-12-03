using System;
using System.Collections.Generic;

namespace IFCurrenciesApi.Models.FinanceUaResponse
{
    public class FinanceUaRates
    {
        public DateTime Date { get; set; }

        public List<Organization> Organizations { get; set; }
    }
}
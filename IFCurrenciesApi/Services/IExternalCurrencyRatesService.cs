using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCurrenciesApi.Services
{
    public interface IExternalCurrencyRatesService
    {
        string GetCurrencyRates();
    }
}

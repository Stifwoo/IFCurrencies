using System.Net;

namespace IFCurrenciesApi.Services
{
    public class FinanceUaService : IExternalCurrencyRatesService
    {
        public string GetCurrencyRates()
        {
            using (var client = new WebClient())
            {
                return client.DownloadString("http://resources.finance.ua/ua/public/currency-cash.json");
            }
        }
    }
}
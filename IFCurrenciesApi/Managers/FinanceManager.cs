using System.Linq;
using IFCurrenciesApi.Helper;
using IFCurrenciesApi.Models.FinanceUaResponse;
using IFCurrenciesApi.Services;
using Currencies = IFCurrenciesApi.Models.Currencies;
using Usd = IFCurrenciesApi.Models.Usd;
using Eur = IFCurrenciesApi.Models.Eur;
using Rub = IFCurrenciesApi.Models.Rub;

namespace IFCurrenciesApi.Managers
{
    public class FinanceManager
    {
        private readonly IBankService _bankService;
        private readonly IExternalApiService _externalApiService;

        public FinanceManager(IBankService bankService, IExternalApiService externalApiService)
        {
            _bankService = bankService;
            _externalApiService = externalApiService;
        }

        public void UpdateCurrencyRates()
        {           
            var currencyRatesInDb = _bankService.GetAllBanksWithLatestCurrencyRates();
            var currencyRatesStr = _externalApiService.GetResponse("http://resources.finance.ua/ua/public/currency-cash.json");
            var currencyRates = JsonSerializator<FinanceUaRates>.Deserialize(currencyRatesStr);

            foreach (var bank in currencyRatesInDb)
            {
                if (bank.Currencies[0].UpdateDate.Date == currencyRates.Date.Date)
                {
                    continue;
                }

                var org = currencyRates.Organizations.FirstOrDefault(b => b.OldId == bank.OldId);

                if (org?.Currencies.Usd != null && org.Currencies.Eur != null && org.Currencies.Rub != null)
                {
                    var rates = new Currencies()
                    {
                        UpdateDate = currencyRates.Date,
                        Usd = new Usd() { BuyRate = org.Currencies.Usd.Ask, SellRate = org.Currencies.Usd.Bid },
                        Eur = new Eur() { BuyRate = org.Currencies.Eur.Ask, SellRate = org.Currencies.Eur.Bid },
                        Rub = new Rub() { BuyRate = org.Currencies.Rub.Ask, SellRate = org.Currencies.Rub.Bid },
                    };

                    bank.Currencies.Add(rates);

                    _bankService.Update(bank);
                }
            }
        }
    }
}
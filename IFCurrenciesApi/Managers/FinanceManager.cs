using System;
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
        private readonly IExternalCurrencyRatesService _externalCurrencyRatesService;

        public FinanceManager(IBankService bankService, IExternalCurrencyRatesService externalCurrencyRatesService)
        {
            _bankService = bankService;
            _externalCurrencyRatesService = externalCurrencyRatesService;
        }

        public void UpdateCurrencyRates()
        {           
            var currencyRatesInDb = _bankService.GetAllBanksWithLatestCurrencyRates();
            var currencyRatesStr = _externalCurrencyRatesService.GetCurrencyRates();
            var currencyRates = JsonSerializator<FinanceUaRates>.Deserialize(currencyRatesStr);

            foreach (var bank in currencyRatesInDb)
            {
                if (bank.Currencies[0].UpdateDate.Date == currencyRates.Date.Date)
                {
                    continue;
                }

                var org = currencyRates.Organizations.FirstOrDefault(b => b.OldId == bank.OldId);

                if (org != null)
                {
                    var rates = new Currencies()
                    {
                        UpdateDate = currencyRates.Date.Date,
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
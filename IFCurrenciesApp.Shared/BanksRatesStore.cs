using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IFCurrenciesApp.Shared.Models;
using IFCurrenciesApp.Shared.Services;
using Newtonsoft.Json;

namespace IFCurrenciesApp.Shared
{
    public static class BanksRatesStore
    {
        public static List<Bank> BankExchangeRates { get; set; }

        public static void LoadData()
        {
            var apiService = new ApiService<List<Bank>>();
            BankExchangeRates = apiService.GetData("http://localhost:65376/api/banks").Result;
        }

        public static void LoadDataFromFile()
        {
            var assembly = typeof(BanksRatesStore).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("IFCurrenciesApp.Shared.Banks.json");
            string text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            BankExchangeRates = JsonConvert.DeserializeObject<List<Bank>>(text);
        }
    }
}

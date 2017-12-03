using System.Data.Entity;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Context
{
    public class IFCurrenciesContext : DbContext, IContext
    {
        public IFCurrenciesContext() : base("IFCurrenciesDb")
        {

        }

        public IDbSet<Bank> Banks { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Currencies> CurrencyRates { get; set; }
    }
}
using System;

namespace IFCurrenciesApp.Shared.Models
{
    public class Currencies
    {
        public DateTime UpdateDate { get; set; }

        public Usd Usd { get; set; }

        public Eur Eur { get; set; }

        public Rub Rub { get; set; }
    }
}

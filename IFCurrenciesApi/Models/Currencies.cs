using System;

namespace IFCurrenciesApi.Models
{
    public class Currencies : EntityBase
    {
        public DateTime UpdateDate { get; set; }

        public Usd Usd { get; set; }

        public Eur Eur { get; set; }

        public Rub Rub { get; set; }
    }
}
using System.Collections.Generic;

namespace IFCurrenciesApp.Shared.Models
{
    public class Bank
    {
        public string Name { get; set; }

        public int OldId { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();

        public List<Currencies> Currencies { get; set; } = new List<Currencies>();
    }
}

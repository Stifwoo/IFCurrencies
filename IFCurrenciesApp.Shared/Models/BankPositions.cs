using System.Collections.Generic;

namespace IFCurrenciesApp.Shared.Models
{
    public class BankPositions
    {
        public string Name { get; set; }

        public string OldId { get; set; }

        public List<Location> Locations { get; set; } = new List<Location>();
    }
}

namespace IFCurrenciesApp.Shared.Models
{
    public class ClosestBankPosition
    {
        public string Name { get; set; }

        public string OldId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }
    }
}

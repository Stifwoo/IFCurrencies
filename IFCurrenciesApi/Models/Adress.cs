namespace IFCurrenciesApi.Models
{
    public class Address : EntityBase
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public Location Location { get; set; }
    }
}
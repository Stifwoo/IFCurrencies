using System.Collections.Generic;
using System.Linq;
using IFCurrenciesApp.Shared.Models;
using Plugin.Geolocator.Abstractions;

namespace IFCurrenciesApp.Shared
{
    public class Presenter
    {
        public Position CurrentPosition { get; private set; } = new Position();

        public List<ClosestBankPosition> ClosestBankPositions { get; private set; } = new List<ClosestBankPosition>();

        public ClosestBankPosition SelectedBankPosition { get; private set; } = new ClosestBankPosition();

        public BankPositions SelectedBankPositions { get; private set; } = new BankPositions();

        public async void UpdatePosition()
        {
            /*var positionService = new LocationService();
            CurrentPosition = await positionService.GetCurrentLocation();*/
            CurrentPosition = new Position(48.91358853, 24.71530415);
        }

        public void FindClosestBanks()
        {
            UpdatePosition();

            foreach (var bank in BanksRatesStore.BankExchangeRates)
            {
                var minDistance = double.MaxValue;
                var minDistancePosition = new Position(bank.Addresses[0].Location.Latitude,
                    bank.Addresses[0].Location.Longitude);

                foreach (var address in bank.Addresses)
                {
                    var bankPosition = new Position(address.Location.Latitude, address.Location.Longitude);
                    var tempDistance =
                        bankPosition.CalculateDistance(CurrentPosition, GeolocatorUtils.DistanceUnits.Kilometers);
                    if (tempDistance < minDistance)
                    {
                        minDistance = tempDistance;
                        minDistancePosition.Latitude = address.Location.Latitude;
                        minDistancePosition.Longitude = address.Location.Longitude;
                    }
                }

                ClosestBankPositions.Add(new ClosestBankPosition()
                {
                    Name = bank.Name,
                    OldId = bank.OldId.ToString(),
                    Latitude = minDistancePosition.Latitude,
                    Longitude = minDistancePosition.Longitude,
                    Distance = (int) (minDistance * 1000)
                });

            }
        }

        public void SetSelectedBank(string imageClassId)
        {
            SelectedBankPosition = ClosestBankPositions.FirstOrDefault(b => b.OldId == imageClassId);

            var bank = BanksRatesStore.BankExchangeRates.FirstOrDefault(b => b.OldId.ToString() == imageClassId);

            SelectedBankPositions.Name = bank.Name;
            SelectedBankPositions.OldId = bank.OldId.ToString();

            foreach (var address in bank.Addresses)
            {
                SelectedBankPositions.Locations.Add(address.Location);
            }
        }
    }
}

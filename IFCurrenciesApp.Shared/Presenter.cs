using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IFCurrenciesApp.Shared.Models;
using IFCurrenciesApp.Shared.Services;
using Microcharts;
using Plugin.Geolocator.Abstractions;
using SkiaSharp;

namespace IFCurrenciesApp.Shared
{
    public class Presenter
    {
        public Position CurrentPosition { get; private set; } = new Position();

        public List<ClosestBankPosition> ClosestBankPositions { get; private set; } = new List<ClosestBankPosition>();

        public ClosestBankPosition SelectedBankPosition { get; private set; } = new ClosestBankPosition();

        public BankPositions SelectedBankPositions { get; private set; } = new BankPositions();

        public async Task<Position> UpdatePosition()
        {
            var positionService = new LocationService();
            return await positionService.GetCurrentLocation();
            //CurrentPosition = new Position(48.91358853, 24.71530415);
        }

        public async Task<bool> FindClosestBanks()
        {
            var position = await UpdatePosition();

            CurrentPosition = position;

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

            return true;
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

        public Entry[] MakeChartEntries(string bankId, string currency, string action)
        {
            var bank = BanksRatesStore.BankExchangeRates.FirstOrDefault(b => b.OldId.ToString() == bankId);
            var entries = new Entry[4];
            if (action == "buy")
            {
                switch (currency)
                {
                    case "USD":
                        entries[0] = InitializeEntry(25.50, bank.Currencies[0].UpdateDate, 21, SKColors.Black);
                        entries[1] = InitializeEntry(26.15, bank.Currencies[0].UpdateDate, 14, SKColors.DarkOrange);
                        entries[2] = InitializeEntry(26.00, bank.Currencies[0].UpdateDate, 7, SKColors.DarkOrange);
                        entries[3] = InitializeEntry(bank.Currencies[0].Usd.BuyRate, bank.Currencies[0].UpdateDate, 0, SKColors.Red);
                        break;
                    case "EUR":
                        entries[0] = InitializeEntry(29.80, bank.Currencies[0].UpdateDate, 21, SKColors.Black);
                        entries[1] = InitializeEntry(29.45, bank.Currencies[0].UpdateDate, 14, SKColors.DarkOrange);
                        entries[2] = InitializeEntry(31.00, bank.Currencies[0].UpdateDate, 7, SKColors.DarkOrange);
                        entries[3] = InitializeEntry(bank.Currencies[0].Eur.BuyRate, bank.Currencies[0].UpdateDate, 0, SKColors.Red);
                        break;
                    case "RUB":
                        entries[0] = InitializeEntry(0.41, bank.Currencies[0].UpdateDate, 21, SKColors.Black);
                        entries[1] = InitializeEntry(0.43, bank.Currencies[0].UpdateDate, 14, SKColors.DarkOrange);
                        entries[2] = InitializeEntry(0.43, bank.Currencies[0].UpdateDate, 7, SKColors.DarkOrange);
                        entries[3] = InitializeEntry(bank.Currencies[0].Rub.BuyRate, bank.Currencies[0].UpdateDate, 0, SKColors.Red);
                        break;
                }
            }
            else
            {
                switch (currency)
                {
                    case "USD":
                        entries[0] = InitializeEntry(26.50, bank.Currencies[0].UpdateDate, 21, SKColors.Black);
                        entries[1] = InitializeEntry(27.15, bank.Currencies[0].UpdateDate, 14, SKColors.DarkOrange);
                        entries[2] = InitializeEntry(27.00, bank.Currencies[0].UpdateDate, 7, SKColors.DarkOrange);
                        entries[3] = InitializeEntry(bank.Currencies[0].Usd.SellRate, bank.Currencies[0].UpdateDate, 0, SKColors.Red);
                        break;
                    case "EUR":
                        entries[0] = InitializeEntry(30.80, bank.Currencies[0].UpdateDate, 21, SKColors.Black);
                        entries[1] = InitializeEntry(30.45, bank.Currencies[0].UpdateDate, 14, SKColors.DarkOrange);
                        entries[2] = InitializeEntry(32.00, bank.Currencies[0].UpdateDate, 7, SKColors.DarkOrange);
                        entries[3] = InitializeEntry(bank.Currencies[0].Eur.SellRate, bank.Currencies[0].UpdateDate, 0, SKColors.Red);
                        break;
                    case "RUB":
                        entries[0] = InitializeEntry(0.44, bank.Currencies[0].UpdateDate, 21, SKColors.Black);
                        entries[1] = InitializeEntry(0.46, bank.Currencies[0].UpdateDate, 14, SKColors.DarkOrange);
                        entries[2] = InitializeEntry(0.46, bank.Currencies[0].UpdateDate, 7, SKColors.DarkOrange);
                        entries[3] = InitializeEntry(bank.Currencies[0].Rub.SellRate, bank.Currencies[0].UpdateDate, 0, SKColors.Red);
                        break;
                }
            }

            return entries;
        }

        private Entry InitializeEntry(double value, DateTime date, int daysBack, SKColor color)
        {
            return new Entry((float) value)
            {
                Label = date.AddDays(-daysBack).Date.ToString("dd/MM/yyyy"),
                ValueLabel = value.ToString("0.00"),
                Color = color
            };
        }

        public double GetRate(string bankId, string currency, string action)
        {
            var bank = BanksRatesStore.BankExchangeRates.FirstOrDefault(b => b.OldId.ToString() == bankId);
            if (action == "buy")
            {
                switch (currency)
                {
                    case "USD":
                        return bank.Currencies[0].Usd.BuyRate;
                    case "EUR":
                        return bank.Currencies[0].Eur.BuyRate;
                    case "RUB":
                        return bank.Currencies[0].Rub.BuyRate;
                }
            }
            else
            {
                switch (currency)
                {
                    case "USD":
                        return bank.Currencies[0].Usd.SellRate;
                    case "EUR":
                        return bank.Currencies[0].Eur.SellRate;
                    case "RUB":
                        return bank.Currencies[0].Rub.SellRate;
                }
            }

            return 0;
        }
    }
}

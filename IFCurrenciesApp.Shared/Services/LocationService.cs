using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace IFCurrenciesApp.Shared.Services
{
    public class LocationService
    {
        public async Task<Position> GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            return await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
        }
    }
}

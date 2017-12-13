using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace IFCurrenciesApp.Shared
{
    public class CustomMap : Map
    {
        public List<Position> RouteCoordinates { get; set; }

        public CustomMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}

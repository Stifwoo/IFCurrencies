using IFCurrenciesApp.Shared.Models;
using IFCurrenciesApp.Shared.Services;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Distance = Xamarin.Forms.Maps.Distance;
using Position = Xamarin.Forms.Maps.Position;

namespace IFCurrenciesApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClosestBankMapPage
	{
	    private readonly Presenter _presenter;

        public ClosestBankMapPage(Presenter presenter)
		{
			InitializeComponent();

		    _presenter = presenter;

            MoveToRegion(_presenter.CurrentPosition);

		    AddPin(_presenter.CurrentPosition.Latitude, _presenter.CurrentPosition.Longitude, "Here I am!");
            AddPin(_presenter.SelectedBankPosition.Latitude, _presenter.SelectedBankPosition.Longitude, _presenter.SelectedBankPosition.Name);

            //DrawPolyline();
        }

	    public void MoveToRegion(Plugin.Geolocator.Abstractions.Position position)
	    {
	        ClosestBankMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(0.5)));
	    }

	    public void AddPin(double latitude, double longitude, string label = "")
	    {	        
	        var pin = new Pin
	        {
	            Type = PinType.Place,
	            Position = new Position(latitude, longitude),
                Label = label
            };
	        ClosestBankMap.Pins.Add(pin);
        }

	    private async void DrawPolyline()
	    {
	        var api = new ApiService<GoogleDirectionsApiResponse>();

	        var response = await api.GetData(
	            $"https://maps.googleapis.com/maps/api/directions/json?origin={_presenter.CurrentPosition.Latitude},{_presenter.CurrentPosition.Longitude}&destination={_presenter.SelectedBankPosition.Latitude},{_presenter.SelectedBankPosition.Longitude}");

            ClosestBankMap.RouteCoordinates.Add(new Position(response.Routes[0].legs[0].steps[0].start_location.lat, response.Routes[0].legs[0].steps[0].start_location.lng));
	        foreach (var step in response.Routes[0].legs[0].steps)
	        {
	            ClosestBankMap.RouteCoordinates.Add(new Position(step.end_location.lat, step.end_location.lng));
	        }

	        MapStack.Children.Clear();
            MapStack.Children.Add(ClosestBankMap);
	    }
    }
}
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Plugin.Geolocator.Abstractions.Position;

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

            AddPin(_presenter.SelectedBankPosition.Latitude, _presenter.SelectedBankPosition.Longitude, _presenter.SelectedBankPosition.Name);
        }

	    public void MoveToRegion(Position position)
	    {
	        ClosestBankMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromKilometers(0.5)));
	    }

	    public void AddPin(double latitude, double longitude, string label)
	    {	        
	        var pin = new Pin
	        {
	            Type = PinType.Place,
	            Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
                Label = label
            };
	        ClosestBankMap.Pins.Add(pin);
        }
    }
}
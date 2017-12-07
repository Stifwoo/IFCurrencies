using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace IFCurrenciesApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllBanksMapPage
	{
	    private readonly Presenter _presenter;

		public AllBanksMapPage(Presenter presenter)
		{
			InitializeComponent();

		    _presenter = presenter;

		    MoveToRegion(_presenter.CurrentPosition);

		    foreach (var position in _presenter.SelectedBankPositions.Locations)
		    {
		        AddPin(position.Latitude, position.Longitude, _presenter.SelectedBankPositions.Name);
		    }
        }

	    public void MoveToRegion(Position position)
	    {
	        AllBanksMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromKilometers(0.75)));
	    }

	    public void AddPin(double latitude, double longitude, string label)
	    {
	        var pin = new Pin
	        {
	            Type = PinType.Place,
	            Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
	            Label = label
	        };
	        AllBanksMap.Pins.Add(pin);
	    }
    }
}
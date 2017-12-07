using Xamarin.Forms.Xaml;

namespace IFCurrenciesApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConverterPage
	{
	    private readonly Presenter _presenter;

        public ConverterPage(Presenter presenter)
		{
			InitializeComponent();

		    _presenter = presenter;
		}
	}
}
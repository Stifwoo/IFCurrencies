namespace IFCurrenciesApp.Shared.Views
{
	public partial class MainPage
	{
	    private readonly Presenter _presenter;

		public MainPage(Presenter presenter)
		{
		    InitializeComponent();

            _presenter = presenter;

            Children.Add(new ExchangeRatesPage(_presenter));
		    Children.Add(new ConverterPage(_presenter));
		}
	}
}

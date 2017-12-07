using System.Globalization;
using Xamarin.Forms;

namespace IFCurrenciesApp.Shared.Views
{
    public partial class App
	{
		public App ()
		{
		    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US", false);

            InitializeComponent();

            BanksRatesStore.LoadDataFromFile();

            var presenter = new Presenter();

			MainPage = new NavigationPage(new MainPage(presenter));
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

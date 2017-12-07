using Xamarin.Forms.Xaml;

namespace IFCurrenciesApp.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapTabbedPage
    {
        private readonly Presenter _presenter;

        public MapTabbedPage(Presenter presenter)
        {
            InitializeComponent();

            _presenter = presenter;

            Children.Add(new ClosestBankMapPage(_presenter));
            Children.Add(new AllBanksMapPage(_presenter));
        }
    }
}
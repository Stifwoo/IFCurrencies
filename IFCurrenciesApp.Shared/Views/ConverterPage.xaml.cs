using System;
using System.Linq;
using IFCurrenciesApp.Shared.Helper;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFCurrenciesApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConverterPage
	{
	    private readonly Presenter _presenter;

	    private string _selectedBankId;
	    private string _selectedCurrency;
	    private string _selectedAction;

        public ConverterPage(Presenter presenter)
		{
			InitializeComponent();

		    _presenter = presenter;            

		    foreach (var bank in BanksRatesStore.BankExchangeRates)
		    {
		        BankPicker.Items.Add(bank.Name);
		    }
		    BankPicker.SelectedIndex = 0;
		    CurrentRateLabel.Text = BanksRatesStore.BankExchangeRates[0].Currencies[0].Usd.BuyRate.ToString("0.00");

		    var chart = new LineChart
		    {
		        Entries = _presenter.MakeChartEntries(BankIdConsts.PruvatBank, "USD", "buy"),
		        LabelOrientation = Orientation.Horizontal,
		        LabelTextSize = 45,
		        LabelColor = SKColors.Black,
		        LineSize = 8,
		        PointSize = 18,
		        ValueLabelOrientation = Orientation.Horizontal,
		        MinValue = 25,
		        BackgroundColor = SKColors.Transparent,
		        LineAreaAlpha = 10
		    };

		    ChartView.Chart = chart;

		    _selectedBankId = BanksRatesStore.BankExchangeRates[0].OldId.ToString();
		    _selectedCurrency = CurrencyPicker.Items[CurrencyPicker.SelectedIndex];
		    _selectedAction = ActionPicker.SelectedIndex == 0 ? "buy" : "sell";

            ActionPicker.SelectedIndexChanged += ActionPicker_SelectedIndexChanged;
		    CurrencyPicker.SelectedIndexChanged += CurrencyPicker_SelectedIndexChanged;
		    BankPicker.SelectedIndexChanged += BankPicker_SelectedIndexChanged;
            ResultButton.Clicked += ResultButton_Clicked;
        }        

        private void BankPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;
            _selectedBankId = BanksRatesStore.BankExchangeRates.FirstOrDefault(b => b.Name == picker.Items[picker.SelectedIndex])?.OldId.ToString();
            UpdateView();
        }

        private void CurrencyPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            _selectedCurrency = picker.Items[picker.SelectedIndex];

            switch (_selectedCurrency)
            {
                case "USD":
                    ChartView.Chart.MinValue = 25;
                    
                    break;
                case "EUR":
                    ChartView.Chart.MinValue = 28;                    
                    break;
                case "RUB":
                    ChartView.Chart.MinValue = (float)0.3;                   
                    break;
            }

            UpdateView();
        }

        private void ActionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            _selectedAction = picker.SelectedIndex == 0 ? "buy" : "sell";
            UpdateView();
        }

	    private void ResultButton_Clicked(object sender, EventArgs e)
	    {
	        if (double.TryParse(AmountEntry.Text.Replace(",", "."), out double input))
            {
                if (input >= 0 && input < 1000000000)
                {
                    input = Math.Round(input, 2);
                    AmountEntry.Text = input.ToString("0.00");
                    var rate = Convert.ToDouble(CurrentRateLabel.Text);
                    var result = input * rate;
                    result = Math.Round(result, 2);

                    ResultLabel.Text = result.ToString("0.00");

                    return;
                }
	        }

	        DisplayAlert("Error", "You entered invalid amount of money", "OK");
	    }

        private void UpdateView()
	    {
	        ChartView.Chart.Entries = _presenter.MakeChartEntries(_selectedBankId, _selectedCurrency, _selectedAction);

            CurrentRateLabel.Text = _presenter.GetRate(_selectedBankId, _selectedCurrency, _selectedAction).ToString("0.00");
        }
    }
}
using System;
using System.Collections.Generic;
using IFCurrenciesApp.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFCurrenciesApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExchangeRatesPage
	{
	    private readonly Presenter _presenter;

		public ExchangeRatesPage(Presenter presenter)
		{
			InitializeComponent();

		    _presenter = presenter;

		    DateLabel.Text = $"Останнє оновлення \n{BanksRatesStore.BankExchangeRates[0].Currencies[0].UpdateDate}";

		    _presenter.FindClosestBanks();

		    FillTable(BanksRatesStore.BankExchangeRates, "USD");

            CurrencyPicker.SelectedIndexChanged += CurrencyPickerOnSelectedIndexChanged;
		}	    

	    private void FillTable(List<Bank> bankRates, string currency)
	    {	        
            for(var i = 0; i < bankRates.Count; i++)
            {
                string buyRate = string.Empty;
                string sellRate = string.Empty;

                switch (currency)
                {
                    case "USD":
                        buyRate = string.Format("{0:0.00}", bankRates[i].Currencies[0].Usd.BuyRate);
                        sellRate = string.Format("{0:0.00}", bankRates[i].Currencies[0].Usd.SellRate);
                        break;
                    case "EUR":
                        buyRate = string.Format("{0:0.00}", bankRates[i].Currencies[0].Eur.BuyRate);
                        sellRate = string.Format("{0:0.00}", bankRates[i].Currencies[0].Eur.SellRate);
                        break;
                    case "RUB":
                        buyRate = string.Format("{0:0.000}", bankRates[i].Currencies[0].Rub.BuyRate);
                        sellRate = string.Format("{0:0.000}", bankRates[i].Currencies[0].Rub.SellRate);
                        break;
                }

                RatesGrid.RowDefinitions.Add(new RowDefinition() { Height = 50 });

                var boxView = new BoxView() { BackgroundColor = Color.White };

                var bankLabel = new Label() { Text = bankRates[i].Name, TextColor = Color.Black, FontSize = 16, Margin = new Thickness(5, 0, 0, 0), BackgroundColor = Color.White, VerticalOptions = LayoutOptions.Center };
                var buyLabel = new Label() { Text = buyRate, FontSize = 16, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var sellLabel = new Label() { Text = sellRate, FontSize = 16, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var distanceLabel = new Label() { Text = $"{_presenter.ClosestBankPositions[i].Distance} м", FontSize = 16, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var pinImage = new Image() { Source = "pin64.png", ClassId = $"{bankRates[i].OldId}", HeightRequest = 30, WidthRequest = 30, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

                var imageTap = new TapGestureRecognizer();
                imageTap.Tapped += ImageTapped;
                pinImage.GestureRecognizers.Add(imageTap);

                RatesGrid.Children.Add(boxView, 0, i + 2);
                Grid.SetColumnSpan(boxView, 5);

                RatesGrid.Children.Add(bankLabel, 0, i + 2);
                RatesGrid.Children.Add(buyLabel, 1, i + 2);
                RatesGrid.Children.Add(sellLabel, 2, i + 2);
                RatesGrid.Children.Add(distanceLabel, 3, i + 2);
                RatesGrid.Children.Add(pinImage, 4, i + 2);
            }

            RatesGrid.RowDefinitions.Add(new RowDefinition() { Height = 50 });
            var boxView2 = new BoxView() { BackgroundColor = Color.White };
            RatesGrid.Children.Add(boxView2, 0, 9);
            Grid.SetColumnSpan(boxView2, 5);
        }

	    private void CleanTable()
	    {
	        var k = RatesGrid.RowDefinitions.IndexOf(HeaderRow);
	        while (k + 1 != RatesGrid.RowDefinitions.Count)
	        {
	            RatesGrid.RowDefinitions.RemoveAt(k + 1);
	        }

	        k = RatesGrid.Children.IndexOf(HeaderImage);
	        while (k + 1 != RatesGrid.Children.Count)
	        {
	            RatesGrid.Children.RemoveAt(k + 1);
	        }
        }

	    void ImageTapped(object sender, EventArgs eventArgs)
	    {
	        var obj = (Image)sender;
	        var imageClassId = obj.ClassId;
            _presenter.SetSelectedBank(imageClassId);
	        Navigation.PushAsync(new MapTabbedPage(_presenter));
	    }

	    private void CurrencyPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
	    {
	        var picker = (Picker) sender;

	        CleanTable();

            FillTable(BanksRatesStore.BankExchangeRates, picker.Items[picker.SelectedIndex]);
        }
    }
}
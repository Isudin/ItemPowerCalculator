using ItemPowerCalculator.ViewModels;

namespace ItemPowerCalculator;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new CalculatorroViewModel();
	}
}


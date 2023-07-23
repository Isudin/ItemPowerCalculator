using ItemPowerCalculator.ViewModels;

namespace ItemPowerCalculator.Views;

public partial class InputView : ContentView
{
    public InputView()
    {
        InitializeComponent();
        BindingContext = new InputViewModel();
    }
}
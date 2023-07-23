using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ItemPowerCalculator.ViewModels
{
    public class InputViewModel : INotifyPropertyChanged
    {
        public ICommand EnteredValue { get; private set; }
            = new Command<string>((string value) =>
            {
                string text = value;
                var t = int.TryParse(text, out int result);
            });

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

using ItemPowerCalculator.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ItemPowerCalculator.ViewModels
{
    public class CalculatorroViewModel : INotifyPropertyChanged
    {
        public CalculatorroViewModel()
        {
            Types = EnumUtils.ListDisplayNames(typeof(ItemType));
        }

        public Item Item { get; set; }
        public ObservableCollection<Input> Inputs { get; set; } = new();
        public ObservableCollection<Input> AttributeInputs { get; set; } = new();

        public List<string> Types { get; set; }

        private string _selectedType;
        private ItemType SelectedTypeEnum => EnumUtils.GetValueByDisplayName<ItemType>(SelectedType);
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                SubTypes = EnumUtils.ListDisplayNames(typeof(ItemSubType), value);
                OnPropertyChanged();
            }
        }

        private List<string> _subTypes;
        public List<string> SubTypes
        {
            get => _subTypes;
            set
            {
                _subTypes = value;
                OnPropertyChanged();
            }
        }

        private string _selectedSubType;
        private ItemSubType SelectedSubTypeEnum => EnumUtils.GetValueByDisplayName<ItemSubType>(SelectedSubType);

        public string SelectedSubType
        {
            get => _selectedSubType;
            set
            {
                _selectedSubType = value;
                PopulateProperties();
                OnPropertyChanged();
            }
        }

        // For some reason, whenever I try to bind Input.Name/Value in BindableLayout,
        // it doesn't work unless these are uncommented. Neither their type nor value matters,
        // they just have to exist so the exception won't be thrown.
        // The referenced values are still okay.
        public string Name => "t";
        public string Value => "t";

        public void PopulateProperties()
        {
            Inputs.Clear();
            AttributeInputs.Clear();
            switch (SelectedTypeEnum)
            {
                case ItemType.Weapon:
                    Item = new Weapon();
                    break;
                case ItemType.Armor:
                    Item = new Armor();
                    break;
                case ItemType.Others:
                    Item = new Jewellery();
                    break;
                case ItemType.None:
                default:
                    return;
            }

            foreach (PropertyInfo input in Item.GetInputProperties(Item.GetType()))
            {
                if (input.PropertyType == typeof(Attribs))
                    foreach (PropertyInfo attributeInput in Item.GetInputProperties(typeof(Attribs)))
                        AttributeInputs.Add(new Input(attributeInput.Name, (int)attributeInput.GetValue((Attribs)input.GetValue(Item))));
                else
                    Inputs.Add(new Input(input.Name, (int)input.GetValue(Item)));
            }
        }

        private void CreateEntries(PropertyInfo[] properties, Type itemType)
        {
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    Entry entry = new()
                    {
                        Placeholder = "Wprowadź wartość",
                        Keyboard = Keyboard.Numeric,
                        IsSpellCheckEnabled = false,
                        ReturnCommand = new Command<string>((string value) =>
                        {
                            string text = value;
                            if (int.TryParse(text, out int result))
                                itemType.GetProperty(property.Name).SetValue(Item, result);
                        }),
                    };
                    // Set relative binding so the ReturnCommandParameter would be entry's Text property
                    Binding binding = new("Text")
                    {
                        Source = new RelativeBindingSource(RelativeBindingSourceMode.Self)
                    };
                    entry.SetBinding(Entry.TextProperty, binding);
                }
            }
        }

        //public ICommand EnteredValue { get; private set; }
        //    // TODO: Remove after testing
        //    = new Command<string>((string value) =>
        //    {
        //        string text = value;
        //        var t = int.TryParse(text, out int result);
        //    });
        public ICommand EnteredValue { get; private set; }
            // TODO: Remove after testing
            = new Command(() =>
            {
                string text = "A";
                var t = int.TryParse(text, out int result);
            });

        // TODO: Add configurable multipliers saved in resources with default values
        public decimal CalculatedPower
        {
            get
            {
                decimal power = 0;
                foreach (Input input in Inputs)
                    power += GetMultiplier(input) * input.Value;

                foreach (Input attributeInput in AttributeInputs)
                    power += GetMultiplier(attributeInput) * attributeInput.Value;

                return power;
            }
        }

        private decimal GetMultiplier(Input input)
        {
            PropertyInfo multiplierProperty = Item.GetMultipierPropertyByName(Item.GetType(), input.Name);
            decimal multiplier = (decimal)multiplierProperty.GetValue(Item);
            return multiplier;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

namespace ItemPowerCalculator.Model
{
    public class Input
    {
        public Input(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public int Value { get; set; }
    }
}

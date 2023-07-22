using ItemPowerCalculator.Model.Attributes;

namespace ItemPowerCalculator.Model
{
    public class Jewellery : Item
    {
        [Input]
        public int ItemSlots { get; set; }
        public List<int> Attributes { get; set; } = new List<int>();
    }
}

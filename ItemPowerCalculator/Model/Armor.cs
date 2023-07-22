using ItemPowerCalculator.Model.Attributes;

namespace ItemPowerCalculator.Model
{
    public class Armor : Item
    {
        [Input]
        public int Resistance { get; set; }
        [Input]
        public int ItemSlots { get; set; }
    }
}

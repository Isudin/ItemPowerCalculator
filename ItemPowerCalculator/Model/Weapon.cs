using ItemPowerCalculator.Model.Attributes;

namespace ItemPowerCalculator.Model
{
    public class Weapon : Item
    {
        [Input]
        public int Damage { get; set; }
        [Input]
        public int Range { get; set; }
        public List<int> Attributes { get; set; } = new List<int>();
    }
}

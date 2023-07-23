using ItemPowerCalculator.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ItemPowerCalculator.Model
{
    public abstract class Item
    {
        [Input]
        public int Weight { get; set; }

        public static PropertyInfo[] GetInputProperties<T>(T _) where T : Item
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var inputProps = properties.Where(prop => prop.IsDefined(typeof(InputAttribute), true));

            return typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(InputAttribute), true)).ToArray();
        }

        public static PropertyInfo[] GetInputProperties(Type type)
        {
            var properties = type.GetProperties();
            var inputProps = properties.Where(prop => prop.IsDefined(typeof(InputAttribute), true));

            return inputProps.ToArray();
        }
    }

    public enum ItemType
    {
        [Display(Name = "Brak")]
        None,
        [Display(Name = "Broń")]
        Weapon,
        [Display(Name = "Pancerz")]
        Armor,
        [Display(Name = "Inne")]
        Others
    }

    public enum ItemSubType
    {
        [Display(Name = "Brak")]
        None,
        [Display(Name = "Jednoręczna biała", GroupName = "Broń")]
        MeleeOneHanded,
        [Display(Name = "Dwuręczna biała", GroupName = "Broń")]
        MeleeTwoHanded,
        [Display(Name = "Jednoręczna dystansowa", GroupName = "Broń")]
        RangedOneHanded,
        [Display(Name = "Dwuręczna dystansowa", GroupName = "Broń")]
        RangedTwoHanded,
        [Display(Name = "Głowa", GroupName = "Pancerz")]
        Helm,
        [Display(Name = "Tułów", GroupName = "Pancerz")]
        Chest,
        [Display(Name = "Spodnie", GroupName = "Pancerz")]
        Pants,
        [Display(Name = "Buty", GroupName = "Pancerz")]
        Boots,
        [Display(Name = "Rękawice", GroupName = "Pancerz")]
        Gloves,
        [Display(Name = "Pas", GroupName = "Inne")]
        Belt,
        [Display(Name = "Naszyjnik", GroupName = "Inne")]
        Necklace,
        [Display(Name = "Pierścień", GroupName = "Inne")]
        Ring,
        [Display(Name = "Mistyczna runa", GroupName = "Inne")]
        Rune,
    }
}

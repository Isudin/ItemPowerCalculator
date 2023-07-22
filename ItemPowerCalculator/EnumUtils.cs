using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace ItemPowerCalculator;
public static class EnumUtils
{
    public static List<string> ListDisplayNames(Type type, string groupName = "")
    {
        var descs = new List<string>();
        var names = Enum.GetNames(type);
        foreach (var name in names)
        {
            var field = type.GetField(name);
            var fds = field.GetCustomAttributes(typeof(DisplayAttribute), true);
            foreach (DisplayAttribute fd in fds.Cast<DisplayAttribute>())
                if (string.IsNullOrEmpty(groupName) || fd.GroupName == groupName)
                    descs.Add(fd.Name);
        }

        return descs;
    }

    public static T GetValueByDisplayName<T>(string name) where T : Enum
    {
        var type = typeof(T);
        foreach (var field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute
                && attribute.Name == name)
                return (T)field.GetValue(null);

            if (field.Name == name)
                return (T)field.GetValue(null);
        }

        throw new ArgumentOutOfRangeException(nameof(name));
    }
}

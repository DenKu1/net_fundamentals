using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Task2
{
    public static class ObjectExtensions
    {
        public static void SetReadOnlyProperty(this object obj, string propertyName, object newValue)
        {
            obj.GetType()
                .GetRuntimeFields().Where(a => Regex.IsMatch(a.Name, $"\\A<{propertyName}>k__BackingField\\Z"))
                .First()
                .SetValue(obj, newValue);
        }

        public static void SetReadOnlyField(this object obj, string filedName, object newValue)
        {
            obj.GetType()
                .GetField(filedName)
                .SetValue(obj, newValue);
        }
    }
}

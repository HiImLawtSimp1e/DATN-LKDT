using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class ObjectExtensions
{
    public static T SetPropertyValue<T>(this T obj, string propertyName, object propertyValue)
    {
        if (obj == null || string.IsNullOrWhiteSpace(propertyName))
        {
            return obj;
        }

        PropertyInfo property = obj.GetType().GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            Type propertyType = property.PropertyType;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && (propertyValue == null || string.IsNullOrWhiteSpace(propertyValue.ToString())))
            {
                property.SetValue(obj, null);
                return obj;
            }

            if (!(Nullable.GetUnderlyingType(propertyType) != null))
            {
                propertyValue = Convert.ChangeType(propertyValue, propertyType);
            }

            property.SetValue(obj, propertyValue);
        }

        return obj;
    }

    public static object GetPropValue(this object src, string propName)
    {
        return src.GetType().GetProperty(propName)?.GetValue(src, null);
    }

    public static bool HasProperty(this object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName) != null;
    }

    public static bool HasProperty(this Type obj, string propertyName)
    {
        return obj.GetProperty(propertyName) != null;
    }

    public static T Clone<T>(this T source)
    {
        return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
    }
}

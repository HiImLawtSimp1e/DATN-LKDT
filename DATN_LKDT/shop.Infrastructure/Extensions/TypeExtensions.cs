using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroBase.Share.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsCollection(this Type type)
        {
            // string implements IEnumerable, but for our purposes we don't consider it a collection.
            if (type == typeof(string)) return false;

            var interfaces = from inf in type.GetTypeInfo().GetInterfaces()
                             where inf == typeof(IEnumerable) ||
                                   (inf.GetTypeInfo().IsGenericType && inf.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                             select inf;

            return interfaces.Count() != 0;
        }

        public static bool IsNullable(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.GetTypeInfo().IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBooleanType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Boolean:
                    return true;
                default:
                    return false;
            }
        }

        public static PropertyInfo GetProperty<T>(this T obj, string name) where T : class
        {
            var fullName = obj.JsonSerialize().Replace("\"", "");
            var t = Type.GetType(fullName);
            var val = System.Reflection.TypeExtensions.GetProperty(t, name);

            return val;
        }

        /// <summary>
        /// Set value to a propertity of a T object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityModel"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public static void SetValueToObject<T>(this T entityModel, PropertyInfo field, object value)
        {
            if (field == null)
            {
                return;
            }

            if (field.PropertyType == typeof(DateTime) || field.PropertyType == typeof(DateTime?))
            {
                DateTime.TryParse(value.ToString(), out DateTime date);
                if (date != DateTime.MinValue)
                {
                    field.SetValue(entityModel, date);
                }
            }
            else if (field.PropertyType == typeof(int) || field.PropertyType == typeof(int?))
            {
                var isSuccess = int.TryParse(value.ToString(), out var val);
                if (isSuccess)
                {
                    field.SetValue(entityModel, val);
                }
            }
            else if (field.PropertyType == typeof(float) || field.PropertyType == typeof(float?))
            {
                var isSuccess = float.TryParse(value.ToString(), out var val);
                if (isSuccess)
                {
                    field.SetValue(entityModel, val);
                }
            }
            else if (field.PropertyType == typeof(decimal) || field.PropertyType == typeof(decimal?))
            {
                var val = decimal.Parse(value.ToString(), System.Globalization.NumberStyles.Float);
                field.SetValue(entityModel, val);
            }
            else if (field.PropertyType == typeof(bool) || field.PropertyType == typeof(bool?))
            {
                var isSuccess = bool.TryParse(value.ToString(), out var val);
                if (isSuccess)
                {
                    field.SetValue(entityModel, val);
                }
            }
            else if (field.PropertyType == typeof(Guid) || field.PropertyType == typeof(Guid?))
            {
                var isSuccess = Guid.TryParse(value.ToString(), out var val);
                if (isSuccess)
                {
                    field.SetValue(entityModel, val);
                }
            }
            else
            {
                field.SetValue(entityModel, value);
            }
        }
    }
}
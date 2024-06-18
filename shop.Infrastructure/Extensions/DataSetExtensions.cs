using MicroBase.Share.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MicroBase.Share.Extensions
{
    public static class DataSetExtensions
    {
        public static IEnumerable<T> DataTableToModel<T>(this DataTable dataTable) where T : new()
        {
            var rows = new List<T>();

            var propertiesInfo = typeof(T).GetProperties();
            foreach (DataRow row in dataTable.Rows)
            {
                var entityModel = new T();
                foreach (PropertyInfo propertyInfo in propertiesInfo)
                {
                    var fieldName = propertyInfo.Name;

                    var attributes = propertyInfo.GetCustomAttributes(typeof(CustomDataSetAttribute), true);
                    if (attributes.Any())
                    {
                        var control = attributes[0] as CustomDataSetAttribute;
                        if (control != null)
                        {
                            fieldName = control.Name;
                        }
                    }

                    if (!row.Table.Columns.Contains(fieldName))
                    {
                        continue;
                    }

                    var colVal = row[fieldName];
                    if (colVal != DBNull.Value)
                    {
                        try
                        {
                            // Attempt to convert the value to the appropriate type and set it
                            var targetType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                            var convertedValue = Convert.ChangeType(colVal, targetType);
                            propertyInfo.SetValue(entityModel, convertedValue);
                        }
                        catch (Exception ex)
                        {
                            // Handle conversion or setting errors if necessary
                            Console.WriteLine($"Error setting property {propertyInfo.Name}: {ex.Message}");
                        }
                    }
                }

                rows.Add(entityModel);
            }

            return rows;
        }
    }
}

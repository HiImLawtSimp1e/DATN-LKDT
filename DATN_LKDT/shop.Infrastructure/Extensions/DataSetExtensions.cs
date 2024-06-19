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
        public static IEnumerable<T> DataTableToModel<T>(this DataTable dataTable)
        {
            var rows = new List<T>();

            var propertiesInfo = ((T)Activator.CreateInstance(typeof(T))).GetType().GetProperties();
            foreach (DataRow row in dataTable.Rows)
            {
                var entityModel = (T)Activator.CreateInstance(typeof(T));
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
                        var field = entityModel.GetType().GetProperty(propertyInfo.Name);
                        //if (field.PropertyType == typeof(Guid) || field.PropertyType == typeof(Guid?))
                        //{
                        //    var rawId = BitConverter.ToString((byte[])colVal).Replace("-", "");
                        //    colVal = rawId.OracleRawToGuid();
                        //}
                        //else if (field.PropertyType == typeof(bool) || field.PropertyType == typeof(bool?))
                        //{
                        //    if (colVal.ToString() == "1")
                        //    {
                        //        colVal = true;
                        //    }
                        //    else
                        //    {
                        //        colVal = false;
                        //    }
                        //}

                        entityModel.SetValueToObject<T>(field, colVal);
                    }
                }

                rows.Add(entityModel);
            }

            return rows;
        }
    }
}
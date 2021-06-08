using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OilStationMVC.CommonFolder
{
    public static class Mapper
    {
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {

            var dataList = new List<T>();
            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.CDate(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.CInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.CLng(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.CDec(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, ObjectConverter.CDate(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ObjectConverter.Cstr(dataRow[dtField.Name]), null);
                            }
                        }
                        else if (propertyInfos.PropertyType == typeof(Boolean))
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.CBool(dataRow[dtField.Name]), null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        public static T MapDataRowToObjectOfClass<T>(this DataRow dataRow) where T : new()
        {

            //var dataList = new  T();
            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read datarow column names and types
            var dtlFieldNames = dataRow.Table.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();
            var classObj = new T();
            foreach (var dtField in dtlFieldNames)
            {
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                var field = objFieldNames.Find(x => x.Name == dtField.Name);

                if (field != null)
                {

                    if (propertyInfos.PropertyType == typeof(DateTime))
                    {
                        propertyInfos.SetValue
                        (classObj, ObjectConverter.CDate(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int))
                    {
                        propertyInfos.SetValue
                        (classObj, ObjectConverter.CInt(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long))
                    {
                        propertyInfos.SetValue
                        (classObj, ObjectConverter.CLng(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal))
                    {
                        propertyInfos.SetValue
                        (classObj, ObjectConverter.CDec(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {
                        if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.CDate(dataRow[dtField.Name]), null);
                        }
                        else
                        {
                            propertyInfos.SetValue
                            (classObj, ObjectConverter.Cstr(dataRow[dtField.Name]), null);
                        }
                    }
                    else if (propertyInfos.PropertyType == typeof(Boolean))
                    {
                        propertyInfos.SetValue
                        (classObj, ObjectConverter.CBool(dataRow[dtField.Name]), null);
                    }
                }
            }
            return classObj;
        }
    }
}
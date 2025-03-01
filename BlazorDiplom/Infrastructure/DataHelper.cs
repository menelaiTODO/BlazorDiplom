using BlazorDiplom.ViewModels;
using FuzzyDataDbCore.Models;
using Microsoft.AnalysisServices.AdomdClient;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace BlazorDiplom.Infrastructure
{
    /// <summary>
    /// Вспомогательный класс для работы с данными
    /// </summary>
    internal static class DataHelper
    {
        public static List<T> MolapQuery<T>(this AdomdConnection conn, string commandText, CustomLinguisticVariable? variable = null, FuzzyFunctionData? funcData = null)
        {
            var cmd = new AdomdCommand(commandText, conn);
            var dr = cmd.ExecuteReader();

            return dr.ToList<T>(variable, funcData);
        }

        /// <summary>
        /// Получение DataSource по enum
        /// </summary>
        public static IEnumerable<KeyValuePair<int, string>> GetDatasoureByEnum<TEnum>(bool needNullValue = true)
            where TEnum : Enum
        {
            var dt = from TEnum n in Enum.GetValues(typeof(TEnum))
                     select new KeyValuePair<int, string>(Convert.ToInt32(n), GetEnumDescription(n));

            if (needNullValue)
                dt = dt.Append(new KeyValuePair<int, string>(0, string.Empty));

            return dt.OrderBy(item => item.Key);
        }

        /// <summary>
        /// DataReader To List
        /// </summary>
        public static List<T> ToList<T>(this IDataReader dr, CustomLinguisticVariable? variable = null, FuzzyFunctionData? funcData = null)
        {
            var list = new List<T>();

            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();

                var filterResult = true;

                foreach (var prop in obj!.GetType().GetProperties())
                {
                    var columnDescription = GetPropertyDescription(obj, prop.Name);

                    if (variable is not null && funcData is not null && columnDescription == variable.MeasureName)
                    {
                        var columnOrdinal = dr.GetOrdinal(columnDescription!);
                        var value = dr[columnOrdinal];

                        var result = funcData.MemberShipFunction(variable!.Points!.Select(item => item.XValue).ToArray(), Convert.ToDouble(value));

                        if (result < variable.MinIndex)
                        {
                            dr.Read();

                            filterResult = false;

                            break;
                        }
                    }

                    if (ExistsDataReaderColumn(dr, columnDescription!))
                        CopyColumnValueToProperty(dr, obj, prop);
                }

                if (!filterResult)
                    continue;

                list.Add(obj);
            }
            return list;
        }
        private static void CopyColumnValueToProperty<T>(IDataReader dr, T obj, PropertyInfo prop)
        {
            try
            {
                var columnDescription = GetPropertyDescription(obj, prop.Name);
                var columnOrdinal = dr.GetOrdinal(columnDescription!);
                var value = dr[columnOrdinal];
                var canBeNull = !prop.PropertyType.IsValueType || (Nullable.GetUnderlyingType(prop.PropertyType) != null);
                var castToType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (canBeNull && value == null)
                    prop.SetValue(obj, null, null);
                else
                    prop.SetValue(obj, Convert.ChangeType(value, castToType, CultureInfo.InvariantCulture), null);
            }
            catch { }
        }

        private static bool ExistsDataReaderColumn(IDataReader dr, string propertyName)
        {
            try
            {
                var obj = dr[propertyName];
                return true;
            }
            catch { return false; }
        }

        private static string? GetPropertyDescription(object value, string propname)
        {
            var propinfo = value.GetType().GetProperty(propname);
            var attributes =
                (DescriptionAttribute[])propinfo!.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        private static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType()?.GetField(value.ToString());

            var attributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}

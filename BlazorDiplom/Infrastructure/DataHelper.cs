using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BlazorDiplom.Infrastructure
{
    /// <summary>
    /// Вспомогательный класс для работы с данными
    /// </summary>
    internal static class DataHelper
    {
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

        /// <summary>
        /// Получение значения свойства по наименованию меры
        /// </summary>
        public static double? GetPropertyValueByColumnName(object obj, string columnName)
        {
            var type = obj.GetType();

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var columnAttribute = property.GetCustomAttributes(typeof(ColumnAttribute), false)
                    .FirstOrDefault() as ColumnAttribute;

                if (columnAttribute != null && columnAttribute.Name == columnName)
                {
                    return Convert.ToDouble(property.GetValue(obj)?.ToString());
                }
            }

            return null;
        }
    }
}

using System.Data;
using System.Globalization;
using System.Reflection;

namespace BlazorDiplom.Infrastructure
{
    internal static class DataHelper
    {
        public static List<T> Query<T>(this AdomdConnection conn, string commandText, params object[] parameters)
        {
            var cmd = new AdomdCommand(commandText, conn);
            var dr = cmd.ExecuteReader();

            return dr.ToList<T>();
        }

        public static List<T> ToList<T>(this IDataReader dr)
        {
            var list = new List<T>();

            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();

                foreach (var prop in obj!.GetType().GetProperties())
                {
                    var columnDescription = GetPropertyDescription(obj, prop.Name);

                    if (ExistsDataReaderColumn(dr, columnDescription!))
                        CopyColumnValueToProperty(dr, obj, prop);
                }

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
                (OLAPMemberNameAttribute[])propinfo!.GetCustomAttributes(
                typeof(OLAPMemberNameAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}

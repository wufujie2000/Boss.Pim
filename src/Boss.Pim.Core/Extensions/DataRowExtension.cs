using System;
using System.Data;

namespace Boss.Pim.Extensions
{
    /// <summary>
    /// DataRow 扩展方法
    /// </summary>
    public static class DataRowExtension
    {
        public static string ToString(this DataRow row, int column)
        {
            object val = row[column];
            return ToString(val);
        }

        public static string ToString(this DataRow row, string column)
        {
            object val = row[column];
            return ToString(val);
        }

        public static Guid ToGuid(this DataRow row, int column, Guid result)
        {
            object val = row[column];
            return ToGuid(val, result);
        }

        public static Guid ToGuid(this DataRow row, string column, Guid result)
        {
            object val = row[column];
            return ToGuid(val, result);
        }

        public static Guid ToGuid(this DataRow row, string column)
        {
            return row.ToGuid(column, Guid.Empty);
        }

        public static int ToInt32(this DataRow row, int column, int result = 0)
        {
            object val = row[column];
            return ToInt32(val, result);
        }

        public static int ToInt32(this DataRow row, string column, int result = 0)
        {
            object val = row[column];
            return ToInt32(val, result);
        }

        public static DateTime ToDateTime(this DataRow row, int column, DateTime result)
        {
            object val = row[column];
            return ToDateTime(val, result);
        }

        public static DateTime ToDateTime(this DataRow row, string column, DateTime result)
        {
            object val = row[column];
            return ToDateTime(val, result);
        }
        public static DateTime ToDateTime(this DataRow row, int column, string result)
        {
            return row.ToDateTime(column, Convert.ToDateTime(result));
        }
        public static DateTime ToDatetime(this DataRow row, string column, string result)
        {
            return row.ToDateTime(column, Convert.ToDateTime(result));
        }

        #region private

        private static string ToString(object val)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return string.Empty;
            }
            return val.ToString();
        }

        private static Guid ToGuid(object val, Guid result)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            Guid dt;
            if (Guid.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        private static int ToInt32(object val, int result = 0)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            int dt;
            if (int.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        private static DateTime ToDateTime(object val, DateTime result)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            DateTime dt;
            if (DateTime.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        #endregion private
    }
}
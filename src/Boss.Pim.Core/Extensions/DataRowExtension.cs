using System;
using System.Data;

namespace Boss.Pim.Extensions
{
    /// <summary>
    /// DataRow 扩展方法
    /// </summary>
    public static class DataRowExtension
    {
        public static string TryToString(this DataRow row, int column)
        {
            object val = row[column];
            return TryToString(val);
        }

        public static string TryToString(this DataRow row, string column)
        {
            object val = row[column];
            return TryToString(val);
        }

        public static Guid TryToGuid(this DataRow row, int column, Guid result)
        {
            object val = row[column];
            return TryToGuid(val, result);
        }

        public static Guid TryToGuid(this DataRow row, string column, Guid result)
        {
            object val = row[column];
            return TryToGuid(val, result);
        }

        public static Guid TryToGuid(this DataRow row, string column)
        {
            return row.TryToGuid(column, Guid.Empty);
        }

        public static decimal TryToDecimal(this DataRow row, int column, decimal result = 0)
        {
            object val = row[column];
            return TryToDecimal(val, result);
        }

        public static int TryToInt32(this DataRow row, int column, int result = 0)
        {
            object val = row[column];
            return TryToInt32(val, result);
        }

        public static int TryToInt32(this DataRow row, string column, int result = 0)
        {
            object val = row[column];
            return TryToInt32(val, result);
        }

        public static DateTime TryToDateTime(this DataRow row, int column, DateTime result)
        {
            object val = row[column];
            return TryToDateTime(val, result);
        }

        public static DateTime? TryToDateTimeOrNull(this DataRow row, int column)
        {
            object val = row[column];
            return TryToDateTimeOrNull(val);
        }

        public static DateTime TryToDateTime(this DataRow row, string column, DateTime result)
        {
            object val = row[column];
            return TryToDateTime(val, result);
        }
        public static DateTime TryToDateTime(this DataRow row, int column, string result)
        {
            return row.TryToDateTime(column, Convert.ToDateTime(result));
        }
        public static DateTime TryToDatetime(this DataRow row, string column, string result)
        {
            return row.TryToDateTime(column, Convert.ToDateTime(result));
        }

        #region private

        private static string TryToString(object val)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return string.Empty;
            }
            return val.ToString();
        }

        private static Guid TryToGuid(object val, Guid result)
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

        private static int TryToInt32(object val, int result = 0)
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

        private static decimal TryToDecimal(object val, decimal result = 0)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            decimal dt;
            if (decimal.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        public static DateTime? TryToDateTimeOrNull(object val, DateTime? result = null)
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

        private static DateTime TryToDateTime(object val, DateTime result)
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
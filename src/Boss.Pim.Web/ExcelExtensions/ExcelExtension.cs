using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Abp.Collections.Extensions;
using NPOI.Extension;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Boss.Pim
{
    /// <summary>
    /// excel扩展
    /// </summary>
    public static class ExcelExtension
    {
        #region 加载Excel数据

        /// <summary>
        /// excel转IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workbook"></param>
        /// <param name="startRow"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="valueConverter"></param>
        /// <returns></returns>
        public static IEnumerable<T> Load<T>(this IWorkbook workbook, int startRow = 1, int sheetIndex = 0,
            ValueConverter valueConverter = null) where T : class, new()
        {
            var sheet = workbook.GetSheetAt(sheetIndex);

            // get the physical rows
            var rows = sheet.GetRowEnumerator();

            IRow headerRow = null;

            // get the writable properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                     BindingFlags.SetProperty);

            // find out the attributes
            var haventCols = true;
            var attributes = new ColumnAttribute[properties.Length];
            for (var j = 0; j < properties.Length; j++)
            {
                var property = properties[j];
                var attrs = property.GetCustomAttributes(typeof(ColumnAttribute), true) as ColumnAttribute[];
                if (attrs != null && attrs.Length > 0)
                {
                    attributes[j] = attrs[0];

                    haventCols = false;
                }
                else
                {
                    attributes[j] = null;
                }
            }

            var list = new List<T>();
            int idx = 0;
            while (rows.MoveNext())
            {
                var row = rows.Current as IRow;

                if (idx == 0)
                    headerRow = row;
                idx++;

                if (row.RowNum < startRow)
                {
                    continue;
                }

                var item = new T();
                for (int i = 0; i < properties.Length; i++)
                {
                    var prop = properties[i];

                    int index = i;
                    var title = string.Empty;
                    var autoIndex = false;

                    if (!haventCols)
                    {
                        var column = attributes[i];
                        if (column == null)
                            continue;
                        else
                        {
                            index = column.Index;
                            title = column.Title;
                            autoIndex = column.AutoIndex;

                            // Try to autodiscover index from title and cache
                            if (!string.IsNullOrEmpty(title))
                            {
                                foreach (var cell in headerRow.Cells)
                                {
                                    if (!string.IsNullOrEmpty(cell.StringCellValue))
                                    {
                                        if (cell.StringCellValue.Equals(title))
                                        {
                                            index = cell.ColumnIndex;

                                            // cache
                                            column.Index = index;

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (index > -1)
                    {
                        var value = row.GetCellValue(index);
                        if (valueConverter != null)
                        {
                            value = valueConverter(row.RowNum, index, value);
                        }
                        if (value != null)
                        {
                            // property type

                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            if (propType == typeof(DateTime))
                            {
                                DateTime dtTemp = DateTime.Now;
                                if (DateTime.TryParse(value.ToString().Replace("年", "").Replace("月", "").Replace("日", ""), out dtTemp))
                                {
                                    prop.SetValue(item, dtTemp, null);
                                }
                            }
                            else
                            {
                                var safeValue = Convert.ChangeType(value, propType, CultureInfo.CurrentCulture);
                                prop.SetValue(item, safeValue, null);
                            }
                        }
                    }
                }

                list.Add(item);
            }

            return list;
        }

        private static object GetCellValue(this IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                return null;
            }

            switch (cell.CellType)
            {
                // This is a trick to get the correct value of the cell.
                // NumericCellValue will return a numeric value no matter the cell value is a date or a number.
                case CellType.Numeric:
                    return cell.ToString();

                case CellType.String:
                    return cell.StringCellValue;

                case CellType.Boolean:
                    return cell.BooleanCellValue;

                case CellType.Error:
                    return cell.ErrorCellValue;

                // how?
                case CellType.Formula:
                    return cell.StringCellValue;

                case CellType.Blank:
                case CellType.Unknown:
                default:
                    return null;
            }
        }

        internal static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        #endregion

        #region ExcelToDataTable

        /// <summary>
        /// 读取excel并转换为datatable，返回后的DataTable，第一列，第一行下标均为0
        /// 默认第一行为标头，第一个sheet
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="headerRowIndex">列头所在行号，-1表示没有列头</param>
        /// <param name="needHeader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this Stream fileStream, int headerRowIndex = 0, bool needHeader = true)
        {
            IWorkbook hssfworkbook;
            using (fileStream)
            {
                hssfworkbook = new XSSFWorkbook(fileStream);
            }
            var sheet = hssfworkbook.GetSheetAt(0);
            var dt = ToDataTable(sheet, headerRowIndex, needHeader);
            return dt;
        }

        /// <summary>
        /// 读取excel并转换为datatable，返回后的DataTable，第一列，第一行下标均为0
        /// 默认第一行为标头，第一个sheet
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="headerRowIndex">列头所在行号，-1表示没有列头</param>
        /// <param name="needHeader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string strFileName, int headerRowIndex = 0, bool needHeader = true)
        {
            IWorkbook hssfworkbook;
            using (var file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new XSSFWorkbook(file);
            }
            var sheet = hssfworkbook.GetSheetAt(0);
            var dt = ToDataTable(sheet, headerRowIndex, needHeader);
            return dt;
        }

        /// <summary>
        /// 将指定sheet中的数据导出到datatable中
        /// </summary>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="headerRowIndex">列头所在行号，-1表示没有列头</param>
        /// <param name="needHeader"></param>
        /// <returns></returns>
        private static DataTable ToDataTable(ISheet sheet, int headerRowIndex = 0, bool needHeader = true)
        {
            DataTable table = new DataTable();
            try
            {
                IRow headerRow;
                int cellCount;
                if (headerRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(headerRowIndex);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)//(int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i));
                                table.Columns.Add(column);
                            }
                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                for (int i = (headerRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        var row = sheet.GetRow(i) ?? sheet.CreateRow(i);

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                dataRow[j] = str;
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;

                                        case CellType.Numeric:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;

                                        case CellType.Boolean:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;

                                        case CellType.Error:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;

                                        case CellType.Formula:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.String:
                                                    string strFormula = row.GetCell(j).StringCellValue;
                                                    if (!string.IsNullOrEmpty(strFormula))
                                                    {
                                                        dataRow[j] = strFormula;
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;

                                                case CellType.Numeric:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue, CultureInfo.InvariantCulture);
                                                    break;

                                                case CellType.Boolean:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;

                                                case CellType.Error:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;

                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;

                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch
                            {
                                //wl.WriteLogs(exception.ToString());
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch
                    {
                        //wl.WriteLogs(exception.ToString());
                    }
                }
            }
            catch
            {
                //wl.WriteLogs(exception.ToString());
            }
            return table;
        }

        #endregion ExcelToDataTable

        #region ToExcel

        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static MemoryStream ToExcel(this DataTable table, string sheetName)
        {
            MemoryStream ms = new MemoryStream();

            using (table)
            {
                IWorkbook workbook = new XSSFWorkbook();
                workbook.CreateNewSheet(table, sheetName);
                workbook.Write(ms);
            }
            return ms;
        }

        /// <summary>
        /// List 转换成 Excel文档流 使用反射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <param name="headerTexts"></param>
        /// <returns></returns>
        public static MemoryStream ToExcel<T>(this IReadOnlyList<T> list, string sheetName, params string[] headerTexts)
        {
            if (list != null && list.Count > 0)
            {
                DataTable dt = new DataTable();
                foreach (var t in headerTexts)
                {
                    dt.Columns.Add(t);
                }
                foreach (var t in list)
                {
                    var type = t.GetType();
                    var dr = dt.NewRow();
                    foreach (string t1 in headerTexts)
                    {
                        var propertyInfo = type.GetProperty(t1);
                        if (propertyInfo != null)
                        {
                            dr[propertyInfo.Name] = propertyInfo.GetValue(t, null);
                        }
                    }
                    dt.Rows.Add(dr);
                }
                return ToExcel(dt, sheetName);
            }
            return null;
        }

        /// <summary>
        /// List 转换成 Excel文档流 推荐使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <param name="headerTexts"></param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public static MemoryStream ToExcel<T>(this IReadOnlyList<T> list, string sheetName, string[] headerTexts, params Func<T, object>[] propertySelectors)
        {
            MemoryStream ms = new MemoryStream();

            var workbook = list.ToWorkbook(sheetName, headerTexts, propertySelectors);

            workbook.Write(ms);

            return ms;
        }

        /// <summary>
        /// 创建Excel  Sheet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static ISheet CreateNewSheet(this IWorkbook workbook, DataTable table, string sheetName)
        {
            var sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);

            // handling header.
            foreach (DataColumn column in table.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value

            // handling value.
            int rowIndex = 1;

            foreach (DataRow row in table.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in table.Columns)
                {
                    var val = row[column].ToString();
                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        if (val.Trim() == "0")
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(0);
                            continue;
                        }
                        if (IsInt(val) && !val.StartsWith("0") && val.Length < 6)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToInt32(val));
                            continue;
                        }
                        if (IsNumeric(val) && !val.StartsWith("0") && val.Length < 6)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(val));
                            continue;
                        }
                    }
                    dataRow.CreateCell(column.Ordinal).SetCellValue(val);
                }

                rowIndex++;
            }
            AutoSizeColumns(sheet);
            return sheet;
        }

        /// <summary>
        /// 创建Excel  Sheet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workbook"></param>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <param name="sheetHeaders"></param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public static ISheet CreateNewSheet<T>(this IWorkbook workbook, IReadOnlyList<T> list, string sheetName, string[] sheetHeaders, params Func<T, object>[] propertySelectors)
        {
            var sheet = workbook.CreateSheet(sheetName);

            AddHeader(sheet, sheetHeaders);

            int startRowIndex = 1;
            AddObjects(sheet, startRowIndex, list, propertySelectors);

            AutoSizeColumns(sheet);

            return sheet;
        }

        /// <summary>
        /// List 转为 IWorkbook
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sheetName"></param>
        /// <param name="headerTexts"></param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public static XSSFWorkbook ToWorkbook<T>(this IReadOnlyList<T> list, string sheetName, string[] headerTexts, params Func<T, object>[] propertySelectors)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            workbook.CreateNewSheet(list, sheetName, headerTexts, propertySelectors);
            return workbook;
        }

        /// <summary>
        /// 设置简单数据验证
        /// </summary>
        /// <param name="dicSheet"></param>
        /// <param name="workbook"></param>
        /// <param name="importSheet"></param>
        /// <param name="validataIndex"></param>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public static void SetSimpleValidationData(this XSSFWorkbook workbook, ISheet dicSheet, ISheet importSheet, int validataIndex, int index, string name)
        {
            var rang = workbook.CreateName();
            rang.RefersToFormula = $"{dicSheet.SheetName}!${(char)('A' + index)}$2:${(char)('A' + index)}${dicSheet.LastRowNum + 1}";
            rang.NameName = name;
            var regions = new CellRangeAddressList(1, 65535, validataIndex, validataIndex);
            var dataValidate = importSheet.GetDataValidationHelper()
                .CreateValidation(importSheet.GetDataValidationHelper().CreateFormulaListConstraint(rang.NameName), regions);
            dataValidate.CreateErrorBox("数据错误", "数据验证不通过");
            dataValidate.ShowErrorBox = true;
            importSheet.AddValidationData(dataValidate);
        }

        /// <summary>
        /// 创建引用方法
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="importSheet"></param>
        /// <param name="dicSheet"></param>
        /// <param name="index"></param>
        /// <param name="funcName"></param>
        public static void CreateVlookupFunc(this XSSFWorkbook workbook, ISheet importSheet, ISheet dicSheet, int index, string funcName)
        {
            var func = workbook.CreateName();
            func.RefersToFormula =
                $"IF(ISBLANK({importSheet.SheetName}!$A1),\"\",IF(ISBLANK(VLOOKUP({importSheet.SheetName}!$A1,{dicSheet.SheetName}!$A:$D,{index},0)),\"\",VLOOKUP({importSheet.SheetName}!$A1,{dicSheet.SheetName}!$A:$D,{index},0)))";
            func.NameName = funcName;
        }

        /// <summary>
        /// 创建引用方法
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="importSheet"></param>
        /// <param name="dicSheet"></param>
        /// <param name="triggerColumn"></param>
        /// <param name="index"></param>
        /// <param name="funcName"></param>
        public static void CreateVlookupFunc(this XSSFWorkbook workbook, ISheet importSheet, ISheet dicSheet, string triggerColumn, int index, string funcName)
        {
            var func = workbook.CreateName();
            func.RefersToFormula =
                $"IF(ISBLANK({importSheet.SheetName}!${triggerColumn}1),\"\",VLOOKUP({importSheet.SheetName}!${triggerColumn}1,{dicSheet.SheetName}!$A:$D,{index},0))";
            func.NameName = funcName;
        }

        #endregion ToExcel

        #region Private

        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private static void AutoSizeColumns(ISheet sheet)
        {
            if (sheet.PhysicalNumberOfRows > 0)
            {
                IRow headerRow = sheet.GetRow(0);

                for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
        }

        private static void AddHeader(ISheet sheet, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }
            IRow row = sheet.CreateRow(0);
            for (var columnIndex = 0; columnIndex < headerTexts.Length; columnIndex++)
            {
                var headerText = headerTexts[columnIndex];
                var cell = row.CreateCell(columnIndex);
                cell.SetCellValue(headerText);

                IFont font = new XSSFFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                cell.CellStyle.SetFont(font);
            }
        }

        private static void AddObjects<T>(ISheet sheet, int startRowIndex, IReadOnlyList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items == null || items.Count <= 0 || propertySelectors.IsNullOrEmpty())
            {
                return;
            }
            for (var i = 0; i < items.Count; i++)
            {
                var dataRow = sheet.CreateRow(i + startRowIndex);
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var val = propertySelectors[j](items[i])?.ToString() ?? "";
                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        if (val.Trim() == "0")
                        {
                            dataRow.CreateCell(j).SetCellValue(0);
                            continue;
                        }
                        if (IsInt(val) && !val.StartsWith("0") && val.Length < 6)
                        {
                            dataRow.CreateCell(j).SetCellValue(Convert.ToInt32(val));
                            continue;
                        }
                        if (IsNumeric(val) && !val.StartsWith("0") && val.Length < 6)
                        {
                            dataRow.CreateCell(j).SetCellValue(Convert.ToDouble(val));
                            continue;
                        }
                    }
                    dataRow.CreateCell(j).SetCellValue(val);
                }
            }
        }

        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        #endregion Private
    }
}
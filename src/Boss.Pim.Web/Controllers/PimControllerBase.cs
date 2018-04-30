using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace Boss.Pim.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class PimControllerBase : AbpController
    {
        protected PimControllerBase()
        {
            LocalizationSourceName = PimConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }


        protected DataTable CheckGetDataTable()
        {
            if (Request.Files == null || Request.Files.Count <= 0)
            {
                throw new UserFriendlyException("不存在文件");
            }
            var file = Request.Files[0];
            if (file == null || file.ContentLength <= 0)
            {
                throw new UserFriendlyException("不存在文件");
            }
            var dt = file.InputStream.ToDataTable();
            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new UserFriendlyException("文件不存在数据");
            }
            return dt;
        }

        protected List<T> CheckTableToList<T>(Func<System.Data.DataRow, int, T> addList)
        {
            var dt = CheckGetDataTable();

            List<T> list = new List<T>();
            var rowIndex = 1;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                rowIndex++;
                list.Add(addList(row, rowIndex));
            }
            return list;
        }

        protected string CheckAndGetRow(int rowIndex, int columnIndex, System.Data.DataRow row, string msg = "", Func<string, bool> otherCheck = null)
        {
            var val = row[columnIndex].ToString().Trim();
            if (string.IsNullOrWhiteSpace(val))
            {
                throw new UserFriendlyException($"对比模板，检查第 {columnIndex + 1} 列，第 {rowIndex} 行 {msg} 值有误");
            }
            if (otherCheck?.Invoke(val) ?? false)
            {
                throw new UserFriendlyException($"对比模板，检查第 {columnIndex + 1} 列，第 {rowIndex} 行 {msg} 值为 {val} 有误");
            }
            return val;
        }

        protected FileResult ReturnExcelFile(MemoryStream stream, string excelFileName)
        {
            using (stream)
            {
                return File(stream.ToArray(), "application/vnd.ms-excel", $"{excelFileName}{DateTime.Now:yyyy-MM-dd HH-mm-ss}.xlsx");
            }
        }
    }
}
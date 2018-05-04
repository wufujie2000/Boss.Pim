using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Services;
using Dapper;

namespace Boss.Pim.Funds
{
    public class NetWorthAppService : AsyncCrudAppService<NetWorth, NetWorthDto, Guid>, INetWorthAppService
    {
        public FundManager FundManager { get; set; }
        public NetWorthManager NetWorthManager { get; set; }
        public NetWorthPeriodAnalyseManager NetWorthPeriodAnalyseManager { get; set; }

        public NetWorthAppService(IRepository<NetWorth, Guid> repository) : base(repository)
        {
        }

        [UnitOfWork(false)]
        public async Task Download3Days()
        {
            Logger.Info("开始下载更新 NetWorth ");
            await DownloadCheck(5);
            Logger.Info("更新 NetWorth 完成");
        }



        public async Task DownloadByDays(string fundCode, int page, int size)
        {
            var fundCodes = fundCode.ToStringArray();
            var list = await AsyncQueryableExecuter.ToListAsync(
              FundManager.GetQuery().Where(a => fundCodes.Contains(a.Code)).Select(a => new { a.Code, a.DkhsCode })
              );
            List<NetWorth> notExistsList = new List<NetWorth>();
            foreach (var fund in list)
            {
                var willExecList = await NetWorthManager.GetNoExistsNetWorth(fund.Code, fund.DkhsCode, size, page);
                if (willExecList != null && willExecList.Count > 0)
                {
                    notExistsList.AddRange(willExecList);
                }
            }
            await FundManager.Insert(notExistsList);
        }

        public async Task DownloadCheck()
        {
            await DownloadCheck(120);
        }

        private async Task DownloadCheck(int days)
        {
            var sql = @"
SELECT DISTINCT
    fun.DkhsCode,
    fun.Code
FROM dbo.FundCenter_Funds fun
    LEFT JOIN dbo.FundCenter_NetWorths net
        ON fun.Code = net.FundCode
WHERE fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      AND NOT EXISTS
(
    SELECT 1 FROM dbo.FundCenter_NotTradeFunds nt WHERE nt.FundCode = fun.Code
)
      AND (
              Date >= GETDATE() - @days
              OR net.Id IS NULL
          )
GROUP BY fun.Code,
         fun.DkhsCode
HAVING COUNT(1) <
(
    SELECT TOP 1
        COUNT(1)
    FROM dbo.FundCenter_NetWorths
    WHERE Date >= GETDATE() - @days
    GROUP BY FundCode
    ORDER BY COUNT(1) DESC
)
";
            using (var conn = new SqlConnection(SQLUtil.DefaultConnStr))
            {
                var list = conn.Query(sql, new { days = days });

                List<NetWorth> notExistsList = new List<NetWorth>();
                foreach (var fund in list)
                {
                    string fundCode = fund.Code;
                    string dkhsCode = fund.DkhsCode;
                    var willExecList = await NetWorthManager.GetNoExistsNetWorth(fundCode, dkhsCode, days);
                    if (willExecList != null && willExecList.Count > 0)
                    {
                        notExistsList.AddRange(willExecList);
                    }
                }
                await FundManager.Insert(notExistsList);
            }
        }

        #region 初始化下载

        //public async Task InitDownload()
        //{
        //    IQueryable<Fund> fundsQuery = await FundDomainService.GetFundsQuery();
        //    var list = await AsyncQueryableExecuter.ToListAsync(fundsQuery.Select(a => new { a.Code, a.DkhsCode }));
        //    foreach (var fund in list)
        //    {
        //        await CheckInsert(fund.Code, fund.DkhsCode);
        //    }
        //}
        //private async Task CheckInsert(string fundCode, string dkhsFundCode)
        //{
        //    try
        //    {
        //        if (await CheckIsNeedNext(fundCode, dkhsFundCode))
        //        {
        //            var modellist = await NetWorthManager.DownloadNetWorthByDkhs(fundCode, dkhsFundCode);
        //            if (modellist.Count > 0)
        //            {
        //                await FundDomainService.CheckInsertNetWorth(modellist, fundCode);
        //                var list = NetWorthPeriodAnalyseManager.CalcGuessPrejudgementSplit10Days(fundCode, modellist, DateTime.Now.Date);

        //                await NetWorthPeriodAnalyseManager.CheckAndInsert(list);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(fundCode + " " + e.Message, e);
        //    }
        //}

        //private async Task<bool> CheckIsNeedNext(string fundCode, string dkhsFundCode, int size = 3, int page = 1)
        //{
        //    bool isNeedNext = false;
        //    var checkList = await NetWorthManager.DownloadNetWorthByDkhs(fundCode, dkhsFundCode, size, page);
        //    if (checkList.Count > 0)
        //    {
        //        var isExists = await FundDomainService.CheckInsertNetWorth(checkList, fundCode);
        //        if (!isExists)
        //        {
        //            isNeedNext = true;
        //        }
        //    }

        //    return isNeedNext;
        //}

        #endregion 初始化下载
    }
}
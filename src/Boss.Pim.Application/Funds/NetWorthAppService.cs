using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.DomainServices;
using Boss.Pim.Funds.Dto;
using Dapper;

namespace Boss.Pim.Funds
{
    public class NetWorthAppService : AsyncCrudAppService<NetWorth, NetWorthDto, Guid>, INetWorthAppService
    {
        public FundDomainService FundDomainService { get; set; }
        public NetWorthManager NetWorthManager { get; set; }
        public NetWorthPeriodAnalyseManager NetWorthPeriodAnalyseManager { get; set; }

        public NetWorthAppService(IRepository<NetWorth, Guid> repository) : base(repository)
        {
        }

        [UnitOfWork(false)]
        public async Task Download3Days()
        {
            Logger.Info("开始下载更新 Fund ");
            IQueryable<Fund> fundsQuery = await FundDomainService.GetFundsQuery();
            var list = await AsyncQueryableExecuter.ToListAsync(fundsQuery.Select(a => new { a.Code, a.DkhsCode }));
            foreach (var fund in list)
            {
                try
                {
                    var modellist = await NetWorthManager.DownloadNetWorthByDkhs(fund.Code, fund.DkhsCode, 3);
                    if (modellist.Count > 0)
                    {
                        await FundDomainService.CheckInsertNetWorth(modellist, fund.Code);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(fund.Code + " " + e.Message, e);
                }
            }
            Logger.Info("更新 Fund 完成");
        }

        public async Task DownloadByDays(string fundCode, int page, int size)
        {
            var fundCodes = fundCode.ToStringArray();
            var list = await AsyncQueryableExecuter.ToListAsync(
              FundDomainService.GetQuery().Where(a => fundCodes.Contains(a.Code)).Select(a => new { a.Code, a.DkhsCode })
              );
            foreach (var fund in list)
            {
                var modellist = await NetWorthManager.DownloadNetWorthByDkhs(fund.Code, fund.DkhsCode, size, page);
                if (modellist.Count > 0)
                {
                    await FundDomainService.CheckInsertNetWorth(modellist, fund.Code);
                }
            }
        }

        public async Task DownloadCheck()
        {
            var sql = @"
SELECT DISTINCT
    fun.DkhsCode,
    fun.Code
FROM dbo.FundCenter_Funds fun
    INNER JOIN dbo.FundCenter_NetWorths net
        ON fun.Code = net.FundCode
WHERE Date > '2018-03-30'
GROUP BY fun.Code,
         fun.DkhsCode
HAVING COUNT(1) < 8;
";
            using (var conn = new SqlConnection(SQLUtil.DefaultConnStr))
            {
                var list = conn.Query(sql);
                foreach (var fund in list)
                {
                    var modellist = await NetWorthManager.DownloadNetWorthByDkhs(fund.Code, fund.DkhsCode, 8);
                    if (modellist.Count > 0)
                    {
                        await FundDomainService.CheckInsertNetWorth(modellist, fund.Code);
                    }
                }
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
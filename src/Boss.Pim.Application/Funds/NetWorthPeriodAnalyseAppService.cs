using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.DomainServices;
using Boss.Pim.Funds.Dto;
using Dapper;

namespace Boss.Pim.Funds
{
    public class NetWorthPeriodAnalyseAppService : AsyncCrudAppService<NetWorthPeriodAnalyse, NetWorthPeriodAnalyseDto, Guid>, INetWorthPeriodAnalyseAppService
    {
        public IRepository<TradeRecord, Guid> TradeRecordRepository { get; set; }
        public NetWorthPeriodAnalyseManager NetWorthPeriodAnalyseManager { get; set; }
        public FundDomainService FundDomainService { get; set; }
        public IRepository<NetWorth, Guid> NetWorthRepository { get; set; }
        public NetWorthPeriodAnalyseAppService(IRepository<NetWorthPeriodAnalyse, Guid> repository) : base(repository)
        {
        }


        public async Task DownoadByFundCode(string fundCodes)
        {
            var funds = fundCodes.ToStringArray();
            await Insert(funds);
        }


        public async Task AsyncDownoad()
        {
            int size = 50;
            int page = 1;

            while (true)
            {
                var isLast = await DownloadByPager(page, size);
                if (isLast)
                {
                    break;
                }
                page++;
            }

        }

        private async Task<bool> DownloadByPager(int page, int size)
        {
            bool isLast = false;
            Logger.Info($"开始下载 NetWorthPeriodAnalyse 第{page}页，每页{size}条");
            using (var conn = new SqlConnection(SQLUtil.DefaultConnStr))
            {
                conn.Execute(@"
DELETE dbo.FundCenter_NetWorthPeriodAnalyses
WHERE Id IN
      (
          SELECT MAX(CONVERT(VARCHAR(64), Id))
          FROM dbo.FundCenter_NetWorthPeriodAnalyses
          GROUP BY FundCode,
                   PeriodStartDate,
                   PeriodDays
          HAVING COUNT(1) > 1
      );
            ");
                var funds = FundDomainService.GetQuery().OrderBy(a => a.Id).PageIndex(page, size).Select(a => a.Code).ToList();
                if (funds.Count < size)
                {
                    isLast = true;
                }
                if (funds.Count > 0)
                {
                    await Insert(funds);
                }
            }
            Logger.Info($"NetWorthPeriodAnalyse 下载完成 第{page}页，每页{size}条");
            return isLast;
        }

        public async Task Insert(ICollection<string> funds)
        {
            var statrDate = DateTime.Now.Date;
            var minStartDate = statrDate.AddDays(-370);
            foreach (var fund in funds)
            {
                if (string.IsNullOrWhiteSpace(fund))
                {
                    continue;
                }
                try
                {
                    var newWorthModellist = await NetWorthRepository.GetAllListAsync(a => fund == a.FundCode && a.Date >= minStartDate);

                    var itemmodellist = NetWorthPeriodAnalyseManager.CalcGuessPrejudgementSplit10Days(fund, newWorthModellist, statrDate);
                    if (itemmodellist != null)
                    {
                        await NetWorthPeriodAnalyseManager.CheckAndInsert(itemmodellist, fund, statrDate);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(fund + " " + e.Message, e);
                }
            }
        }
    }
}
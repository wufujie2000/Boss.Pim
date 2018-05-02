using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using AutoMapper.QueryableExtensions;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Jobs;
using Boss.Pim.Funds.Services;
using Dapper;

namespace Boss.Pim.Funds
{
    public class NetWorthPeriodAnalyseAppService : AsyncCrudAppService<NetWorthPeriodAnalyse, NetWorthPeriodAnalyseDto, Guid>, INetWorthPeriodAnalyseAppService
    {
        public IBackgroundJobManager BackgroundJobManager { get; set; }
        public IRepository<TradeRecord, Guid> TradeRecordRepository { get; set; }
        public NetWorthPeriodAnalyseManager NetWorthPeriodAnalyseManager { get; set; }
        public FundManager FundDomainService { get; set; }
        public IRepository<NetWorth, Guid> NetWorthRepository { get; set; }
        public NetWorthPeriodAnalyseAppService(IRepository<NetWorthPeriodAnalyse, Guid> repository) : base(repository)
        {
        }

        public override Task<PagedResultDto<NetWorthPeriodAnalyseDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var today = DateTime.Now.Date;
            var list = Repository.GetAll().Where(a => a.PeriodStartDate == today && a.PeriodDays <= 120).ToList();
            return base.GetAll(input);
        }

        public async Task<List<NetWorthPeriodAnalyseDto>> Analyse(List<string> fundCodes, DateTime date)
        {
            var periodStartDate = date.Date;
            var list = await AsyncQueryableExecuter.ToListAsync(
                Repository.GetAll().Where(a => a.PeriodStartDate == periodStartDate && fundCodes.Contains(a.FundCode) && a.PeriodDays <= 120).ProjectTo<NetWorthPeriodAnalyseDto>()
                );
            return list;
        }

        public async Task DownoadByFundCode(string fundCodes)
        {
            var funds = fundCodes.ToStringArray();
            await NetWorthPeriodAnalyseManager.Insert(funds);
        }


        public async Task AsyncDownoad()
        {
            int size = 50;
            int page = 1;
            //每次查询都在减少，因此永远是第1页
            while (true)
            {
                var isLast = await DownloadByPager(page, size);
                if (isLast)
                {
                    break;
                }
            }

        }

        private async Task<bool> DownloadByPager(int page, int size)
        {
            bool isLast = false;
            Logger.Info($"开始下载 NetWorthPeriodAnalyse 第{page}页，每页{size}条");
            var today = DateTime.Now.Date;
            var notquery = Repository.GetAll()
                .Where(a => a.PeriodStartDate == today)
                .Select(a => a.FundCode).Distinct();
            var funds = FundDomainService.GetQuery()
                .Where(a => !notquery.Contains(a.Code))
                .OrderBy(a => a.Id).PageIndex(page, size).Select(a => a.Code).ToList();
            if (funds.Count < size)
            {
                isLast = true;
            }
            if (funds.Count > 0)
            {
                //await BackgroundJobManager.EnqueueAsync<InsertNetWorthPeriodAnalyseJob, ICollection<string>>(funds);
                await NetWorthPeriodAnalyseManager.Insert(funds);
            }
            Logger.Info($"NetWorthPeriodAnalyse 下载完成 第{page}页，每页{size}条");
            return isLast;
        }
    }
}
using System;
using System.ComponentModel;
using System.Threading;
using Abp.Dependency;
using Abp.Hangfire;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Hangfire;

namespace Boss.Pim.Funds.Workers
{
    public class FundRankWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public IPeriodIncreaseAppService IPeriodIncreaseAppService { get; set; }
        public FundRankWorker() : base(Cron.Daily(1, 15))
        {

        }

        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            AsyncHelper.RunSync(() => IPeriodIncreaseAppService.DownloadRank());
        }
    }
}
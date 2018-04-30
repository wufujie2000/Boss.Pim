using System;
using System.Threading;
using Abp.Dependency;
using Abp.Hangfire;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Hangfire;

namespace Boss.Pim.Funds.Workers
{
    public class RatingPoolWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public IRatingPoolAppService IRatingPoolAppService { get; set; }

        public RatingPoolWorker() : base(Cron.Daily(0, 30))
        {

        }

        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            AsyncHelper.RunSync(() => IRatingPoolAppService.DownloadStockstar());
        }
    }
}
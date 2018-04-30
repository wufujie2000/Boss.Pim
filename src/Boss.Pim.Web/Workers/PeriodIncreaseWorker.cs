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
    public class PeriodIncreaseWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public IPeriodIncreaseAppService IPeriodIncreaseAppService { get; set; }
        public PeriodIncreaseWorker() : base(Cron.Daily(6, 15))
        {

        }
        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            AsyncHelper.RunSync(() => IPeriodIncreaseAppService.AsyncDownoad());
        }
    }
}
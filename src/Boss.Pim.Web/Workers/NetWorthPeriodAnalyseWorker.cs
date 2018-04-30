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
    public class NetWorthPeriodAnalyseWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public INetWorthPeriodAnalyseAppService INetWorthPeriodAnalyseAppService { get; set; }
        public NetWorthPeriodAnalyseWorker() : base(Cron.Daily(6, 10))
        {

        }

        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            AsyncHelper.RunSync(() => INetWorthPeriodAnalyseAppService.AsyncDownoad());
        }
    }
}
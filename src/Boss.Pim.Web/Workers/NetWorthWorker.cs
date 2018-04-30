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
    public class NetWorthWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public INetWorthAppService INetWorthAppService { get; set; }
        public NetWorthWorker() : base(Cron.Daily(4, 50))
        {

        }

        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            AsyncHelper.RunSync(() => INetWorthAppService.Download3Days());
        }
    }
}
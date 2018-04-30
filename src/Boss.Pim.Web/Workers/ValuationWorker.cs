using System;
using System.Threading;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Hangfire;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Hangfire;

namespace Boss.Pim.Funds.Workers
{
    public class ValuationWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public IValuationAppService AppService { get; set; }
        public ValuationWorker() : base(Cron.Daily(10, 30))
        {

        }

        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            AsyncHelper.RunSync(() => AppService.DownloadAllFundEasyMoney());
        }
    }
}
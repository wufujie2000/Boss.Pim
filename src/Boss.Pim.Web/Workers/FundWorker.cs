using System;
using Abp.Dependency;
using Abp.Hangfire;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Hangfire;

namespace Boss.Pim.Funds.Workers
{
    public class FundWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public IFundAppService IFundAppService { get; set; }
        public FundWorker() : base(Cron.Daily(0, 10))
        {

        }

        public override void DoWork()
        {
            AsyncHelper.RunSync(() => IFundAppService.Download());
        }
    }
}
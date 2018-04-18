using System;
using System.Threading;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;

namespace Boss.Pim.Funds.Workers
{
    public class ValuationWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        public IValuationAppService AppService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timer"></param>
        public ValuationWorker(AbpTimer timer)
        : base(timer)
        {
            Timer.Period = 60 * 1000;
        }

        private object doworklock = new object();

        /// <summary>
        /// 循环执行
        /// </summary>
        protected override void DoWork()
        {
            lock (doworklock)
            {
                try
                {
                    var now = DateTime.Now;
                    if (now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday)
                    {
                        return;
                    }
                    if ((now.Hour == 10 && now.Minute == 30))
                    {
                        AsyncHelper.RunSync(() => AppService.DownloadAllFundEasyMoney());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                }
                Thread.Sleep(60 * 1000);
            }
        }
    }
}
using System;
using System.Threading;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;

namespace Boss.Pim.Funds.Workers
{
    public class ValuationDetailWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        public IValuationAppService AppService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timer"></param>
        public ValuationDetailWorker(AbpTimer timer)
        : base(timer)
        {
            Timer.Period = 60 * 1000 * 3;
        }

        private readonly object _doWorkLock = new object();

        /// <summary>
        /// 循环执行
        /// </summary>
        protected override void DoWork()
        {
            lock (_doWorkLock)
            {
                try
                {
                    var now = DateTime.Now;
                    if (now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday)
                    {
                        return;
                    }
                    if ((now.Hour == 11 && now.Minute > 30 && now.Minute < 55)
                        || (now.Hour == 12 && now.Minute < 40 && now.Minute > 5)
                        || (now.Hour == 14)
                        || (now.Hour == 15 && now.Minute < 10)
                        )
                    {
                        AsyncHelper.RunSync(() => AppService.DownloadOptionalEasyMoney());
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
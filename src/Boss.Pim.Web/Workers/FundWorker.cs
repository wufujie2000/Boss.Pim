using System;
using System.Threading;
using Abp.Dependency;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;

namespace Boss.Pim.Funds.Workers
{
    public class FundWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        public IFundAppService IFundAppService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timer"></param>
        public FundWorker(AbpTimer timer)
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
                var now = DateTime.Now;
                if (now.DayOfWeek == DayOfWeek.Sunday)
                {
                    return;
                }
                try
                {
                    if ((now.Hour == 0 && now.Minute == 10))
                    {
                        AsyncHelper.RunSync(() => IFundAppService.Download());
                    }
                    Thread.Sleep(60 * 1000);

                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                }
            }
        }
    }
}
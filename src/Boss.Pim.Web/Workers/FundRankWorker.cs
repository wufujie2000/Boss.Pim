using System;
using System.Threading;
using Abp.Dependency;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;

namespace Boss.Pim.Funds.Workers
{
    public class FundRankWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        public IPeriodIncreaseAppService IPeriodIncreaseAppService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timer"></param>
        public FundRankWorker(AbpTimer timer)
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
                    if ((now.Hour == 1 && now.Minute == 15))
                    {
                        AsyncHelper.RunSync(() => IPeriodIncreaseAppService.DownloadRank());
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
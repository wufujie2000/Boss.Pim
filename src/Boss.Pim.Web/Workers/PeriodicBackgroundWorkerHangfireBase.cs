using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Threading.BackgroundWorkers;
using Hangfire;

namespace Abp.Hangfire
{
    public abstract class PeriodicBackgroundWorkerHangfireBase : BackgroundWorkerBase, ISingletonDependency
    {
        protected readonly string _cronExpression;
        protected PeriodicBackgroundWorkerHangfireBase(string cronExpression)
        {
            _cronExpression = cronExpression;
        }

        public override void Start()
        {
            base.Start();
            try
            {
                AddOrUpdate(() => DoWork(), _cronExpression);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        private void AddOrUpdate(Expression<Action> methodCall, string cronExpression)
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, TimeZoneInfo.Local);
        }

        /// <summary>
        /// 具体的任务执行
        /// </summary>
        public abstract void DoWork();
    }
}

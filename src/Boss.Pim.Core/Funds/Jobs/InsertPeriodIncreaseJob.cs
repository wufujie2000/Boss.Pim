using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Threading;
using Boss.Pim.Funds.Services;

namespace Boss.Pim.Funds.Jobs
{
    public class InsertPeriodIncreaseJob : BackgroundJob<ICollection<string>>, ITransientDependency
    {
        public PeriodIncreaseManager PeriodIncreaseManager { get; set; }
        public override void Execute(ICollection<string> args)
        {
            AsyncHelper.RunSync(() => PeriodIncreaseManager.Insert(args));
        }
    }
}

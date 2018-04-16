using System.Configuration;
using System.Reflection;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Runtime.Caching.Redis;
using Abp.Threading.BackgroundWorkers;
using Boss.Pim.Funds.Workers;
using Castle.Facilities.Logging;
using Hangfire;

namespace Boss.Pim
{
    [DependsOn(
        typeof(PimApplicationModule),
        typeof(PimDataModule),
        typeof(AbpRedisCacheModule),

        typeof(AbpHangfireModule)
    )]
    public class WinServiceModule : AbpModule
    {
        private static string NlogConfigFilePath = ConfigurationManager.AppSettings["NlogConfigFilePath"];

        public override void PreInitialize()
        {
            IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseNLog().WithConfig(NlogConfigFilePath));

            Configuration.Auditing.IsEnabled = false;

            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("HangfireDb");
            });
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;

            //Uncomment this line to use Redis cache instead of in-memory cache.
            Configuration.Caching.UseRedis();


            //Configuration.Modules
            //   .UseAbplusRebusRabbitMqConsumer()
            //   .SetNumberOfWorkers(3)
            //   .UseLogging(c => c.NLog())
            //   .ConnectTo(RabbitMqConnectionString)
            //   .UseQueue(Assembly.GetExecutingAssembly().GetName().Name)
            //   .RegisterHandlerInAssemblys(Assembly.GetExecutingAssembly());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }


        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();

            workManager.Add(IocManager.Resolve<FundWorker>());
            workManager.Add(IocManager.Resolve<NetWorthPeriodAnalyseWorker>());
            workManager.Add(IocManager.Resolve<NetWorthWorker>());
            workManager.Add(IocManager.Resolve<PeriodIncreaseWorker>());
            workManager.Add(IocManager.Resolve<RatingPoolWorker>());
            workManager.Add(IocManager.Resolve<SetOptionalWorker>());
            workManager.Add(IocManager.Resolve<ValuationDetailWorker>());
            workManager.Add(IocManager.Resolve<ValuationWorker>());
            workManager.Add(IocManager.Resolve<FundRankWorker>());
        }

    }
}
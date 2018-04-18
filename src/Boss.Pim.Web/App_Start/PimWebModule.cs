using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Boss.Pim.Api;
using Castle.MicroKernel.Registration;
using Hangfire;
using Microsoft.Owin.Security;
using Abp.Runtime.Caching.Redis;
using Abp.Threading.BackgroundWorkers;
using Boss.Pim.Funds.Workers;

namespace Boss.Pim.Web
{
    [DependsOn(
        typeof(PimDataModule),
        typeof(PimApplicationModule),
        typeof(PimWebApiModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpRedisCacheModule),
        typeof(AbpHangfireModule),
        typeof(AbpWebMvcModule))]
    public class PimWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<PimNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("HangfireDb");
            });

            //Uncomment this line to use Redis cache instead of in-memory cache.
            Configuration.Caching.UseRedis();

            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
            );

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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

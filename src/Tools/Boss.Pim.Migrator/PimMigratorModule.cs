using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Boss.Pim.EntityFramework;

namespace Boss.Pim.Migrator
{
    [DependsOn(typeof(PimDataModule))]
    public class PimMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<PimDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
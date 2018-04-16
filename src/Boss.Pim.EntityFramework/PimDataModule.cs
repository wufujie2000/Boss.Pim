using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Boss.Pim.EntityFramework;

namespace Boss.Pim
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(PimCoreModule))]
    public class PimDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<PimDbContext>());
            Database.SetInitializer<PimDbContext>(null);

            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PimDbContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<PimDbContext, Migrations.Configuration>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

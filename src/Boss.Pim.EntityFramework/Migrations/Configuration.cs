using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Boss.Pim.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace Boss.Pim.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Pim.EntityFramework.PimDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Pim";
        }

        protected override void Seed(Pim.EntityFramework.PimDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}

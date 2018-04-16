using System.Linq;
using Boss.Pim.EntityFramework;
using Boss.Pim.MultiTenancy;

namespace Boss.Pim.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly PimDbContext _context;

        public DefaultTenantCreator(PimDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}

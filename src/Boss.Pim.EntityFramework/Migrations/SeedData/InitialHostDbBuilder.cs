using Boss.Pim.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Boss.Pim.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly PimDbContext _context;

        public InitialHostDbBuilder(PimDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}

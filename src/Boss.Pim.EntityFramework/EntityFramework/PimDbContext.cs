using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Abp.Zero.EntityFramework;
using Boss.Pim.Authorization.Roles;
using Boss.Pim.Authorization.Users;
using Boss.Pim.EntityFramework.Extensions;
using Boss.Pim.Funds;
using Boss.Pim.MultiTenancy;

namespace Boss.Pim.EntityFramework
{
    public class PimDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* NOTE:
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */

        public PimDbContext()
            : base("Default")
        {
        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in PimDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of PimDbContext since ABP automatically handles it.
         */

        public PimDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        //This constructor is used in tests
        public PimDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {
        }

        public PimDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {
        }

        #region FundCenter

        public virtual IDbSet<Analyse> Analyses { get; set; }
        public virtual IDbSet<Fund> Funds { get; set; }
        public virtual IDbSet<FundAllocateHoldSymbol> FundAllocateHoldSymbols { get; set; }
        public virtual IDbSet<FundAllocateIndustry> FundAllocateIndustries { get; set; }
        public virtual IDbSet<FundAllocation> FundAllocations { get; set; }
        public virtual IDbSet<FundManager> FundManagers { get; set; }
        public virtual IDbSet<FundRank> FundRanks { get; set; }
        public virtual IDbSet<NetWorth> NetWorths { get; set; }
        public virtual IDbSet<NetWorthPeriodAnalyse> NetWorthPeriodAnalyses { get; set; }
        public virtual IDbSet<NotTradeFund> NotTradeFunds { get; set; }
        public virtual IDbSet<PeriodIncrease> PeriodIncreases { get; set; }
        public virtual IDbSet<RatingPool> RatingPools { get; set; }
        public virtual IDbSet<TradeRate> TradeRates { get; set; }
        public virtual IDbSet<TradeLog> TradeLogs { get; set; }
        public virtual IDbSet<TradeRecord> TradeRecords { get; set; }
        public virtual IDbSet<Valuation> Valuations { get; set; }

        #endregion FundCenter

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CoreModelCreating(modelBuilder);
            FundCenterModelCreating(modelBuilder);

            //禁用级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
        }

        private void CoreModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("Core_");
        }

        private void FundCenterModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ChangeTablePrefix("FundCenter_"
                , typeof(Analyse)
                , typeof(Fund)
                , typeof(FundAllocateHoldSymbol)
                , typeof(FundAllocateIndustry)
                , typeof(FundAllocation)
                , typeof(FundManager)
                , typeof(FundRank)
                , typeof(NetWorth)
                , typeof(NetWorthPeriodAnalyse)
                , typeof(NotTradeFund)
                , typeof(PeriodIncrease)
                , typeof(RatingPool)
                , typeof(TradeRate)
                , typeof(TradeLog)
                , typeof(TradeRecord)
                , typeof(Valuation)
                );
        }
    }
}
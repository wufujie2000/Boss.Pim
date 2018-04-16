namespace Boss.Pim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fundRank : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundCenter_FundRanks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FundCode = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        UnitNetWorth = c.Single(nullable: false),
                        AccumulatedNetWorth = c.Single(nullable: false),
                        DailyGrowthRate = c.Single(nullable: false),
                        ZGrowthRate = c.Single(nullable: false),
                        YGrowthRate = c.Single(nullable: false),
                        Y3GrowthRate = c.Single(nullable: false),
                        Y6GrowthRate = c.Single(nullable: false),
                        N1GrowthRate = c.Single(nullable: false),
                        N2GrowthRate = c.Single(nullable: false),
                        N3GrowthRate = c.Single(nullable: false),
                        N5GrowthRate = c.Single(nullable: false),
                        JNGrowthRate = c.Single(nullable: false),
                        LNGrowthRate = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundCenter_FundRanks");
        }
    }
}

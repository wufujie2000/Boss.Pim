namespace Boss.Pim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTradeLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundCenter_TradeLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Long(nullable: false),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                        UnitNetWorth = c.Single(nullable: false),
                        Amount = c.Single(nullable: false),
                        Portion = c.Single(nullable: false),
                        ServiceRate = c.Single(nullable: false),
                        ServiceCharge = c.Single(nullable: false),
                        TradeType = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundCenter_TradeLogs");
        }
    }
}

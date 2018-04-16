namespace Boss.Pim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRateDaysRange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundCenter_TradeRates", "MinMoneyRange", c => c.Int(nullable: false));
            AddColumn("dbo.FundCenter_TradeRates", "MaxMoneyRange", c => c.Int(nullable: false));
            AddColumn("dbo.FundCenter_TradeRates", "MinDayRange", c => c.Int(nullable: false));
            AddColumn("dbo.FundCenter_TradeRates", "MaxDayRange", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundCenter_TradeRates", "MaxDayRange");
            DropColumn("dbo.FundCenter_TradeRates", "MinDayRange");
            DropColumn("dbo.FundCenter_TradeRates", "MaxMoneyRange");
            DropColumn("dbo.FundCenter_TradeRates", "MinMoneyRange");
        }
    }
}

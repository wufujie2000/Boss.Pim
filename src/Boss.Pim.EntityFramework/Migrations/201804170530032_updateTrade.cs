namespace Boss.Pim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundCenter_TradeRecords", "ConfirmAmount", c => c.Single(nullable: false));
            AddColumn("dbo.FundCenter_TradeRecords", "ConfirmShare", c => c.Single(nullable: false));
            AddColumn("dbo.FundCenter_TradeRecords", "BuyServiceCharge", c => c.Single(nullable: false));
            AddColumn("dbo.FundCenter_TradeRecords", "TradeRecordType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundCenter_TradeRecords", "TradeRecordType");
            DropColumn("dbo.FundCenter_TradeRecords", "BuyServiceCharge");
            DropColumn("dbo.FundCenter_TradeRecords", "ConfirmShare");
            DropColumn("dbo.FundCenter_TradeRecords", "ConfirmAmount");
        }
    }
}

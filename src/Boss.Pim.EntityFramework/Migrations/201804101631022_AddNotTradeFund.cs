namespace Boss.Pim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotTradeFund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundCenter_NotTradeFunds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        SourcePlatform = c.String(maxLength: 256),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundCenter_NotTradeFunds");
        }
    }
}

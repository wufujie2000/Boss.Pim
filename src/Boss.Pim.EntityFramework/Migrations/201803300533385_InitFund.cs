namespace Boss.Pim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitFund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundCenter_Analyses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        Score = c.Int(nullable: false),
                        AnalyseDescription = c.String(maxLength: 1024),
                        ScoreDescription = c.String(maxLength: 1024),
                        CompanyScore = c.Int(nullable: false),
                        AssetScore = c.Int(nullable: false),
                        Profitability = c.Int(nullable: false),
                        SharpeRatio = c.Int(nullable: false),
                        AntiRisk = c.Int(nullable: false),
                        ScoreShort = c.Int(nullable: false),
                        ScoreMedium = c.Int(nullable: false),
                        ScoreLong = c.Int(nullable: false),
                        Stability = c.Int(nullable: false),
                        ExcessIncome = c.Int(nullable: false),
                        TimingSelection = c.Int(nullable: false),
                        StockSelection = c.Int(nullable: false),
                        IndexFollowing = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_FundAllocateHoldSymbols",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FundCode = c.String(maxLength: 128),
                        Symbol = c.String(maxLength: 128),
                        AbbrName = c.String(maxLength: 512),
                        Asset = c.Single(nullable: false),
                        Percent = c.Single(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        SymbolType = c.Int(nullable: false),
                        Change = c.Single(nullable: false),
                        Percentage = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_FundAllocateIndustries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FundCode = c.String(maxLength: 128),
                        SectorName = c.String(maxLength: 128),
                        Percent = c.Single(nullable: false),
                        ChangeType = c.Int(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Change = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_FundAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(maxLength: 128),
                        SymbolAsset = c.Single(nullable: false),
                        SymbolPercent = c.Single(nullable: false),
                        BondAsset = c.Single(nullable: false),
                        BondPercent = c.Single(nullable: false),
                        CashAsset = c.Single(nullable: false),
                        CashPercent = c.Single(nullable: false),
                        OtherAsset = c.Single(nullable: false),
                        OtherPercent = c.Single(nullable: false),
                        EndDate = c.String(maxLength: 64),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_FundManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCompanyId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        AppointmentDate = c.DateTime(nullable: false),
                        Introduction = c.String(maxLength: 2048),
                        ScoreShort = c.Int(nullable: false),
                        ScoreMedium = c.Int(nullable: false),
                        ScoreLong = c.Int(nullable: false),
                        score = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_Funds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 128),
                        DkhsCode = c.String(maxLength: 128),
                        ShortName = c.String(maxLength: 512),
                        Name = c.String(maxLength: 1024),
                        ShortNameInitials = c.String(maxLength: 512),
                        ShortNamePinYin = c.String(maxLength: 512),
                        TypeName = c.String(maxLength: 512),
                        IsOptional = c.Boolean(nullable: false),
                        TrupName = c.String(maxLength: 256),
                        EstabDate = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_NetWorthPeriodAnalyses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        PeriodStartDate = c.DateTime(nullable: false),
                        PeriodDays = c.Int(nullable: false),
                        Avg = c.Single(nullable: false),
                        Later = c.Single(nullable: false),
                        Max = c.Single(nullable: false),
                        Min = c.Single(nullable: false),
                        MaxAvg = c.Single(nullable: false),
                        MinAvg = c.Single(nullable: false),
                        DieFu = c.Single(nullable: false),
                        ZhangFu = c.Single(nullable: false),
                        BoWave = c.Single(nullable: false),
                        SafeLow = c.Single(nullable: false),
                        SafeHigh = c.Single(nullable: false),
                        SafeTradeCent = c.Single(nullable: false),
                        PayWaveRate = c.Single(nullable: false),
                        MaxPayCent = c.Single(nullable: false),
                        MaxLoseCent = c.Single(nullable: false),
                        GreatBuy = c.Single(nullable: false),
                        GreatSale = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_NetWorths",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FundCode = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        UnitNetWorth = c.Single(nullable: false),
                        AccumulatedNetWorth = c.Single(nullable: false),
                        DailyGrowthRate = c.Single(nullable: false),
                        PurchaseStatus = c.String(maxLength: 128),
                        RedemptionState = c.String(maxLength: 128),
                        DividendsDistribution = c.String(maxLength: 128),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_PeriodIncreases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(maxLength: 64),
                        ReturnRate = c.Single(nullable: false),
                        SameTypeAverage = c.Single(nullable: false),
                        Hs300 = c.Single(nullable: false),
                        Rank = c.Int(nullable: false),
                        SameTypeTotalQty = c.Int(nullable: false),
                        DifferentQty = c.Int(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_RatingPools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        HtsecRating3 = c.Int(nullable: false),
                        HtsecRating5 = c.Int(nullable: false),
                        ZssecRating3 = c.Int(nullable: false),
                        ZssecRating5 = c.Int(nullable: false),
                        ShsecRating3 = c.Int(nullable: false),
                        ShsecRating5 = c.Int(nullable: false),
                        JajxRating3 = c.Int(nullable: false),
                        JajxRating5 = c.Int(nullable: false),
                        MstarRating3 = c.Int(nullable: false),
                        MstarRating5 = c.Int(nullable: false),
                        GalaxyRating3 = c.Int(nullable: false),
                        GalaxyRating5 = c.Int(nullable: false),
                        TxsecRating3 = c.Int(nullable: false),
                        TxsecRating5 = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_TradeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        Title = c.String(maxLength: 128),
                        RateType = c.Int(nullable: false),
                        MoneyRange = c.Int(nullable: false),
                        DayRange = c.Int(nullable: false),
                        SourceRate = c.Single(nullable: false),
                        Rate = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_TradeRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Long(nullable: false),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        BuyTime = c.DateTime(nullable: false),
                        BuyUnitNetWorth = c.Single(nullable: false),
                        BuyAmount = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FundCenter_Valuations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FundCode = c.String(nullable: false, maxLength: 128),
                        SourcePlatform = c.String(maxLength: 256),
                        EstimatedTime = c.DateTime(nullable: false),
                        LastUnitNetWorth = c.Single(nullable: false),
                        EstimatedUnitNetWorth = c.Single(nullable: false),
                        ReturnRate = c.Single(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FundCenter_Valuations");
            DropTable("dbo.FundCenter_TradeRecords");
            DropTable("dbo.FundCenter_TradeRates");
            DropTable("dbo.FundCenter_RatingPools");
            DropTable("dbo.FundCenter_PeriodIncreases");
            DropTable("dbo.FundCenter_NetWorths");
            DropTable("dbo.FundCenter_NetWorthPeriodAnalyses");
            DropTable("dbo.FundCenter_Funds");
            DropTable("dbo.FundCenter_FundManagers");
            DropTable("dbo.FundCenter_FundAllocations");
            DropTable("dbo.FundCenter_FundAllocateIndustries");
            DropTable("dbo.FundCenter_FundAllocateHoldSymbols");
            DropTable("dbo.FundCenter_Analyses");
        }
    }
}

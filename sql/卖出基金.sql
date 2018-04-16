SELECT fun.TypeName �������,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
       fun.ShortName ��������,
       fun.Code �������,
       val.ReturnRate ��ֵ�Ƿ�,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) ��������,
       ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * 100, 3) �������,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * tra.BuyAmount
       - DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 ��ʣ����,
       ROUND(
                ((gue10.GreatSale / val.EstimatedUnitNetWorth - 1) + (gue20.GreatSale / val.EstimatedUnitNetWorth - 1)
                 + (gue30.GreatSale / val.EstimatedUnitNetWorth - 1)
                ) * 100 / 3,
                2
            ) �����,
       ROUND(
                (val.EstimatedUnitNetWorth / gue10.GreatBuy - 1) + (val.EstimatedUnitNetWorth / gue20.GreatBuy - 1)
                + (val.EstimatedUnitNetWorth / gue30.GreatBuy - 1) / 3,
                4
            ) �����,
       y3.Rank ��3������,
       y6.Rank ��6������,
       y.Rank ��1������,
       z.Rank ��1������,
       n1.Rank ��1������,
       ISNULL(trasell.Rate, 0) * tra.BuyAmount ����������,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 ��Ϣ,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * tra.BuyAmount ������,
       y3.ReturnRate ��3���Ƿ�,
       y6.ReturnRate ��6���Ƿ�,
       y.ReturnRate ��1���Ƿ�,
       z.ReturnRate ��1���Ƿ�,
       n1.ReturnRate ��1���Ƿ�,
       z.SameTypeTotalQty ͬ���������,
       z.SameTypeAverage ͬ��ƽ��ֵ,
       ana.Score ����,
       ana.ScoreDescription ��������,
       ana.SharpeRatio ���ձ���,
       ana.ScoreShort ��������,
       ana.CompanyScore ��˾����,
       ana.AssetScore �ʲ�����,
       trasell.Rate ��������,
       ana.ScoreMedium ��������,
       ana.ScoreLong ��������,
       ana.Profitability ӯ������,
       ana.AntiRisk ����������,
       ana.Stability �ȶ���,
       ana.TimingSelection ��ʱ����,
       ana.IndexFollowing ָ������,
       ana.Experience ��������,
       ana.AnalyseDescription ��������,
       CONVERT(VARCHAR(32), tra.BuyTime, 23) ��������,
       fun.DkhsCode
FROM dbo.FundCenter_TradeRecords tra
    INNER JOIN dbo.FundCenter_Funds fun
        ON fun.Code = tra.FundCode
    LEFT JOIN dbo.FundCenter_Valuations val
        ON val.FundCode = tra.FundCode
    LEFT JOIN dbo.FundCenter_TradeRates trasell
        ON trasell.FundCode = fun.Code
           AND trasell.RateType = 2
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) >= trasell.MinDayRange
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) <= trasell.MaxDayRange
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue10
        ON gue10.FundCode = fun.Code
           AND DATEDIFF(DAY, gue10.PeriodStartDate, tra.BuyTime) = 0
           AND gue10.PeriodDays = 10
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue20
        ON gue20.FundCode = fun.Code
           AND DATEDIFF(DAY, gue20.PeriodStartDate, tra.BuyTime) = 0
           AND gue20.PeriodDays = 20
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue30
        ON gue30.FundCode = fun.Code
           AND DATEDIFF(DAY, gue30.PeriodStartDate, tra.BuyTime) = 0
           AND gue30.PeriodDays = 30
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue40
        ON gue40.FundCode = fun.Code
           AND DATEDIFF(DAY, gue40.PeriodStartDate, tra.BuyTime) = 0
           AND gue40.PeriodDays = 40
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue50
        ON gue50.FundCode = fun.Code
           AND DATEDIFF(DAY, gue50.PeriodStartDate, tra.BuyTime) = 0
           AND gue50.PeriodDays = 50
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue60
        ON gue60.FundCode = fun.Code
           AND DATEDIFF(DAY, gue60.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 60
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue70
        ON gue70.FundCode = fun.Code
           AND DATEDIFF(DAY, gue70.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 70
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue80
        ON gue80.FundCode = fun.Code
           AND DATEDIFF(DAY, gue80.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 80
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue90
        ON gue90.FundCode = fun.Code
           AND DATEDIFF(DAY, gue90.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 90
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue100
        ON gue100.FundCode = fun.Code
           AND DATEDIFF(DAY, gue100.PeriodStartDate, tra.BuyTime) = 0
           AND gue100.PeriodDays = 100
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue110
        ON gue110.FundCode = fun.Code
           AND DATEDIFF(DAY, gue110.PeriodStartDate, tra.BuyTime) = 0
           AND gue110.PeriodDays = 110
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue120
        ON gue120.FundCode = fun.Code
           AND DATEDIFF(DAY, gue120.PeriodStartDate, tra.BuyTime) = 0
           AND gue120.PeriodDays = 120
    LEFT JOIN dbo.FundCenter_PeriodIncreases n1
        ON n1.FundCode = fun.Code
           AND n1.Title IN ( N'1N' )
           AND (
                   DATEDIFF(DAY, n1.CreationTime, GETDATE()) = 0
                   OR DATEDIFF(DAY, n1.LastModificationTime, GETDATE()) = 0
               )
           AND n1.Rank > 0
    LEFT JOIN dbo.FundCenter_PeriodIncreases y6
        ON y6.FundCode = fun.Code
           AND y6.Title IN ( N'6Y' )
           AND (
                   DATEDIFF(DAY, y6.CreationTime, GETDATE()) = 0
                   OR DATEDIFF(DAY, y6.LastModificationTime, GETDATE()) = 0
               )
           AND y6.Rank > 0
    LEFT JOIN dbo.FundCenter_PeriodIncreases y3
        ON y3.FundCode = fun.Code
           AND y3.Title IN ( N'3Y' )
           AND (
                   DATEDIFF(DAY, y3.CreationTime, GETDATE()) = 0
                   OR DATEDIFF(DAY, y3.LastModificationTime, GETDATE()) = 0
               )
           AND y3.Rank > 0
    LEFT JOIN dbo.FundCenter_PeriodIncreases y
        ON y.FundCode = fun.Code
           AND y.Title IN ( N'Y' )
           AND (
                   DATEDIFF(DAY, y.CreationTime, GETDATE()) = 0
                   OR DATEDIFF(DAY, y.LastModificationTime, GETDATE()) = 0
               )
           AND y.Rank > 0
    LEFT JOIN dbo.FundCenter_PeriodIncreases z
        ON z.FundCode = fun.Code
           AND z.Title IN ( N'Z' )
           AND (
                   DATEDIFF(DAY, z.CreationTime, GETDATE()) = 0
                   OR DATEDIFF(DAY, z.LastModificationTime, GETDATE()) = 0
               )
           AND z.Rank > 0
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
WHERE tra.UserId = 3
      AND val.ReturnRate < 0
      AND ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * 100, 3) > 1
ORDER BY ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * 100, 1) DESC;



SELECT fun.TypeName �������,
       fun.Code �������,
       fun.ShortName ��������,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
       val.ReturnRate ��ֵ�Ƿ�,
       tra.BuyAmount ������,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) ��������,
       ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * 100, 3) �������,
       ROUND((gue10.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) ��10�������,
       ROUND((gue20.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) ��20�������,
       ROUND((gue30.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) ��30�������,
       ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1), 3) ��10�콨��,
       ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1), 3) ��20�콨��,
       ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1), 3) ��30�콨��,
       ana.Score ����,
       ana.ScoreDescription ��������,
       ana.SharpeRatio ���ձ���,
       ana.ScoreShort ��������,
       ana.CompanyScore ��˾����,
       ana.AssetScore �ʲ�����,
       ROUND((gue40.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) ��40�������,
       ROUND((gue50.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) ��50�������,
       ROUND((gue60.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) ��60�������,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * tra.BuyAmount ������,
       y3.Rank ��3������,
       y3.ReturnRate ��3���Ƿ�,
       y6.Rank ��6������,
       y6.ReturnRate ��6���Ƿ�,
       y.Rank ��1������,
       y.ReturnRate ��1���Ƿ�,
       z.Rank ��1������,
       z.ReturnRate ��1���Ƿ�,
       z.SameTypeTotalQty ͬ���������,
       z.SameTypeAverage ͬ��ƽ��ֵ,
       n1.Rank ��1������,
       n1.ReturnRate ��1���Ƿ�,
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
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'1N' )
               AND per.Rank > 0
     ) n1
        ON n1.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'2N' )
               AND per.Rank > 0
     ) n2
        ON n2.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'3N' )
               AND per.Rank > 0
     ) n3
        ON n3.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'5N' )
               AND per.Rank > 0
     ) n5
        ON n5.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'6Y' )
               AND per.Rank > 0
     ) y6
        ON y6.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'3Y' )
               AND per.Rank > 0
     ) y3
        ON y3.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'Y' )
               AND per.Rank > 0
     ) y
        ON y.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'Z' )
               AND per.Rank > 0
     ) z
        ON z.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
WHERE tra.UserId = 3
ORDER BY ROUND((val.EstimatedUnitNetWorth - tra.BuyUnitNetWorth) * 100 / tra.BuyUnitNetWorth, 1) DESC,
         DATEDIFF(DAY, tra.BuyTime, GETDATE()),
         ROUND((gue10.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 1) DESC,
         ROUND((gue20.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 1) DESC,
         ROUND((gue30.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 1) DESC,
         ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1), 1),
         ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1), 1),
         ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1), 1),
         ana.Score,
         CONVERT(VARCHAR(32), tra.BuyTime, 23);
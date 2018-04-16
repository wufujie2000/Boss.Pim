SELECT fun.TypeName 基金分类,
       fun.Code 基金编码,
       fun.ShortName 基金名称,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) 估值时间,
       val.ReturnRate 估值涨幅,
       tra.BuyAmount 购买金额,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) 持有天数,
       ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * 100, 3) 估收益比,
       ROUND((gue10.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) 估10天收益比,
       ROUND((gue20.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) 估20天收益比,
       ROUND((gue30.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) 估30天收益比,
       ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1), 3) 估10天建议,
       ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1), 3) 估20天建议,
       ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1), 3) 估30天建议,
       ana.Score 评分,
       ana.ScoreDescription 评分描述,
       ana.SharpeRatio 夏普比率,
       ana.ScoreShort 短期评分,
       ana.CompanyScore 公司评分,
       ana.AssetScore 资产评分,
       ROUND((gue40.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) 估40天收益比,
       ROUND((gue50.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) 估50天收益比,
       ROUND((gue60.GreatSale / tra.BuyUnitNetWorth - 1) * 100, 3) 估60天收益比,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * tra.BuyAmount 估收益,
       y3.Rank 近3月排名,
       y3.ReturnRate 近3月涨幅,
       y6.Rank 近6月排名,
       y6.ReturnRate 近6月涨幅,
       y.Rank 近1月排名,
       y.ReturnRate 近1月涨幅,
       z.Rank 近1周排名,
       z.ReturnRate 近1周涨幅,
       z.SameTypeTotalQty 同类基金总数,
       z.SameTypeAverage 同类平均值,
       n1.Rank 近1年排名,
       n1.ReturnRate 近1年涨幅,
       ana.ScoreMedium 中期评分,
       ana.ScoreLong 长期评分,
       ana.Profitability 盈利能力,
       ana.AntiRisk 抗风险能力,
       ana.Stability 稳定性,
       ana.TimingSelection 择时能力,
       ana.IndexFollowing 指数评分,
       ana.Experience 经验评分,
       ana.AnalyseDescription 分析描述,
       CONVERT(VARCHAR(32), tra.BuyTime, 23) 购买日期,
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
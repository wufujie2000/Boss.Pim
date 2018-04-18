SELECT fun.TypeName 基金分类,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) 估值时间,
       fun.ShortName 基金名称,
       fun.Code 基金编码,
       val.ReturnRate 估值涨幅,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) 持有天数,
       ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) 估收益比,
       trasell.Title 费率,
       trasell.Rate 费率,
       (((val.EstimatedUnitNetWorth - tra.BuyUnitNetWorth) / val.EstimatedUnitNetWorth * tra.BuyUnitNetWorth) * 100
        - ISNULL(trasell.Rate, 0)
       ) * tra.BuyAmount - DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 估剩收益,
       y3.Rank 近3月排名,
       y6.Rank 近6月排名,
       y.Rank 近1月排名,
       z.Rank 近1周排名,
       n1.Rank 近1年排名,
       ISNULL(trasell.Rate, 0) * tra.BuyAmount 卖出手续费,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 利息,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * tra.BuyAmount 估收益,
       y3.ReturnRate 近3月涨幅,
       y6.ReturnRate 近6月涨幅,
       y.ReturnRate 近1月涨幅,
       z.ReturnRate 近1周涨幅,
       n1.ReturnRate 近1年涨幅,
       z.SameTypeTotalQty 同类基金总数,
       z.SameTypeAverage 同类平均值,
       ana.Score 评分,
       ana.ScoreDescription 评分描述,
       ana.SharpeRatio 夏普比率,
       ana.ScoreShort 短期评分,
       ana.CompanyScore 公司评分,
       ana.AssetScore 资产评分,
       trasell.Rate 卖出费率,
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
    LEFT JOIN dbo.FundCenter_TradeRates trasell
        ON trasell.FundCode = fun.Code
           AND trasell.RateType = 2
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) >= trasell.MinDayRange
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) < trasell.MaxDayRange
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
      AND (
              val.ReturnRate < 0
              OR ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) > 3
          )
      AND ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) > 1
ORDER BY ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) DESC;






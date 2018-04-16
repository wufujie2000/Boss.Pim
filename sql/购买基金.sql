DECLARE @lastYear VARCHAR(10);
DECLARE @fullLastYear VARCHAR(10);
SET @lastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 2001);
SET @fullLastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 1);

SELECT fun.TypeName 基金分类,
       fun.Code 基金编码,
       fun.ShortName 基金名称,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) 估值时间,
       val.ReturnRate 估值涨幅,
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
       CONVERT(VARCHAR(64), gue15.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue15.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue15.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(
                     VARCHAR(64),
                     ROUND((gue15.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2)
                 ) + '/'
       + CONVERT(VARCHAR(64), ROUND((gue15.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) 天数#建议#收益比,
       CONVERT(VARCHAR(64), gue.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(VARCHAR(64), ROUND((gue.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2))
       + '/'
       + CONVERT(VARCHAR(64), ROUND((gue.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) 天数#建议#收益比,
       CONVERT(VARCHAR(64), gue45.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue45.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue45.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(
                     VARCHAR(64),
                     ROUND((gue45.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2)
                 ) + '/'
       + CONVERT(VARCHAR(64), ROUND((gue45.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) 天数#建议#收益比,
       CONVERT(VARCHAR(64), gue60.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue60.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue60.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(
                     VARCHAR(64),
                     ROUND((gue60.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2)
                 ) + '/'
       + CONVERT(VARCHAR(64), ROUND((gue60.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) 天数#建议#收益比,
       val.EstimatedUnitNetWorth 估值净值,
       gue.GreatBuy 建议买入值,
       ana.Score 综合评分,
       ana.ScoreShort 短期评分,
       q04.Rank 去年4季度排名,
       q04.ReturnRate 年4季度涨幅,
       q03.Rank 去年3季度排名,
       q03.ReturnRate 去年3季度涨幅,
       q02.Rank 去年2季度排名,
       q02.ReturnRate 去年2季度涨幅,
       q01.Rank 去年1季度排名,
       q01.ReturnRate 去年1季度涨幅,
       ylast.Rank 去年排名,
       ylast.ReturnRate 去年涨幅,
       ana.ScoreDescription 评分描述,
       ana.SharpeRatio 夏普比率,
       n2.Rank 近2年排名,
       n2.ReturnRate 近2年涨幅,
       n3.Rank 近3年排名,
       ana.ScoreMedium 中期评分,
       n3.ReturnRate 近3年涨幅,
       n5.Rank 近5年排名,
       ana.ScoreLong 长期评分,
       n5.ReturnRate 近5年涨幅,
       z.Hs300 沪深300值,
       z.DifferentQty 差异数量,
       ana.AnalyseDescription 分析描述
FROM dbo.FundCenter_Funds fun
    LEFT JOIN dbo.FundCenter_GuessPrejudgements gue
        ON gue.FundCode = fun.Code
           AND gue.TradeBugTime IS NULL
           AND gue.PeriodDays = 30
    LEFT JOIN dbo.FundCenter_GuessPrejudgements gue15
        ON gue15.FundCode = fun.Code
           AND gue15.TradeBugTime IS NULL
           AND gue15.PeriodDays = 15
    LEFT JOIN dbo.FundCenter_GuessPrejudgements gue45
        ON gue45.FundCode = fun.Code
           AND gue45.TradeBugTime IS NULL
           AND gue45.PeriodDays = 45
    LEFT JOIN dbo.FundCenter_GuessPrejudgements gue60
        ON gue60.FundCode = fun.Code
           AND gue60.TradeBugTime IS NULL
           AND gue60.PeriodDays = 60
    LEFT JOIN dbo.FundCenter_Valuations val
        ON val.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'年1季度' )
               AND per.Rank > 0
     ) q01
        ON q01.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'年2季度' )
               AND per.Rank > 0
     ) q02
        ON q02.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'年3季度' )
               AND per.Rank > 0
     ) q03
        ON q03.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'年4季度' )
               AND per.Rank > 0
     ) q04
        ON q04.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @fullLastYear + N'年度' )
               AND per.Rank > 0
     ) ylast
        ON ylast.FundCode = fun.Code
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
WHERE fun.IsOptional = 1
      AND fun.TypeName NOT IN ( N'ETF-场内', N'QDII-指数', N'分级杠杆' )
      --AND val.EstimatedUnitNetWorth < CONVERT(FLOAT, gue.GreatSale)
      --AND (
      --        DATEDIFF(DAY, val.EstimatedTime, GETDATE()) = 0
      --        AND DATEPART(HOUR, GETDATE()) > 9
      --        OR 1 = 1
      --    )
      AND ISNULL(y.Rank, -1) < 50
      AND ISNULL(y3.Rank, -1) < 50
      AND ISNULL(y6.Rank, -1) < 30
      AND ISNULL(n1.ReturnRate, 999) > 20
      AND fun.Code NOT IN ( '005112', '002538', '000798', '002539', '004196', '001647' )
ORDER BY ROUND(ISNULL(y6.Rank, -1) / CONVERT(FLOAT, ISNULL(y6.SameTypeTotalQty, -1)), 5),
         ROUND(ISNULL(y3.Rank, -1) / CONVERT(FLOAT, ISNULL(y3.SameTypeTotalQty, -1)), 5),
         ROUND(ISNULL(y.Rank, -1) / CONVERT(FLOAT, ISNULL(y.SameTypeTotalQty, -1)), 5);
--ORDER BY ROUND(gue.GreatBuy - val.EstimatedUnitNetWorth, 1) DESC,
--         ROUND(gue15.GreatBuy - val.EstimatedUnitNetWorth, 1) DESC,
--         ROUND(gue45.GreatBuy - val.EstimatedUnitNetWorth, 1) DESC,
--         ROUND(gue60.GreatBuy - val.EstimatedUnitNetWorth, 1) DESC,
--         (gue.GreatSale - val.EstimatedUnitNetWorth) / val.EstimatedUnitNetWorth DESC,
--         (gue15.GreatSale - val.EstimatedUnitNetWorth) / val.EstimatedUnitNetWorth DESC,
--         (gue45.GreatSale - val.EstimatedUnitNetWorth) / val.EstimatedUnitNetWorth DESC,
--         (gue60.GreatSale - val.EstimatedUnitNetWorth) / val.EstimatedUnitNetWorth DESC,
--         val.ReturnRate,
--         y.Rank,
--         y3.Rank;





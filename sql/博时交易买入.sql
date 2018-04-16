DECLARE @lastYear VARCHAR(10);
DECLARE @fullLastYear VARCHAR(10);
SET @lastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 2001);
SET @fullLastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 1);
DECLARE @PeriodStartDate DATETIME;
SET @PeriodStartDate =
(
    SELECT MAX(PeriodStartDate) FROM FundCenter_NetWorthPeriodAnalyses
);


SELECT fun.TypeName 基金分类,
       fun.Code 基金编码,
       fun.ShortName 基金名称,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) 估值时间,
       val.ReturnRate 估值涨幅,
       ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) 估10天收益比,
       ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) 估20天收益比,
       ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) 估30天收益比,
       y3.Rank 近3月排名,
       y6.Rank 近6月排名,
       y.Rank 近1月排名,
       ROUND(ISNULL(y3.Rank, -1) / CONVERT(FLOAT, ISNULL(y3.SameTypeTotalQty, -1)), 5) 近3月排比,
       y3.ReturnRate 近3月涨幅,
       ROUND(ISNULL(y6.Rank, -1) / CONVERT(FLOAT, ISNULL(y6.SameTypeTotalQty, -1)), 5) 近6月排比,
       y6.ReturnRate 近6月涨幅,
       ROUND(ISNULL(y.Rank, -1) / CONVERT(FLOAT, ISNULL(y.SameTypeTotalQty, -1)), 5) 近1月排比,
       y.ReturnRate 近1月涨幅,
       ROUND((val.EstimatedUnitNetWorth / gue10.GreatBuy - 1), 4) 估10天建议,
       ROUND((val.EstimatedUnitNetWorth / gue20.GreatBuy - 1), 4) 估20天建议,
       ROUND((val.EstimatedUnitNetWorth / gue30.GreatBuy - 1), 4) 估30天建议,
       trabuy.Title 买入费率,
       trabuy.Rate 买入费率,
       trasell.Title 卖出费率,
       trasell.Rate 卖出费率,
       ana.Score 评分,
       ana.ScoreDescription 评分描述,
       ana.SharpeRatio 夏普比率,
       ana.ScoreShort 短期评分,
       ana.CompanyScore 公司评分,
       ana.AssetScore 资产评分,
       ROUND((gue40.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) 估40天收益比,
       ROUND((gue50.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) 估50天收益比,
       ROUND((gue60.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) 估60天收益比,
       z.Rank 近1周排名,
       z.ReturnRate 近1周涨幅,
       ROUND((val.EstimatedUnitNetWorth / gue40.GreatBuy - 1), 3) 估40天建议,
       ROUND((val.EstimatedUnitNetWorth / gue50.GreatBuy - 1), 3) 估50天建议,
       ROUND((val.EstimatedUnitNetWorth / gue60.GreatBuy - 1), 3) 估60天建议,
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
       fun.DkhsCode
FROM dbo.FundCenter_Funds fun
    LEFT JOIN dbo.FundCenter_Valuations val
        ON val.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue10
        ON gue10.FundCode = fun.Code
           AND DATEDIFF(DAY, gue10.PeriodStartDate, @PeriodStartDate) = 0
           AND gue10.PeriodDays = 10
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue20
        ON gue20.FundCode = fun.Code
           AND DATEDIFF(DAY, gue20.PeriodStartDate, @PeriodStartDate) = 0
           AND gue20.PeriodDays = 20
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue30
        ON gue30.FundCode = fun.Code
           AND DATEDIFF(DAY, gue30.PeriodStartDate, @PeriodStartDate) = 0
           AND gue30.PeriodDays = 30
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue40
        ON gue40.FundCode = fun.Code
           AND DATEDIFF(DAY, gue40.PeriodStartDate, @PeriodStartDate) = 0
           AND gue40.PeriodDays = 40
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue50
        ON gue50.FundCode = fun.Code
           AND DATEDIFF(DAY, gue50.PeriodStartDate, @PeriodStartDate) = 0
           AND gue50.PeriodDays = 50
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue60
        ON gue60.FundCode = fun.Code
           AND DATEDIFF(DAY, gue60.PeriodStartDate, @PeriodStartDate) = 0
           AND gue60.PeriodDays = 60
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue70
        ON gue70.FundCode = fun.Code
           AND DATEDIFF(DAY, gue70.PeriodStartDate, @PeriodStartDate) = 0
           AND gue60.PeriodDays = 70
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue80
        ON gue80.FundCode = fun.Code
           AND DATEDIFF(DAY, gue80.PeriodStartDate, @PeriodStartDate) = 0
           AND gue60.PeriodDays = 80
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue90
        ON gue90.FundCode = fun.Code
           AND DATEDIFF(DAY, gue90.PeriodStartDate, @PeriodStartDate) = 0
           AND gue60.PeriodDays = 90
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue100
        ON gue100.FundCode = fun.Code
           AND DATEDIFF(DAY, gue100.PeriodStartDate, @PeriodStartDate) = 0
           AND gue100.PeriodDays = 100
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue110
        ON gue110.FundCode = fun.Code
           AND DATEDIFF(DAY, gue110.PeriodStartDate, @PeriodStartDate) = 0
           AND gue110.PeriodDays = 110
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue120
        ON gue120.FundCode = fun.Code
           AND DATEDIFF(DAY, gue120.PeriodStartDate, @PeriodStartDate) = 0
           AND gue120.PeriodDays = 120
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
    --LEFT JOIN dbo.FundCenter_FundAllocations allo
    --    ON allo.FundCode = fun.Code
    --LEFT JOIN dbo.FundCenter_FundAllocateHoldSymbols alloHold
    --    ON alloHold.FundCode = fun.Code
    --LEFT JOIN dbo.FundCenter_FundAllocateIndustries alloInd
    --    ON alloInd.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_TradeRates trabuy
        ON trabuy.FundCode = fun.Code
           AND trabuy.RateType = 1
    LEFT JOIN dbo.FundCenter_TradeRates trasell
        ON trasell.FundCode = fun.Code
           AND trasell.RateType = 2
WHERE z.Rank > 0
      --AND val.EstimatedUnitNetWorth IS NOT NULL
      --AND fun.IsOptional = 1
      --      AND fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )

      AND (
              ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 1
              OR ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 1
              OR ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 1
              OR ROUND((gue40.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 1
              OR ROUND((gue50.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 1
              OR ROUND((gue60.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 1
          )
      AND (
              ROUND(ISNULL(y.Rank, -1) / CONVERT(FLOAT, ISNULL(y.SameTypeTotalQty, -1)), 5) < 0.07
              OR ROUND(ISNULL(y3.Rank, -1) / CONVERT(FLOAT, ISNULL(y3.SameTypeTotalQty, -1)), 5) < 0.07
              OR ROUND(ISNULL(y6.Rank, -1) / CONVERT(FLOAT, ISNULL(y6.SameTypeTotalQty, -1)), 5) < 0.07
          )
      AND trabuy.Rate IS NOT NULL
      AND trabuy.MoneyRange <= 100
      AND ISNULL(trabuy.Rate, -1) < 0.05
      AND ISNULL(trasell.Rate, -1) < 0.75
--AND ISNULL(y.Rank, -1) < 200
--AND ISNULL(y3.Rank, -1) < 100
--AND ISNULL(y6.Rank, -1) < 100
--AND ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) > 5
--AND fun.Code NOT IN ( '150124', '000990', '002423' )
--AND ISNULL(n1.ReturnRate, 999) > 20
ORDER BY ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 1) DESC,
         ROUND((val.EstimatedUnitNetWorth / gue10.GreatBuy - 1), 1),
         ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 2) DESC,
         ROUND((val.EstimatedUnitNetWorth / gue20.GreatBuy - 1), 2),
         ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) DESC,
         ROUND((val.EstimatedUnitNetWorth / gue30.GreatBuy - 1), 3),
         ROUND(ISNULL(y3.Rank, -1) / CONVERT(FLOAT, ISNULL(y3.SameTypeTotalQty, -1)), 2),
         ROUND(ISNULL(y6.Rank, -1) / CONVERT(FLOAT, ISNULL(y6.SameTypeTotalQty, -1)), 2),
         ROUND(ISNULL(y.Rank, -1) / CONVERT(FLOAT, ISNULL(y.SameTypeTotalQty, -1)), 2),
         val.ReturnRate DESC;



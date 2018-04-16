DECLARE @lastYear VARCHAR(10);
DECLARE @fullLastYear VARCHAR(10);
SET @lastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 2001);
SET @fullLastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 1);
DECLARE @PeriodStartDate DATETIME;
SET @PeriodStartDate =
(
    SELECT MAX(PeriodStartDate) FROM FundCenter_NetWorthPeriodAnalyses
);


SELECT fun.TypeName �������,
       fun.Code �������,
       fun.ShortName ��������,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
       val.ReturnRate ��ֵ�Ƿ�,
       ROUND((gue10.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) ��10�������,
       ROUND((gue20.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) ��20�������,
       ROUND((gue30.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) ��30�������,
       y3.Rank ��3������,
       y6.Rank ��6������,
       y.Rank ��1������,
       ROUND(ISNULL(y3.Rank, -1) / CONVERT(FLOAT, ISNULL(y3.SameTypeTotalQty, -1)), 5) ��3���ű�,
       y3.ReturnRate ��3���Ƿ�,
       ROUND(ISNULL(y6.Rank, -1) / CONVERT(FLOAT, ISNULL(y6.SameTypeTotalQty, -1)), 5) ��6���ű�,
       y6.ReturnRate ��6���Ƿ�,
       ROUND(ISNULL(y.Rank, -1) / CONVERT(FLOAT, ISNULL(y.SameTypeTotalQty, -1)), 5) ��1���ű�,
       y.ReturnRate ��1���Ƿ�,
       ROUND((val.EstimatedUnitNetWorth / gue10.GreatBuy - 1), 4) ��10�콨��,
       ROUND((val.EstimatedUnitNetWorth / gue20.GreatBuy - 1), 4) ��20�콨��,
       ROUND((val.EstimatedUnitNetWorth / gue30.GreatBuy - 1), 4) ��30�콨��,
       trabuy.Title �������,
       trabuy.Rate �������,
       trasell.Title ��������,
       trasell.Rate ��������,
       ana.Score ����,
       ana.ScoreDescription ��������,
       ana.SharpeRatio ���ձ���,
       ana.ScoreShort ��������,
       ana.CompanyScore ��˾����,
       ana.AssetScore �ʲ�����,
       ROUND((gue40.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) ��40�������,
       ROUND((gue50.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) ��50�������,
       ROUND((gue60.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 3) ��60�������,
       z.Rank ��1������,
       z.ReturnRate ��1���Ƿ�,
       ROUND((val.EstimatedUnitNetWorth / gue40.GreatBuy - 1), 3) ��40�콨��,
       ROUND((val.EstimatedUnitNetWorth / gue50.GreatBuy - 1), 3) ��50�콨��,
       ROUND((val.EstimatedUnitNetWorth / gue60.GreatBuy - 1), 3) ��60�콨��,
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
         WHERE per.Title IN ( @lastYear + N'��1����' )
               AND per.Rank > 0
     ) q01
        ON q01.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'��2����' )
               AND per.Rank > 0
     ) q02
        ON q02.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'��3����' )
               AND per.Rank > 0
     ) q03
        ON q03.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @lastYear + N'��4����' )
               AND per.Rank > 0
     ) q04
        ON q04.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( @fullLastYear + N'���' )
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
      --      AND fun.TypeName NOT IN ( '���-FOF', '������', '�����', '��������', 'ծȯ����-����', '����' )

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



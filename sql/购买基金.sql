DECLARE @lastYear VARCHAR(10);
DECLARE @fullLastYear VARCHAR(10);
SET @lastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 2001);
SET @fullLastYear = CONVERT(VARCHAR(10), DATEPART(YEAR, GETDATE()) - 1);

SELECT fun.TypeName �������,
       fun.Code �������,
       fun.ShortName ��������,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
       val.ReturnRate ��ֵ�Ƿ�,
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
       CONVERT(VARCHAR(64), gue15.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue15.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue15.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(
                     VARCHAR(64),
                     ROUND((gue15.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2)
                 ) + '/'
       + CONVERT(VARCHAR(64), ROUND((gue15.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) ����#����#�����,
       CONVERT(VARCHAR(64), gue.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(VARCHAR(64), ROUND((gue.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2))
       + '/'
       + CONVERT(VARCHAR(64), ROUND((gue.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) ����#����#�����,
       CONVERT(VARCHAR(64), gue45.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue45.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue45.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(
                     VARCHAR(64),
                     ROUND((gue45.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2)
                 ) + '/'
       + CONVERT(VARCHAR(64), ROUND((gue45.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) ����#����#�����,
       CONVERT(VARCHAR(64), gue60.PeriodDays) + '#'
       + CONVERT(VARCHAR(64), ROUND(gue60.GreatBuy - val.EstimatedUnitNetWorth, 3)) + '/'
       + CONVERT(VARCHAR(64), ROUND(gue60.GreatBuy - val.EstimatedUnitNetWorth, 1)) + '#'
       + +CONVERT(
                     VARCHAR(64),
                     ROUND((gue60.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 2)
                 ) + '/'
       + CONVERT(VARCHAR(64), ROUND((gue60.GreatSale - val.EstimatedUnitNetWorth) * 100 / val.EstimatedUnitNetWorth, 0)) ����#����#�����,
       val.EstimatedUnitNetWorth ��ֵ��ֵ,
       gue.GreatBuy ��������ֵ,
       ana.Score �ۺ�����,
       ana.ScoreShort ��������,
       q04.Rank ȥ��4��������,
       q04.ReturnRate ��4�����Ƿ�,
       q03.Rank ȥ��3��������,
       q03.ReturnRate ȥ��3�����Ƿ�,
       q02.Rank ȥ��2��������,
       q02.ReturnRate ȥ��2�����Ƿ�,
       q01.Rank ȥ��1��������,
       q01.ReturnRate ȥ��1�����Ƿ�,
       ylast.Rank ȥ������,
       ylast.ReturnRate ȥ���Ƿ�,
       ana.ScoreDescription ��������,
       ana.SharpeRatio ���ձ���,
       n2.Rank ��2������,
       n2.ReturnRate ��2���Ƿ�,
       n3.Rank ��3������,
       ana.ScoreMedium ��������,
       n3.ReturnRate ��3���Ƿ�,
       n5.Rank ��5������,
       ana.ScoreLong ��������,
       n5.ReturnRate ��5���Ƿ�,
       z.Hs300 ����300ֵ,
       z.DifferentQty ��������,
       ana.AnalyseDescription ��������
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
WHERE fun.IsOptional = 1
      AND fun.TypeName NOT IN ( N'ETF-����', N'QDII-ָ��', N'�ּ��ܸ�' )
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





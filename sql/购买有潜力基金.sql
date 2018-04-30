DECLARE @PeriodStartDate DATETIME;
DECLARE @ClosingDate DATETIME;

SET @PeriodStartDate =
(
    SELECT MAX(PeriodStartDate) FROM dbo.FundCenter_NetWorthPeriodAnalyses
);


IF OBJECT_ID('tempdb..#dtGreat') IS NOT NULL
    DROP TABLE #dtGreat;
SELECT *
INTO #dtGreat
FROM
(
    SELECT fun.Code,
           CASE
               WHEN gue20.GreatSale IS NULL THEN
           (gue10.GreatSale)
               WHEN gue30.GreatSale IS NULL THEN
           (gue10.GreatSale + gue20.GreatSale) / 2
               WHEN gue40.GreatSale IS NULL THEN
           (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale) / 3
               WHEN gue50.GreatSale IS NULL THEN
           --ELSE
           (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale + gue40.GreatSale) / 4
               WHEN gue60.GreatSale IS NULL THEN
           --ELSE
           (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale + gue40.GreatSale + gue50.GreatSale) / 5
               ELSE
           (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale + gue40.GreatSale + gue50.GreatSale + gue60.GreatSale)
           / 6
           END GreatSale,
           CASE
               WHEN gue20.GreatBuy IS NULL THEN
           (gue10.GreatBuy) / 1
               WHEN gue30.GreatBuy IS NULL THEN
           (gue10.GreatBuy + gue20.GreatBuy) / 2
               WHEN gue40.GreatBuy IS NULL THEN
           (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy) / 3
               WHEN gue50.GreatBuy IS NULL THEN
           --ELSE
           (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy + gue40.GreatBuy) / 4
               WHEN gue60.GreatBuy IS NULL THEN
           --ELSE
           (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy + gue40.GreatBuy + gue50.GreatBuy) / 5
               ELSE
           (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy + gue40.GreatBuy + gue50.GreatBuy + gue60.GreatBuy) / 6
           END GreatBuy
    FROM dbo.FundCenter_Funds fun
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
) t;




SELECT TOP 200
    CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
    fun.Code �������,
    val.ReturnRate ��ֵ�Ƿ�,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / #dtGreat.GreatSale) * 100, 3) �����,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / #dtGreat.GreatBuy), 3) �����,
    y3.Rank ��3������,
    y6.Rank ��6������,
    y.Rank ��1������,
    z.Rank ��1������,
    n1.Rank ��1������,
    ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 1) ��3���ű�,
    ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 1) ��6���ű�,
    ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 1) ��1���ű�,
    rate.Title ����,
    rate.Rate ����,
    fun.TypeName �������,
    fun.ShortName ��������,
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
    CONVERT(VARCHAR(32), @PeriodStartDate, 23) �׶�����,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue10.GreatSale) * 100, 3) ��10�������,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue20.GreatSale) * 100, 3) ��20�������,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue30.GreatSale) * 100, 3) ��30�������,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue40.GreatSale) * 100, 3) ��40�������,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue50.GreatSale) * 100, 3) ��50�������,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue60.GreatSale) * 100, 3) ��60�������,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue10.GreatBuy), 3) ��10�콨��,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue20.GreatBuy), 3) ��20�콨��,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue30.GreatBuy), 3) ��30�콨��,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue40.GreatBuy), 3) ��40�콨��,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue50.GreatBuy), 3) ��50�콨��,
    ROUND((1 / val.EstimatedUnitNetWorth - 1 / gue60.GreatBuy), 3) ��60�콨��,
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
    LEFT JOIN #dtGreat
        ON #dtGreat.Code = fun.Code
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
    --LEFT JOIN dbo.FundCenter_FundAllocations allo
    --    ON allo.FundCode = fun.Code
    --LEFT JOIN dbo.FundCenter_FundAllocateHoldSymbols alloHold
    --    ON alloHold.FundCode = fun.Code
    --LEFT JOIN dbo.FundCenter_FundAllocateIndustries alloInd
    --    ON alloInd.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_NotTradeFunds ntf
        ON ntf.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_TradeRates rate
        ON rate.FundCode = fun.Code
           AND rate.RateType = 1
           AND rate.Title NOT IN ( '500�� �� ������ < 1000��', '100�� �� ������ < 500��', '100�� �� ������ < 200��',
                                   '200�� �� ������ < 500��', '50�� �� ������ < 200��', '100�� �� ������ < 300��',
                                   '300�� �� ������ < 500��', '50�� �� ������ < 100��', '50�� �� ������ < 250��',
                                   '250�� �� ������ < 500��', '50�� �� ������ < 500��', '100�� �� ������ < 1000��', '������ �� 500��',
                                   '100�� �� ������ < 250��', '10����Ԫ �� ������ < 30����Ԫ', '30����Ԫ �� ������ < 60����Ԫ'
                                 )
WHERE fun.TypeName NOT IN ( '���-FOF', '������', '�����', '��������', 'ծȯ����-����', '����' )
      --AND fun.IsOptional = 1
      AND ntf.Id IS NULL
      --AND fun.Code IN
      --    (
      --        SELECT DISTINCT
      --            per.FundCode
      --        FROM dbo.FundCenter_PeriodIncreases per
      --        WHERE (
      --                  per.Title IN ( N'Z' )
      --                  AND ISNULL(per.ReturnRate, 999) >= 3
      --                  OR per.Title IN ( N'Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 4
      --                  OR per.Title IN ( N'3Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 5
      --                  OR per.Title IN ( N'6Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 8
      --                  OR per.Title IN ( N'1N' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 11
      --                  OR per.Title IN ( N'2N' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 40
      --              )
      --    )
      AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 2), 0) < 0.5
      AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 2), 0) < 0.4
      AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 2), 0) < 0.4
--AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(z.Rank, -1)) / z.SameTypeTotalQty, 2), 0) < 0.3
--AND z.Rank > 0
--      AND val.EstimatedUnitNetWorth IS NOT NULL
--AND val.EstimatedTime<GETDATE()-0.5
--AND y.ReturnRate < -8
--AND ana.Score > 71
ORDER BY ����� DESC,
         ����� DESC,
         ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 1),
         ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 1),
         ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 1),
         val.ReturnRate DESC;



--UPDATE dbo.FundCenter_Funds
--SET IsOptional = 1
--WHERE IsOptional = 0
--      --AND Code IN
--      --    (
--      --        SELECT DISTINCT
--      --            per.FundCode
--      --        FROM dbo.FundCenter_PeriodIncreases per
--      --        WHERE (
--      --                  per.Title IN ( N'Z' )
--      --                  AND ISNULL(per.ReturnRate, 999) >= 3
--      --                  OR per.Title IN ( N'Y' )
--      --                     AND ISNULL(per.ReturnRate, 999) >= 4
--      --                  OR per.Title IN ( N'3Y' )
--      --                     AND ISNULL(per.ReturnRate, 999) >= 5
--      --                  OR per.Title IN ( N'6Y' )
--      --                     AND ISNULL(per.ReturnRate, 999) >= 8
--      --                  OR per.Title IN ( N'1N' )
--      --                     AND ISNULL(per.ReturnRate, 999) >= 20
--      --                  OR per.Title IN ( N'2N' )
--      --                     AND ISNULL(per.ReturnRate, 999) >= 40
--      --              )
--      --    )
--      AND Code IN
--          (
--              SELECT TOP 300
--                  fun.Code �������
--              FROM dbo.FundCenter_Funds fun
--                  LEFT JOIN dbo.FundCenter_Valuations val
--                      ON val.FundCode = fun.Code
--                  LEFT JOIN #dtGreat
--                      ON #dtGreat.Code = fun.Code
--              WHERE fun.TypeName NOT IN ( '���-FOF', '������', '�����', '��������', 'ծȯ����-����', '����' )
--                    AND fun.IsOptional = 0
--                    AND NOT EXISTS
--              (
--                  SELECT 1 FROM FundCenter_NotTradeFunds ntf WHERE ntf.FundCode = fun.Code
--              )
--              ORDER BY ROUND((1 / val.EstimatedUnitNetWorth - 1 / #dtGreat.GreatSale) * 100, 3) DESC,
--                       ROUND((1 / #dtGreat.GreatBuy - 1 / val.EstimatedUnitNetWorth), 3) DESC,
--                       val.ReturnRate DESC
--          );



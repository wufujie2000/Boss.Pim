DECLARE @PeriodStartDate DATETIME;
DECLARE @PeriodIncreaseDate DATETIME;
DECLARE @NetWorthDate DATETIME;
DECLARE @SellDate DATETIME;

SET @NetWorthDate =
(
    SELECT MAX(PeriodStartDate) FROM dbo.FundCenter_NetWorthPeriodAnalyses
);
SET @SellDate = @NetWorthDate + 7;
SET @PeriodIncreaseDate = @NetWorthDate;
SET @PeriodStartDate = @NetWorthDate;


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



IF OBJECT_ID('tempdb..#dtGuess') IS NOT NULL
    DROP TABLE #dtGuess;
SELECT *
INTO #dtGuess
FROM
(
    SELECT fun.TypeName �������,
           fun.ShortName ��������,
           fun.Code �������,
           CONVERT(VARCHAR(32), val.EstimatedTime, 20) ʱ��,
           val.ReturnRate �Ƿ�,
           ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 1) ��3���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 1) ��6���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 1) ��1���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(z.Rank, -1)) / z.SameTypeTotalQty, 1) ��1���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(n1.Rank, -1)) / n1.SameTypeTotalQty, 1) ��1���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(n2.Rank, -1)) / n2.SameTypeTotalQty, 1) ��2���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(n3.Rank, -1)) / n3.SameTypeTotalQty, 1) ��3���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(n5.Rank, -1)) / n5.SameTypeTotalQty, 1) ��5���ű�,
           ROUND((#dtGreat.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 4) ������,
           ROUND((val.EstimatedUnitNetWorth / #dtGreat.GreatBuy - 1) * 100, 4) ����,
           rate.Title ���ʷ�Χ,
           rate.Rate ����,
           y3.Rank ��3������,
           y6.Rank ��6������,
           y.Rank ��1������,
           z.Rank ��1������,
           n1.Rank ��1������,
           z.SameTypeTotalQty ͬ���������,
           z.SameTypeAverage ͬ��ƽ��ֵ,
           y3.ReturnRate ��3���Ƿ�,
           y6.ReturnRate ��6���Ƿ�,
           y.ReturnRate ��1���Ƿ�,
           z.ReturnRate ��1���Ƿ�,
           n1.ReturnRate ��1���Ƿ�,
           ana.Score ����,
           ana.ScoreDescription ��������,
           ana.SharpeRatio ���ձ���,
           ana.ScoreShort ��������,
           ana.CompanyScore ��˾����,
           ana.AssetScore �ʲ�����,
           CONVERT(VARCHAR(32), @PeriodStartDate, 23) �׶�����,
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
        INNER JOIN dbo.FundCenter_Valuations val
            ON val.FundCode = fun.Code
               AND DATEDIFF(DAY, @NetWorthDate, val.EstimatedTime) = 0
        LEFT JOIN #dtGreat
            ON #dtGreat.Code = fun.Code
        LEFT JOIN dbo.FundCenter_PeriodIncreases n5
            ON n5.FundCode = fun.Code
               AND n5.Title IN ( N'5N' )
               AND (
                       DATEDIFF(DAY, n5.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, n5.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND n5.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases n3
            ON n3.FundCode = fun.Code
               AND n3.Title IN ( N'3N' )
               AND (
                       DATEDIFF(DAY, n3.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, n3.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND n3.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases n2
            ON n2.FundCode = fun.Code
               AND n2.Title IN ( N'2N' )
               AND (
                       DATEDIFF(DAY, n2.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, n2.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND n2.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases n1
            ON n1.FundCode = fun.Code
               AND n1.Title IN ( N'1N' )
               AND (
                       DATEDIFF(DAY, n1.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, n1.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND n1.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases y6
            ON y6.FundCode = fun.Code
               AND y6.Title IN ( N'6Y' )
               AND (
                       DATEDIFF(DAY, y6.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, y6.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND y6.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases y3
            ON y3.FundCode = fun.Code
               AND y3.Title IN ( N'3Y' )
               AND (
                       DATEDIFF(DAY, y3.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, y3.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND y3.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases y
            ON y.FundCode = fun.Code
               AND y.Title IN ( N'Y' )
               AND (
                       DATEDIFF(DAY, y.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, y.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND y.Rank > 0
        LEFT JOIN dbo.FundCenter_PeriodIncreases z
            ON z.FundCode = fun.Code
               AND z.Title IN ( N'Z' )
               AND (
                       DATEDIFF(DAY, z.CreationTime, @PeriodIncreaseDate) = 0
                       OR DATEDIFF(DAY, z.LastModificationTime, @PeriodIncreaseDate) = 0
                   )
               AND z.Rank > 0
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
                                       '100�� �� ������ < 250��', '10����Ԫ �� ������ < 30����Ԫ', '30����Ԫ �� ������ < 60����Ԫ',
                                       '20����Ԫ �� ������ < 100����Ԫ', '100����Ԫ �� ������ < 200����Ԫ'
                                     )
    WHERE fun.TypeName NOT IN ( '���-FOF', '������', '�����', '��������', 'ծȯ����-����', '����' )
          AND ntf.Id IS NULL
) g;



SELECT TOP 500
    *
FROM #dtGuess
--WHERE ISNULL(��3���ű�, 1) <= 0.09
--      AND ISNULL(��1���ű�, 1) <= 0.09
--      AND ISNULL(��6���ű�, 1) <= 0.09
--      AND ISNULL(��1���ű�, -1) <= 0.1
--      AND ISNULL(��1���ű�, -1) <= 0.1
--      AND ���� > 5
--      AND ������ > 5
--AND ���� > 60
--AND �������� > 60
--AND �������� > 60
ORDER BY
    --ʵ�Ƿ� DESC,
    ��3���ű�,
    ��1���ű�,
    ��6���ű�,
    ������ DESC,
    ���� DESC,
    ��1���ű�,
    ��1���ű�;



--SELECT AVG(ʵ�Ƿ�) �Ƿ�,
--       COUNT(1) ����
--FROM #dtActual
--WHERE ���� > 5
--      AND ������ > 5
--      AND ISNULL(��3���ű�, 1) <= 0.1
--      AND ISNULL(��1���ű�, 1) < 0.1
--      AND ISNULL(��6���ű�, 1) < 0.1
--      AND ISNULL(��1���ű�, -1) < 0.4
--      AND ISNULL(��1���ű�, -1) < 0.4
--      AND ���� > 60
--      AND �������� > 60
--      AND �������� > 60;
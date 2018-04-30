--SELECT fun.TypeName �������,
--       CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
--       fun.ShortName ��������,
--       fun.Code �������,
--       val.ReturnRate ��ֵ�Ƿ�,
--       DATEDIFF(DAY, tra.BuyTime, GETDATE()) ��������,
--       ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth)) * 100, 3) �������,
--       trasell.Title ����,
--       trasell.Rate ����,
--       --(((val.EstimatedUnitNetWorth - tra.BuyUnitNetWorth) / val.EstimatedUnitNetWorth * tra.BuyUnitNetWorth) * 100
--       -- - ISNULL(trasell.Rate, 0)
--       --) * tra.BuyAmount - DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 ��ʣ����,
--       y3.Rank ��3������,
--       y6.Rank ��6������,
--       y.Rank ��1������,
--       z.Rank ��1������,
--       n1.Rank ��1������,
--       ISNULL(trasell.Rate, 0) * tra.BuyAmount ����������,
--       DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 ��Ϣ,
--       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * tra.BuyAmount ������,
--       y3.ReturnRate ��3���Ƿ�,
--       y6.ReturnRate ��6���Ƿ�,
--       y.ReturnRate ��1���Ƿ�,
--       z.ReturnRate ��1���Ƿ�,
--       n1.ReturnRate ��1���Ƿ�,
--       z.SameTypeTotalQty ͬ���������,
--       z.SameTypeAverage ͬ��ƽ��ֵ,
--       ana.Score ����,
--       ana.ScoreDescription ��������,
--       ana.SharpeRatio ���ձ���,
--       ana.ScoreShort ��������,
--       ana.CompanyScore ��˾����,
--       ana.AssetScore �ʲ�����,
--       trasell.Rate ��������,
--       ana.ScoreMedium ��������,
--       ana.ScoreLong ��������,
--       ana.Profitability ӯ������,
--       ana.AntiRisk ����������,
--       ana.Stability �ȶ���,
--       ana.TimingSelection ��ʱ����,
--       ana.IndexFollowing ָ������,
--       ana.Experience ��������,
--       ana.AnalyseDescription ��������,
--       CONVERT(VARCHAR(32), tra.BuyTime, 23) ��������,
--       fun.DkhsCode
--FROM dbo.FundCenter_TradeRecords tra
--    INNER JOIN dbo.FundCenter_Funds fun
--        ON fun.Code = tra.FundCode
--    LEFT JOIN dbo.FundCenter_Valuations val
--        ON val.FundCode = tra.FundCode
--    LEFT JOIN dbo.FundCenter_TradeRates trasell
--        ON trasell.FundCode = fun.Code
--           AND trasell.RateType = 2
--           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) >= trasell.MinDayRange
--           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) < trasell.MaxDayRange
--    LEFT JOIN dbo.FundCenter_PeriodIncreases n1
--        ON n1.FundCode = fun.Code
--           AND n1.Title IN ( N'1N' )
--           AND (
--                   DATEDIFF(DAY, n1.CreationTime, GETDATE()) = 0
--                   OR DATEDIFF(DAY, n1.LastModificationTime, GETDATE()) = 0
--               )
--           AND n1.Rank > 0
--    LEFT JOIN dbo.FundCenter_PeriodIncreases y6
--        ON y6.FundCode = fun.Code
--           AND y6.Title IN ( N'6Y' )
--           AND (
--                   DATEDIFF(DAY, y6.CreationTime, GETDATE()) = 0
--                   OR DATEDIFF(DAY, y6.LastModificationTime, GETDATE()) = 0
--               )
--           AND y6.Rank > 0
--    LEFT JOIN dbo.FundCenter_PeriodIncreases y3
--        ON y3.FundCode = fun.Code
--           AND y3.Title IN ( N'3Y' )
--           AND (
--                   DATEDIFF(DAY, y3.CreationTime, GETDATE()) = 0
--                   OR DATEDIFF(DAY, y3.LastModificationTime, GETDATE()) = 0
--               )
--           AND y3.Rank > 0
--    LEFT JOIN dbo.FundCenter_PeriodIncreases y
--        ON y.FundCode = fun.Code
--           AND y.Title IN ( N'Y' )
--           AND (
--                   DATEDIFF(DAY, y.CreationTime, GETDATE()) = 0
--                   OR DATEDIFF(DAY, y.LastModificationTime, GETDATE()) = 0
--               )
--           AND y.Rank > 0
--    LEFT JOIN dbo.FundCenter_PeriodIncreases z
--        ON z.FundCode = fun.Code
--           AND z.Title IN ( N'Z' )
--           AND (
--                   DATEDIFF(DAY, z.CreationTime, GETDATE()) = 0
--                   OR DATEDIFF(DAY, z.LastModificationTime, GETDATE()) = 0
--               )
--           AND z.Rank > 0
--    LEFT JOIN dbo.FundCenter_Analyses ana
--        ON ana.FundCode = fun.Code
--WHERE tra.UserId = 3
--ORDER BY ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth)) * 100, 3) DESC;





DECLARE @PeriodStartDate DATETIME;
DECLARE @PeriodIncreaseDate DATETIME;
DECLARE @NetWorthDate DATETIME;
DECLARE @SellDate DATETIME;

SET @NetWorthDate = '2018-4-17';
SET @SellDate = '2018-4-25';
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
           DATEDIFF(DAY, tra.BuyTime, GETDATE()) ��������,
           ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * 100, 4) ������,
           ROUND((val.EstimatedUnitNetWorth / #dtGreat.GreatSale - 1) * 100, 4) ������,
           ROUND((#dtGreat.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 4) ������,
           ROUND((val.EstimatedUnitNetWorth / #dtGreat.GreatBuy - 1) * 100, 4) ����,
           y3.Rank ��3������,
           y6.Rank ��6������,
           y.Rank ��1������,
           z.Rank ��1������,
           n1.Rank ��1������,
           z.SameTypeTotalQty ͬ���������,
           z.SameTypeAverage ͬ��ƽ��ֵ,
           ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 1) ��3���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 1) ��6���ű�,
           ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 1) ��1���ű�,
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
        INNER JOIN dbo.FundCenter_TradeRecords tra
            ON tra.FundCode = fun.Code
               AND tra.UserId = 3
        LEFT JOIN dbo.FundCenter_Valuations val
            ON val.FundCode = fun.Code
        LEFT JOIN #dtGreat
            ON #dtGreat.Code = fun.Code
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
    --LEFT JOIN dbo.FundCenter_TradeRates rate
    --    ON rate.FundCode = fun.Code
    --       AND rate.RateType = 1
    --       AND rate.Title NOT IN ( '500�� �� ������ < 1000��', '100�� �� ������ < 500��', '100�� �� ������ < 200��',
    --                               '200�� �� ������ < 500��', '50�� �� ������ < 200��', '100�� �� ������ < 300��',
    --                               '300�� �� ������ < 500��', '50�� �� ������ < 100��', '50�� �� ������ < 250��',
    --                               '250�� �� ������ < 500��', '50�� �� ������ < 500��', '100�� �� ������ < 1000��', '������ �� 500��',
    --                               '100�� �� ������ < 250��', '10����Ԫ �� ������ < 30����Ԫ', '30����Ԫ �� ������ < 60����Ԫ'
    --                             )
    WHERE fun.TypeName NOT IN ( '���-FOF', '������', '�����', '��������', 'ծȯ����-����', '����' )
          AND ntf.Id IS NULL
          AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 2), 0) < 0.2
          AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 2), 0) < 0.1
          AND ISNULL(ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 2), 0) < 0.2
) g;




SELECT gue.�������,
       gue.��������,
       gue.�������,
       ROUND((sell.UnitNetWorth / buy.UnitNetWorth - 1) * 100, 4) �Ƿ�,
       gue.������,
       gue.������,
       gue.��3������,
       gue.��6������,
       gue.��1������,
       gue.��1������,
       gue.��1������,
       gue.ͬ���������,
       gue.ͬ��ƽ��ֵ,
       gue.��3���ű�,
       gue.��6���ű�,
       gue.��1���ű�,
       gue.��3���Ƿ�,
       gue.��6���Ƿ�,
       gue.��1���Ƿ�,
       gue.��1���Ƿ�,
       gue.��1���Ƿ�,
       gue.����,
       gue.��������,
       gue.���ձ���,
       gue.��������,
       gue.��˾����,
       gue.�ʲ�����,
       gue.�׶�����,
       gue.��������,
       gue.��������,
       gue.ӯ������,
       gue.����������,
       gue.�ȶ���,
       gue.��ʱ����,
       gue.ָ������,
       gue.��������,
       gue.��������,
       gue.DkhsCode
FROM #dtGuess gue
    INNER JOIN dbo.FundCenter_NetWorths buy
        ON buy.FundCode = gue.�������
    INNER JOIN dbo.FundCenter_NetWorths sell
        ON sell.FundCode = gue.�������
WHERE buy.UnitNetWorth > 0
      AND sell.UnitNetWorth > 0
      AND buy.Date = @NetWorthDate
      AND DATEDIFF(DAY, sell.Date, @SellDate) = 0
ORDER BY ROUND(������, 0) DESC,
         ������,
         ��6���ű�,
         ��3���ű�,
         ��1���ű�,
         �Ƿ� DESC;



SELECT SUM((sell.UnitNetWorth / buy.UnitNetWorth - 1) * 100) �Ƿ�,
       100000 * (SUM(sell.UnitNetWorth / buy.UnitNetWorth - 1) - 0.01) ����
FROM #dtGuess gue
    INNER JOIN dbo.FundCenter_NetWorths buy
        ON buy.FundCode = gue.�������
    INNER JOIN dbo.FundCenter_NetWorths sell
        ON sell.FundCode = gue.�������
WHERE buy.UnitNetWorth > 0
      AND sell.UnitNetWorth > 0
      AND buy.Date = @NetWorthDate
      AND DATEDIFF(DAY, sell.Date, @SellDate) = 0;
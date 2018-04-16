DECLARE @PeriodStartDate DATETIME;
DECLARE @ClosingDate DATETIME;
SET @PeriodStartDate =
(
    SELECT MAX(PeriodStartDate) FROM dbo.FundCenter_NetWorthPeriodAnalyses
);

IF OBJECT_ID('tempdb..##NetWorthAvg') IS NOT NULL
    DROP TABLE ##NetWorthAvg;
SELECT *
INTO ##NetWorthAvg
FROM
(
    SELECT fun.Code FundCode,
           @PeriodStartDate PeriodStartDate,
           ROUND(
                    (CASE
                         WHEN gue20.GreatSale IS NULL THEN
                             gue10.GreatSale
                         WHEN gue30.GreatSale IS NULL THEN
                    (gue10.GreatSale + gue20.GreatSale) / 2
                         WHEN gue40.GreatSale IS NULL THEN
                    (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale) / 3
                         WHEN gue50.GreatSale IS NULL THEN
                    (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale + gue40.GreatSale) / 4
                         WHEN gue60.GreatSale IS NULL THEN
                    (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale + gue40.GreatSale + gue50.GreatSale) / 5
                         ELSE
                    (gue10.GreatSale + gue20.GreatSale + gue30.GreatSale + gue40.GreatSale + gue50.GreatSale
                     + gue60.GreatSale
                    ) / 6
                     END - 1
                    ) * 100,
                    3
                ) 收益和,
           ROUND(
                    CASE
                        WHEN gue20.GreatBuy IS NULL THEN
                    (gue10.GreatBuy)
                        WHEN gue30.GreatBuy IS NULL THEN
                    (gue10.GreatBuy + gue20.GreatBuy) / 2
                        WHEN gue40.GreatBuy IS NULL THEN
                    (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy) / 3
                        WHEN gue50.GreatBuy IS NULL THEN
                    (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy + gue40.GreatBuy) / 4
                        WHEN gue60.GreatBuy IS NULL THEN
                    (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy + gue40.GreatBuy + gue50.GreatBuy) / 5
                        ELSE
                    (gue10.GreatBuy + gue20.GreatBuy + gue30.GreatBuy + gue40.GreatBuy + gue50.GreatBuy
                     + gue60.GreatBuy
                    ) / 6
                    END,
                    2
                ) 建议和
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
    WHERE gue10.Id IS NOT NULL
) t;

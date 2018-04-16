UPDATE dbo.FundCenter_Funds
SET IsOptional = 0
WHERE IsOptional = 1;


UPDATE dbo.FundCenter_Funds
SET IsOptional = 1
WHERE IsOptional = 0
      --这些分类涨幅都很小
      AND TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      --由于评分大多根据历史业绩计算的，容易过滤掉新的潜力基金，因此暂时去掉此条件
      --AND Code IN
      --    (
      --        SELECT FundCode FROM dbo.FundCenter_Analyses WHERE Score > 75
      --    )
      AND Code IN
          (
              SELECT DISTINCT
                  per.FundCode
              FROM dbo.FundCenter_PeriodIncreases per
              WHERE (
                        per.Title IN ( N'Z' )
                        AND ISNULL(per.ReturnRate, 999) >= 2
                        OR per.Title IN ( N'Y' )
                           AND ISNULL(per.ReturnRate, 999) >= 3
                        OR per.Title IN ( N'3Y' )
                           AND ISNULL(per.ReturnRate, 999) >= 5
                        OR per.Title IN ( N'6Y' )
                           AND ISNULL(per.ReturnRate, 999) >= 8
                        OR per.Title IN ( N'1N' )
                           AND ISNULL(per.ReturnRate, 999) >= 15
                        OR per.Title IN ( N'2N' )
                           AND ISNULL(per.ReturnRate, 999) >= 40
                    )
          )
      AND (
              Code IN
              (
                  SELECT FundCode
                  FROM dbo.FundCenter_RatingPools
                  WHERE (
                            MstarRating3 IN ( 4, 5 )
                            OR MstarRating5 IN ( 4, 5 )
                        )
              )
              OR Code IN
                 (
                     SELECT FundCode
                     FROM dbo.FundCenter_RatingPools
                     WHERE (
                               JajxRating3 IN ( 4, 5 )
                               OR JajxRating5 IN ( 4, 5 )
                           )
                 )
              OR Code IN
                 (
                     SELECT FundCode
                     FROM dbo.FundCenter_RatingPools
                     WHERE (
                               ShsecRating3 IN ( 4, 5 )
                               OR ShsecRating5 IN ( 4, 5 )
                           )
                 )
              OR Code IN
                 (
                     SELECT FundCode
                     FROM dbo.FundCenter_RatingPools
                     WHERE (
                               ZssecRating3 IN ( 4, 5 )
                               OR ZssecRating5 IN ( 4, 5 )
                           )
                 )
              OR Code IN
                 (
                     SELECT FundCode
                     FROM dbo.FundCenter_RatingPools
                     WHERE (
                               HtsecRating3 IN ( 5 )
                               OR HtsecRating5 IN ( 5 )
                           )
                 )
              OR Code IN
                 (
                     SELECT FundCode
                     FROM dbo.FundCenter_RatingPools
                     WHERE (
                               TxsecRating3 IN ( 5 )
                               OR TxsecRating5 IN ( 5 )
                           )
                 )
              OR Code IN
                 (
                     SELECT FundCode
                     FROM dbo.FundCenter_RatingPools
                     WHERE (
                               GalaxyRating3 IN ( 5 )
                               OR GalaxyRating5 IN ( 5 )
                           )
                 )
              OR Code IN
                 (
                     --业绩排名考前的基金
                     SELECT DISTINCT
                         per.FundCode
                     FROM FundCenter_PeriodIncreases per
                     WHERE Rank > 0
                           AND ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5) < 0.0446
                           AND per.Title IN ( 'Z', 'Y', '3Y', '6Y' )
                 )
          );





SELECT fun.TypeName 基金分类,
       fun.Code 基金编码,
       fun.ShortName 基金名称,
       ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5),
       per.Rank,
       per.SameTypeTotalQty,
       per.ReturnRate,
       per.Title
FROM dbo.FundCenter_Funds fun
    INNER JOIN FundCenter_PeriodIncreases per
        ON per.FundCode = fun.Code
    INNER JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
WHERE Rank > 0
      AND fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      --AND fun.Code IN
      --    (
      --        SELECT DISTINCT
      --            per.FundCode
      --        FROM dbo.FundCenter_PeriodIncreases per
      --        WHERE (
      --                  per.Title IN ( N'Z' )
      --                  AND ISNULL(per.ReturnRate, 999) >= 5
      --                  OR per.Title IN ( N'Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 6
      --                  OR per.Title IN ( N'3Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 7
      --                  OR per.Title IN ( N'6Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 8
      --                  OR per.Title IN ( N'1N' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 20
      --                  OR per.Title IN ( N'2N' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 40
      --              )
      --    )
      --AND fun.IsOptional = 0
      --AND ana.Score < 60
      AND ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5) < 0.0446
      AND per.Title IN ( 'Z', 'Y', '3Y', '6Y' )
      AND fun.Code IN ( '519601' )
ORDER BY ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5);






SELECT *
FROM dbo.FundCenter_Funds
WHERE TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      AND Code NOT IN
          (
              SELECT FundCode FROM dbo.FundCenter_NotTradeFunds
          );




--删除重复净值分析
DELETE dbo.FundCenter_NetWorthPeriodAnalyses
WHERE Id IN
      (
          SELECT MAX(CONVERT(VARCHAR(64), Id))
          FROM dbo.FundCenter_NetWorthPeriodAnalyses
          GROUP BY FundCode,
                   PeriodStartDate,
                   PeriodDays
          HAVING COUNT(1) > 1
      );



--删除重复净值
DELETE dbo.FundCenter_NetWorths
WHERE Id IN
      (
          SELECT MAX(CONVERT(VARCHAR(64), Id))
          FROM dbo.FundCenter_NetWorths
          GROUP BY FundCode,
                   Date
          HAVING COUNT(1) > 1
      );



--删除重复排名
DELETE dbo.FundCenter_FundRanks
WHERE Id IN
      (
          SELECT MAX(CONVERT(VARCHAR(64), Id))
          FROM dbo.FundCenter_FundRanks
          GROUP BY FundCode,
                   Date
          HAVING COUNT(1) > 1
      );


------删除重复涨幅排名
DELETE dbo.FundCenter_PeriodIncreases
WHERE Id IN
      (
          SELECT MAX(CONVERT(VARCHAR(64), Id))
          FROM dbo.FundCenter_PeriodIncreases
          GROUP BY Title,
                   FundCode,
                   ClosingDate
          HAVING COUNT(1) > 1
      );



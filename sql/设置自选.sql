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
      --AND Code IN
      --    (
      --        SELECT DISTINCT
      --            per.FundCode
      --        FROM dbo.FundCenter_PeriodIncreases per
      --        WHERE (
      --                  per.Title IN ( N'Z' )
      --                  AND ISNULL(per.ReturnRate, 999) >= 2
      --                  OR per.Title IN ( N'Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 3
      --                  OR per.Title IN ( N'3Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 5
      --                  OR per.Title IN ( N'6Y' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 8
      --                  OR per.Title IN ( N'1N' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 15
      --                  OR per.Title IN ( N'2N' )
      --                     AND ISNULL(per.ReturnRate, 999) >= 40
      --              )
      --    )
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
              OR (Code IN
                  (
                      --业绩排名考前的基金
                      SELECT DISTINCT
                          per.FundCode
                      FROM FundCenter_PeriodIncreases per
                      WHERE Rank > 0
                            AND ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5) <= 0.1
                            AND (
                                    DATEDIFF(DAY, per.CreationTime, GETDATE()) = 0
                                    OR DATEDIFF(DAY, per.LastModificationTime, GETDATE()) = 0
                                )
                  )
                 )
          );


UPDATE dbo.FundCenter_Funds
SET IsOptional = 1
WHERE IsOptional = 0
      AND Code IN
          (
              SELECT DISTINCT FundCode FROM dbo.FundCenter_TradeRecords
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
      AND ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5) <= 0.1
      AND (
              DATEDIFF(DAY, per.CreationTime, GETDATE()) = 0
              OR DATEDIFF(DAY, per.LastModificationTime, GETDATE()) = 0
          )
ORDER BY ROUND((CONVERT(FLOAT, per.Rank) / CONVERT(FLOAT, per.SameTypeTotalQty)), 5);





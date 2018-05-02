SELECT fun.TypeName,
       fun.Code,
       fun.ShortName
FROM dbo.FundCenter_Funds fun
WHERE fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      AND NOT EXISTS
(
    SELECT 1
    FROM dbo.FundCenter_NetWorthPeriodAnalyses net
    WHERE fun.Code = net.FundCode
          AND DATEDIFF(DAY, net.PeriodStartDate, GETDATE()) = 0
          AND net.PeriodDays = 10
)
      AND NOT EXISTS
(
    SELECT 1
    FROM dbo.FundCenter_NotTradeFunds net
    WHERE fun.Code = net.FundCode
);







SELECT fun.TypeName,
       fun.Code,
       fun.ShortName
FROM dbo.FundCenter_Funds fun
WHERE fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      AND NOT EXISTS
(
    SELECT 1
    FROM dbo.FundCenter_PeriodIncreases per
    WHERE fun.Code = per.FundCode
          AND (
                  DATEDIFF(DAY, per.LastModificationTime, GETDATE()) = 0
                  OR DATEDIFF(DAY, per.CreationTime, GETDATE()) = 0
              )
          AND per.Title = 'Z'
)
      AND NOT EXISTS
(
    SELECT 1
    FROM dbo.FundCenter_NotTradeFunds net
    WHERE fun.Code = net.FundCode
);






SELECT *
FROM dbo.FundCenter_NetWorths
WHERE FundCode IN
      (
          SELECT fun.Code
          FROM dbo.FundCenter_Funds fun
          WHERE fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
                AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.FundCenter_NetWorthPeriodAnalyses net
              WHERE fun.Code = net.FundCode
                    AND DATEDIFF(DAY, net.PeriodStartDate, GETDATE()) = 0
                    AND net.PeriodDays = 10
          )
                AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.FundCenter_NotTradeFunds net
              WHERE fun.Code = net.FundCode
          )
      )
ORDER BY FundCode,
         Date DESC;



SELECT fun.TypeName,
       fun.DkhsCode,
       fun.Code,
       fun.Name,
       fun.ShortName
FROM dbo.FundCenter_Funds fun
    LEFT JOIN dbo.FundCenter_NetWorths net
        ON fun.Code = net.FundCode
WHERE net.Id IS NULL
      AND TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      AND NOT EXISTS
(
    SELECT 1 FROM dbo.FundCenter_NotTradeFunds nt WHERE nt.FundCode = fun.Code
);





SELECT DISTINCT
    fun.DkhsCode,
    fun.Code,
    COUNT(1)
FROM dbo.FundCenter_Funds fun
    LEFT JOIN dbo.FundCenter_NetWorths net
        ON fun.Code = net.FundCode
WHERE fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
      AND NOT EXISTS
(
    SELECT 1 FROM dbo.FundCenter_NotTradeFunds nt WHERE nt.FundCode = fun.Code
)
      AND (
              Date >= GETDATE() - 120
              OR net.Id IS NULL
          )
GROUP BY fun.Code,
         fun.DkhsCode
HAVING COUNT(1) <
(
    SELECT TOP 1
        COUNT(1)
    FROM dbo.FundCenter_NetWorths
    WHERE Date >= GETDATE() - 120
    GROUP BY FundCode
    ORDER BY COUNT(1) DESC
)
ORDER BY COUNT(1) DESC;



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
                   CreationTime
          HAVING COUNT(1) > 1
      );





DELETE dbo.FundCenter_NetWorths
WHERE UnitNetWorth <= 0;



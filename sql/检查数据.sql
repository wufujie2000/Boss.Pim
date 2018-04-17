SELECT DISTINCT
    fun.DkhsCode,
    fun.Code
FROM dbo.FundCenter_Funds fun
    INNER JOIN dbo.FundCenter_NetWorths net
        ON fun.Code = net.FundCode
WHERE Date > '2018-03-30'
GROUP BY fun.Code,
         fun.DkhsCode
HAVING COUNT(1) < 8;



SELECT *
FROM dbo.FundCenter_NetWorths
WHERE Date > ''
      AND Date NOT IN ( '2018-02-13', '2018-02-14', '2017-04-13', '2017-10-30', '2017-07-17', '2017-09-29',
                        '2017-12-31'
                      )
      AND FundCode IN ( '000001', '000003', '000004', '000043' )
      AND Date IN
          (
              SELECT Date
              FROM dbo.FundCenter_NetWorths
              WHERE Date > GETDATE() - 100
                    AND FundCode IN ( '000001', '000003', '000004', '000043' )
              GROUP BY Date
              HAVING COUNT(1) <> 4
          );




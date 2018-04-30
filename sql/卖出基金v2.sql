SELECT FundCode,
       TradeType,
       SUM(Amount)
FROM dbo.FundCenter_TradeLogs
GROUP BY FundCode,
         TradeType;



SELECT tra.FundCode,
       tra.Time,
       tra.Amount,
	   tra.Portion,
       buy.Amount - sell.Amount
FROM FundCenter_TradeLogs tra
    LEFT JOIN
     (
         SELECT FundCode,
                SUM(Amount) Amount
         FROM dbo.FundCenter_TradeLogs
         WHERE TradeType IN ( 0, 2 )
         GROUP BY FundCode
     ) buy
        ON buy.FundCode = tra.FundCode
    LEFT JOIN
     (
         SELECT FundCode,
                SUM(Amount) Amount
         FROM dbo.FundCenter_TradeLogs
         WHERE TradeType IN ( 1, 3, 4 )
         GROUP BY FundCode
     ) sell
        ON sell.FundCode = tra.FundCode
ORDER BY tra.FundCode,
         tra.Time;


SELECT fun.TypeName �������,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) ��ֵʱ��,
       fun.ShortName ��������,
       fun.Code �������,
       val.ReturnRate ��ֵ�Ƿ�,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) ��������,
       ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) �������,
       trasell.Title ����,
       trasell.Rate ����,
       (((val.EstimatedUnitNetWorth - tra.BuyUnitNetWorth) / val.EstimatedUnitNetWorth * tra.BuyUnitNetWorth) * 100
        - ISNULL(trasell.Rate, 0)
       ) * tra.BuyAmount - DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 ��ʣ����,
       y3.Rank ��3������,
       y6.Rank ��6������,
       y.Rank ��1������,
       z.Rank ��1������,
       n1.Rank ��1������,
       ISNULL(trasell.Rate, 0) * tra.BuyAmount ����������,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) * tra.BuyAmount * 0.0004 ��Ϣ,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * tra.BuyAmount ������,
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
       trasell.Rate ��������,
       ana.ScoreMedium ��������,
       ana.ScoreLong ��������,
       ana.Profitability ӯ������,
       ana.AntiRisk ����������,
       ana.Stability �ȶ���,
       ana.TimingSelection ��ʱ����,
       ana.IndexFollowing ָ������,
       ana.Experience ��������,
       ana.AnalyseDescription ��������,
       CONVERT(VARCHAR(32), tra.BuyTime, 23) ��������,
       fun.DkhsCode
FROM dbo.FundCenter_TradeRecords tra
    INNER JOIN dbo.FundCenter_Funds fun
        ON fun.Code = tra.FundCode
    LEFT JOIN dbo.FundCenter_Valuations val
        ON val.FundCode = tra.FundCode
    LEFT JOIN dbo.FundCenter_TradeRates trasell
        ON trasell.FundCode = fun.Code
           AND trasell.RateType = 2
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) >= trasell.MinDayRange
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) < trasell.MaxDayRange
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
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
WHERE tra.UserId = 3
      AND (
              val.ReturnRate < 0
              OR ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) > 3
          )
      AND ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) > 1
ORDER BY ROUND(((1 / val.EstimatedUnitNetWorth - 1 / tra.BuyUnitNetWorth) - ISNULL(trasell.Rate, 0)) * 100, 3) DESC;






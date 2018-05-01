﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Values;

namespace Boss.Pim.Web.Controllers
{
    public class FundController : PimControllerBase
    {
        public ITradeLogAppService TradeLogAppService { get; set; }

        public IRepository<NetWorthPeriodAnalyse, Guid> NetWorthPeriodAnalyseRepository { get; set; }

        public ActionResult Index(double y3 = 0.09, double y = 0.09, double y6 = 0.09, double n1 = 0.1, double z = 0.1, double sug = 5, double pro = 5)
        {
            var sql = @"
DECLARE @PeriodStartDate DATETIME;
DECLARE @PeriodIncreaseDate DATETIME;
DECLARE @NetWorthDate DATETIME;

SET @NetWorthDate = GETDATE();
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
    SELECT fun.TypeName 基金分类,
           fun.ShortName 基金名称,
           fun.Code 基金编码,
           CONVERT(VARCHAR(32), val.EstimatedTime, 20) 时间,
           val.ReturnRate 涨幅,
           ROUND(CONVERT(FLOAT, ISNULL(z.Rank, -1)) / z.SameTypeTotalQty, 1) 近1周排比,
           ROUND(CONVERT(FLOAT, ISNULL(n1.Rank, -1)) / n1.SameTypeTotalQty, 1) 近1年排比,
           ROUND((#dtGreat.GreatSale / val.EstimatedUnitNetWorth - 1) * 100, 4) 买收益,
           ROUND((val.EstimatedUnitNetWorth / #dtGreat.GreatBuy - 1) * 100, 4) 买建议,
           rate.Title 费率范围,
           rate.Rate 费率,
           y3.Rank 近3月排名,
           y6.Rank 近6月排名,
           y.Rank 近1月排名,
           z.Rank 近1周排名,
           n1.Rank 近1年排名,
           z.SameTypeTotalQty 同类基金总数,
           z.SameTypeAverage 同类平均值,
           y3.ReturnRate 近3月涨幅,
           y6.ReturnRate 近6月涨幅,
           y.ReturnRate 近1月涨幅,
           z.ReturnRate 近1周涨幅,
           n1.ReturnRate 近1年涨幅,
           ana.Score 评分,
           ana.ScoreDescription 评分描述,
           ana.SharpeRatio 夏普比率,
           ana.ScoreShort 短期评分,
           ana.CompanyScore 公司评分,
           ana.AssetScore 资产评分,
           CONVERT(VARCHAR(32), @PeriodStartDate, 23) 阶段日期,
           ana.ScoreMedium 中期评分,
           ana.ScoreLong 长期评分,
           ana.Profitability 盈利能力,
           ana.AntiRisk 抗风险能力,
           ana.Stability 稳定性,
           ana.TimingSelection 择时能力,
           ana.IndexFollowing 指数评分,
           ana.Experience 经验评分,
           ana.AnalyseDescription 分析描述,
           ROUND(CONVERT(FLOAT, ISNULL(y3.Rank, -1)) / y3.SameTypeTotalQty, 1) 近3月排比,
           ROUND(CONVERT(FLOAT, ISNULL(y6.Rank, -1)) / y6.SameTypeTotalQty, 1) 近6月排比,
           ROUND(CONVERT(FLOAT, ISNULL(y.Rank, -1)) / y.SameTypeTotalQty, 1) 近1月排比,
           ROUND(CONVERT(FLOAT, ISNULL(n2.Rank, -1)) / n2.SameTypeTotalQty, 1) 近2年排比,
           ROUND(CONVERT(FLOAT, ISNULL(n3.Rank, -1)) / n3.SameTypeTotalQty, 1) 近3年排比,
           ROUND(CONVERT(FLOAT, ISNULL(n5.Rank, -1)) / n5.SameTypeTotalQty, 1) 近5年排比,
           fun.DkhsCode
    FROM dbo.FundCenter_Funds fun
        INNER JOIN dbo.FundCenter_Valuations val
            ON val.FundCode = fun.Code
               AND (
                       DATEDIFF(DAY, val.CreationTime, @NetWorthDate) = 0
                       OR DATEDIFF(DAY, val.LastModificationTime, @NetWorthDate) = 0
                   )
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
               AND rate.Title NOT IN ( '500万 ≤ 购买金额 < 1000万', '100万 ≤ 购买金额 < 500万', '100万 ≤ 购买金额 < 200万',
                                       '200万 ≤ 购买金额 < 500万', '50万 ≤ 购买金额 < 200万', '100万 ≤ 购买金额 < 300万',
                                       '300万 ≤ 购买金额 < 500万', '50万 ≤ 购买金额 < 100万', '50万 ≤ 购买金额 < 250万',
                                       '250万 ≤ 购买金额 < 500万', '50万 ≤ 购买金额 < 500万', '100万 ≤ 购买金额 < 1000万', '购买金额 ≥ 500万',
                                       '100万 ≤ 购买金额 < 250万', '10万美元 ≤ 购买金额 < 30万美元', '30万美元 ≤ 购买金额 < 60万美元',
                                       '20万美元 ≤ 购买金额 < 100万美元', '100万美元 ≤ 购买金额 < 200万美元'
                                     )
    WHERE fun.TypeName NOT IN ( '混合-FOF', '货币型', '理财型', '其他创新', '债券创新-场内', '其他' )
          AND ntf.Id IS NULL
) g;



IF OBJECT_ID('tempdb..#dtWillBuy') IS NOT NULL
    DROP TABLE #dtWillBuy;
SELECT *
INTO #dtWillBuy
FROM
(
    SELECT TOP 500
        *
    FROM #dtGuess
    WHERE ISNULL(近3月排比, 1) <= @y3
          AND ISNULL(近1月排比, 1) <= @y
          AND ISNULL(近6月排比, 1) <= @y6
          AND ISNULL(近1年排比, -1) <= @n1
          AND ISNULL(近1周排比, -1) <= @z
          AND 买建议 > @sug
          AND 买收益 > @pro
    --AND 评分 > 60
    --AND 中期评分 > 60
    --AND 短期评分 > 60
    ORDER BY
        --实涨幅 DESC,
        近3月排比,
        近1月排比,
        近6月排比,
        近1周排比,
        近1年排比,
        买收益 DESC,
        买建议 DESC
) w;


SELECT *
FROM #dtWillBuy;

SELECT GreatBuy 最低买入值,
       GreatSale 最高卖出值,
       CONVERT(VARCHAR(32),PeriodStartDate,23) 日期,
       PeriodDays 阶段天数,
       Avg 平均值,
       Later 月末值,
       Max 最大值,
       Min 最小值,
       MaxAvg 平均最大值,
       MinAvg 平均最小值,
       DieFu 跌幅值,
       ZhangFu 涨幅值,
       BoWave 波动值,
       SafeLow 安全期最低值,
       SafeHigh 安全期最高值,
       SafeTradeCent 安全期买卖价,
       PayWaveRate 盈利波动,
       MaxPayCent 最大盈利值,
       MaxLoseCent 最大亏损净值,
       FundCode
FROM dbo.FundCenter_NetWorthPeriodAnalyses
WHERE FundCode IN
      (
          SELECT 基金编码 FROM #dtWillBuy
      )
      AND DATEDIFF(DAY, PeriodStartDate, @PeriodStartDate) = 0
ORDER BY FundCode,
         PeriodDays;
";
            SQLUtil db = new SQLUtil();
            db.AddParameter("@y6", y6);
            db.AddParameter("@y3", y3);
            db.AddParameter("@y", y);
            db.AddParameter("@z", z);
            db.AddParameter("@n1", n1);
            db.AddParameter("@sug", sug);
            db.AddParameter("@pro", pro);
            var ds = db.ExecDataSet(sql);
            ViewBag.dt1 = ds.Tables[0];
            ViewBag.dt2 = ds.Tables[1];
            return View();
        }


        public async Task<ActionResult> BuyList()
        {
            var sql = @"
SELECT fun.TypeName 基金分类,
       CONVERT(VARCHAR(32), val.EstimatedTime, 20) 估值时间,
       fun.ShortName 基金名称,
       fun.Code 基金编码,
       val.ReturnRate 估值涨幅,
       DATEDIFF(DAY, tra.BuyTime, GETDATE()) 持有天数,
       ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * 100, 3) 估收益比,
       ROUND(
                ((gue10.GreatSale / val.EstimatedUnitNetWorth - 1) + (gue20.GreatSale / val.EstimatedUnitNetWorth - 1)
                 + (gue30.GreatSale / val.EstimatedUnitNetWorth - 1)
                ) * 100 / 3,
                2
            ) 收益和,
       ROUND(
                (val.EstimatedUnitNetWorth / gue10.GreatBuy - 1) + (val.EstimatedUnitNetWorth / gue20.GreatBuy - 1)
                + (val.EstimatedUnitNetWorth / gue30.GreatBuy - 1) / 3,
                4
            ) 建议和,
       y3.Rank 近3月排名,
       y6.Rank 近6月排名,
       y.Rank 近1月排名,
       z.Rank 近1周排名,
       n1.Rank 近1年排名,
       y3.ReturnRate 近3月涨幅,
       y6.ReturnRate 近6月涨幅,
       y.ReturnRate 近1月涨幅,
       z.ReturnRate 近1周涨幅,
       n1.ReturnRate 近1年涨幅,
       (val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1) * tra.BuyAmount 估收益,
       z.SameTypeTotalQty 同类基金总数,
       z.SameTypeAverage 同类平均值,
       ana.Score 评分,
       ana.ScoreDescription 评分描述,
       ana.SharpeRatio 夏普比率,
       ana.ScoreShort 短期评分,
       ana.CompanyScore 公司评分,
       ana.AssetScore 资产评分,
       trasell.Rate 卖出费率,
       ana.ScoreMedium 中期评分,
       ana.ScoreLong 长期评分,
       ana.Profitability 盈利能力,
       ana.AntiRisk 抗风险能力,
       ana.Stability 稳定性,
       ana.TimingSelection 择时能力,
       ana.IndexFollowing 指数评分,
       ana.Experience 经验评分,
       ana.AnalyseDescription 分析描述,
       CONVERT(VARCHAR(32), tra.BuyTime, 23) 购买日期,
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
           AND DATEDIFF(DAY, tra.BuyTime, GETDATE()) <= trasell.MaxDayRange
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue10
        ON gue10.FundCode = fun.Code
           AND DATEDIFF(DAY, gue10.PeriodStartDate, tra.BuyTime) = 0
           AND gue10.PeriodDays = 10
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue20
        ON gue20.FundCode = fun.Code
           AND DATEDIFF(DAY, gue20.PeriodStartDate, tra.BuyTime) = 0
           AND gue20.PeriodDays = 20
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue30
        ON gue30.FundCode = fun.Code
           AND DATEDIFF(DAY, gue30.PeriodStartDate, tra.BuyTime) = 0
           AND gue30.PeriodDays = 30
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue40
        ON gue40.FundCode = fun.Code
           AND DATEDIFF(DAY, gue40.PeriodStartDate, tra.BuyTime) = 0
           AND gue40.PeriodDays = 40
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue50
        ON gue50.FundCode = fun.Code
           AND DATEDIFF(DAY, gue50.PeriodStartDate, tra.BuyTime) = 0
           AND gue50.PeriodDays = 50
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue60
        ON gue60.FundCode = fun.Code
           AND DATEDIFF(DAY, gue60.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 60
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue70
        ON gue70.FundCode = fun.Code
           AND DATEDIFF(DAY, gue70.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 70
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue80
        ON gue80.FundCode = fun.Code
           AND DATEDIFF(DAY, gue80.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 80
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue90
        ON gue90.FundCode = fun.Code
           AND DATEDIFF(DAY, gue90.PeriodStartDate, tra.BuyTime) = 0
           AND gue60.PeriodDays = 90
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue100
        ON gue100.FundCode = fun.Code
           AND DATEDIFF(DAY, gue100.PeriodStartDate, tra.BuyTime) = 0
           AND gue100.PeriodDays = 100
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue110
        ON gue110.FundCode = fun.Code
           AND DATEDIFF(DAY, gue110.PeriodStartDate, tra.BuyTime) = 0
           AND gue110.PeriodDays = 110
    LEFT JOIN dbo.FundCenter_NetWorthPeriodAnalyses gue120
        ON gue120.FundCode = fun.Code
           AND DATEDIFF(DAY, gue120.PeriodStartDate, tra.BuyTime) = 0
           AND gue120.PeriodDays = 120
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'1N' )
               AND per.Rank > 0
     ) n1
        ON n1.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'2N' )
               AND per.Rank > 0
     ) n2
        ON n2.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'3N' )
               AND per.Rank > 0
     ) n3
        ON n3.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'5N' )
               AND per.Rank > 0
     ) n5
        ON n5.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'6Y' )
               AND per.Rank > 0
     ) y6
        ON y6.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'3Y' )
               AND per.Rank > 0
     ) y3
        ON y3.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'Y' )
               AND per.Rank > 0
     ) y
        ON y.FundCode = fun.Code
    LEFT JOIN
     (
         SELECT *
         FROM dbo.FundCenter_PeriodIncreases per
         WHERE per.Title IN ( N'Z' )
               AND per.Rank > 0
     ) z
        ON z.FundCode = fun.Code
    LEFT JOIN dbo.FundCenter_Analyses ana
        ON ana.FundCode = fun.Code
WHERE tra.UserId = 3
      AND val.ReturnRate < 0.5
      AND ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * 100, 3) > 1
ORDER BY ROUND((val.EstimatedUnitNetWorth / tra.BuyUnitNetWorth - 1 - ISNULL(trasell.Rate, 0)) * 100, 1) DESC;
";

            SQLUtil db = new SQLUtil();
            var dt = db.ExecDataTable(sql);
            return View(dt);
        }

        [AbpMvcAuthorize]
        public ActionResult Buy()
        {
            return View();
        }

        [AbpMvcAuthorize]
        public ActionResult BuyImport()
        {
            return View();
        }

        [AbpMvcAuthorize]
        public async Task<JsonResult> BuyPostImport()
        {
            var list = CheckTableToList((row, rowIndex) =>
             {
                 var fundCode = CheckAndGetRow(rowIndex, 0, row);
                 var time = CheckAndGetRow(rowIndex, 1, row).TryToDateTime();
                 var amount = CheckAndGetRow(rowIndex, 2, row).TryToFloat();
                 var type = (TradeRecordType)(CheckAndGetRow(rowIndex, 3, row).TryToInt());
                 var rate = CheckAndGetRow(rowIndex, 4, row).TryToFloat();
                 return new TradeLogBuyInput
                 {
                     Amount = amount,
                     FundCode = fundCode,
                     ServiceRate = rate,
                     Time = time,
                     TradeType = type
                 };
             });
            foreach (var item in list)
            {
                await TradeLogAppService.Buy(item);
            }
            return Json(1);
        }

        [AbpMvcAuthorize]
        public async Task<ActionResult> BuyPost(TradeLogBuyInput input)
        {
            await TradeLogAppService.Buy(input);
            return Redirect("/Fund/Buy");
        }

        public ActionResult Sell()
        {
            return View();
        }
        [AbpMvcAuthorize]
        public ActionResult SellImport()
        {
            return View();
        }

        [AbpMvcAuthorize]
        public async Task<JsonResult> SellPostImport()
        {
            var list = CheckTableToList((row, rowIndex) =>
            {
                var fundCode = CheckAndGetRow(rowIndex, 0, row);
                var time = CheckAndGetRow(rowIndex, 1, row).TryToDateTime();
                var amount = CheckAndGetRow(rowIndex, 2, row).TryToFloat();
                var type = (TradeRecordType)(CheckAndGetRow(rowIndex, 3, row).TryToInt());
                var rate = CheckAndGetRow(rowIndex, 4, row).TryToFloat();
                return new TradeLogSellInput
                {
                    Portion = 0,
                    FundCode = fundCode,
                    ServiceRate = rate,
                    Time = time,
                    Amount = amount,
                    TradeType = type
                };
            });
            foreach (var item in list)
            {
                await TradeLogAppService.Sell(item);
            }
            return Json(1);
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> SellPost(TradeLogSellInput input)
        {
            await TradeLogAppService.Sell(input);
            return Redirect("/Fund/Sell");
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Hangfire;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Boss.Pim.AdoNet;
using Hangfire;

namespace Boss.Pim.Funds.Workers
{
    public class SetOptionalWorker : PeriodicBackgroundWorkerHangfireBase, ISingletonDependency
    {
        public SetOptionalWorker() : base(Cron.Daily(9, 30))
        {

        }
        /// <summary>
        /// 循环执行
        /// </summary>
        public override void DoWork()
        {
            SetOptional();
        }

        private void SetOptional()
        {
            var sql = @"
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
          )
";

            SQLUtil db = new SQLUtil();
            db.ExecNonQuery(sql);
        }
    }
}
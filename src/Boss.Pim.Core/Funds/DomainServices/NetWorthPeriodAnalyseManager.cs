using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Boss.Pim.Extensions;

namespace Boss.Pim.Funds.DomainServices
{
    public class NetWorthPeriodAnalyseManager : PimDomainServiceBase, ISingletonDependency
    {
        public IRepository<NetWorthPeriodAnalyse, Guid> GuessPrejudgementRepository { get; set; }
        /// <summary>
        /// 按10天拆分 预判
        /// </summary>
        /// <returns></returns>
        public List<NetWorthPeriodAnalyse> CalcGuessPrejudgementSplit10Days(string fundCode, List<NetWorth> newWorthModellist, DateTime startDate)
        {
            List<NetWorthPeriodAnalyse> list = new List<NetWorthPeriodAnalyse>();
            var yearlist = newWorthModellist.Where(a => a.FundCode == fundCode).ToList();
            if (yearlist.Count <= 0)
            {
                return list;
            }
            for (int i = 10; i <= 370;)
            {
                bool isLast = false;
                var info = CalcGuessPrejudgement(fundCode, yearlist, startDate, i, ref isLast);
                if (info == null || isLast)
                {
                    break;
                }
                list.Add(info);
                i = i + 10;
            }
            return list;
        }

        public NetWorthPeriodAnalyse CalcGuessPrejudgement(string fundCode, List<NetWorth> yearlist, DateTime startDate, int days, ref bool isLast)
        {
            if (yearlist.Count <= 0)
            {
                return null;
            }
            var modellist = yearlist.Where(a => a.Date >= startDate.AddDays(-days) && a.Date <= startDate).ToList();
            if (modellist.Count <= 0)
            {
                return null;
            }

            if (yearlist.Count <= modellist.Count)
            {
                isLast = true;
            }

            var list = modellist.Select(a => a.UnitNetWorth).ToList();
            if (list.Count <= 0)
            {
                return null;
            }
            var avg = list.Average();//平均值
            var later = modellist.OrderBy(a => a.Date).Last().UnitNetWorth;//月末值
            var max = list.Max();//最大值
            var min = list.Min();//最小值
            float maxavg = 0;
            float minavg = 0;
            var avgList = list.Where(a => a > avg).ToList();
            if (avgList.Any())
            {
                maxavg = avgList.Average();//平均最大值
                minavg = avgList.Average();//平均最小值
            }
            var diefu = avg - minavg;//跌幅值
            var zhangfu = maxavg - avg;//涨幅值
            var bowave = diefu + zhangfu;//波动值
            var safelow = minavg - bowave;//安全期最低值
            var safehigh = maxavg - bowave;//安全期最高值
            var safetradecent = avg - bowave;//安全期买卖价
            var paywaverate = bowave / safetradecent;//盈利波动
            var maxpaycent = safetradecent + paywaverate;//最大盈利值
            var maxlosecent = safetradecent - paywaverate;//最大亏损净值
            var greatbuy = GetGreatBuy(avg, later, min);//最低买入值
            var greatsale = GetGreatSale(avg, later, max);//最高卖出值

            return new NetWorthPeriodAnalyse
            {
                FundCode = fundCode,

                PeriodDays = days,
                PeriodStartDate = startDate.Date,

                Avg = avg.RoundDigits(),
                BoWave = bowave.RoundDigits(),
                DieFu = diefu.RoundDigits(),
                GreatBuy = greatbuy.RoundDigits(),
                GreatSale = greatsale.RoundDigits(),
                Later = later.RoundDigits(),
                Max = max.RoundDigits(),
                MaxAvg = maxavg.RoundDigits(),
                MaxLoseCent = maxlosecent.RoundDigits(),
                MaxPayCent = maxpaycent.RoundDigits(),
                Min = min.RoundDigits(),
                MinAvg = minavg.RoundDigits(),
                PayWaveRate = paywaverate.RoundDigits(),
                SafeHigh = safehigh.RoundDigits(),
                SafeLow = safelow.RoundDigits(),
                SafeTradeCent = safetradecent.RoundDigits(),
                ZhangFu = zhangfu.RoundDigits()
            };
        }

        public async Task CheckAndInsert(List<NetWorthPeriodAnalyse> list, string fundCode, DateTime periodStartDate)
        {
            if (list.Count <= 0)
            {
                return;
            }
            if (GuessPrejudgementRepository.GetAll().Any(a => a.FundCode == fundCode && a.PeriodStartDate == periodStartDate))
            {
                return;
            }
            int size = 100;
            int page = 1;
            while (true)
            {
                var execList = list.Skip((page - 1) * size).Take(size).ToList();
                if (execList.Count <= 0)
                {
                    break;
                }
                using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                {
                    foreach (var item in list)
                    {
                        await GuessPrejudgementRepository.InsertAsync(item);
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }

        /// <summary>
        /// 最低买入值
        /// </summary>
        /// <param name="a">平均值</param>
        /// <param name="b">月末值</param>
        /// <param name="c">最小值</param>
        /// <returns></returns>
        public float GetGreatBuy(float avg, float later, float min)
        {
            if (later < avg)
            {
                return (float)Math.Round(min * (float)0.98, 4);
            }
            else
            {
                return min;
            }
        }

        /// <summary>
        /// 最高卖出值
        /// </summary>
        /// <param name="a">平均值</param>
        /// <param name="b">月末值</param>
        /// <param name="c">最大值</param>
        /// <returns></returns>
        public float GetGreatSale(float avg, float later, float max)
        {
            if (later < avg)
            {
                return max;
            }
            else
            {
                return (float)Math.Round(max * (float)1.02, 4);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace Boss.Pim.Funds.Services
{
    public class FundManager : PimDomainServiceBase, ISingletonDependency
    {
        public IRepository<NotTradeFund, Guid> NotTradeFundRepository { get; set; }
        public IRepository<FundRank, Guid> FundRankRepository { get; set; }
        public IRepository<TradeRecord, Guid> TradeRecordRepository { get; set; }
        public IRepository<Analyse> AnalyseRepository { get; set; }
        public IRepository<Fund> FundRepository { get; set; }
        public IRepository<NetWorth, Guid> NetWorthRepository { get; set; }

        /// <summary>
        /// 需要执行下载操作的基金编码集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetDownloadFundCodes(bool isOnlyOptional = false, int score = 65)
        {
            IQueryable<Fund> fundsQuery = await GetFundsQuery(isOnlyOptional, score);
            return await AsyncQueryableExecuter.ToListAsync(fundsQuery.Select(a => a.Code));
        }

        private async Task<List<string>> GetSelectFundCodes(bool isOnlyOptional = false, int score = 65)
        {
            List<string> result = new List<string>();

            if (!isOnlyOptional)
            {
                var anaFunds = await AsyncQueryableExecuter.ToListAsync(AnalyseRepository.GetAll().Where(a => a.Score > score).Select(a => a.FundCode).Distinct());
                result.AddRange(anaFunds);
            }

            var tradeFunds = await AsyncQueryableExecuter.ToListAsync(TradeRecordRepository.GetAll().Select(a => a.FundCode).Distinct());
            result.AddRange(tradeFunds);
            var selList = result.Distinct().ToList();
            return selList;
        }

        public async Task<IQueryable<Fund>> GetFundsQuery(bool isOnlyOptional = false, int score = 65)
        {
            List<string> selList = await GetSelectFundCodes(isOnlyOptional, score);

            return GetQuery().Where(a => a.IsOptional == true || selList.Contains(a.Code));
        }

        public IQueryable<Fund> GetQuery()
        {
            var notquery = NotTradeFundRepository.GetAll().Select(a => a.FundCode).Distinct();
            var query = FundRepository.GetAll()
                .Where(a => !a.TypeName.Contains("货币型") && !a.TypeName.Contains("混合-FOF") && !a.TypeName.Contains("其他创新") && !a.TypeName.Contains("债券创新-场内") && !a.TypeName.Contains("理财型") && !a.TypeName.Contains("其他"))
                .Where(a => !notquery.Contains(a.Code))
                ;
            return query;
        }

        public async Task Insert(List<NetWorth> notExistsList)
        {
            int size = 50;
            int page = 1;
            while (true)
            {
                var execList = notExistsList.Skip((page - 1) * size).Take(size).ToList();
                if (execList.Count <= 0)
                {
                    break;
                }
                using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                {
                    foreach (var item in execList)
                    {
                        if (item.UnitNetWorth > 0)
                        {
                            await NetWorthRepository.InsertAsync(item);
                        }
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }

        public async Task CheckInsertFundRank(List<FundRank> list)
        {
            int size = 50;
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
                    foreach (var item in execList)
                    {
                        if (!FundRankRepository.GetAll().Any(a => a.FundCode == item.FundCode && a.Date == item.Date))
                        {
                            await FundRankRepository.InsertAsync(item);
                        }
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }

        public async Task InsertOrUpdateAnalyses(List<Analyse> list)
        {
            if (list == null || list.Count <= 0)
            {
                return;
            }
            int size = 50;
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
                    foreach (var item in execList)
                    {
                        var info = AnalyseRepository.GetAll()
                            .FirstOrDefault(a => a.FundCode == item.FundCode);
                        if (info != null)
                        {
                            info.AntiRisk = item.AntiRisk;
                            info.AssetScore = item.AssetScore;
                            info.CompanyScore = item.CompanyScore;
                            info.AnalyseDescription = item.AnalyseDescription;
                            info.ScoreDescription = item.ScoreDescription;
                            info.ExcessIncome = item.ExcessIncome;
                            info.Stability = item.Stability;
                            info.StockSelection = item.StockSelection;
                            info.TimingSelection = item.TimingSelection;
                            info.Profitability = item.Profitability;
                            info.Score = item.Score;
                            info.ScoreLong = item.ScoreLong;
                            info.ScoreMedium = item.ScoreMedium;
                            info.ScoreShort = item.ScoreShort;
                            info.SharpeRatio = item.SharpeRatio;
                            info.IndexFollowing = item.IndexFollowing;
                            info.Experience = item.Experience;

                            await AnalyseRepository.UpdateAsync(info);
                        }
                        else
                        {
                            await AnalyseRepository.InsertAsync(item);
                        }
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }




        public async Task CheckInsertFunds(List<Fund> list)
        {
            if (list == null || list.Count <= 0)
            {
                return;
            }

            var strList = list.Select(c => c.Code).ToList();
            var dbExistsList = FundRepository.GetAll().Where(s => strList.Contains(s.Code))
                .Select(b => b.Code).Distinct().ToList();
            List<Fund> notExistsList = new List<Fund>();
            ListAddIfExists(list.Where(a => !dbExistsList.Contains(a.Code)).Distinct().ToList(), notExistsList);

            int size = 50;
            int page = 1;
            while (true)
            {
                var execList = notExistsList.Skip((page - 1) * size).Take(size).ToList();
                if (execList.Count <= 0)
                {
                    break;
                }
                using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                {
                    foreach (var item in execList)
                    {
                        await FundRepository.InsertAsync(item);

                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }

        private void ListAddIfExists(List<Fund> list, List<Fund> existsList)
        {
            foreach (var item in list)
            {
                if (!existsList.Exists(a => a.Code == item.Code))
                {
                    existsList.Add(item);
                }
            }
        }

        private void ListAddIfExists<T>(List<T> list, List<T> existsList, Predicate<T> match)
        {
            foreach (var item in list)
            {
                if (!existsList.Exists(match))
                {
                    existsList.Add(item);
                }
            }
        }

    }
}
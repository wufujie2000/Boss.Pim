using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Services;
using Boss.Pim.Sdk.Dkhs.Responses;
using Boss.Pim.Utils;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class FundAllocationAppService : AsyncCrudAppService<FundAllocation, FundAllocationDto>, IFundAllocationAppService
    {
        public WebSrcUtil WebSrcUtil { get; set; }
        public FundManager FundDomainService { get; set; }
        public IRepository<Fund> FundRepository { get; set; }
        public IRepository<FundAllocateHoldSymbol, Guid> FundAllocateHoldSymbolRepository { get; set; }
        public IRepository<FundAllocateIndustry, Guid> FundAllocateIndustryRepository { get; set; }

        public FundAllocationAppService(IRepository<FundAllocation> repository) : base(repository)
        {
        }

        public async Task Download(List<string> fundCodes)
        {
            var funds = FundRepository.GetAll().Where(a => fundCodes.Contains(a.Code)).ToList();
            await Dowload(funds);
        }

        public async Task Download()
        {
            IQueryable<Fund> fundsQuery = await FundDomainService.GetFundsQuery();
            var funds = await AsyncQueryableExecuter.ToListAsync(fundsQuery);
            await Dowload(funds);
        }

        private async Task Dowload(List<Fund> funds)
        {
            foreach (var fund in funds)
            {
                try
                {
                    var dkhsFundCode = await CheckGetDkhsFundCode(fund);
                    if (dkhsFundCode.IsNullOrWhiteSpace())
                    {
                        continue;
                    }
                    var info = await Download(dkhsFundCode);
                    if (info == null)
                    {
                        continue;
                    }
                    await CheckAndInsert(new FundAllocation
                    {
                        BondAsset = info.bond_asset.TryToFloat(),
                        BondPercent = info.bond_percent.TryToFloat(),
                        CashAsset = info.cash_asset.TryToFloat(),
                        CashPercent = info.cash_percent.TryToFloat(),
                        EndDate = info.end_date,
                        FundCode = fund.Code,
                        OtherAsset = info.other_asset.TryToFloat(),
                        OtherPercent = info.other_percent.TryToFloat(),
                        SymbolAsset = info.symbol_asset.TryToFloat(),
                        SymbolPercent = info.symbol_percent.TryToFloat()
                    });

                    if (info.industry != null && info.industry.Any())
                    {
                        List<FundAllocateIndustry> list = new List<FundAllocateIndustry>();
                        foreach (var item in info.industry)
                        {
                            list.Add(new FundAllocateIndustry
                            {
                                Change = item.change,
                                ChangeType = item.change_type,
                                EndDate = item.end_date.TryToDateTime(),
                                FundCode = fund.Code,
                                Percent = item.percent.TryToFloat(),
                                SectorName = item.sector_name
                            });
                        }
                        await CheckAndInsert(list, fund.Code);
                    }

                    if (info.hold_symbol != null && info.hold_symbol.Any())
                    {
                        List<FundAllocateHoldSymbol> list = new List<FundAllocateHoldSymbol>();
                        foreach (var item in info.hold_symbol)
                        {
                            list.Add(new FundAllocateHoldSymbol
                            {
                                AbbrName = item.abbr_name,
                                Asset = item.asset.TryToFloat(),
                                Change = item.change,
                                EndDate = item.end_date.TryToDateTime(),
                                FundCode = fund.Code,
                                Percent = item.percent.TryToFloat(),
                                Percentage = item.percentage.TryToFloat(),
                                Symbol = item.symbol,
                                SymbolType = item.symbol_type
                            });
                        }
                        await CheckAndInsert(list, fund.Code);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(fund + " " + e.Message, e);
                }
            }
        }

        public async Task<string> CheckGetDkhsFundCode(Fund fund)
        {
            if (string.IsNullOrWhiteSpace(fund.DkhsCode))
            {
                string surl = $"https://www.dkhs.com/api/v1/search/symbols/?page=1&page_size=20&q={fund.Code}&symbol_type=3";
                var sstr = await WebSrcUtil.GetToString(surl, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(sstr))
                {
                    return null;
                }
                var sdata = JsonConvert.DeserializeObject<DkhsSearch>(sstr);

                var dkhsCode = sdata.results.FirstOrDefault()?.symbol;
                if (string.IsNullOrWhiteSpace(dkhsCode))
                {
                    return null;
                }
                using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                {
                    fund.DkhsCode = dkhsCode;
                    FundRepository.Update(fund);
                    await uow.CompleteAsync();
                }
            }
            return fund.DkhsCode;
        }

        public async Task<DkhsAssetAllocation> Download(string fundCode)
        {
            string url = $"https://www.dkhs.com/api/v1/symbols/{fundCode}/asset_conf/";
            var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            var data = JsonConvert.DeserializeObject<DkhsAssetAllocation>(str);
            return data;
        }

        private async Task CheckAndInsert(FundAllocation info)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                var dbInfo = Repository.GetAll().FirstOrDefault(a => a.FundCode == info.FundCode);
                if (dbInfo != null)
                {
                    dbInfo.BondAsset = info.BondAsset;
                    dbInfo.BondPercent = info.BondPercent;
                    dbInfo.CashAsset = info.CashAsset;
                    dbInfo.CashPercent = info.CashPercent;
                    dbInfo.OtherAsset = info.OtherAsset;
                    dbInfo.OtherPercent = info.OtherPercent;
                    dbInfo.SymbolAsset = info.SymbolAsset;
                    dbInfo.SymbolPercent = info.SymbolPercent;

                    dbInfo.EndDate = info.EndDate;
                }
                else
                {
                    await Repository.InsertAsync(info);
                }
                await uow.CompleteAsync();
            }
        }

        private async Task CheckAndInsert(List<FundAllocateHoldSymbol> list, string fundCode)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                FundAllocateHoldSymbolRepository.Delete(a => a.FundCode == fundCode);
                foreach (var item in list)
                {
                    await FundAllocateHoldSymbolRepository.InsertAsync(item);
                }
                await uow.CompleteAsync();
            }
        }

        private async Task CheckAndInsert(List<FundAllocateIndustry> list, string fundCode)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                FundAllocateIndustryRepository.Delete(a => a.FundCode == fundCode);
                foreach (var item in list)
                {
                    await FundAllocateIndustryRepository.InsertAsync(item);
                }
                await uow.CompleteAsync();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Services;
using Boss.Pim.Sdk.Dkhs;
using Boss.Pim.Sdk.Dkhs.Responses;
using Boss.Pim.Utils;
using Dapper;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class FundAppService : AsyncCrudAppService<Fund, FundDto>, IFundAppService
    {
        public WebSrcUtil WebSrcUtil { get; set; }
        public IRepository<NotTradeFund, Guid> NotTradeFundRepository { get; set; }
        public FundManager FundDomainService { get; set; }


        public FundAppService(IRepository<Fund> repository) : base(repository)
        {
        }

        public async Task SetOptional(List<string> fundCodes)
        {
            var sql = @"UPDATE dbo.FundCenter_Funds SET IsOptional = 1 WHERE IsOptional = 0 AND Code in @codes";
            using (var conn = new SqlConnection(SQLUtil.DefaultConnStr))
            {
                await conn.ExecuteAsync(sql, new { codes = fundCodes });
            }
        }

        public async Task SetNotTradeFund(string fundCodes)
        {
            var codes = fundCodes.ToStringArray();
            var funds = Repository.GetAll().Where(a => codes.Contains(a.Code)).Select(b => b.Code).ToList();
            var sourcePlatform = "蚂蚁金服";
            if (funds.Any())
            {
                foreach (var item in funds)
                {
                    if (!NotTradeFundRepository.GetAll().Any(a => item == a.FundCode && a.SourcePlatform == sourcePlatform))
                    {
                        await NotTradeFundRepository.InsertAsync(new NotTradeFund
                        {
                            FundCode = item,
                            SourcePlatform = sourcePlatform
                        });
                    }
                }
            }
        }

        public async Task DownloadByEastMoney()
        {
            var url = "http://fund.eastmoney.com/js/fundcode_search.js";
            var htmlText = await WebSrcUtil.GetToString(url);

            htmlText = htmlText.Replace("var r = ", "").Replace(";", "");
            htmlText = htmlText.Replace("[[", "").Replace("]]", "").Replace("[", "").Replace("]", "").Replace(@"""", "");
            var childs = htmlText.Split(',');
            List<Fund> modellist = new List<Fund>();
            for (int n = 0; n < childs.Length; n = n + 5)
            {
                modellist.Add(new Fund
                {
                    Code = childs[n],
                    ShortNameInitials = childs[n + 1],
                    ShortName = childs[n + 2],
                    TypeName = childs[n + 3],
                    ShortNamePinYin = childs[n + 4]
                });
            }
            if (modellist.Count > 0)
            {
                await FundDomainService.CheckInsertFunds(modellist);
            }
        }

        [UnitOfWork(false)]
        public async Task Download()
        {
            Logger.Info("开始下载 更新所有基金");
            int size = 50;
            int page = 1;
            while (true)
            {
                string url =
                    $"https://www.dkhs.com/api/v1/symbols/funds/?page={page}&page_size={size}";
                var str = await WebSrcUtil.GetToString(url, Encoding.Default);
                if (string.IsNullOrWhiteSpace(str))
                {
                    return;
                }
                var data = JsonConvert.DeserializeObject<DkhsResponse<DkhsFundResult>>(str);
                if (data?.results == null || data.results.Length <= 0)
                {
                    Logger.Info("更新所有基金 完成");
                    return;
                }
                await InsertOrUpdateAnalyses(data);

                await InsertFunds(data);

                if (data.current_page > data.total_page || data.results.Length < size)
                {
                    Logger.Info("更新所有基金 完成");
                    return;
                }
                page++;
            }
        }

        private async Task InsertFunds(DkhsResponse<DkhsFundResult> data)
        {
            List<Fund> fundList = new List<Fund>();
            foreach (var item in data.results)
            {
                fundList.Add(new Fund
                {
                    Code = item.code,
                    DkhsCode = item.symbol,
                    Name = item.fund_name,

                    ShortName = item.abbr_name,
                    TypeName = item.symbol_stype_display,
                    ShortNameInitials = item.chi_spell,
                });
            }
            await FundDomainService.CheckInsertFunds(fundList);
        }

        private async Task InsertOrUpdateAnalyses(DkhsResponse<DkhsFundResult> data)
        {
            List<Analyse> analyseList = new List<Analyse>();
            foreach (var item in data.results)
            {
                analyseList.Add(new Analyse
                {
                    FundCode = item.code,

                    AntiRisk = item.anti_risk ?? -1,
                    AssetScore = item.asset_score ?? -1,
                    CompanyScore = item.company_score ?? -1,
                    AnalyseDescription = item.analyse_description,
                    ScoreDescription = item.score_description,
                    ExcessIncome = item.excess_income ?? -1,
                    Stability = item.stability ?? -1,
                    StockSelection = item.stock_selection ?? -1,
                    TimingSelection = item.timing_selection ?? -1,
                    Profitability = item.profitability ?? -1,
                    Score = item.score ?? -1,
                    ScoreLong = item.score_long ?? -1,
                    ScoreMedium = item.score_medium ?? -1,
                    ScoreShort = item.score_short ?? -1,
                    SharpeRatio = item.sharpe_ratio ?? -1,
                    IndexFollowing = item.index_following ?? -1,
                    Experience = item.experience ?? -1
                });
            }
            await FundDomainService.InsertOrUpdateAnalyses(analyseList);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Sdk.Stockstar;
using Boss.Pim.Utils;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class RatingPoolAppService : AsyncCrudAppService<RatingPool, RatingPoolDto>, IRatingPoolAppService
    {
        public WebSrcUtil WebSrcUtil { get; set; }

        public RatingPoolAppService(IRepository<RatingPool> repository) : base(repository)
        {
        }

        public async Task DownloadStockstar()
        {
            Logger.Info("开始下载 更新所有基金评分");
            string url = $"http://canal.stockstar.com/Base/V_JRJ_FUND_LATEST_INFO/limit=8000&sort=NUM%20desc&full=1&d=145653";
            var str = await WebSrcUtil.GetToString(url, Encoding.Default);
            if (string.IsNullOrWhiteSpace(str))
            {
                return;
            }
            str = str.Replace("var V_JRJ_FUND_LATEST_INFO=", "").Replace(";", "");
            var data = JsonConvert.DeserializeObject<StockstarResponse>(str);
            if (data?.rows == null || data.rows.Length <= 0)
            {
                return;
            }
            List<RatingPool> modellist = new List<RatingPool>();
            foreach (var item in data.rows)
            {
                modellist.Add(new RatingPool
                {
                    FundCode = item.FUND_CODE,

                    GalaxyRating3 = item.GALAXY_THYR_RATING.TryToInt(-1),
                    GalaxyRating5 = -1,

                    HtsecRating3 = item.HTSEC_THYR_RATING.TryToInt(-1),
                    HtsecRating5 = -1,

                    JajxRating3 = item.JAJX_THYR_RATING.TryToInt(-1),
                    JajxRating5 = -1,

                    MstarRating3 = item.MSTAR_THYR_RATING.TryToInt(-1),
                    MstarRating5 = item.MSTAR_FYR_RATING.TryToInt(-1),

                    ShsecRating3 = item.SHSEC_THYR_RATING.TryToInt(-1),
                    ShsecRating5 = -1,

                    TxsecRating3 = item.TXSEC_THYR_RATING.TryToInt(-1),
                    TxsecRating5 = -1,

                    ZssecRating3 = item.ZSSEC_THYR_RATING.TryToInt(-1),
                    ZssecRating5 = -1,
                });
            }
            if (modellist.Count > 0)
            {
                await CheckAndInsert(modellist);
            }
            Logger.Info("更新所有基金评分 完成");
        }

        private async Task CheckAndInsert(List<RatingPool> list)
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
                        var info = Repository.GetAll()
                            .FirstOrDefault(a => a.FundCode == item.FundCode);
                        if (info != null)
                        {
                            if (item.GalaxyRating3 > 0)
                            {
                                info.GalaxyRating3 = item.GalaxyRating3;
                            }
                            if (item.GalaxyRating5 > 0)
                            {
                                info.GalaxyRating5 = item.GalaxyRating5;
                            }
                            if (item.HtsecRating3 > 0)
                            {
                                info.HtsecRating3 = item.HtsecRating3;
                            }
                            if (item.HtsecRating5 > 0)
                            {
                                info.HtsecRating5 = item.HtsecRating5;
                            }
                            if (item.JajxRating3 > 0)
                            {
                                info.JajxRating3 = item.JajxRating3;
                            }
                            if (item.JajxRating5 > 0)
                            {
                                info.JajxRating5 = item.JajxRating5;
                            }
                            if (item.MstarRating3 > 0)
                            {
                                info.MstarRating3 = item.MstarRating3;
                            }
                            if (item.MstarRating5 > 0)
                            {
                                info.MstarRating5 = item.MstarRating5;
                            }
                            if (item.ShsecRating3 > 0)
                            {
                                info.ShsecRating3 = item.ShsecRating3;
                            }
                            if (item.ShsecRating5 > 0)
                            {
                                info.ShsecRating5 = item.ShsecRating5;
                            }
                            if (item.TxsecRating3 > 0)
                            {
                                info.TxsecRating3 = item.TxsecRating3;
                            }
                            if (item.TxsecRating5 > 0)
                            {
                                info.TxsecRating5 = item.TxsecRating5;
                            }

                            if (item.ZssecRating3 > 0)
                            {
                                info.ZssecRating3 = item.ZssecRating3;
                            }
                            if (item.ZssecRating5 > 0)
                            {
                                info.ZssecRating5 = item.ZssecRating5;
                            }

                            await Repository.UpdateAsync(info);
                        }
                        else
                        {
                            await Repository.InsertAsync(item);
                        }
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }
    }
}
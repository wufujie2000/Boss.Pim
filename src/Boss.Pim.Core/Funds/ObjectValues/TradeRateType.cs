using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Pim.Funds.ObjectValues
{
    /// <summary>
    /// 交易费率类型
    /// </summary>
    public enum TradeRateType
    {
        认购费率,
        申购费率,

        赎回费率,

        管理费,
        托管费,
        销售服务费,

        贷款费率
    }
}

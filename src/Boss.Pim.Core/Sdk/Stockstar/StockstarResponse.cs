using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boss.Pim.Sdk.Stockstar.Responses;

namespace Boss.Pim.Sdk.Stockstar
{
    /// <summary>
    /// 证券之星 http://www.stockstar.com/ 接口返回数据
    /// </summary>
    [System.Serializable]
    public class StockstarResponse
    {
        public int offset { get; set; }
        public StockstarRatingPoolRow[] rows { get; set; }
        public int total_rows { get; set; }
        public object[] query { get; set; }
        public int millis { get; set; }
    }
}

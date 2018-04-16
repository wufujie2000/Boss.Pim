namespace Boss.Pim.Sdk.Eastmoney
{
    public class EastmoneyResponse<TData> : EastmoneyResponse<TData, object>
    {

    }

    /// <summary>
    /// 天天基金网 接口返回数据
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TExpansion"></typeparam>
    public class EastmoneyResponse<TData, TExpansion>
    {
        public TData[] Datas { get; set; }
        //public int ErrCode { get; set; }
        //public object ErrMsg { get; set; }
        //public int TotalCount { get; set; }
        public TExpansion Expansion { get; set; }
    }
}

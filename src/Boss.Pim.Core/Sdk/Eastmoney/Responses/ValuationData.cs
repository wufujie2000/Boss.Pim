namespace Boss.Pim.Sdk.Eastmoney.Responses
{
    public class ValuationData
    {
        public string FCODE { get; set; }
        public string SHORTNAME { get; set; }
        /// <summary>
        /// 估值日期
        /// </summary>
        public string GZTIME { get; set; }
        /// <summary>
        /// 估值
        /// </summary>
        public float? GSZ { get; set; }
        /// <summary>
        /// 估值涨幅
        /// </summary>
        public float? GSZZL { get; set; }
        public string ISBUY { get; set; }
        public bool BUY { get; set; }
        public string LISTTEXCH { get; set; }
        public string ISLISTTRADE { get; set; }
    }
}
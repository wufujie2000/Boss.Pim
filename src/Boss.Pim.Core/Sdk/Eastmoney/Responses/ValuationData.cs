namespace Boss.Pim.Sdk.Eastmoney.Responses
{
    public class ValuationData
    {
        public string FCODE { get; set; }
        public string SHORTNAME { get; set; }
        /// <summary>
        /// ��ֵ����
        /// </summary>
        public string GZTIME { get; set; }
        /// <summary>
        /// ��ֵ
        /// </summary>
        public float? GSZ { get; set; }
        /// <summary>
        /// ��ֵ�Ƿ�
        /// </summary>
        public float? GSZZL { get; set; }
        public string ISBUY { get; set; }
        public bool BUY { get; set; }
        public string LISTTEXCH { get; set; }
        public string ISLISTTRADE { get; set; }
    }
}
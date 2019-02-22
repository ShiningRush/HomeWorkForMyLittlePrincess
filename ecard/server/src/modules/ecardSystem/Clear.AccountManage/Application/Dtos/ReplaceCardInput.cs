namespace Clear.AccountManage.Application
{
    public class ReplaceCardInput
    {
        public string AccountId { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 旧卡号
        /// </summary>
        public string OldCardNo { get; set; }

        /// <summary>
        /// 新卡号
        /// </summary>
        public string NewCardNo { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }
}
namespace Clear.AccountManage.Application
{
    public class CancellationCardInput
    {
        public string AccountId { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }
}
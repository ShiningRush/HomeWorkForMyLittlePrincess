namespace Clear.AccountManage.Application
{
    public class BindingCardInput
    {
        public string AccountId { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
    }
}
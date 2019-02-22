using System;

namespace Clear.AccountManage.Application
{
    public class GetAccountsInput : YiBan.Common.Pages.PagerParameter
    {
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get;  set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get;  set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IDCardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid? CreatorId { get; set; }
    }
}
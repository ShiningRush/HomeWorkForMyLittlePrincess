using Clear.AccountManage.Domain.Accounts;
using PlatformService.BridgeComponent.Service.Data;
using YiBan.Common.Extensions;

namespace Clear.AccountManage.Application
{
    public class GetCardsOutput
    {
        /// <summary>
        /// 所属的账户ID
        /// </summary>
        public string AccountId { get;  set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        [DataItem("CardType")]
        public string CardType { get; set; }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// 卡号 
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 账户姓名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 账户身份标识
        /// </summary>
        public string AccountIDCardNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CardStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get { return Status.GetDescription(); } }

        /// <summary>
        /// 是否验证支付密码
        /// </summary> 
        public bool IsPasswordAuth { get; set; }
        /// <summary>
        /// 是否只能退卡
        /// </summary>
        public bool IsOnlyCancellation { get; set; }
    }
}
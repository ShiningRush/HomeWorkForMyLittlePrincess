using PlatformService.BridgeComponent.Service.Data;
using System.ComponentModel.DataAnnotations;

namespace Clear.AccountManage.Application
{
    public class AccountRechargeInput
    {
        public string AccountId { get; set; }

        /// <summary>
        /// 资金类型
        /// </summary>
        [MaxLength(32, ErrorMessage = "资金类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "资金类型不能为空")]
        [DataItem]
        public string CapitalType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [MaxLength(32, ErrorMessage = "支付方式最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "支付方式不能为空")]
        [DataItem]
        public string PayType { get; set; }
    }
}
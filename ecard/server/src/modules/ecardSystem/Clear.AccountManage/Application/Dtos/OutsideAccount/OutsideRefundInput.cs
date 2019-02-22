using PlatformService.BridgeComponent.Service.Data;
using System.ComponentModel.DataAnnotations;

namespace Clear.AccountManage.Application
{
    public class OutsideRefundInput:OutsideFeeInput
    {
        /// <summary>
        /// 支付方式 
        /// </summary> 
        [MaxLength(32, ErrorMessage = "支付方式最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "支付方式不能为空")]
        [DataItem]
        public virtual string PayType { get; set; }
    }
}
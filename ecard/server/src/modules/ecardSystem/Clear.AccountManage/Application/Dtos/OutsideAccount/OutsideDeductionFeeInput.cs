using System.ComponentModel.DataAnnotations;

namespace Clear.AccountManage.Application
{
    public class OutsideDeductionFeeInput: OutsideFeeInput
    {
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(256, ErrorMessage = "描述最大长度256")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "描述不能为空")]
        public string Desc { get; set; }
    }
}
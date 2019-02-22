using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clear.AccountManage.Application
{
    [AutoMapTo(typeof(Account))]
    public class OutsideCreditCardInput
    {
        /// <summary>
        /// 账户类型 对应字典类型代码 AccountType
        /// </summary> 
        [MaxLength(32, ErrorMessage = "账户类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账户类型不能为空")]
        public string AccountType { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        [MaxLength(32, ErrorMessage = "用户名称最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 证件类型 对应字典类型 IDCardType
        /// </summary> 
        [MaxLength(32, ErrorMessage = "证件类型 对应字典类型 IDCardType最大长度32")]
        public string IDCardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "证件号码最大长度64")]
        public string IDCardNo { get; set; }
        /// <summary>
        /// 性别 对应字典表类型代码 Sex
        /// </summary> 
        [MaxLength(8, ErrorMessage = "性别最大长度8")]
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary> 
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary> 
        [MaxLength(16, ErrorMessage = "手机号码最大长度16")]
        public string Mobile { get; set; }
      
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(128, ErrorMessage = "备注最大长度128")]
        public string Remark { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary> 
        [MaxLength(64, ErrorMessage = "籍贯最大长度64")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// 卡号 
        /// </summary>
        [MaxLength(32, ErrorMessage = "卡号最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡号不能为空")]
        public string CardNo { get; set; }
    }
}
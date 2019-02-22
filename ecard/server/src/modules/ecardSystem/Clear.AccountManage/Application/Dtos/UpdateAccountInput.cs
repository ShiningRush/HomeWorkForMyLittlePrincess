using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using System.Linq;

namespace Clear.AccountManage.Application
{
    [AutoMapTo(typeof(Account))]
    public class UpdateAccountInput:Abp.Runtime.Validation.ICustomValidate
    {
        [MaxLength(64, ErrorMessage = "账号Id最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号Id不能为空")]
        public string Id { get; set; }
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
        /// 支付密码
        /// </summary> 
        [MaxLength(256, ErrorMessage = "支付密码最大长度256")]
        public string Password { get; set; }
        /// <summary>
        /// 支付密码halt
        /// </summary> 
        [MaxLength(128, ErrorMessage = "支付密码halt最大长度128")]
        public string PasswordHalt { get; set; }
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
        /// 现住地址
        /// </summary> 
        [MaxLength(64, ErrorMessage = "现住地址最大长度64")]
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary> 
        [MaxLength(32, ErrorMessage = "联系人最大长度32")]
        public string Linkman { get; set; }
        /// 联系人关系
        /// </summary> 
        [MaxLength(16, ErrorMessage = "联系人关系最大长度16")]
        public string LinkmanRelation { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary> 
        [MaxLength(16, ErrorMessage = "联系人电话最大长度16")]
        public string LinkmanTel { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(128, ErrorMessage = "备注最大长度128")]
        public string Remark { get; set; }
        /// <summary>
        /// 国籍 对应字典表类型代码 Nationality
        /// </summary> 
        [MaxLength(32, ErrorMessage = "国籍最大长度32")]
        public string Nationality { get; set; }
        /// <summary>
        /// 民族 对应字典表类型代码 Nation
        /// </summary> 
        [MaxLength(32, ErrorMessage = "民族最大长度32")]
        public string Nation { get; set; }
        /// <summary>
        /// 婚姻状况 对应字典表类型代码 MaritalStatus
        /// </summary> 
        [MaxLength(8, ErrorMessage = "婚姻状况最大长度8")]
        public string MaritalStatus { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary> 
        [MaxLength(64, ErrorMessage = "籍贯最大长度64")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 职业
        /// </summary> 
        [MaxLength(32, ErrorMessage = "职业最大长度32")]
        public string Occupation { get; set; }
        /// <summary>
        /// 血型 对应字典表类型代码 BloodType
        /// </summary> 
        [MaxLength(8, ErrorMessage = "血型最大长度8")]
        public string BloodType { get; set; }
        /// <summary>
        /// 学历 对应字典表类型代码 Education
        /// </summary> 
        [MaxLength(8, ErrorMessage = "学历最大长度8")]
        public string Education { get; set; }
        /// <summary>
        /// 联系人地址
        /// </summary> 
        [MaxLength(64, ErrorMessage = "联系人地址最大长度64")]
        public string LinkmanAddress { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary> 
        [MaxLength(128, ErrorMessage = "家庭住址最大长度128")]
        public string HomeAddress { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary> 
        [MaxLength(64, ErrorMessage = "电子邮箱最大长度64")]
        public string Email { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "单位名称最大长度64")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary> 
        [MaxLength(128, ErrorMessage = "单位地址最大长度128")]
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 单位电话
        /// </summary> 
        [MaxLength(16, ErrorMessage = "单位电话最大长度16")]
        public string CompanyTel { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }

        public List<CreateCardDto> UpdateCards { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (UpdateCards != null && UpdateCards.GroupBy(s => s.CardType).Any(s => s.Count() > 1))
            {
                context.Results.Add(new ValidationResult("提交的卡类型存在重复"));
            }
        }
    }
}
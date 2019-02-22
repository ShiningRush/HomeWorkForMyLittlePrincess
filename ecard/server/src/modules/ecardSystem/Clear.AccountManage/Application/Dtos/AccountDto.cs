using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using YiBan.Common.Extensions;

namespace Clear.AccountManage.Application
{
    [AutoMapFrom(typeof(Account))]
    public class AccountDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 账户类型 对应字典类型代码 AccountType
        /// </summary> 
        [DataItem("AccountType")]
        public string AccountType { get; set; }

        /// <summary>
        /// 账户类型名称
        /// </summary> 
        public string AccountTypeName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 证件类型 对应字典类型 IDCardType
        /// </summary> 
        [DataItem("IDCardType")]
        public string IDCardType { get; set; }
        /// <summary>
        /// 证件类型 对应字典类型 IDCardType
        /// </summary> 
        public string IDCardTypeName { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary> 
        public string IDCardNo { get; set; }
        /// <summary>
        /// 性别 对应字典表类型代码 Sex
        /// </summary> 
        [DataItem("Sex")]
        public string Sex { get; set; }
        /// <summary>
        /// 性别中文
        /// </summary>
        public string SexName { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary> 
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary> 
        public string Mobile { get; set; }
        /// <summary>
        /// 现住地址
        /// </summary> 
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary> 
        public string Linkman { get; set; }
        /// 联系人关系
        /// </summary> 
        public string LinkmanRelation { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary> 
        public string LinkmanTel { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { get; set; }
        /// <summary>
        /// 国籍 对应字典表类型代码 Nationality
        /// </summary> 
        [DataItem("Nationality")]
        public string Nationality { get; set; }
        /// <summary>
        /// 国籍 对应字典表类型代码 Nationality
        /// </summary> 
        public string NationalityName { get; set; }
        /// <summary>
        /// 民族 对应字典表类型代码 Nation
        /// </summary> 
        [DataItem("Nation")]
        public string Nation { get; set; }
        /// <summary>
        /// 民族 对应字典表类型代码 Nation
        /// </summary> 
        public string NationName { get; set; }
        /// <summary>
        /// 婚姻状况 对应字典表类型代码 MaritalStatus
        /// </summary> 
        [DataItem("MaritalStatus")]
        public string MaritalStatus { get; set; }
        /// <summary>
        /// 婚姻状况 对应字典表类型代码 MaritalStatus
        /// </summary> 
        public string MaritalStatusName { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary> 
        public string NativePlace { get; set; }
        /// <summary>
        /// 职业
        /// </summary> 
        public string Occupation { get; set; }
        /// <summary>
        /// 血型 对应字典表类型代码 BloodType
        /// </summary> 
        [DataItem("BloodType")]
        public string BloodType { get; set; }
        /// <summary>
        /// 血型 对应字典表类型代码 BloodType
        /// </summary> 
        public string BloodTypeName { get; set; }
        /// <summary>
        /// 学历 对应字典表类型代码 Education
        /// </summary> 
        [DataItem("Education")]
        public string Education { get; set; }
        /// <summary>
        /// 学历 对应字典表类型代码 Education
        /// </summary> 
        public string EducationName { get; set; }
        /// <summary>
        /// 联系人地址
        /// </summary> 
        public string LinkmanAddress { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary> 
        public string HomeAddress { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary> 
        public string Email { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary> 
        public string CompanyName { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary> 
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 单位电话
        /// </summary> 
        public string CompanyTel { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary> 
        public virtual string CreatorName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        public virtual Guid Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 账户状态 0:正常 1:冻结 对应字典类型代码 AccountStatus
        /// </summary> 
        public  AccountStatus Status { get;  set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get { return Status.GetDescription(); } }
        [DataItem]
        public List<CardDto> Cards { get; set; }
        [DataItem]
        public List<CapitalDto> Capitals { get; set; }
    }
}
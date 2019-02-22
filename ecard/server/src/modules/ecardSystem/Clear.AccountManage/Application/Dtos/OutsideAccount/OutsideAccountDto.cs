using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using System;

namespace Clear.AccountManage.Application
{
    [AutoMapFrom(typeof(Account))]
    public class OutsideAccountDto
    {

        public string Id { get; set; }
        /// <summary>
        /// 账户类型 对应字典类型代码 AccountType
        /// </summary> 
        public string AccountType { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 证件类型 
        /// </summary> 
        public string IDCardType { get; set; }
     
        /// <summary>
        /// 证件号码
        /// </summary> 
        public string IDCardNo { get; set; }
        /// <summary>
        /// 性别 对应字典表类型代码 Sex
        /// </summary> 
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary> 
        public DateTime BirthDay { get; set; }
    }
}
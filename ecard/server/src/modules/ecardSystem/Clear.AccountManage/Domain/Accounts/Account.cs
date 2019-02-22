using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 账户信息
    /// </summary> 
    [Table("ec_account")]
    public partial class Account : EntityWithGeneratesDomainEvents<string>, IMayHaveCreator, IHasModifier
    {
        protected Account()
        {
            Cards = new List<Card>();
            Capitals = new List<Capital>();
            Status = AccountStatus.Normal;
        }


        /// <summary>
        /// 账号代码 自动按照1XXXXXXXXXX格式生成11位数字
        /// </summary> 
        [MaxLength(64, ErrorMessage = "账号代码最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号代码不能为空")]
        [Column("Id")]
        [Key]
        public override string Id { get; set; }
        /// <summary>
        /// 账户类型 对应字典类型代码 AccountType
        /// </summary> 
        [MaxLength(32, ErrorMessage = "账户类型最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "账户类型不能为空")]
        [Column("AccountType")]
        public virtual string AccountType { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        [MaxLength(32, ErrorMessage = "用户名称最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名称不能为空")]
        [Column("Name")]
        [Index(IsUnique = false)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 账户状态 0:正常 1:冻结 对应字典类型代码 AccountStatus
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "账户状态 0:正常 1:冻结 对应字典类型代码 AccountStatus不能为空")]
        [Column("Status")]
        public virtual AccountStatus Status { get;private set; }
        /// <summary>
        /// 证件类型 对应字典类型 IDCardType
        /// </summary> 
        [MaxLength(32, ErrorMessage = "证件类型 对应字典类型 IDCardType最大长度32")]
        [Column("IDCardType")]
        [Index("Idx_IDCard_Account", Order = 1, IsUnique = false)]
        public virtual string IDCardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "证件号码最大长度64")]
        [Column("IDCardNo")]
        [Index("Idx_IDCard_Account", Order = 2, IsUnique = false)]
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 支付密码
        /// </summary> 
        [MaxLength(256, ErrorMessage = "支付密码最大长度256")]
        [Column("Password")]
        public virtual string Password { get; set; }
        /// <summary>
        /// 支付密码halt
        /// </summary> 
        [MaxLength(128, ErrorMessage = "支付密码halt最大长度128")]
        [Column("PasswordHalt")]
        public virtual string PasswordHalt { get; set; }
        /// <summary>
        /// 性别 对应字典表类型代码 Sex
        /// </summary> 
        [MaxLength(8, ErrorMessage = "性别最大长度8")]
        [Column("Sex")]
        public virtual string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "出生日期不能为空")]
        [Column("BirthDay")]
        public virtual DateTime BirthDay { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary> 
        [MaxLength(16, ErrorMessage = "手机号码最大长度16")]
        [Column("Mobile")]
        [Index("Idx_Mobile", IsUnique = false)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 现住地址
        /// </summary> 
        [MaxLength(64, ErrorMessage = "现住地址最大长度64")]
        [Column("Address")]
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary> 
        [MaxLength(32, ErrorMessage = "联系人最大长度32")]
        [Column("Linkman")]
        public virtual string Linkman { get; set; }
        /// 联系人关系
        /// </summary> 
        [MaxLength(16, ErrorMessage = "联系人关系最大长度16")]
        [Column("LinkmanRelation")]
        public virtual string LinkmanRelation { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary> 
        [MaxLength(16, ErrorMessage = "联系人电话最大长度16")]
        [Column("LinkmanTel")]
        public virtual string LinkmanTel { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(128, ErrorMessage = "备注最大长度128")]
        [Column("Remark")]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 国籍 对应字典表类型代码 Nationality
        /// </summary> 
        [MaxLength(32, ErrorMessage = "国籍最大长度32")]
        [Column("Nationality")]
        public virtual string Nationality { get; set; }
        /// <summary>
        /// 民族 对应字典表类型代码 Nation
        /// </summary> 
        [MaxLength(32, ErrorMessage = "民族最大长度32")]
        [Column("Nation")]
        public virtual string Nation { get; set; }
        /// <summary>
        /// 婚姻状况 对应字典表类型代码 MaritalStatus
        /// </summary> 
        [MaxLength(8, ErrorMessage = "婚姻状况最大长度8")]
        [Column("MaritalStatus")]
        public virtual string MaritalStatus { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary> 
        [MaxLength(64, ErrorMessage = "籍贯最大长度64")]
        [Column("NativePlace")]
        public virtual string NativePlace { get; set; }
        /// <summary>
        /// 职业
        /// </summary> 
        [MaxLength(32, ErrorMessage = "职业最大长度32")]
        [Column("Occupation")]
        public virtual string Occupation { get; set; }
        /// <summary>
        /// 血型 对应字典表类型代码 BloodType
        /// </summary> 
        [MaxLength(8, ErrorMessage = "血型最大长度8")]
        [Column("BloodType")]
        public virtual string BloodType { get; set; }
        /// <summary>
        /// 学历 对应字典表类型代码 Education
        /// </summary> 
        [MaxLength(8, ErrorMessage = "学历最大长度8")]
        [Column("Education")]
        public virtual string Education { get; set; }
        /// <summary>
        /// 联系人地址
        /// </summary> 
        [MaxLength(64, ErrorMessage = "联系人地址最大长度64")]
        [Column("LinkmanAddress")]
        public virtual string LinkmanAddress { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary> 
        [MaxLength(128, ErrorMessage = "家庭住址最大长度128")]
        [Column("HomeAddress")]
        public virtual string HomeAddress { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary> 
        [MaxLength(64, ErrorMessage = "电子邮箱最大长度64")]
        [Column("Email")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "单位名称最大长度64")]
        [Column("CompanyName")]
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary> 
        [MaxLength(128, ErrorMessage = "单位地址最大长度128")]
        [Column("CompanyAddress")]
        public virtual string CompanyAddress { get; set; }
        /// <summary>
        /// 单位电话
        /// </summary> 
        [MaxLength(16, ErrorMessage = "单位电话最大长度16")]
        [Column("CompanyTel")]
        public virtual string CompanyTel { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        [Column("Creator")]
        public virtual Guid? Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        [Column("CreateTime")]
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary> 
        [Column("LastModifier")]
        public virtual Guid? LastModifier { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary> 
        [Column("LastModifyTime")]
        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Avatar { get; set; }


        /// <summary>
        /// 资金
        /// </summary>
        [ForeignKey("AccountId")]
        public virtual ICollection<Capital> Capitals { get;  set; }

        /// <summary>
        /// 资金
        /// </summary>
        [ForeignKey("AccountId")]
        public virtual ICollection<Card> Cards { get;  set; }


       
    }
}

using Abp.Domain.Entities;
using Clear.UserPermission.Domain.Authorization;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;

namespace Clear.UserPermission.Entities
{
    /// <summary>
    /// 系统用户
    /// </summary> 
    [Table("sys_user")]
    public partial class User : UserBase, IMustHaveCreator, IHasModifier,ISoftDelete
    {
        private const string DEFAULT_PASSWORD = "123456";

        public const string SYSTEM_USERNAME = "admin";

        public const string SYSTEM_USERFILTER = "systemUser";

        public User()
        {
            IsReset = true;
            IsStop = false;
            IsDeleted = false;

            //Departments = new EntityCollection<Department>();
            //Roles = new EntityCollection<Role>();
        }

        /// <summary>
        /// 所属机构
        /// </summary> 
        [Column("OrganizeId")]
        public override Guid OrganizeId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary> 
        [MaxLength(64, ErrorMessage = "登录名最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "登录名不能为空")]
        [Index(IsUnique = true)]
        [Column("LoginName")]
        public override string LoginName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "用户名称最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名称不能为空")]
        [Column("UserName")]
        public override string UserName { get; set; }
        /// <summary>
        /// 工号
        /// </summary> 
        [MaxLength(32, ErrorMessage = "工号最大长度32")]
        [Column("JobNo")]
        public virtual string JobNo { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary> 
        [MaxLength(128, ErrorMessage = "用户密码最大长度128")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户密码不能为空")]
        [Column("Password")]
        public virtual string Password { get; set; }
        /// <summary>
        /// 密码Salt
        /// </summary> 
        [MaxLength(128, ErrorMessage = "密码Salt最大长度128")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码Salt不能为空")]
        [Column("Salt")]
        public virtual string Salt { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary> 
        [MaxLength(16, ErrorMessage = "手机号码最大长度16")]
        [Column("Mobile")]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary> 
        [MaxLength(128, ErrorMessage = "邮箱最大长度128")]
        [Column("EMail")]
        public virtual string EMail { get; set; }
        /// <summary>
        /// 职级
        /// </summary> 
        [MaxLength(64, ErrorMessage = "职级最大长度64")]
        [Column("ProfessionalLevel")]
        public virtual string ProfessionalLevel { get; set; }
        /// <summary>
        /// 职务
        /// </summary> 
        [MaxLength(64, ErrorMessage = "职务最大长度64")]
        [Column("Duty")]
        public virtual string Duty { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(256, ErrorMessage = "备注最大长度256")]
        [Column("Remark")]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "是否停用不能为空")]
        [Column("IsStop")]
        public virtual bool IsStop { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "是否删除不能为空")]
        [Column("IsDeleted")]
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 是否重置密码
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "是否重置密码不能为空")]
        [Column("IsReset")]
        public virtual bool IsReset { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "创建人不能为空")]
        [Column("Creator")]
        public virtual Guid Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "创建时间不能为空")]
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


        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 部门列表
        /// </summary>
        public virtual ICollection<Department> Departments { get; set; }
        
        /// <summary>
        /// 重置成默认密码
        /// </summary>
        public void RestDefaultPassword()
        {
            Salt = HashHelper.CreateRandomSalt();
            Password = HashHelper.ComputeSaltedHash(DEFAULT_PASSWORD, Salt);
            IsReset = true;
        }

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            string passwordHash = HashHelper.ComputeSaltedHash(password, Salt);
            return passwordHash.Equals(Password);
        }

        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="newPassword"></param>
        public void SetNewPassword(string newPassword)
        {
            Salt = HashHelper.CreateRandomSalt();
            Password = HashHelper.ComputeSaltedHash(newPassword, Salt);
            IsReset = false;
        }

    }

}

using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clear.UserPermission.Entities
{
    /// <summary>
    /// 角色信息表
    /// </summary> 
    [Table("sys_role")]
    public partial class Role : Entity<Guid>, IMustHaveCreator, IHasModifier, IMustHaveOrganize
    {
        public Role()
        {
            IsEnabled = true;
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 机构主键
        /// </summary> 
        [Column("OrganizeId")]
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "角色名称最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色名称不能为空")]
        [Column("Name")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 数据权限 0：本人；1：所在部门；2：所在机构
        /// </summary> 
        [Column("DataAuthority")]
        public virtual int DataAuthority { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        [Column("SortCode")]
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "是否有效不能为空")]
        [Column("IsEnabled")]
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(256, ErrorMessage = "备注最大长度256")]
        [Column("Description")]
        public virtual string Description { get; set; }
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
        /// <summary>
        /// 角色用户列表
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public virtual ICollection<ModuleAuth> ModuleAuths { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public virtual ICollection<Module> Modules { get; set; }

    }

}

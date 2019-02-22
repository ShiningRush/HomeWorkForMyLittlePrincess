using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clear.UserPermission.Entities
{
    /// <summary>
    /// 公司&机构信息
    /// </summary> 
    [Table("sys_organize")]
    public partial class Organize : OrganizeBase, IMustHaveCreator, IHasModifier,ISoftDelete
    {
        /// <summary>
        /// 默认的顶级机构编码
        /// </summary>
        public const string DEFAULT_TOP_ORGANIZECODE = "0";

        public const string DEFAULT_TOP_ORGANIZEID = "a020e1a2-f2f3-43b4-adf1-f678312e67c5";

        public Organize()
        {
            IsDeleted = false;
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 父级主键
        /// </summary> 
        [Column("ParentId")]
        public override Guid? ParentId { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary> 
        [MaxLength(32, ErrorMessage = "机构代码最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "机构代码不能为空")]
        [Index(IsUnique =true)]
        [Column("Code")]
        public override string Code { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "机构名称最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "机构名称不能为空")]
        [Column("Name")]
        public override string Name { get; set; }
        /// <summary>
        /// 机构性质
        /// </summary> 
        [MaxLength(32, ErrorMessage = "机构性质最大长度32")]
        [Column("Nature")]
        public virtual string Nature { get; set; }
        /// <summary>
        /// 电话
        /// </summary> 
        [MaxLength(32, ErrorMessage = "电话最大长度32")]
        [Column("Phone")]
        public virtual string Phone { get; set; }
        /// <summary>
        /// 传真
        /// </summary> 
        [MaxLength(32, ErrorMessage = "传真最大长度32")]
        [Column("Fax")]
        public virtual string Fax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary> 
        [MaxLength(32, ErrorMessage = "邮编最大长度32")]
        [Column("Postalcode")]
        public virtual string Postalcode { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary> 
        [MaxLength(64, ErrorMessage = "电子邮箱最大长度64")]
        [Column("Email")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 负责人主键
        /// </summary> 
        [Column("ManagerId")]
        public virtual Guid? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual User Manager { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary> 
        [MaxLength(64, ErrorMessage = "详细地址最大长度64")]
        [Column("Address")]
        public virtual string Address { get; set; }
        /// <summary>
        /// 公司官网
        /// </summary> 
        [MaxLength(256, ErrorMessage = "公司官网最大长度256")]
        [Column("WebAddress")]
        public virtual string WebAddress { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary> 
        [Column("FoundedTime")]
        public virtual DateTime? FoundedTime { get; set; }
        /// <summary>
        /// 层
        /// </summary> 
        [Column("Layer")]
        public virtual int Layer { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        [Column("SortCode")]
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary> 
        [Column("IsDeleted")]
        public virtual bool IsDeleted { get; set; }
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
        /// 机构部门信息
        /// </summary>
        [ForeignKey("OrganizeId")]
        public virtual ICollection<Department> Departments { get; set; }

        /// <summary>
        /// 机构角色信息
        /// </summary>
        [ForeignKey("OrganizeId")]
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 机构用户信息
        /// </summary>
        [ForeignKey("OrganizeId")]
        public virtual ICollection<User> Users { get; set; }
    }

}

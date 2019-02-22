using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Clear.UserPermission.Entities
{
    /// <summary>
    /// 机构部门表
    /// </summary> 
    [Table("sys_department")]
    public partial class Department : Entity<Guid>, IMustHaveCreator, IHasModifier, IMustHaveOrganize, ITreeEntity<Guid>
    {
        public Department()
        {
            CreateTime = DateTime.Now;
        }
        
        /// <summary>
        /// 机构主键
        /// </summary> 
        [Column("OrganizeId")]
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary> 
        [Column("ParentId")]
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary> 
        [MaxLength(32, ErrorMessage = "部门名称最大长度32")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "部门名称不能为空")]
        [Column("Name")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 负责人主键
        /// </summary> 
        [Column("ManagerId")]
        public virtual Guid? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual User Manager { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary> 
        [MaxLength(32, ErrorMessage = "联系电话最大长度32")]
        [Column("Phone")]
        public virtual string Phone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary> 
        [MaxLength(64, ErrorMessage = "电子邮件最大长度64")]
        [Column("Email")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 部门传真
        /// </summary> 
        [MaxLength(32, ErrorMessage = "部门传真最大长度32")]
        [Column("Fax")]
        public virtual string Fax { get; set; }
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
        /// 备注
        /// </summary> 
        [MaxLength(200, ErrorMessage = "备注最大长度200")]
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
        /// 用户列表
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
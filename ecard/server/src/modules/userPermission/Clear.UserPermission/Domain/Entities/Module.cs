using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clear.UserPermission.Entities
{
    /// <summary>
    /// 系统功能模块表
    /// </summary> 
    [Table("sys_module")]
    public partial class Module : Entity<Guid>, IMustHaveCreator, IHasModifier, ITreeEntity<Guid>
    {
        public Module()
        {
            IsEnabled = true;
            CreateTime = DateTime.Now;
            AllowDelete = true;
            AllowEdit = true;
        }
        /// <summary>
        /// 父级主键
        /// </summary> 
        [Column("ParentId")]
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "编码最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "编码不能为空")]
        [Column("Code")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "名称最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称不能为空")]
        [Column("Name")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary> 
        [MaxLength(64, ErrorMessage = "图标最大长度64")]
        [Column("Icon")]
        public virtual string Icon { get; set; }
        /// <summary>
        /// 导航地址
        /// </summary> 
        [MaxLength(256, ErrorMessage = "导航地址最大长度256")]
        [Column("UrlAddress")]
        public virtual string UrlAddress { get; set; }
        /// <summary>
        /// 导航目标
        /// </summary> 
        [MaxLength(32, ErrorMessage = "导航目标最大长度32")]
        [Column("Target")]
        public virtual string Target { get; set; }
        /// <summary>
        /// 允许自动展开
        /// </summary> 
        [Column("AllowAutoExpand")]
        public virtual bool AllowAutoExpand { get; set; }
        /// <summary>
        /// 允许编辑
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "允许编辑不能为空")]
        [Column("AllowEdit")]
        public virtual bool AllowEdit { get; set; }
        /// <summary>
        /// 允许删除
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "允许删除不能为空")]
        [Column("AllowDelete")]
        public virtual bool AllowDelete { get; set; }
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
        /// 模块权限
        /// </summary>
        public virtual ICollection<ModuleAuth> ModuleAuths { get; set; }


        public virtual ICollection<Role> Roles { get; set; }
    }
}

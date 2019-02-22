using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Clear.UserPermission.Entities
{
    /// <summary>
    /// 系统功能按钮表
    /// </summary> 
    [Table("sys_moduleauth")]
    public partial class ModuleAuth : Entity<Guid>
    {
       
        /// <summary>
        /// 功能主键
        /// </summary> 
        [Column("ModuleId")]
        public virtual Guid ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual Module AssignedModule { get; set; }

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
        /// 排序码
        /// </summary> 
        [Column("SortCode")]
        public virtual int SortCode { get; set; }
        /// <summary>
        /// API方法
        /// </summary> 
        [MaxLength(256, ErrorMessage = "API方法最大长度256")]
        [Column("WebAPI")]
        public virtual string WebAPI { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

    }

}

using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Clear.CommonContext.Domain.DataItemAggregate
{
    /// <summary>
    /// 数据字典分类表
    /// </summary> 
    [Table("sys_dataitem")]
    public partial class DataItem : Entity<Guid>, IMustHaveCreator, IHasModifier, ITreeEntity<Guid>
    {
       
        /// <summary>
        /// 父级主键
        /// </summary> 
        [Column("ParentId")]
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "分类编码最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "分类编码不能为空")]
        [Index(IsUnique = true)]
        [Column("ItemCode")]
        public virtual string ItemCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary> 
        [MaxLength(64, ErrorMessage = "分类名称最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "分类名称不能为空")]
        [Column("ItemName")]
        public virtual string ItemName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        [Column("SortCode")]
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [MaxLength(256, ErrorMessage = "备注最大长度256")]
        [Column("Description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "允许编辑不能为空")]
        [Column("AllowEdit")]
        [DefaultValue(true)]
        public virtual bool AllowEdit { get; set; }
        /// <summary>
        /// 允许删除
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "允许删除不能为空")]
        [Column("AllowDelete")]
        [DefaultValue(false)]
        public virtual bool AllowDelete { get; set; }
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
        /// 分类字典信息
        /// </summary>
        public virtual ICollection<DataitemDetail> DataItemDetails { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public DataItem()
        {
            AllowEdit = true;
            AllowDelete = true;
        }
    }
}

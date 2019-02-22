using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Clear.CommonContext.Domain.DataItemAggregate
{
    /// <summary>
    /// 数据字典明细表、存储系统参数和系统中用的键值对数据
    /// </summary> 
    [Table("sys_dataitemdetail")]
    public partial class DataitemDetail : Entity<Guid>, IMustHaveCreator, IHasModifier
    {
        public DataitemDetail()
        {
            IsDelete = false;
            IsEnabled = true;
            AllowEdit = true;
            AllowDelete = true;
        }
        /// <summary>
        /// 分类主键
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "分类主键不能为空")]
        [Column("ItemId")]
        [Index("Idx_Unique_ItemDetail", Order= 1, IsUnique = true)]
        public virtual Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual DataItem DataItem { get; set; }
        /// <summary>
        /// 编码
        /// </summary> 
        [MaxLength(64, ErrorMessage = "编码最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "编码不能为空")]
        [Column("ItemCode")]
        [Index("Idx_Unique_ItemDetail", Order = 2, IsUnique = true)]
        public virtual string ItemCode { get; set; }
        /// <summary>
        /// 值
        /// </summary> 
        [MaxLength(64, ErrorMessage = "值最大长度64")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "值不能为空")]
        [Column("ItemValue")]
        public virtual string ItemValue { get; set; }
        /// <summary>
        /// 简拼
        /// </summary> 
        [MaxLength(256, ErrorMessage = "简拼最大长度256")]
        [Column("SimpleSpelling")]
        public virtual string SimpleSpelling { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "是否默认不能为空")]
        [Column("IsDefault")]
        [DefaultValue(false)]
        public virtual bool IsDefault { get; set; }

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
        /// 排序码
        /// </summary> 
        [Column("SortCode")]
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "是否删除不能为空")]
        [Column("IsDelete")]
        public virtual bool IsDelete { get; set; }
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
        /// 创建人
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "创建人不能为空")]
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

    }
}
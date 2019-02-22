using Abp.AutoMapper;
using Clear.CommonContext.Domain.DataItemAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clear.CommonContext.AppService.Dtos
{
    [AutoMap(typeof(DataitemDetail))]
    public class DataItemDetailDto
    {
        /// <summary>
        /// 分类主键
        /// </summary> 
        public virtual string ItemId { get; set; }
        /// <summary>
        /// 编码
        /// </summary> 
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "代码必须是数字或字母！")]
        public virtual string ItemCode { get; set; }
        /// <summary>
        /// 值
        /// </summary> 
        public virtual string ItemValue { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary> 
        public virtual bool IsDefault { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary> 
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public virtual string Description { get; set; }
        /// <summary>
        /// 允许编辑
        /// </summary> 
        public virtual bool AllowEdit { get; set; }
        /// <summary>
        /// 允许删除
        /// </summary> 
        public virtual bool AllowDelete { get; set; }

    }
}

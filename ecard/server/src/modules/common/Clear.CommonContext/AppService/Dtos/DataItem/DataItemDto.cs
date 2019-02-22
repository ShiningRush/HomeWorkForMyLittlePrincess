
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Clear.CommonContext.Domain.DataItemAggregate;

namespace Clear.CommonContext.AppService.Dtos
{
    [AutoMap(typeof(DataItem))]
    public class DataItemDto
    {
        /// <summary>
        /// 父级主键
        /// </summary> 
        public string ParentId { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary> 
        [RegularExpression("^[a-zA-Z0-9]+$",ErrorMessage ="代码必须是数字或字母！")]
        public string ItemCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary> 
        public string ItemName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public int SortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public string Description { get; set; }

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

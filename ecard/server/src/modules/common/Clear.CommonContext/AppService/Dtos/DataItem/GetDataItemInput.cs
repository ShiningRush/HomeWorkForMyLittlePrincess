using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService.Dtos
{
    /// <summary>
    /// 获取分类信息数据实体
    /// </summary>
    public class GetDataItemInput
    {
        /// <summary>
        /// 主键
        /// </summary> 
        public Guid? Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary> 
        public string ItemName { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary> 
        public Guid? ParentId { get; set; }
    }
}

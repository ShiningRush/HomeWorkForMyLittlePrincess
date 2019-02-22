using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService.Dtos
{
    /// <summary>
    /// 获取字典信息数据实体
    /// </summary>
    public class GetDataItemDetailInput
    {
        /// <summary>
        /// 主键
        /// </summary> 
        public string Id { get; set; }
        /// <summary>
        /// 分类主键
        /// </summary> 
        public virtual Guid? ItemId { get; set; }
        /// <summary>
        /// 关键字
        /// </summary> 
        public virtual string Keyword { get; set; }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService.Dtos
{
    /// <summary>
    /// 公用数据字典表DTO
    /// </summary>
    public class GetDetailOutputDTO
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary> 
        public bool IsDefault { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public int SortCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
